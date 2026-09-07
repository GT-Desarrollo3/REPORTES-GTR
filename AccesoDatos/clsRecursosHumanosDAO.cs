using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using Comun;

namespace AccesoDatos
{
    public class clsRecursosHumanosDAO
    {
        private readonly static clsRecursosHumanosDAO instancia = new clsRecursosHumanosDAO();

        public static clsRecursosHumanosDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataFichaEmpleados(string transpesa, string bra, string altra, string amt, string aduanas, string inomac, char estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Reporte_Ficha_Empleados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                comando.Parameters.Add(new SqlParameter("@INOMAC", inomac));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataVencimientoContratos(string fechaini, string fechafin, string area, string transpesa, string bra, string altra, string amt, string aduanas)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Reporte_Vencimiento_Contratos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@AREA", area));
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPeriodosVacaciones(int persona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Periodos_Vacaciones_Control", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@EMPLEADO", persona));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetUtilizacionVacaciones(int persona, int periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Utilizacion_Vacaciones_Control", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@EMPLEADO", persona));
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPagosVacaciones(int persona, int periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Pagos_Vacaciones_Control", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@EMPLEADO", persona));
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public bool InsertUtilizacion(int periodo, int persona, string fechaini, string fechafin, string tipo, string usuario, bool mediodia)
        {
            try
            {
                bool resultado;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                string query = "";
                if (mediodia == true)
                {
                    query = "INSERT INTO PR_VacacionUtilizacion_Control VALUES " +
                    "(" + periodo + "," + persona + ",'" + fechaini + "','" + fechafin + "','" + tipo +
                    "',0.5,'" + usuario + "',GETDATE(),NULL)";
                }
                else
                {
                    query = "INSERT INTO PR_VacacionUtilizacion_Control VALUES " +
                    "(" + periodo + "," + persona + ",'" + fechaini + "','" + fechafin + "','" + tipo +
                    "',DATEDIFF(DAY,'" + fechaini + "','" + fechafin + "')+1,'" + usuario + "',GETDATE(),NULL)";
                }
                SqlCommand comando = new SqlCommand(query, conexion);
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                conexion.Close();
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public bool BorrarUtilizacion(int periodo, int persona, int secuencia)
        {
            try
            {
                bool resultado;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                string query = "";
                query = "DELETE FROM PR_VacacionUtilizacion_Control WHERE " +
                "NumeroPeriodo = " + periodo + " AND Empleado = " + persona + " AND Secuencia = " + secuencia;
                SqlCommand comando = new SqlCommand(query, conexion);
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                conexion.Close();
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSCTR(int valor, int persona)
        {
            try
            {
                bool resultado;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                string query = "";
                query = "UPDATE EmpleadoMast SET SCTR=" + valor +
                        " WHERE Empleado =" + persona;
                SqlCommand comando = new SqlCommand(query, conexion);
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                conexion.Close();
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetSCTR(string transpesa, string bra)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_SCTR", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetFaltasySuspensiones(string periodoini, string periodofin, string concepto, int estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Faltas_Suspensiones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO_INI", periodoini));
                comando.Parameters.Add(new SqlParameter("@PERIODO_FIN", periodofin));
                comando.Parameters.Add(new SqlParameter("@CONCEPTO", concepto));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public int GetCorrelativoDocumento(string tipodoc)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_CorrelativoDOC", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TIPO_DOC", tipodoc));
                return Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool InsertDocumento(int numero, int empleado, string tipo, string asunto, string fecha,string Cuerpo,string Usuario)
        {
            try
            {
                bool resultado;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_InsertDoc", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NUMERO", numero));
                comando.Parameters.Add(new SqlParameter("@EMPLEADO", empleado));
                comando.Parameters.Add(new SqlParameter("@TIPO", tipo));
                comando.Parameters.Add(new SqlParameter("@ASUNTO", asunto));
                comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                comando.Parameters.Add(new SqlParameter("@CUERPO", Cuerpo));
                comando.Parameters.Add(new SqlParameter("@USUARIO", Usuario));
                int rowsaffected = comando.ExecuteNonQuery();


                //string query = "";
                //query = "INSERT INTO Empleado_File VALUES " +
                //"(" + numero + "," + empleado + ",'" + tipo + "','" + asunto + "'," + fecha +",GETDATE())";
                //SqlCommand comando = new SqlCommand(query, conexion);
                //int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                conexion.Close();
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetAportacionesRetenciones(string compañia, string periodo, string planilla, string proceso)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Aportaciones_Retenciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                comando.Parameters.Add(new SqlParameter("@PLANILLA", planilla));
                comando.Parameters.Add(new SqlParameter("@PROCESO", proceso));
          
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAsistencias(string fechaini, string fechafin, string Sucursal, string Area, string Empleado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Asistencias", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                comando.Parameters.Add(new SqlParameter("@Area", Area));
                comando.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable GetAsistenciasLineal(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_AsistenciasLineal", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetFaltas(int Opcion, string fechaini, string fechafin, string Sucursal, string Area, string Empleado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Faltas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                comando.Parameters.Add(new SqlParameter("@Area", Area));
                comando.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_RRHH_Asistencias_CrearEliminarAsistencia(int Opcion, int idAsistenciaN, int Persona, string Sucursal, DateTime FechaIngreso, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Asistencias_CrearEliminarAsistencia", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idAsistenciaN", idAsistenciaN));
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                comando.Parameters.Add(new SqlParameter("@FechaIngreso", FechaIngreso));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable GetLlegada(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Llegadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                //Console.Write(dtTemp[dc.ColumnName]);
                //Console.Write(comando);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPersonalNoGrato()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT * from HR_PersonaNoGrata";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetPersonaNoGrata()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Persona_NoGrata", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPersonaNoGrataxPeriodo(string periodo, string persona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Persona_NoGrata_Por_Periodo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                comando.Parameters.Add(new SqlParameter("@PERSONA", persona));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPersonaNoGrata_Registro(string IDPersona, string ApePaterno, string ApeMaterno, string Nombres, string DNI, string Fecha, string Motivo, string User)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Persona_NoGrata_Registro", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@ApePaterno", ApePaterno));
                comando.Parameters.Add(new SqlParameter("@ApeMaterno", ApeMaterno));
                comando.Parameters.Add(new SqlParameter("@Nombres", Nombres));
                comando.Parameters.Add(new SqlParameter("@DNI", DNI));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@User", User));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPersonaNoGrata_Eliminar(string IDPersona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;

                comando = new SqlCommand("ReportesApp_RRHH_Persona_NoGrata_Eliminar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPersonaNoGrata_Permisos(string User)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Persona_NoGrata_Permisos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@usuario", User));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetPersonaNoGrataPeriodo(string fechaini, string fechafin, string persona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Persona_NoGrata_Periodo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@PERSONA", persona));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public void GetFaltaConductor(string idconductor,string fecha, string tipo, string descripcion)
        {
               DataTable dtTemp = new DataTable();
               SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
               conexion.Open();
               SqlCommand comando;
               comando = new SqlCommand("ReportesApp_RRHH_Falta_Conductores", conexion);
               comando.CommandType = CommandType.StoredProcedure;
               comando.Parameters.AddWithValue("@IDCONDUCTOR", idconductor);
               comando.Parameters.AddWithValue("@FECHA", fecha);
               comando.Parameters.AddWithValue("@TIPO_FALTA", tipo);
               comando.Parameters.AddWithValue("@DESCRIPCION", descripcion);
               comando.ExecuteNonQuery();
        }

        public DataTable GetMuestraFaltaConductores()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Lista_Falta_Conductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListCondutoresTextbox()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("Select Nombre from OP_TR_Conductor CO INNER JOIN EmpleadoMast E ON E.Empleado=CO.IdPersona Where CO.Estado='A' AND CO.IndTercero='P' AND E.Estado='A' AND E.CodigoCargo='2' Order BY Nombre DESC", conexion);
                //comando = new SqlCommand("Select Nombre from OP_TR_Conductor Where Estado='A' AND IndTercero='P'", conexion);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarMemosCompromisos(string Usuario, int opcion ,string fechain, string fechafin, int idpersona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_ListarMemos", conexion);
                comando.Parameters.Add(new SqlParameter("@USUARIO", Usuario));
                comando.Parameters.Add(new SqlParameter("@OPCION", opcion));
                comando.Parameters.Add(new SqlParameter("@FECHAINICIO", fechain));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@IDPERSONA", idpersona));
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetIDCondutoresTextbox(string nombre)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("Select IdConductor from OP_TR_Conductor Where Nombre LIKE '" + nombre + "' ", conexion);
                //comando = new SqlCommand("Select IdConductor from OP_TR_Conductor Where Nombre = @CONDUCTOR ", conexion);
                //comando.Parameters.Add(new SqlParameter("@CONDUCTOR", nombre));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetContratosVencidosTodos(string fechaini, string fechafin, string bra, string transpesa, string altra, string amt, string aduanas)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Vencimiento_Contratos_Todos", conexion);
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasTrabajores(string transpesa, string bra, string altra, string amt, string aduanas, string periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Planillas_Trabajadores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasTrabajoresPeriodo(string transpesa, string bra, string altra, string amt, string aduanas, string periodo,string filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Planillas_Trabajadores_Por_Mes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                comando.Parameters.Add(new SqlParameter("@FILTRO", filtro));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataTrabajoresCesados(string transpesa, string bra, string altra, string amt, string aduanas, string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Trabajadores_Cesados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                comando.Parameters.Add(new SqlParameter("@FECHAINICIO", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasDetalle(string transpesa, string bra, string altra, string amt, string aduanas, string periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Planillas_Detalle_Por_Periodo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasAsistenciasCargar(string empresa, string periodo, string planilla, string usuario, int Cesados, int Operacion, string Nombre, string Cargo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_AsistenciasView]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@empresa", empresa));
                comando.Parameters.Add(new SqlParameter("@periodo", periodo));
                comando.Parameters.Add(new SqlParameter("@usuario", usuario));
                comando.Parameters.Add(new SqlParameter("@cesados", Cesados));
                comando.Parameters.Add(new SqlParameter("@Planilla", planilla));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                comando.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                comando.Parameters.Add(new SqlParameter("@Cargo", Cargo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable GetDataPlanillasAsistenciasTipoCargar()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_AsistenciasCargarTipo]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarBonosPeriodo(string Periodo, string fini, string ffin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_BonoConductores_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillaOficialMensual(string Compania, string Periodo, string TipoPlanilla, string TodaPlanilla)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Reporte_PlanillaOficial", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", Compania));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@TipoPlanilla", TipoPlanilla));
                comando.Parameters.Add(new SqlParameter("@AllTipoPlanilla", TodaPlanilla));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarDatoFiltros()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_ListarDatoFiltros", conexion);
                comando.CommandType = CommandType.StoredProcedure;
               /// comando.Parameters.Add(new SqlParameter("@Compania", ''));                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //Hoja de Recorrido:------
        //------------------------
        public DataTable GetDataHojaRecorrido_ListarProcesos(string Compania, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_HojaRecorrido_ListarProcesos]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@empresa", Compania));
                comando.Parameters.Add(new SqlParameter("@usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataHojaRecorrido_ListarGrupos(int IdProceso, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_HojaRecorrido_ListarGrupos]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdProceso", IdProceso));
                comando.Parameters.Add(new SqlParameter("@usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataHojaRecorrido_ListarFormato(int IdProceso, int IdGrupo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_HojaRecorrido_ListarFormato]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdProceso", IdProceso));
                comando.Parameters.Add(new SqlParameter("@IdGrupo", IdGrupo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        //Fin Hoja de Recorrido:------
        //------------------------
        public DataTable GetDataHojaRecorrido_Registrar(int Opcion, string compania, string codigohoja, int Anio, int idproceso, int idgrupo, int idpersona, string fecha,
                    int idpersona1, string label1, bool firma1,  string txtObs1, int idpersona2, string label2, bool firma2,  string txtObs2, int idpersona3, string label3,
                    bool firma3,  string txtObs3, int idpersona4, string label4, bool firma4,  string txtObs4, int idpersona5, string label5, bool firma5,  string txtObs5,
                    int idpersona6, string label6, bool firma6, string txtObs6, int idpersona7, string label7, bool firma7,  string txtObs7, int idpersona8, string label8,
                    bool firma8, string txtObs8, int idpersona9, string label9, bool firma9, string txtObs9, int idpersona10, string label10, bool firma10,  string txtObs10,
                    int idpersona11, string label11, bool firma11,  string txtObs11, int idpersona12, string label12, bool firma12,  string txtObs12, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_Registrar_Modificar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Accion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Compania", compania));
                comando.Parameters.Add(new SqlParameter("@CodigoHoja", codigohoja));
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.Parameters.Add(new SqlParameter("@IdProceso", idproceso));
                comando.Parameters.Add(new SqlParameter("@IdGrupo", idgrupo));
                comando.Parameters.Add(new SqlParameter("@IdPersona", idpersona));
                comando.Parameters.Add(new SqlParameter("@fecha", fecha));
                comando.Parameters.Add(new SqlParameter("@Estado", 1));
                comando.Parameters.Add(new SqlParameter("@IdPersona1", idpersona1));
                comando.Parameters.Add(new SqlParameter("@Area1", label1));
                comando.Parameters.Add(new SqlParameter("@Firma1", firma1));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma1", imagen1));
                comando.Parameters.Add(new SqlParameter("@Observa1", txtObs1));
                comando.Parameters.Add(new SqlParameter("@IdPersona2", idpersona2));
                comando.Parameters.Add(new SqlParameter("@Area2", label2));
                comando.Parameters.Add(new SqlParameter("@Firma2", firma2));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma2", imagen2));
                comando.Parameters.Add(new SqlParameter("@Observa2", txtObs2));
                comando.Parameters.Add(new SqlParameter("@IdPersona3", idpersona3));
                comando.Parameters.Add(new SqlParameter("@Area3", label3));
                comando.Parameters.Add(new SqlParameter("@Firma3", firma3));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma3", imagen3));
                comando.Parameters.Add(new SqlParameter("@Observa3", txtObs3));
                comando.Parameters.Add(new SqlParameter("@IdPersona4", idpersona4));
                comando.Parameters.Add(new SqlParameter("@Area4", label4));
                comando.Parameters.Add(new SqlParameter("@Firma4", firma4));
               // comando.Parameters.Add(new SqlParameter("@ImgeFirma4", imagen4));
                comando.Parameters.Add(new SqlParameter("@Observa4", txtObs4));
                comando.Parameters.Add(new SqlParameter("@IdPersona5", idpersona5));
                comando.Parameters.Add(new SqlParameter("@Area5", label5));
                comando.Parameters.Add(new SqlParameter("@Firma5", firma5));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma5", imagen5));
                comando.Parameters.Add(new SqlParameter("@Observa5", txtObs5));
                comando.Parameters.Add(new SqlParameter("@IdPersona6", idpersona6));
                comando.Parameters.Add(new SqlParameter("@Area6", label6));
                comando.Parameters.Add(new SqlParameter("@Firma6", firma6));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma6", imagen6));
                comando.Parameters.Add(new SqlParameter("@Observa6", txtObs6));
                comando.Parameters.Add(new SqlParameter("@IdPersona7", idpersona7));
                comando.Parameters.Add(new SqlParameter("@Area7", label7));
                comando.Parameters.Add(new SqlParameter("@Firma7", firma7));
               // comando.Parameters.Add(new SqlParameter("@ImgeFirma7", imagen7));
                comando.Parameters.Add(new SqlParameter("@Observa7", txtObs7));
                comando.Parameters.Add(new SqlParameter("@IdPersona8", idpersona8));
                comando.Parameters.Add(new SqlParameter("@Area8", label8));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma8", imagen8));
                comando.Parameters.Add(new SqlParameter("@Firma8", firma8));
                comando.Parameters.Add(new SqlParameter("@Observa8", txtObs8));
                comando.Parameters.Add(new SqlParameter("@IdPersona9", idpersona9));
                comando.Parameters.Add(new SqlParameter("@Area9", label9));
                comando.Parameters.Add(new SqlParameter("@Firma9", firma9));
               // comando.Parameters.Add(new SqlParameter("@ImgeFirma9", imagen9));
                comando.Parameters.Add(new SqlParameter("@Observa9", txtObs9));
                comando.Parameters.Add(new SqlParameter("@IdPersona10", idpersona10));
                comando.Parameters.Add(new SqlParameter("@Area10", label10));
                comando.Parameters.Add(new SqlParameter("@Firma10", firma10));
               // comando.Parameters.Add(new SqlParameter("@ImgeFirma10", imagen10));
                comando.Parameters.Add(new SqlParameter("@Observa10", txtObs10));
                comando.Parameters.Add(new SqlParameter("@IdPersona11", idpersona11));
                comando.Parameters.Add(new SqlParameter("@Area11", label11));
                comando.Parameters.Add(new SqlParameter("@Firma11", firma11));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma11", imagen11));
                comando.Parameters.Add(new SqlParameter("@Observa11", txtObs11));
                comando.Parameters.Add(new SqlParameter("@IdPersona12", idpersona12));
                comando.Parameters.Add(new SqlParameter("@Area12", label12));
                comando.Parameters.Add(new SqlParameter("@Firma12", firma12));
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma12", imagen12));
                comando.Parameters.Add(new SqlParameter("@Observa12", txtObs12));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataHojaRecorrido_ListarRegistros(string compania, string FechaInicio, string FechaFin, int idPerdsona, int idProceso, int idGrupo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_ListarFormatos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@empresa", compania));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Procesos", idProceso));
                comando.Parameters.Add(new SqlParameter("@Grupos", idGrupo));
                comando.Parameters.Add(new SqlParameter("@Persona", idPerdsona));                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAreaPersona(int idpersona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select e.FechaIngreso, hr.CodigoPuesto, hr.descripcion Cargo,dep.description Area from hr_puestoempresa hr inner join empleadomast e " +
                "on hr.CodigoPuesto = e.CodigoCargo inner join departmentmst dep on e.DeptoOrganizacion = dep.Department where e.Empleado =" + idpersona;
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataHojaRecorrido_RegistrarPorFormato(int Opcion,int idproceso, int idgrupo, int persona, string fecha, int idpersona1, string area1, int idpersona2, string area2,
                                                                  int idpersona3, string area3, int idpersona4, string area4, int idpersona5, string area5, int idpersona6, string area6,
                                                                  int idpersona7, string area7, int idpersona8, string area8, int idpersona9, string area9, int idpersona10,
                                                                    string area10, int idpersona11, string area11, int idpersona12, string area12, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_Registrar_Formatos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IdProceso", idproceso));
                comando.Parameters.Add(new SqlParameter("@IdGrupo", idgrupo));
                comando.Parameters.Add(new SqlParameter("@IdPersona", persona));
                comando.Parameters.Add(new SqlParameter("@fecha", fecha));
                comando.Parameters.Add(new SqlParameter("@IdPersona1", idpersona1));
                comando.Parameters.Add(new SqlParameter("@Area1", area1));
                comando.Parameters.Add(new SqlParameter("@IdPersona2", idpersona2));
                comando.Parameters.Add(new SqlParameter("@Area2", area2));
                comando.Parameters.Add(new SqlParameter("@IdPersona3", idpersona3));
                comando.Parameters.Add(new SqlParameter("@Area3", area3));
                comando.Parameters.Add(new SqlParameter("@IdPersona4", idpersona4));
                comando.Parameters.Add(new SqlParameter("@Area4", area4));
                comando.Parameters.Add(new SqlParameter("@IdPersona5", idpersona5));
                comando.Parameters.Add(new SqlParameter("@Area5", area5));
                comando.Parameters.Add(new SqlParameter("@IdPersona6", idpersona6));
                comando.Parameters.Add(new SqlParameter("@Area6", area6));
                comando.Parameters.Add(new SqlParameter("@IdPersona7", idpersona7));
                comando.Parameters.Add(new SqlParameter("@Area7", area7));
                comando.Parameters.Add(new SqlParameter("@IdPersona8", idpersona8));
                comando.Parameters.Add(new SqlParameter("@Area8", area8));
                comando.Parameters.Add(new SqlParameter("@IdPersona9", idpersona9));
                comando.Parameters.Add(new SqlParameter("@Area9", area9));
                comando.Parameters.Add(new SqlParameter("@IdPersona10", idpersona10));
                comando.Parameters.Add(new SqlParameter("@Area10", area10));
                comando.Parameters.Add(new SqlParameter("@IdPersona11", idpersona11));
                comando.Parameters.Add(new SqlParameter("@Area11", area11));
                comando.Parameters.Add(new SqlParameter("@IdPersona12", idpersona12));
                comando.Parameters.Add(new SqlParameter("@Area12", area12));
                comando.Parameters.Add(new SqlParameter("@Usuario", usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataHojaRecorrido_ListarHojaEditar(string Compania, int anio, string codigohoja, int idproceso, int idgrupo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_ListarHojaEditar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", Compania));
                comando.Parameters.Add(new SqlParameter("@Anio", anio));
                comando.Parameters.Add(new SqlParameter("@CodigoHoja", codigohoja));
                comando.Parameters.Add(new SqlParameter("@IdProceso", idproceso));
                comando.Parameters.Add(new SqlParameter("@IdGrupo", idgrupo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataHojaRecorrido_Firmar(int Posicion,string Compania, int anio, string codigohoja, int idproceso, int idgrupo,int IdPersona, byte[] imagenfirma,
                                                        string Observacion,string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_Registrar_Firma", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Posicion", Posicion));
                comando.Parameters.Add(new SqlParameter("@Compania", Compania));
                comando.Parameters.Add(new SqlParameter("@CodigoHoja", codigohoja));
                comando.Parameters.Add(new SqlParameter("@Anio", anio));
                comando.Parameters.Add(new SqlParameter("@IdProceso", idproceso));
                comando.Parameters.Add(new SqlParameter("@IdGrupo", idgrupo));
                comando.Parameters.Add(new SqlParameter("@IdPersona", IdPersona));                
                //comando.Parameters.Add(new SqlParameter("@ImgeFirma1", imagenfirma));
                comando.Parameters.Add(new SqlParameter("@Observa1", Observacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAreaPersonaFirmar(string USUARIO)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select EMPLEADO AS IDPERSONA FROM EmpleadoMast where CodigoUsuario= '" + USUARIO + "'";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataHojaRecorrido_RegistroFirma(int opcion, int idpersona, byte[] imagen1, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_Registrar_FirmaMaestro", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));
                comando.Parameters.Add(new SqlParameter("@IdPersona", idpersona));
                comando.Parameters.Add(new SqlParameter("@ImgeFirma1", imagen1));
                comando.Parameters.Add(new SqlParameter("@Usuario", usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataHojaRecorrido_Accesos(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_Accesos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //GERARDO - 01/08/23
        public DataTable GetDataBonoRegistrarExcepciones(int opcion, int idpersona, string TipoBono, int idMotivoBono, string Periodo, decimal monto, string motivo, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_BonoConductores_RegistrarExcepciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));
                comando.Parameters.Add(new SqlParameter("@Idpersona", idpersona));
                comando.Parameters.Add(new SqlParameter("@TipoBono", TipoBono));
                comando.Parameters.Add(new SqlParameter("@idMotivoBono", idMotivoBono));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Monto", monto));
                comando.Parameters.Add(new SqlParameter("@Motivo", motivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        //GERARDO - 01/08/23

        public DataTable GetListarExcepciones(int idConductor, string periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_BonoConductores_ListarExcepciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Periodo", periodo));               
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //INICIO ASISTENCIA CONDUCTORES
        public DataTable GetDataPlanillasAsistenciasListarOperaciones(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_AsistenciasView_ListarOperaciones]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@usuario", usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

       /* public DataTable GetDataPlanillasAsistenciasCargar(string empresa, string periodo, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_AsistenciasView]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@empresa", empresa));
                comando.Parameters.Add(new SqlParameter("@periodo", periodo));
                comando.Parameters.Add(new SqlParameter("@usuario", usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }*/

        public DataTable GetDataPlanillasAsistenciasListarTrabajadores(string Compania, string Periodo, string Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_ListarTrabajadores]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", Compania));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasAsistenciasMapearTrabajadores(string Compania, string IdPersona, string Periodo, string Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_MapearTrabajadores]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", Compania));
                comando.Parameters.Add(new SqlParameter("@IdPersona", IdPersona));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_RRHH_Asistencias_QuitarMapeo(int IdPersona, string Periodo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_QuitarMapeo]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdPersona", IdPersona));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable GetDataPlanillasAsistenciasListarMapeados(string Compania, string Periodo, string Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_ListarMapeados]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", Compania));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasAsistenciasRegistrar(string Compania, string Periodo, string Planilla, string xmlAsistencias, int IdTipoAsist, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_Registrar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", Compania));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@xml_Asistencias", xmlAsistencias));
                comando.Parameters.Add(new SqlParameter("@IdTipoAsist", IdTipoAsist));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataPlanillasAsistenciasProcesarLicenciasYVacaciones(string Compania, string Periodo, string Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_ProcesaLicenciasYVacaciones]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@empresa", Compania));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

       /* public DataTable GetDataPlanillasAsistenciasTipoCargar()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_AsistenciasCargarTipo]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }*/

        public DataTable GetDataPlanillasAsistenciasBuscarXCompensar(string empresa, string Plla, int IDPersona, string FInicio, string FFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_BuscarPorXCompensar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@empresa", empresa));
                comando.Parameters.Add(new SqlParameter("@Plla", Plla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaIni", FInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FFin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataPlanillasAsistenciasCompensar(string Compania, string Periodo, string Planilla, int IDPersona, string FechaTrabajada, int IdTipoAsistTrabajada, string FechaCompensa, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_Compensar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", Compania));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaTrabajada", FechaTrabajada));
                comando.Parameters.Add(new SqlParameter("@IdTipoAsistTrabajada", IdTipoAsistTrabajada));
                comando.Parameters.Add(new SqlParameter("@FechaCompensa", FechaCompensa));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataPlanillasAsistenciasLiberarCompensacion(string Compania, string Planilla, int IDPersona, string FechaTrabajada, string FechaCompensa, string Usuario)
        {
            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_Compensar_Liberar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", Compania));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaTrabajada", FechaTrabajada));
                comando.Parameters.Add(new SqlParameter("@FechaCompensa", FechaCompensa));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataPlanillasAsistenciasRegularizaFechaParaCompensar(string Compania, string Periodo, string Planilla, int IDPersona, string FechaRegulariza, string Usuario)
        {
            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_RegularizaFechaParaCompensar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", Compania));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaRegulariza", FechaRegulariza));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasAsistencias_Noches_Registrar(int IDPersona, string Fecha, string CantidadNoche, string Observaciones, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_Noche_Registrar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@Cantidad", CantidadNoche));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observaciones));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataPlanillasAsistenciasBuscarXCompensarNoche(string empresa, string Plla, int IDPersona, string FInicio, string FFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_Noche_BuscarPorXCompensar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@empresa", empresa));
                comando.Parameters.Add(new SqlParameter("@Plla", Plla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaIni", FInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FFin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataPlanillasAsistencias_Noches_Compensar(string empresa, string Periodo, string Plla, int IDPersona, string xmlNoches, string FechaCompensa, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_Noche_Compensar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", empresa));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Plla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@xmlNoches", xmlNoches));
                comando.Parameters.Add(new SqlParameter("@FechaCompensa", FechaCompensa));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataPlanillasAsistencias_Noches_Liberar(string empresa, string Plla, int IDPersona, string FechaLiberar, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_Noche_Liberar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", empresa));
                comando.Parameters.Add(new SqlParameter("@Planilla", Plla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaLiberar", FechaLiberar));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        //FIN ASISTENCIAS.
        //----------------


        public DataTable GetDataGenerarCierre(string Compania, string Periodo, string xmlDatos, string Usuario)
        {
            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_BonoConductores_CierrePeriodo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", Compania));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@XmlDatos", xmlDatos));
                comando.Parameters.Add(new SqlParameter("@UsuarioCierre", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataBonoPeriodosCerrados()
        {
            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_BonoConductores_ListaPeriodosCerrados", conexion);
                comando.CommandType = CommandType.StoredProcedure;               
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataBonoPeriodosCerradosDetalle(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_BonoConductores_ListaPeriodosCerradosDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", "10000000"));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataPlanillasAsistencias_CompensacionAdelantada_Registrar(string empresa, string Periodo, string Plla, int IDPersona, string FechaCompensa, string FechaAsiste, string Motivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_CompensacionAdelantada_Registrar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", empresa));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Plla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaCompensa", FechaCompensa));
                comando.Parameters.Add(new SqlParameter("@FechaAsiste", FechaAsiste));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable GetDataPlanillasAsistencias_CompensacionAdelantada_Liberar(string empresa, string Periodo, string Plla, int IDPersona, string FechaLibera, string FechaAsiste, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_Asistencias_CompensacionAdelantada_Liberar]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", empresa));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Planilla", Plla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaLibera", FechaLibera));
                comando.Parameters.Add(new SqlParameter("@FechaAsiste", FechaAsiste));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_RRHH_Asistencias_BuscarCompensacionAdelantada(string Empresa, string Plla, int IDPersona, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Asistencias_BuscarCompensacionAdelantada", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Empresa", Empresa));
                comando.Parameters.Add(new SqlParameter("@Plla", Plla));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@FechaIni", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }


        public int GetCorrelativoNoDeudo(string tipodoc)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_ConsultaNoDeudo_Correlativo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TIPO_DOC", tipodoc));
                return Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public DataTable GetDataHojaRecorrido_ConstanciaNoDeudo_Registrar(int numero, int empleado, string tipo, string asunto, string fecha, string Cuerpo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_ConsultaNoDeudo_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NUMERO", numero));
                comando.Parameters.Add(new SqlParameter("@EMPLEADO", empleado));
                comando.Parameters.Add(new SqlParameter("@TIPO", tipo));
                comando.Parameters.Add(new SqlParameter("@ASUNTO", asunto));
                comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                comando.Parameters.Add(new SqlParameter("@CUERPO", Cuerpo));
                comando.Parameters.Add(new SqlParameter("@USUARIO", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataRRHH_ConstanciaNoDeudo(int IDPersona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("[dbo].[ReportesApp_RRHH_HojaRecorrido_ConsultaNoDeudoxPersona]", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }



        public DataTable GetDataHojaRecorrido_ConstanciaNoDeudo_Alertar_Pendientes(int Opcion, int IDPersona, string Motivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_HojaRecorrido_ConsultaNoDeudoxPersona_Alertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }

        }

        public DataTable ReportesApp_RRHH_Obligaciones_Menu_Insertar(string xmlDetalle, string Usuario,bool checkCTS)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                if (checkCTS == false)
                {
                    comando = new SqlCommand("ReportesApp_RRHH_Obligaciones_Menu_Insertar", conexion);
                }
                else
                {
                    comando = new SqlCommand("ReportesApp_RRHH_Obligaciones_Menu_Insertar_CTS", conexion);
                }
               
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@xmlDetalle", xmlDetalle));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //GERARDO - 14/7/23
        public DataTable ReportesApp_RRHH_Reporte_IngresoXSubsidios(string fini, string ffin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_Reporte_IngresoXSubsidios", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        //GERARDO - 14/7/23

        //GERARDO - 01/08/23
        public DataTable ReportesApp_RRHH_BonoConductores_ListarMotivos(string TipoBono)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_BonoConductores_ListarMotivos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TipoBono", TipoBono));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }
        //GERARDO - 01/08/23

        //GERARDO - 21/08/23
        public DataTable ReportesApp_RRHH_Vacaciones_ListarAreas(int Accion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Vacaciones_ListarAreas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Accion", Accion));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Vacaciones_InsertarVacaciones(int Persona, DateTime FechaInicio, DateTime FechaFin, int TotalDias, string xml, int DiasAnticipacion, int Alerta, int Todos, int reemplazo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Vacaciones_InsertarVacaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@TotalDias", TotalDias));
                comando.Parameters.Add(new SqlParameter("@xml", xml));
                comando.Parameters.Add(new SqlParameter("@DiasAnticipacion", DiasAnticipacion));
                comando.Parameters.Add(new SqlParameter("@Alerta", Alerta));
                comando.Parameters.Add(new SqlParameter("@Todos", Todos));
                comando.Parameters.Add(new SqlParameter("@Reemplazo", reemplazo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_RRHH_Vacaciones_ListarVacaciones(string Nombre, int Accion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Vacaciones_ListarVacaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@Accion", Accion));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Vacaciones_EliminarVacaciones(int Persona, int idVacaciones)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_RRHH_Vacaciones_EliminarVacaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@idVacaciones", idVacaciones));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        //GERARDO - 21/08/23

        public bool ReportesApp_RRHH_BonoConductores_ReabrirAsistencias(string fechainicio, string fechacierre)
        {
            try
            {
                 SqlCommand cmd = null;
                 bool respuesta = false;

            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_BonoConductores_ReabrirAsistencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechainicio));
                cmd.Parameters.Add(new SqlParameter("@FechaCierre", fechacierre));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return respuesta;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //GERARDO - 20/10/23
        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_SolicitudesPersonal_ListarTablas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_RegistrarSolicitudPersonal(int CodAreaSpring, int CodigoPuesto, int NroVacantes, int idTipo, string Prioridad, int CodReemplazo, string Observacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_SolicitudesPersonal_RegistrarSolicitudPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CodAreaSpring", CodAreaSpring));
                cmd.Parameters.Add(new SqlParameter("@CodigoPuesto", CodigoPuesto));
                cmd.Parameters.Add(new SqlParameter("@NroVacantes", NroVacantes));
                cmd.Parameters.Add(new SqlParameter("@idTipo", idTipo));
                cmd.Parameters.Add(new SqlParameter("@Prioridad", Prioridad));
                cmd.Parameters.Add(new SqlParameter("@CodReemplazo", CodReemplazo));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitudesPersonal(int CodAreaSpring, string FechaInicio, string FechaFin, int idEstadoSolicitud)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitudesPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CodAreaSpring", CodAreaSpring));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idEstadoSolicitud", idEstadoSolicitud));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitud(int idSolicitudPersonal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitud", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idSolicitudPersonal", idSolicitudPersonal));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_EditarSolicitud(int Opcion, int idSolicitudPersonal, int idEstadoSolicitud, DateTime FechaEntrega, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_SolicitudesPersonal_EditarSolicitud", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idSolicitudPersonal", idSolicitudPersonal));
                cmd.Parameters.Add(new SqlParameter("@idEstadoSolicitud", idEstadoSolicitud));
                cmd.Parameters.Add(new SqlParameter("@FechaEntrega", FechaEntrega));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato(int Accion, int idCandidato, int idSolicitudPersonal, string DNI, string Nombre, DateTime FechaEntrevista, string Estado,
                                                                                      string Observacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Accion", Accion));
                cmd.Parameters.Add(new SqlParameter("@idCandidato", idCandidato));
                cmd.Parameters.Add(new SqlParameter("@idSolicitudPersonal", idSolicitudPersonal));
                cmd.Parameters.Add(new SqlParameter("@DNI", DNI));
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@FechaEntrevista", FechaEntrevista));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_SolicitudesPersonal_ListarCandidatos(int idSolicitudPersonal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_SolicitudesPersonal_ListarCandidatos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idSolicitudPersonal", idSolicitudPersonal));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarUniformes(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_ListarUniformes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarEmpleados(string filtroNombre)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_ListarEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@filtroNombre", filtroNombre));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarEmpleadosPuesto(string filtroPuesto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_ListarEmpleadosPuesto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@filtroPuesto", filtroPuesto));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarVidaUtil(string idArea, int idCargo, int idUniforme)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_ListarVidaUtil", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idArea", idArea));
                cmd.Parameters.Add(new SqlParameter("@idCargo", idCargo));
                cmd.Parameters.Add(new SqlParameter("@idUniforme", idUniforme));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_AsignarUniformes(int idUniforme, int idPersonal, string idArea, int idCargo, int VidaUtil, string Talla, int Cantidad, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_AsignarUniformes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idUniforme", idUniforme));
                cmd.Parameters.Add(new SqlParameter("@idPersonal", idPersonal));
                cmd.Parameters.Add(new SqlParameter("@idArea", idArea));
                cmd.Parameters.Add(new SqlParameter("@idCargo", idCargo));
                cmd.Parameters.Add(new SqlParameter("@VidaUtil", VidaUtil));
                cmd.Parameters.Add(new SqlParameter("@Talla", Talla));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarRegistros(string Personal, string FechaInicio, string FechaFin, int idUniforme)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_ListarRegistros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Personal", Personal));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idUniforme", idUniforme));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado(int Opcion, int idAsignarUniforme, int VidaUtil, DateTime NuevaFecha, int Cantidad, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idAsignarUniforme", idAsignarUniforme));
                cmd.Parameters.Add(new SqlParameter("@VidaUtil", VidaUtil));
                cmd.Parameters.Add(new SqlParameter("@NuevaFecha", NuevaFecha));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ControlUniformes_ListarHistorial(string Personal, string FechaInicio, string FechaFin, int idUniforme)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ControlUniformes_ListarHistorial", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Personal", Personal));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idUniforme", idUniforme));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }
        //GERARDO - 20/10/23

        public DataTable ReportesApp_RRHH_EliminarAsistenciaNoche(int IDPersona, string fecha, string usuario, string recibo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_EliminarAsistenciaNoche", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@Fecha", fecha));
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@NroRecibo", recibo));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_AsistenciasNoche_EliminarNoche(int IDPersona, DateTime Fecha)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_AsistenciasNoche_EliminarNoche", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_AsistenciasNoche_CompensarNoche(int Opcion, int IDPersona, DateTime Fecha, string CodGasto, DateTime FechaComp, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_AsistenciasNoche_CompensarNoche", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                cmd.Parameters.Add(new SqlParameter("@FechaComp", FechaComp));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_Noche_ListarPlanillas(int IDPersona, string CodGasto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_Noche_ListarPlanillas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_ObtenerFechaServidor()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_ObtenerFechaHora_Servidor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReporteApp_RRHH_RegistrarAsistenciaExterna(string TipoRegistro, string fechaPC, string fechaServidor, string usuario,string compania)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReporteApp_RRHH_RegistrarAsistenciaExterna", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                comando.Parameters.AddWithValue("@TipoRegistro", TipoRegistro);
                comando.Parameters.AddWithValue("@fechaPC", fechaPC);
                comando.Parameters.AddWithValue("@fechaServidor", fechaServidor);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@compania", compania);


                SqlDataReader dr = comando.ExecuteReader();


                if (dr.Read())
                {

                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_ListarAsistenciaExterna(string fechaInicio, string fechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarAsistenciaExterna", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_ListarTardanzas(int Opcion, string FechaInicio, string FechaFin, string Sucursal, string Area, string Empleado, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_ListarTardanzas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@FECHA_INI", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FECHA_FIN", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(int Opcion, int idMotivo, int idPersona, DateTime FechaIngreso, string Motivo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_CrearEliminarMotivo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idMotivo", idMotivo));
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaIngreso", FechaIngreso));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_ListarTardanzasExcel(string FechaInicio, string FechaFin, string HoraInicio, string HoraFin, string Empleado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_ListarTardanzasExcel", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@HoraInicio", HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@HoraFin", HoraFin));
                cmd.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_AsistenciasView_ListarIndicadores(DateTime Fecha)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_AsistenciasView_ListarIndicadores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_ContarCompensaciones(int idPersona)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_ContarCompensaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_Compensar_Volcan(int IDPersona, DateTime FechaCompensa, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_Compensar_Volcan", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaCompensa", FechaCompensa));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_ListarCompensacion_Volcan(int Opcion, int IDPersona, DateTime FechaIni, DateTime FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_ListarCompensacion_Volcan", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaIni", FechaIni));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_EliminarCompensacion_Volcan(int IDPersona, int IDTipoAsist, DateTime Fecha, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_EliminarCompensacion_Volcan", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@IDTipoAsist", IDTipoAsist));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_CompensarAdelantado_Volcan(int IDPersona, DateTime FechaSeleccionada, DateTime FechaCompensa, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_CompensarAdelantado_Volcan", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaSeleccionada", FechaSeleccionada));
                cmd.Parameters.Add(new SqlParameter("@FechaCompensa", FechaCompensa));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_EliminarCompAdelantado_Volcan(int IDPersona, int IDTipoAsist, DateTime FechaComp, DateTime FechaAsist, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_EliminarCompAdelantado_Volcan", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@IDTipoAsist", IDTipoAsist));
                cmd.Parameters.Add(new SqlParameter("@FechaComp", FechaComp));
                cmd.Parameters.Add(new SqlParameter("@FechaAsist", FechaAsist));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_AsistenciaExtendida(string Periodo, string xml_Asistencias, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Asistencias_AsistenciaExtendida", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@xml_Asistencias", xml_Asistencias));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_PlanConductores_Listar(DateTime Fecha)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_PlanConductores_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operacion_OperacionxConductor(int p)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operacion_OperacionxConductor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdPersona", p));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Vacaciones_ProgramarVacacionesPendientes(int Opcion, int idProgV, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                   DateTime FechaFin, int Reemplazo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Vacaciones_ProgramarVacacionesPendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idProgV", idProgV));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@DiasPendientes", DiasPendientes));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Reemplazo", Reemplazo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Vacaciones_ListarVacacionesPendientes(string Nombre, string Area)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Vacaciones_ListarVacacionesPendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Asistencias_ListarRetornos(string Operacion, string Fecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_Asistencias_ListarRetornos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_RRHH_Capacitaciones_ListarEmpleados(int Opcion, string Personal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_Capacitaciones_ListarEmpleados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Personal", Personal));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_RRHH_Capacitaciones_RegistrarEditarEliminarCapacitacion(int Opcion, int idCapacitacion, int Persona, int Proveedor, byte[] Archivo,
                                                                                             string Titulo, string Extension, int Duracion, DateTime FechaInicio,
                                                                                             decimal Monto, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_RRHH_Capacitaciones_RegistrarEditarEliminarCapacitacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@Proveedor", Proveedor));

                SqlParameter pArchivo = new SqlParameter("@Archivo", SqlDbType.VarBinary, -1);
                if (Archivo == null) { pArchivo.Value = DBNull.Value; }
                else { pArchivo.Value = Archivo; }
                cmd.Parameters.Add(pArchivo);

                cmd.Parameters.Add(new SqlParameter("@Titulo", Titulo));
                cmd.Parameters.Add(new SqlParameter("@Extension", Extension));
                cmd.Parameters.Add(new SqlParameter("@Duracion", Duracion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@Monto", Monto));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_RRHH_Capacitaciones_ListarCapacitaciones(string Empleado, string FechaInicio, string FechaFin, string Estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_Capacitaciones_ListarCapacitaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }
    }
}


