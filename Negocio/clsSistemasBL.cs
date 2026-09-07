using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;


namespace Negocio
{
    public class clsSistemasBL
    {
        private readonly static clsSistemasBL instancia = new clsSistemasBL();

        public static clsSistemasBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetCelulares(int opcion, int persona)
        {
            return clsSistemasDAO.Instancia.GetCelulares(opcion,persona);
        }

        public string InsertCelular(string marca,string modelo,string imei, string observaciones)
        {
            return clsSistemasDAO.Instancia.InsertCelular(marca,modelo,imei, observaciones);
        }

        public string UpdateCelular(string id,string marca, string modelo, string imei, string observaciones)
        {
            return clsSistemasDAO.Instancia.UpdateCelular(id,marca, modelo, imei, observaciones);
        }

        public string BajaCelular(string id, string observaciones)
        {
            return clsSistemasDAO.Instancia.BajaCelular(id, observaciones);
        }

        public DataTable GetAsignacionesCel(String usuario,int persona)
        {
            return clsSistemasDAO.Instancia.GetAsignacionesCel(usuario,persona);
        }
        public DataTable GetAsignacionesCel_Consultas(int opcion, int persona)
        {
            return clsSistemasDAO.Instancia.GetAsignacionesCel_Consultas(opcion, persona);
        }
         public DataTable GetAsignacionesCel_Update(int opcion, int persona, int codigo,string usuario)
        {
            return clsSistemasDAO.Instancia.GetAsignacionesCel_Update(opcion, persona,codigo, usuario);
        }

        public DataTable GetAsignacionesActivas()
        {
            return clsSistemasDAO.Instancia.GetAsignacionesActivas();
        }

        public string ValidaTelefono(string imei)
        {
            return clsSistemasDAO.Instancia.ValidaTelefono(imei);
        }

        public DataTable GetTelefono(string imei)
        {
            return clsSistemasDAO.Instancia.GetTelefono(imei);
        }

        public string ValidaEmpleado(int empleado)
        {
            return clsSistemasDAO.Instancia.ValidaEmpleado(empleado);
        }

        public string ValidaNumero(string numero)
        {
            return clsSistemasDAO.Instancia.ValidaNumero(numero);
        }

        public string InsertAsignacion(string idempleado, string numero, string idtelefono, string observaciones, string fechaasignacion) 
        {
            return clsSistemasDAO.Instancia.InsertAsignacion(Convert.ToInt32(idempleado),numero,Convert.ToInt32(idtelefono),
                observaciones,fechaasignacion);
        }

        public string ModificarAsignacion(string idasignacion, string idtelefant, string idempleado, string numero, string idtelefono, string observaciones, string fechaasignacion)
        {
            return clsSistemasDAO.Instancia.ModificarAsignacion(Convert.ToInt32(idasignacion), Convert.ToInt32(idtelefant), 
                Convert.ToInt32(idempleado), numero, Convert.ToInt32(idtelefono), observaciones, fechaasignacion);
        }

        public string EliminarAsignacion(string idasignacion, string idtelefono)
        {
            return clsSistemasDAO.Instancia.EliminarAsignacion(Convert.ToInt32(idasignacion), Convert.ToInt32(idtelefono));
        }

        public DataTable ListaRPC(string compañia)
        {
            return clsSistemasDAO.Instancia.ListaRPC(compañia);
        }

        public DataTable GetListaAccesoSpring(string usuario, string nombres, char estado)
        {
            return clsSistemasDAO.Instancia.GetListaAccesoSpring(usuario, nombres, estado);
        }
        public DataTable GetGestionEquipos_CategoriasListar()
        {
            return clsSistemasDAO.Instancia.GetGestionEquipos_CategoriasListar();
        }
        public DataTable GetGestionEquipos_Registra_Modifica_Elimina(int Opcion, int IDEquipo, int IDCategoria, string Descripcion, string Marca, string Modelo, string Procesador,
               string Placa, string Memoria, string Disco, string Kase, string Lector, string Monitor, string Teclado, string Mouse, string Otros, string IP, string Hostname,
               string Mac,string SistemaOP, string UsuarioWin, string ClaveWin, string AnyDesk_Nro, string AnyDesk_Pass, int Persona, string Area, string User)
        {
            return clsSistemasDAO.Instancia.GetGestionEquipos_Registra_Modifica_Elimina(Opcion,IDEquipo, IDCategoria, Descripcion, Marca, Modelo, Procesador,
               Placa, Memoria, Disco, Kase, Lector, Monitor, Teclado, Mouse, Otros, IP, Hostname,Mac,
               SistemaOP, UsuarioWin, ClaveWin, AnyDesk_Nro, AnyDesk_Pass, Persona, Area, User);
        }
        public DataTable GetGestionEquipos_Listar()
        {
            return clsSistemasDAO.Instancia.GetGestionEquipos_Listar();
        }
        public DataSet ListarEquipos_Sala()
        {
            return clsSistemasDAO.Instancia.ListarEquipos_Sala();
        }

        public Boolean ReportesApp_Registrar_AgendarReuninon(string idUsuario, string fechaInicio, string horaInicio, string horaFin, int idOficina, int numeroSala, string linkReunion, string xmlEquipo)
        {
            return clsSistemasDAO.Instancia.ReportesApp_Registrar_AgendarReuninon(idUsuario, fechaInicio, horaInicio, horaFin, idOficina, numeroSala, linkReunion, xmlEquipo);
        }

        public Boolean ReportesApp_Actualizar_AgendarReuninon(int idAgendarReunion, string idUsuario, string fechaInicio, string horaInicio, string horaFin, int idOficina, int numeroSala, string linkReunion, string xmlEquipo)
        {
            return clsSistemasDAO.Instancia.ReportesApp_Actualizar_AgendarReuninon(idAgendarReunion, idUsuario, fechaInicio, horaInicio, horaFin, idOficina, numeroSala, linkReunion, xmlEquipo);
        }
        public DataTable ReportesApp_ListarReunionesAgendadas(string fechaInicio, string fechaFin, string estadoSolicitud, int idOficina, int idSala)
        {
            return clsSistemasDAO.Instancia.sp_ReportesApp_ListarReunionesAgendadas(fechaInicio, fechaFin, estadoSolicitud, idOficina, idSala);
        }


        public Boolean ReportesApp_Autorizar_Rechazar_Finalizar_SolicitudReunion(string idUsuarioAutoriza, int idAgendarReunion, string Autorizar_Rechazar_Finalizar)
        {
            return clsSistemasDAO.Instancia.ReportesApp_Autorizar_Rechazar_Finalizar_SolicitudReunion(idUsuarioAutoriza, idAgendarReunion, Autorizar_Rechazar_Finalizar);
        }

        // fin
    }


}
