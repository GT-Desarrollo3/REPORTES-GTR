using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsSeguridadBL
    {
        private readonly static clsSeguridadBL instancia = new clsSeguridadBL();

        public static clsSeguridadBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataListaFaltaConducta(string nombre)
        {
            return clsSeguridadDAO.Instancia.GetDataListaFaltaConducta(nombre);
        }

        public DataTable GetListaFaltaConducta()
        {
            return clsSeguridadDAO.Instancia.GetListaFaltaConducta();
        }

        public DataTable GetDataFichaEmpleados(string transpesa, string bra, string altra, string amt, string aduanas, char estado)
        {
            return clsSeguridadDAO.Instancia.GetDataFichaEmpleados(transpesa, bra, altra, amt, aduanas, estado);
        }

        public DataTable GetListaCalificativoConductores(string fecha,string OPERACION)
        {
            return clsSeguridadDAO.Instancia.GetListaCalificativoConductores(fecha, OPERACION);
        }

        public DataTable GetDataCargarExcelCalificativo(string cadena,string periodo, string Usuario)
        {
            return clsSeguridadDAO.Instancia.GetDataCargarExcelCalificativo(cadena, periodo, Usuario);
        }

        public DataTable GetListarDetalleCalificativoCoductores(string FechaInicioDetalle, string FechaFinDetalle, int _IdPersonaDetalle)
        {
            return clsSeguridadDAO.Instancia.GetListarDetalleCalificativoCoductores(  FechaInicioDetalle,FechaFinDetalle,_IdPersonaDetalle);
        }

        public DataTable ReportesApp_ListarComboTiposEPPS()
        {
            return clsSeguridadDAO.Instancia.ReportesApp_ListarComboTiposEPPS();
        }

        public bool ReportesApp_Seguridad_Registrar_Actializar_AnularEPPS(string CodInterno, int idTipoEpps, int Operacion, string areaProceso, string area, int idEPPS)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_Registrar_Actializar_AnularEPPS(CodInterno, idTipoEpps, Operacion, areaProceso, area, idEPPS);
        }

        public DataTable ReportesApp_ListarEPPS(string filtroNombre, string FechaInicio, string FechaFin, int idTipoEPPS)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_ListarEPPS(filtroNombre, FechaInicio, FechaFin, idTipoEPPS);
        }

        public DataTable GetRegistrarTipoEPPS(int accion, int TipoEPPS, string Nombre, byte Estado)
        {
            return clsSeguridadDAO.Instancia.GetRegistrarTipoEPPS(accion, TipoEPPS, Nombre, Estado);
        }

        public DataTable GetListarTipoEPPS()
        {
            return clsSeguridadDAO.Instancia.GetListarTipoEPPS();
        }

        public DataTable GetListarPuestos()
        {
            return clsSeguridadDAO.Instancia.GetListarPuestos();
        }

        public DataTable GetRegistrarVidaUtilEPPS(int TipoEPPS, string areaProceso, string area, int cantidadMeses)
        {
            return clsSeguridadDAO.Instancia.GetRegistrarVidaUtilEPPS(TipoEPPS, areaProceso, area, cantidadMeses);
        }

        public DataTable GetListarVidaUtilEPPS()
        {
            return clsSeguridadDAO.Instancia.GetListarVidaUtilEPPS();
        }

        public DataTable FiltrarVidaUtilEPPS(string area)
        {
            return clsSeguridadDAO.Instancia.FiltrarVidaUtilEPPS(area);
        }

        public DataTable GetEditarVidaUtilEPPS(int idVidaUtil, int TipoEPPS, string areaProceso, string area, int cantidadMeses)
        {
            return clsSeguridadDAO.Instancia.GetEditarVidaUtilEPPS(idVidaUtil, TipoEPPS, areaProceso, area, cantidadMeses);
        }

        public DataTable ReportesApp_Seguridad_ListarEmpleados(string filtroNombre)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ListarEmpleados(filtroNombre);
        }

        public DataTable ReportesApp_Seguridad_InsertarEPPSPersonal(int idPersonal, string xml, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_InsertarEPPSPersonal(idPersonal, xml, Usuario);
        }

        public DataTable ReportesApp_Seguridad_ListarEPPSxPersona(string filtroNombre, string FechaInicio, string FechaFin, int idTipoEPPS)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ListarEPPSxPersona(filtroNombre, FechaInicio, FechaFin, idTipoEPPS);
        }

        public DataTable ReportesApp_Seguridad_EliminarEPPSxPersona(int idEPPSPersonal, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_EliminarEPPSxPersona(idEPPSPersonal, Usuario);
        }
        public bool ReportesApp_Seguridad_Registrar_Elimina_EditarGastoReten(int idGasto, string Nombre, int idPersona, string NroPlanilla, DateTime fechaRegistro, decimal importe, string destino, string observacion, int tipoOperacion, string esVale)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_Registrar_Elimina_EditarGastoReten(idGasto, Nombre, idPersona, NroPlanilla, fechaRegistro, importe, destino, observacion, tipoOperacion, esVale);
        }

        public DataTable ReportesApp_Seguridad_ListarRetenes(string fecha,string FechaFin,string nombrePersonal)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ListarRetenes(fecha, FechaFin, nombrePersonal);
        }


        public DataTable ReportesApp_Seguridad_ListarHistorialRetenes(string fecha, string FechaFin, string nombrePersonal)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ListarHistorialRetenes(fecha, FechaFin, nombrePersonal);
        }

        public DataTable ReportesApp_Seguridad_BuscarPersonal(string nombrePersonal)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_BuscarPersonal(nombrePersonal);
        }

        public DataTable ReportesApp_Seguridad_EditarFechaAsignacion(int idEPPSPersonal, DateTime nuevaFecha, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_EditarFechaAsignacion(idEPPSPersonal, nuevaFecha, Usuario);
        }

        public DataTable ReportesApp_Seguridad_SuspenderTempEPPS(int idEPPSPersonal, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_SuspenderTempEPPS(idEPPSPersonal, Usuario);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos(int Opcion, int idGrupo)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos(Opcion, idGrupo);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_AsignarGrupos(int Persona, int idGrupo, int idOperacion)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_AsignarGrupos(Persona, idGrupo, idOperacion);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_RegistrarAsistente(int Opcion, int idAsistente, int idCapacitacion, int idPersona, int idGrupo, int idArea, int Nota, string Condicion, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_RegistrarAsistente(Opcion, idAsistente, idCapacitacion, idPersona, idGrupo, idArea, Nota, Condicion, Usuario);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarAsistentes(int idCapacitacion)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAsistentes(idCapacitacion);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_InsertarModificarCapacitacion(int Opcion, int idCapacitacion, string Titulo, int idTipo, int idLugar, DateTime Fecha, decimal Horas,
                                                                                                    string Instructor, int idProgramacion, int idDetalle, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_InsertarModificarCapacitacion(Opcion, idCapacitacion, Titulo, idTipo, idLugar, Fecha, Horas, Instructor, idProgramacion, idDetalle, Usuario); }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarPersonal(string Filtro)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarPersonal(Filtro);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarCapacitaciones(string Capacitacion, string Instructor, string Personal, string FechaInicio, string FechaFin, int BuscarFecha, string Operacion, int Faltantes)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarCapacitaciones(Capacitacion, Instructor, Personal, FechaInicio, FechaFin, BuscarFecha, Operacion, Faltantes);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_FiltrarCapacitacion(int idCapacitacion)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_FiltrarCapacitacion(idCapacitacion);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasPermisos(string Opcion)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasPermisos(Opcion);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarOperacionGrupo()
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarOperacionGrupo();
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarCombo(int Opcion)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarCombo(Opcion);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ProgramarCapacitacion(int Opcion, string Tema, int idProceso, string L, string M, string X, string J, string V, string S, int idMes, DateTime HoraInicio,
                                                                                           DateTime HoraFin, int idLugar, string xml, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ProgramarCapacitacion(Opcion, Tema, idProceso, L, M, X, J, V, S, idMes, HoraInicio, HoraFin, idLugar, xml, Usuario); }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarProgramaciones(int TipoProg, string Operacion)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarProgramaciones(TipoProg, Operacion); }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_EliminarProgramacion(int TipoProg, int idProgramacion, int Nro)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_EliminarProgramacion(TipoProg, idProgramacion, Nro); }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_FiltrarProgramaciones(string Tema)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_FiltrarProgramaciones(Tema);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_EliminarCapacitacion(int idCapacitacion)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_EliminarCapacitacion(idCapacitacion);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_InsertarTitulo(int Especifico, string Titulo, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_InsertarTitulo(Especifico, Titulo, Usuario); }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_FiltrarTemas(string Titulo)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_FiltrarTemas(Titulo);
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ProgramarFechas(int idPersonal, int idTitulo, DateTime FechaProgramada, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ProgramarFechas(idPersonal, idTitulo, FechaProgramada, Usuario);
        }

        public DataTable ReportesApp_Seguridad_AlcoholTest_BuscarSede(string Usuario, int Opcion)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_AlcoholTest_BuscarSede(Usuario, Opcion); }

        public DataTable ReportesApp_Seguridad_AlcoholTest_BuscarPersonal(string DNI)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_AlcoholTest_BuscarPersonal(DNI); }

        public DataTable ReportesApp_Seguridad_AlcoholTest_IngresarResultado(int idPersona, int ResultadoTest, string Horario, string Sede, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_AlcoholTest_IngresarResultado(idPersona, ResultadoTest, Horario, Sede, Usuario); }

        public DataTable ReportesApp_Seguridad_AlcoholTest_ListarResultados(string Sede, string FechaInicio, string FechaFin)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_AlcoholTest_ListarResultados(Sede, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Seguridad_AlcoholTest_EliminarResultados(int Opcion, int idAlcoholTest, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_AlcoholTest_EliminarResultados(Opcion, idAlcoholTest, Usuario); }

        public DataTable ReportesApp_Seguridad_AlcoholTest_AgregarImagen(int idAlcoholTest, byte[] Imagen, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_AlcoholTest_AgregarImagen(idAlcoholTest, Imagen, Usuario); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades(int Opcion, string Descripcion, string xml)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades(Opcion, Descripcion, xml); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(int Opcion, string Descripcion)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(Opcion, Descripcion); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades(int Opcion, int idAO)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades(Opcion, idAO); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_DesvincularObjetivosActividades(int Opcion, int idObjetivo, int idActividad)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_DesvincularObjetivosActividades(Opcion, idObjetivo, idActividad); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_AsignarObjetivosActividades(int Opcion, int idOO, string xml)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_AsignarObjetivosActividades(Opcion, idOO, xml); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(int Opcion, string Filtro)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(Opcion, Filtro); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_GenerarGestionActividades(int Persona, int idActividad, decimal Peso, string Validacion, string Cronograma, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_GenerarGestionActividades(Persona, idActividad, Peso, Validacion, Cronograma, Usuario); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarGestionActividades(string Actividad, string Area, string FechaInicio, string FechaFin)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarGestionActividades(Actividad, Area, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarPersonalResponsable(int idGestionActividad)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonalResponsable(idGestionActividad); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_AsignarPersonalResponsable(int idGestionActividad, int Persona, decimal Peso)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_AsignarPersonalResponsable(idGestionActividad, Persona, Peso); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_BuscarPersonalResponsable(int Opcion, int idResponsable, int idGestionActividad)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_BuscarPersonalResponsable(Opcion, idResponsable, idGestionActividad); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ValidarUsuario(int idGestionActividad, string Validacion, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_ValidarUsuario(idGestionActividad, Validacion, Usuario); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_AsignarCronograma(int idCronograma, int idGestionActividad, DateTime FechaCronograma, decimal Meta, decimal Meta2, string TipoCronograma)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_AsignarCronograma(idCronograma, idGestionActividad, FechaCronograma, Meta, Meta2, TipoCronograma); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarCronogramaActividades(int idGestionActividad, string FechaInicio, string FechaFin, string TipoCronograma)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarCronogramaActividades(idGestionActividad, FechaInicio, FechaFin, TipoCronograma); }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ModificarCronograma(int Opcion, int idGestionActividad, int idCronograma, decimal MetaUsuario, string TipoCronograma, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_GestionSeguridad_ModificarCronograma(Opcion, idGestionActividad, idCronograma, MetaUsuario, TipoCronograma, Usuario); }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ListarAccidentes(int Opcion)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ListarAccidentes(Opcion); }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias(int Opcion, int idRegistroInc, int idPersona, string Persona, string Area, string Sede,
                                                                   string Grado, string Fecha, string Hora, int TipoIncidente, string TipoDanio, string Tracto, string Carreta, string Operacion,
                                                                   string DescripcionDanio, string Observacion, byte[] Incidente, byte[] IAdicional, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias(Opcion, idRegistroInc, idPersona, Persona, Area, Sede, Grado, Fecha, Hora,
                                                                   TipoIncidente, TipoDanio, Tracto, Carreta, Operacion, DescripcionDanio, Observacion, Incidente, IAdicional, Usuario);
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ListarIncidencias(string TipoIncidente, string Sede, string FechaInicio, string FechaFin)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ListarIncidencias(TipoIncidente, Sede, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_InsertarIncidente(string Incidente)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_InsertarIncidente(Incidente); }

        public DataTable ReportesApp_Seguridad_RegistroEMO_ListarPersonal(string Filtro)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroEMO_ListarPersonal(Filtro); }

        public DataTable ReportesApp_Seguridad_RegistroEMO_ListarEMO(string Conductor, string Estado)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroEMO_ListarEMO(Conductor, Estado); }

        public DataTable ReportesApp_Seguridad_RegistroEMO_RegistrarEMO(int Opcion, int idPersona, string TipoSangre, string TipoEnfermedad, DateTime FechaInicioValidez,
                                                                        DateTime FechaFinValidez, string Categoria, string Usuario, string RutaLocal)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroEMO_RegistrarEMO(Opcion, idPersona, TipoSangre, TipoEnfermedad, FechaInicioValidez, FechaFinValidez, Categoria, Usuario, RutaLocal); }

        public DataTable ReportesApp_Seguridad_RegistroEMO_RegistrarEMOResultado(int Opcion, int idPersona, string TipoSangre, string TipoEnfermedad, DateTime FechaInicioValidez,
                                                                        DateTime FechaFinValidez, string Categoria, string Usuario, string RutaLocal, string Resultados, string Resultados2, string Resultados3)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroEMO_RegistrarEMOResultado(Opcion, idPersona, TipoSangre, TipoEnfermedad, FechaInicioValidez, FechaFinValidez, Categoria, Usuario, RutaLocal,
                                                                        Resultados, Resultados2, Resultados3);
        }

        public DataTable ReportesApp_Seguridad_RegistroEMO_ListarHistorialEMO(string Conductor, string FechaInicio, string FechaFin)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_RegistroEMO_ListarHistorialEMO(Conductor, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_BuscarUsuarios(string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarUsuarios(Usuario); }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_ListarAreas(int Opcion)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarAreas(Opcion); }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_RegistrarEditarDocumentos(int Opcion, int idDocumentoSIG, byte[] Archivo, string Titulo, string Area,
                                                                                       string Proceso, int Version, string Extension, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_DocumentosSIG_RegistrarEditarDocumentos(Opcion, idDocumentoSIG, Archivo, Titulo, Area, Proceso, Version, Extension, Usuario); }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_ListarDocumentos(string Titulo, string Area, string Proceso, string FechaInicio, string FechaFin)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarDocumentos(Titulo, Area, Proceso, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_EliminarDocumentos(int Opcion, int idDocumentoSIG)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_DocumentosSIG_EliminarDocumentos(Opcion, idDocumentoSIG); }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(int Opcion, int idDocumentoSIG)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(Opcion, idDocumentoSIG); }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_ListarOperaciones(int Opcion)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarOperaciones(Opcion); }

        public DataTable ReportesApp_Seguridad_PersonalExterno_InsertarEliminarNombres(int Opcion, int Nro, string Apellidos, string Nombres, string DNI)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_PersonalExterno_InsertarEliminarNombres(Opcion, Nro, Apellidos, Nombres, DNI); }

        public DataTable ReportesApp_Seguridad_PersonalExterno_ListarNombres()
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_PersonalExterno_ListarNombres(); }

        public DataTable ReportesApp_Seguridad_PersonalExterno_RegistrarExterno(int Opcion, string EmpresaExt, byte[] Archivo, string TituloArchivo, string ExtensionArchivo,
                                                                                int PersonaResp, string Area, string Motivo, DateTime FechaIngreso, string Usuario)
        {
            return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_PersonalExterno_RegistrarExterno(Opcion, EmpresaExt, Archivo, TituloArchivo, ExtensionArchivo, PersonaResp,
                                                                                                    Area, Motivo, FechaIngreso, Usuario);
        }

        public DataTable ReportesApp_Seguridad_PersonalExterno_ListarExterno(string FechaInicio, string FechaFin, string Area, string PersonalExt, string EmpresaExt)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_PersonalExterno_ListarExterno(FechaInicio, FechaFin, Area, PersonalExt, EmpresaExt); }

        public DataTable ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos(int Opcion, int idRegistroExt, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos(Opcion, idRegistroExt, Usuario); }

        public DataTable ReportesApp_Seguridad_PersonalExterno_IngresarTiempos(int idRegistroExt, DateTime FechaIngreso, DateTime FechaSalida, string Usuario)
        { return clsSeguridadDAO.Instancia.ReportesApp_Seguridad_PersonalExterno_IngresarTiempos(idRegistroExt, FechaIngreso, FechaSalida, Usuario); }
    }
}
