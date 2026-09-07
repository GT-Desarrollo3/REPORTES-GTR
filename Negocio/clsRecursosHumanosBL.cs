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
    public class clsRecursosHumanosBL
    {
        private readonly static clsRecursosHumanosBL instancia = new clsRecursosHumanosBL();

        public static clsRecursosHumanosBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataFichaEmpleados(string transpesa,string bra,string altra,string amt,string aduanas, string inomac, char estado)
        { return clsRecursosHumanosDAO.Instancia.GetDataFichaEmpleados(transpesa, bra, altra, amt,aduanas,inomac, estado); }
        
        public DataTable GetDataVencimientoContratos(string fechaini,string fechafin,string area,string transp, string bra, string altra, string amt, string aduanas)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataVencimientoContratos(fechaini,fechafin,area,transp,bra,altra,amt,aduanas);
        }

        public DataTable GetPeriodosVacaciones(int persona)
        {
            return clsRecursosHumanosDAO.Instancia.GetPeriodosVacaciones(persona);
        }
        public DataTable GetUtilizacionVacaciones(int persona,int periodo)
        {
            return clsRecursosHumanosDAO.Instancia.GetUtilizacionVacaciones(persona,periodo);
        }

        public DataTable GetPagosVacaciones(int persona, int periodo)
        {
            return clsRecursosHumanosDAO.Instancia.GetPagosVacaciones(persona, periodo);
        }

        public bool InsertUtilizacion(int periodo, int persona, string fechaini, string fechafin, string tipo, string usuario,bool mediodia)
        {
            return clsRecursosHumanosDAO.Instancia.InsertUtilizacion(periodo, persona, fechaini, fechafin, tipo, usuario, mediodia);
        }

        public bool BorrarUtilizacion(int periodo, int persona, int secuencia)
        {
            return clsRecursosHumanosDAO.Instancia.BorrarUtilizacion(periodo, persona, secuencia);
        }

        public bool UpdateSCTR(int valor, int persona)
        {
            return clsRecursosHumanosDAO.Instancia.UpdateSCTR(valor, persona);
        }

        public DataTable GetSCTR(string transpesa, string bra)
        {
            return clsRecursosHumanosDAO.Instancia.GetSCTR(transpesa,bra);
        }
        public DataTable GetFaltasySuspensiones(string periodoini, string periodofin, string concepto, int estado)
        {
            return clsRecursosHumanosDAO.Instancia.GetFaltasySuspensiones(periodoini, periodofin, concepto, estado);
        }
        public int GetCorrelativoDocumento(string tipodoc) 
        {
            return clsRecursosHumanosDAO.Instancia.GetCorrelativoDocumento(tipodoc) ;
        }

        public bool InsertDocumento(int numero, int empleado, string tipo, string asunto, string fecha, string Cuerpo, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.InsertDocumento(numero, empleado, tipo, asunto, fecha,Cuerpo,Usuario);
        }

        public DataTable GetAportacionesRetenciones(string compañia, string periodo, string planilla, string proceso)
        {
            return clsRecursosHumanosDAO.Instancia.GetAportacionesRetenciones(compañia, periodo, planilla, proceso);
        }

        public DataTable GetAsistencia(string fehacini, string fechafin, string tipo, string Sucursal, string Area, string Empleado)
        {
            if (tipo == "Marcas") { return clsRecursosHumanosDAO.Instancia.GetAsistencias(fehacini, fechafin, Sucursal, Area, Empleado); }
            if (tipo == "Faltas") { return clsRecursosHumanosDAO.Instancia.GetFaltas(1, fehacini, fechafin, Sucursal, Area, Empleado); }
            else { return clsRecursosHumanosDAO.Instancia.GetFaltas(2, fehacini, fechafin, Sucursal, Area, Empleado); }
        }

        public DataTable ReportesApp_RRHH_Asistencias_CrearEliminarAsistencia(int Opcion, int idAsistenciaN, int Persona, string Sucursal, DateTime FechaIngreso, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarAsistencia(Opcion, idAsistenciaN, Persona, Sucursal, FechaIngreso, Usuario); }

        public DataTable GetAsistenciaLineal(string fehacini, string fechafin)
        {
                return clsRecursosHumanosDAO.Instancia.GetAsistenciasLineal(fehacini, fechafin);      
        }

        /*
        public DataTable GetLlegada(string fechaini, string fechafin, string turno)
        {
            if (turno == "Llegadas")
            {
                return clsRecursosHumanosDAO.Instancia.GetLlegada(fechaini, fechafin);
            }
            else 
            {
               return clsRecursosHumanosDAO.Instancia.GetFaltas(fechaini, fechafin);
            }
        }
        */

        public DataTable GetPersonalNoGrato()
        {
            return clsRecursosHumanosDAO.Instancia.GetPersonalNoGrato();
        }

        public DataTable GetPersonaNoGrata()
        {
            return clsRecursosHumanosDAO.Instancia.GetPersonaNoGrata();
        }
        public DataTable GetPersonaNoGrata_Registro(string IDPersona, string ApePaterno, string ApeMaterno, string Nombres, string DNI, string Fecha, string Motivo, string User)
        {
            return clsRecursosHumanosDAO.Instancia.GetPersonaNoGrata_Registro(IDPersona, ApePaterno, ApeMaterno, Nombres, DNI, Fecha, Motivo, User);
        }

        public DataTable GetPersonaNoGrata_Eliminar(string IDPersona)
        {
            return clsRecursosHumanosDAO.Instancia.GetPersonaNoGrata_Eliminar(IDPersona);
        }

        public DataTable GetPersonaNoGrata_Permisos(string User)
        {
            return clsRecursosHumanosDAO.Instancia.GetPersonaNoGrata_Permisos(User);
        }

        public DataTable GetPersonaNoGrataxPeriodo(string periodo, string persona)
        {
            return clsRecursosHumanosDAO.Instancia.GetPersonaNoGrataxPeriodo(periodo, persona);
        }

        public DataTable GetPersonaNoGrataPeriodo(string fechaini, string fechafin, string persona)
        {
            return clsRecursosHumanosDAO.Instancia.GetPersonaNoGrataPeriodo(fechaini, fechafin, persona);
        }

        public void GetFaltaConductor(string idconductor,string fecha, string tipo, string descripcion)
        {
            clsRecursosHumanosDAO.Instancia.GetFaltaConductor(idconductor, fecha, tipo, descripcion);
        }

        public DataTable GetMuestraFaltaConductores()
        {
            return clsRecursosHumanosDAO.Instancia.GetMuestraFaltaConductores();
        }
        public DataTable GetListCondutoresTextbox()
        {
            return clsRecursosHumanosDAO.Instancia.GetListCondutoresTextbox();
        }
        public DataTable GetListarMemosCompromisos(string UsuarioModulo, int opcion, string fechain, string fechafin, int idpersona)
        {
            return clsRecursosHumanosDAO.Instancia.GetListarMemosCompromisos(UsuarioModulo, opcion, fechain,  fechafin, idpersona);
        }
        
        public DataTable GetIDCondutoresTextbox(string conductor)
        {
            return clsRecursosHumanosDAO.Instancia.GetIDCondutoresTextbox(conductor);
        }
        public DataTable GetVencimientoContratosTodos(string fechaini, string fechafin, string bra, string transpesa, string altra, string amt, string aduanas)
        {
            return clsRecursosHumanosDAO.Instancia.GetContratosVencidosTodos(fechaini, fechafin, bra, transpesa, altra, amt, aduanas);
        }
        public DataTable GetDataPlanillasTrabajores(string transpesa, string bra, string altra, string amt, string aduanas, string periodo)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasTrabajores(transpesa, bra, altra, amt, aduanas, periodo);
        }
        public DataTable GetDataPlanillasTrabajoresPeriodo(string transpesa, string bra, string altra, string amt, string aduanas, string periodo, string filtro)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasTrabajoresPeriodo(transpesa, bra, altra, amt, aduanas, periodo, filtro);
        }
        public DataTable GetDataTrabajoresCesados(string transpesa, string bra, string altra, string amt, string aduanas, string fechaini, string fechafin)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataTrabajoresCesados(transpesa, bra, altra, amt, aduanas, fechaini, fechafin);
        }
        public DataTable GetDataPlanillasDetalle(string transpesa, string bra, string altra, string amt, string aduanas, string periodo)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasDetalle(transpesa, bra, altra, amt, aduanas, periodo);
        }
       /* public DataTable GetDataPlanillasAsistenciasCargar(string empresa, string periodo, string usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasCargar(empresa, periodo, usuario);
        }
        public DataTable GetDataPlanillasAsistenciasTipoCargar()
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasTipoCargar();
        }*/

        public DataTable GetListarBonosPeriodo(string Periodo, string fini, string ffin)
        {
            return clsRecursosHumanosDAO.Instancia.GetListarBonosPeriodo(Periodo, fini,  ffin);
        }

        public DataTable GetDataPlanillaOficialMensual(string Compania, string Periodo, string TipoPlanilla, string TodaPlanilla)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillaOficialMensual(Compania, Periodo, TipoPlanilla, TodaPlanilla);
        }
        
        public DataTable GetListarDatoFiltros()
        {
            return clsRecursosHumanosDAO.Instancia.GetListarDatoFiltros();
        }

        //Hoja de Recorrido.
        //------------------
        public DataTable GetDataHojaRecorrido_ListarProcesos(string Compania, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_ListarProcesos(Compania, Usuario);
        }
        public DataTable GetDataHojaRecorrido_ListarGrupos(int IdProceso, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_ListarGrupos(IdProceso, Usuario);
        }
        public DataTable GetDataHojaRecorrido_ListarFormato(int IdProceso, int IdGrupo)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_ListarFormato(IdProceso, IdGrupo);
        }
        //Fin Hoja de Recorrido.
        //------------------

        public DataTable GetDataHojaRecorrido_Registrar(int Opcion, string compania, string codigohoja, int Anio, int idproceso, int idgrupo, int idpersona, string fecha,
                    int idpersona1, string label1, bool firma1, string txtObs1, int idpersona2, string label2, bool firma2, string txtObs2, int idpersona3, string label3,
                    bool firma3, string txtObs3, int idpersona4, string label4, bool firma4,  string txtObs4, int idpersona5, string label5, bool firma5,  string txtObs5,
                    int idpersona6, string label6, bool firma6, string txtObs6, int idpersona7, string label7, bool firma7, string txtObs7, int idpersona8, string label8,
                    bool firma8, string txtObs8, int idpersona9, string label9, bool firma9,  string txtObs9, int idpersona10, string label10, bool firma10,  string txtObs10,
                    int idpersona11, string label11, bool firma11,  string txtObs11, int idpersona12, string label12, bool firma12,  string txtObs12, string Usuario)
        {
           return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_Registrar( Opcion,  compania,  codigohoja,  Anio,  idproceso,  idgrupo,  idpersona,  fecha,
                     idpersona1, label1, firma1, txtObs1, idpersona2, label2, firma2,  txtObs2, idpersona3, label3,
                     firma3,  txtObs3, idpersona4, label4, firma4,  txtObs4, idpersona5, label5, firma5,  txtObs5,
                     idpersona6, label6, firma6,  txtObs6, idpersona7, label7, firma7,  txtObs7, idpersona8, label8,
                     firma8,  txtObs8, idpersona9, label9, firma9,  txtObs9, idpersona10, label10, firma10,  txtObs10,
                     idpersona11, label11, firma11,  txtObs11, idpersona12, label12, firma12,  txtObs12, Usuario);
        }


        public DataTable GetDataHojaRecorrido_ListarRegistros(string compania, string FechaInicio, string FechaFin, int idPerdsona, int idProceso, int idGrupo)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_ListarRegistros(compania, FechaInicio, FechaFin, idPerdsona
                                                            , idProceso, idGrupo);
        }

        public DataTable GetAreaPersona(int idpersona)
        {
            return clsRecursosHumanosDAO.Instancia.GetAreaPersona(idpersona);
        }

        public DataTable GetDataHojaRecorrido_RegistrarPorFormato(int Opcion,int idproceso, int idgrupo, int persona, string fecha, int idpersona1, string Area1, int idpersona2, string Area2,
                                                                  int idpersona3, string Area3, int idpersona4, string Area4, int idpersona5, string Area5, int idpersona6, string Area6,
                                                                  int idpersona7, string p8, int idpersona8, string p9, int idpersona9, string p10, int idpersona10, 
                                                                  string p11, int idpersona11, string p12, int idpersona12, string p13, string p14)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_RegistrarPorFormato(Opcion, idproceso, idgrupo, persona, fecha, idpersona1, Area1,
                                                                     idpersona2, Area2, idpersona3, Area3, idpersona4, Area4, idpersona5, Area5, idpersona6, Area6,
                                                                   idpersona7, p8, idpersona8, p9, idpersona9, p10, idpersona10,
                                                                   p11, idpersona11, p12, idpersona12, p13, p14);
        }

        public DataTable GetDataHojaRecorrido_ListarHojaEditar(string Compania, int anio, string codigohoja, int idproceso, int idgrupo, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_ListarHojaEditar(Compania, anio, codigohoja, idproceso, idgrupo, Usuario);
        }

        public DataTable GetDataHojaRecorrido_Firmar(int Posicion, string Compania, int anio, string codigohoja, int idproceso, int idgrupo, int IdPersona, byte[] imagenfirma,
                                                        string Observacion, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_Firmar( Posicion, Compania,  anio,  codigohoja,  idproceso,  idgrupo, IdPersona,  imagenfirma,
                                                         Observacion, Usuario);
        }

        public DataTable GetAreaPersonaFirmar(string USUARIO)
        {
            return clsRecursosHumanosDAO.Instancia.GetAreaPersonaFirmar(USUARIO);
        }
        
        public DataTable GetDataHojaRecorrido_RegistroFirma(int opcion, int idpersona, byte[] imagen1, string usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_RegistroFirma(opcion, idpersona, imagen1, usuario);
        }

        public DataTable GetDataHojaRecorrido_Accesos(string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_Accesos(Usuario);
        }

        //GERARDO - 01/08/23
        public DataTable GetDataBonoRegistrarExcepciones(int Opcion, int idpersona, string TipoBono, int idMotivoBono, string Periodo, decimal monto, string motivo, string usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataBonoRegistrarExcepciones(Opcion, idpersona, TipoBono, idMotivoBono, Periodo, monto, motivo, usuario);
        }
        //GERARDO - 01/08/23

        public DataTable GetListarExcepciones(int idConductor, string periodo)
        {
            return clsRecursosHumanosDAO.Instancia.GetListarExcepciones(idConductor, periodo);
        }


        //ASISTENCIAS - CONDUCTORES
        //-------------------------
        public DataTable GetDataPlanillasAsistenciasListarOperaciones(string usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasListarOperaciones(usuario);
        }

        public DataTable GetDataPlanillasAsistenciasCargar(string empresa, string periodo, string planilla, string usuario, int Cesados, int Operacion, string Nombre, string Cargo)
        { return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasCargar(empresa, periodo, planilla, usuario, Cesados, Operacion, Nombre, Cargo); }

        public DataTable GetDataPlanillasAsistenciasListarTrabajadores(string Compania, string Periodo, string Planilla, string usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasListarTrabajadores(Compania, Periodo, Planilla, usuario);
        }
        public DataTable GetDataPlanillasAsistenciasMapearTrabajadores(string Compania, string IdPersona, string Periodo, string Planilla, string usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasMapearTrabajadores(Compania, IdPersona, Periodo, Planilla, usuario);
        }

        public DataTable ReportesApp_RRHH_Asistencias_QuitarMapeo(int IdPersona, string Periodo, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_QuitarMapeo(IdPersona, Periodo, Usuario); }

        public DataTable GetDataPlanillasAsistenciasListarMapeados(string Compania, string Periodo, string Planilla, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasListarMapeados(Compania, Periodo, Planilla, Usuario);
        }
        public DataTable GetDataPlanillasAsistenciasRegistrar(string Compania, string Periodo, string Planilla, string xmlAsistencias, int IdTipoAsist, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasRegistrar(Compania, Periodo, Planilla, xmlAsistencias, IdTipoAsist, Usuario);
        }
        public DataTable GetDataPlanillasAsistenciasProcesarLicenciasYVacaciones(string Compania, string Periodo, string Planilla, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasProcesarLicenciasYVacaciones(Compania, Periodo, Planilla, Usuario);
        }
        public DataTable GetDataPlanillasAsistenciasTipoCargar()
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasTipoCargar();
        }
        public DataTable GetDataPlanillasAsistenciasBuscarXCompensar(string empresa, string Plla, int IDPersona, string FInicio, string FFin)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasBuscarXCompensar(empresa, Plla, IDPersona, FInicio, FFin);

        }
        public DataTable GetDataPlanillasAsistenciasCompensar(string Compania, string Periodo, string Planilla, int IDPersona, string FechaTrabajada, int IdTipoAsistTrabajada, string FechaCompensa, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasCompensar(Compania, Periodo, Planilla, IDPersona, FechaTrabajada, IdTipoAsistTrabajada, FechaCompensa, Usuario);
        }
        public DataTable GetDataPlanillasAsistenciasLiberarCompensacion(string Compania, string Planilla, int IDPersona, string FechaTrabajada, string FechaCompensa, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasLiberarCompensacion(Compania, Planilla, IDPersona, FechaTrabajada, FechaCompensa, Usuario);
        }
        public DataTable GetDataPlanillasAsistenciasRegularizaFechaParaCompensar(string Compania, string Periodo, string Planilla, int IDPersona, string FechaRegulariza, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasRegularizaFechaParaCompensar(Compania, Periodo, Planilla, IDPersona, FechaRegulariza, Usuario);
        }

        public DataTable GetDataPlanillasAsistencias_Noches_Registrar(int IDPersona, string Fecha, string CantidadNoche, string Observaciones, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistencias_Noches_Registrar(IDPersona, Fecha, CantidadNoche, Observaciones, Usuario);
        }
        public DataTable GetDataPlanillasAsistenciasBuscarXCompensarNoche(string empresa, string Plla, int IDPersona, string FInicio, string FFin)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistenciasBuscarXCompensarNoche(empresa, Plla, IDPersona, FInicio, FFin);

        }
        public DataTable GetDataPlanillasAsistencias_Noches_Compensar(string empresa, string Periodo, string Plla, int IDPersona, string xmlNoches, string FechaCompensa, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistencias_Noches_Compensar(empresa, Periodo, Plla, IDPersona, xmlNoches, FechaCompensa, Usuario);
        }
        public DataTable GetDataPlanillasAsistencias_Noches_Liberar(string empresa, string Plla, int IDPersona, string FechaLiberar, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistencias_Noches_Liberar(empresa, Plla, IDPersona, FechaLiberar, Usuario);
        }


        //FIN ASISTENCIAS - CONDUCTORES

        public DataTable GetDataGenerarCierre(string Compania, string Periodo, string xmlDatos, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataGenerarCierre(Compania,  Periodo,  xmlDatos,  Usuario);
        }

        public DataTable GetDataBonoPeriodosCerrados()
        {
            return clsRecursosHumanosDAO.Instancia.GetDataBonoPeriodosCerrados();
        }

        public DataTable GetDataBonoPeriodosCerradosDetalle(string Periodo)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataBonoPeriodosCerradosDetalle(Periodo);
        }

        public DataTable GetDataPlanillasAsistencias_CompensacionAdelantada_Registrar(string empresa, string Periodo, string Plla, int IDPersona, string FechaCompensa, string FechaAsiste, string Motivo, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistencias_CompensacionAdelantada_Registrar(empresa, Periodo, Plla, IDPersona, FechaCompensa, FechaAsiste, Motivo, Usuario); }

        public DataTable GetDataPlanillasAsistencias_CompensacionAdelantada_Liberar(string empresa, string Periodo, string Plla, int IDPersona, string FechaLibera, string FechaAsiste, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.GetDataPlanillasAsistencias_CompensacionAdelantada_Liberar(empresa, Periodo, Plla, IDPersona, FechaLibera, FechaAsiste, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_BuscarCompensacionAdelantada(string Empresa, string Plla, int IDPersona, string FechaInicio, string FechaFin)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_BuscarCompensacionAdelantada(Empresa, Plla, IDPersona, FechaInicio, FechaFin); }

        public DataTable GetDataRRHH_ConstanciaNoDeudo(int IDPersona)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataRRHH_ConstanciaNoDeudo(IDPersona);
        }
        public int GetCorrelativoNoDeudo(string tipodoc)
        {
            return clsRecursosHumanosDAO.Instancia.GetCorrelativoNoDeudo(tipodoc);
        }
        public DataTable GetDataHojaRecorrido_ConstanciaNoDeudo_Registrar(int numero, int empleado, string tipo, string asunto, string fecha, string Cuerpo, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_ConstanciaNoDeudo_Registrar(numero, empleado, tipo, asunto, fecha, Cuerpo, Usuario);
        }

        public DataTable GetDataHojaRecorrido_ConstanciaNoDeudo_Alertar_Pendientes(int Opcion, int IDPersona, string Motivo, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.GetDataHojaRecorrido_ConstanciaNoDeudo_Alertar_Pendientes(Opcion, IDPersona, Motivo, Usuario);
        }

        public DataTable ReportesApp_RRHH_Obligaciones_Menu_Insertar(string xmlDetalle, string Usuario,bool checkCTS)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Obligaciones_Menu_Insertar(xmlDetalle, Usuario, checkCTS);
        }

        public DataTable ReportesApp_RRHH_Reporte_IngresoXSubsidios(string fini, string ffin)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Reporte_IngresoXSubsidios(fini, ffin);
        }

        public DataTable ReportesApp_RRHH_BonoConductores_ListarMotivos(string TipoBono)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_BonoConductores_ListarMotivos(TipoBono);
        }

        public DataTable ReportesApp_RRHH_Vacaciones_ListarAreas(int Accion)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Vacaciones_ListarAreas(Accion);
        }

        public DataTable ReportesApp_RRHH_Vacaciones_InsertarVacaciones(int Persona, DateTime FechaInicio, DateTime FechaFin, int TotalDias, string xml, int DiasAnticipacion, int Alerta, int Todos, int reemplazo)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Vacaciones_InsertarVacaciones(Persona, FechaInicio, FechaFin, TotalDias, xml, DiasAnticipacion, Alerta, Todos, reemplazo);
        }

        public DataTable ReportesApp_RRHH_Vacaciones_ListarVacaciones(string Nombre, int Accion)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Vacaciones_ListarVacaciones(Nombre, Accion);
        }

        public DataTable ReportesApp_RRHH_Vacaciones_EliminarVacaciones(int Persona, int idVacaciones)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Vacaciones_EliminarVacaciones(Persona, idVacaciones);
        }

        public bool ReportesApp_RRHH_BonoConductores_ReabrirAsistencias(string fechainicio, string fechacierre)
        {
            try
            {
                return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_BonoConductores_ReabrirAsistencias(fechainicio, fechacierre);
            }
            catch (Exception )
            {
                
                throw;
            }
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(int Opcion)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(Opcion);
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_RegistrarSolicitudPersonal(int CodAreaSpring, int CodigoPuesto, int NroVacantes, int idTipo, string Prioridad, int CodReemplazo, string Observacion, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_SolicitudesPersonal_RegistrarSolicitudPersonal(CodAreaSpring, CodigoPuesto, NroVacantes, idTipo, Prioridad, CodReemplazo, Observacion, Usuario); }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitudesPersonal(int CodAreaSpring, string FechaInicio, string FechaFin, int idEstadoSolicitud)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitudesPersonal(CodAreaSpring, FechaInicio, FechaFin, idEstadoSolicitud);
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitud(int idSolicitudPersonal)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitud(idSolicitudPersonal);
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_EditarSolicitud(int Opcion, int idSolicitudPersonal, int idEstadoSolicitud, DateTime FechaEntrega, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_SolicitudesPersonal_EditarSolicitud(Opcion, idSolicitudPersonal, idEstadoSolicitud, FechaEntrega, Usuario);
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato(int Accion, int idCandidato, int idSolicitudPersonal, string DNI, string Nombre, DateTime FechaEntrevista, string Estado,
                                                                                      string Observacion, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato(Accion, idCandidato, idSolicitudPersonal, DNI, Nombre, FechaEntrevista, Estado, Observacion, Usuario);
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarCandidatos(int idSolicitudPersonal)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarCandidatos(idSolicitudPersonal);
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarUniformes(int Opcion)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_ListarUniformes(Opcion);
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarEmpleados(string filtroNombre)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_ListarEmpleados(filtroNombre);
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarEmpleadosPuesto(string filtroPuesto)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_ListarEmpleadosPuesto(filtroPuesto);
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarVidaUtil(string idArea, int idCargo, int idUniforme)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_ListarVidaUtil(idArea, idCargo, idUniforme);
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_AsignarUniformes(int idUniforme, int idPersonal, string idArea, int idCargo, int VidaUtil, string Talla, int Cantidad, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_AsignarUniformes(idUniforme, idPersonal, idArea, idCargo, VidaUtil, Talla, Cantidad, Usuario); }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarRegistros(string Personal, string FechaInicio, string FechaFin, int idUniforme)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_ListarRegistros(Personal, FechaInicio, FechaFin, idUniforme);
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado(int Opcion, int idAsignarUniforme, int VidaUtil, DateTime NuevaFecha, int Cantidad, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado(Opcion, idAsignarUniforme, VidaUtil, NuevaFecha, Cantidad, Usuario); }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarHistorial(string Personal, string FechaInicio, string FechaFin, int idUniforme)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ControlUniformes_ListarHistorial(Personal, FechaInicio, FechaFin, idUniforme);
        }

        public DataTable ReportesApp_RRHH_EliminarAsistenciaNoche(int IDPersona, string fecha, string usuario,string NroRecibo)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_EliminarAsistenciaNoche(IDPersona, fecha, usuario,NroRecibo);
        }

        public DataTable ReportesApp_RRHH_AsistenciasNoche_EliminarNoche(int IDPersona, DateTime fecha)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_AsistenciasNoche_EliminarNoche(IDPersona, fecha); }

        public DataTable ReportesApp_RRHH_AsistenciasNoche_CompensarNoche(int Opcion, int IDPersona, DateTime Fecha, string CodGasto, DateTime FechaComp, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_AsistenciasNoche_CompensarNoche(Opcion, IDPersona, Fecha, CodGasto, FechaComp, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_Noche_ListarPlanillas(int IDPersona, string CodGasto)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_Noche_ListarPlanillas(IDPersona, CodGasto); }

        public DataTable ReportesApp_RRHH_ObtenerFechaServidor()
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_ObtenerFechaServidor();
        }

        public bool ReporteApp_RRHH_RegistrarAsistenciaExterna(string TipoRegistro, string fechaPC, string fechaServidor, string usuario,string compania)
        {
            return clsRecursosHumanosDAO.Instancia.ReporteApp_RRHH_RegistrarAsistenciaExterna(TipoRegistro, fechaPC, fechaServidor, usuario,compania);
        }

        public DataTable ReportesApp_ListarAsistenciaExterna(string fechaInicio, string fechaFin)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_ListarAsistenciaExterna(fechaInicio,fechaFin);
        }

        public DataTable ReportesApp_RRHH_Asistencias_ListarTardanzas(int Opcion, string FechaInicio, string FechaFin, string Sucursal, string Area, string Empleado, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_ListarTardanzas(Opcion, FechaInicio, FechaFin, Sucursal, Area, Empleado, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(int Opcion, int idMotivo, int idPersona, DateTime FechaIngreso, string Motivo, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(Opcion, idMotivo, idPersona, FechaIngreso, Motivo, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_ListarTardanzasExcel(string FechaInicio, string FechaFin, string HoraInicio, string HoraFin, string Empleado)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_ListarTardanzasExcel(FechaInicio, FechaFin, HoraInicio, HoraFin, Empleado); }

        public DataTable ReportesApp_RRHH_AsistenciasView_ListarIndicadores(DateTime Fecha)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_AsistenciasView_ListarIndicadores(Fecha); }

        public DataTable ReportesApp_RRHH_Asistencias_ContarCompensaciones(int idPersona)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_ContarCompensaciones(idPersona); }

        public DataTable ReportesApp_RRHH_Asistencias_Compensar_Volcan(int IDPersona, DateTime FechaCompensa, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_Compensar_Volcan(IDPersona, FechaCompensa, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_ListarCompensacion_Volcan(int Opcion, int IDPersona, DateTime FechaIni, DateTime FechaFin)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_ListarCompensacion_Volcan(Opcion, IDPersona, FechaIni, FechaFin); }

        public DataTable ReportesApp_RRHH_Asistencias_EliminarCompensacion_Volcan(int IDPersona, int IDTipoAsist, DateTime Fecha, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_EliminarCompensacion_Volcan(IDPersona, IDTipoAsist, Fecha, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_CompensarAdelantado_Volcan(int IDPersona, DateTime FechaSeleccionada, DateTime FechaCompensa, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_CompensarAdelantado_Volcan(IDPersona, FechaSeleccionada, FechaCompensa, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_EliminarCompAdelantado_Volcan(int IDPersona, int IDTipoAsist, DateTime FechaComp, DateTime FechaAsist, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_EliminarCompAdelantado_Volcan(IDPersona, IDTipoAsist, FechaComp, FechaAsist, Usuario); }

        public DataTable ReportesApp_RRHH_Asistencias_AsistenciaExtendida(string Periodo, string xml_Asistencias, string Usuario)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_AsistenciaExtendida(Periodo, xml_Asistencias, Usuario); }

        public DataTable ReportesApp_Operaciones_PlanConductores_Listar(DateTime Fecha)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_Operaciones_PlanConductores_Listar(Fecha); }

        public DataTable ReportesApp_Operacion_OperacionxConductor(int p)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_Operacion_OperacionxConductor(p); }

        public DataTable ReportesApp_RRHH_Vacaciones_ProgramarVacacionesPendientes(int Opcion, int idProgV, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                   DateTime FechaFin, int Reemplazo, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Vacaciones_ProgramarVacacionesPendientes(Opcion, idProgV, Persona, DiasPendientes, FechaInicio,
                                                                                                             FechaFin, Reemplazo, Usuario);
        }

        public DataTable ReportesApp_RRHH_Vacaciones_ListarVacacionesPendientes(string Nombre, string Area)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Vacaciones_ListarVacacionesPendientes(Nombre, Area); }

        public DataTable ReportesApp_RRHH_Asistencias_ListarRetornos(string Operacion, string Fecha)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Asistencias_ListarRetornos(Operacion, Fecha); }

        public DataTable ReportesApp_RRHH_Capacitaciones_ListarEmpleados(int Opcion, string Personal)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Capacitaciones_ListarEmpleados(Opcion, Personal); }

        public DataTable ReportesApp_RRHH_Capacitaciones_RegistrarEditarEliminarCapacitacion(int Opcion, int idCapacitacion, int Persona, int Proveedor, byte[] Archivo,
                                                                                             string Titulo, string Extension, int Duracion, DateTime FechaInicio,
                                                                                             decimal Monto, string Usuario)
        {
            return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Capacitaciones_RegistrarEditarEliminarCapacitacion(Opcion, idCapacitacion, Persona, Proveedor, Archivo,
                                                                                   Titulo, Extension, Duracion, FechaInicio, Monto, Usuario);
        }

        public DataTable ReportesApp_RRHH_Capacitaciones_ListarCapacitaciones(string Empleado, string FechaInicio, string FechaFin, string Estado)
        { return clsRecursosHumanosDAO.Instancia.ReportesApp_RRHH_Capacitaciones_ListarCapacitaciones(Empleado, FechaInicio, FechaFin, Estado); }
    }
}
