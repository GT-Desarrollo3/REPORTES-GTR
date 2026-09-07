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
    public class clsMantenimientoDAO
    {
        private readonly static clsMantenimientoDAO instancia = new clsMantenimientoDAO();
        public static clsMantenimientoDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataConsumo(string fini, string ffin, string Grupo, string TipoMaquina, string Placa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiendo_Reporte_Consumo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@Grupo", Grupo));
                comando.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.CommandTimeout = 900000;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        //public DataTable GetDataCosto_Unidades(string fini, string ffin)
        public DataTable GetDataCosto_Unidades()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiendo_Reporte_Costo_por_Unidades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                //comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataTractos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiendo_Lista_Tractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetKilometraje(string tracto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("SELECT NumeroPlaca,Odometro from OP_TR_Vehiculo Where Estado=2 AND IndTercero='P' AND CentroCosto='010201' AND NumeroPlaca= '" + tracto + "'", conexion);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetMaestroMantenimiento()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("SELECT Tipo from MaestroMantenimiento", conexion);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetMantenimientoM1()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("SELECT TipoMantenimiento from MantenimientoM1 M1 INNER JOIN MaestroMantenimiento M ON M1.Tipo=M.Tipo", conexion);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetMantenimientoM2()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("SELECT TipoMantenimiento from MantenimientoM2 M2 INNER JOIN MaestroMantenimiento M ON M2.Tipo=M.Tipo", conexion);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetMantenimientoM3()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("SELECT TipoMantenimiento from MantenimientoM3 M3 INNER JOIN MaestroMantenimiento M ON M3.Tipo=M.Tipo", conexion);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetMarcaTracto(string tracto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("LISTA_TRACTOS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRACTO", tracto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetInsertMantenimientoTracto(string tracto, string AceitMotor,string FiltComb,string Boyas,string Baterias,string CalibMotor,string Filtro1,string Filtro2,string FiltroAire,string Intercooler,string PuenteDelantero,string Alineamiento,string Sensores,string Alternador,string Arrancador,string TemplFajaAlternador,string TemplFajaVentilador,string TemplFajaBombaAgua,string Aceitaja_de_Cambios,string AceitCajaDirec,string MantBocamasa,string AceitCorona,string KMCambioAceiteMotor,string ViajesRestante,string ActUnidadOperativa,string Marca,string Modelo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("Insert_MantenimientoTractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", tracto));
                comando.Parameters.Add(new SqlParameter("@Aceite_Motor", AceitMotor));
                comando.Parameters.Add(new SqlParameter("@Filtro_Combustible", FiltComb));
                comando.Parameters.Add(new SqlParameter("@Limpieza_de_Boyas", Boyas));
                comando.Parameters.Add(new SqlParameter("@Mantenimiento_de_Baterias", Baterias));
                comando.Parameters.Add(new SqlParameter("@Calibracion_de_Motor", CalibMotor));
                comando.Parameters.Add(new SqlParameter("@Filtro_de_Aire_1", Filtro1));
                comando.Parameters.Add(new SqlParameter("@Filtro_de_Aire_2", Filtro2));
                comando.Parameters.Add(new SqlParameter("@Filtro_Secado_de_Aire", FiltroAire));
                comando.Parameters.Add(new SqlParameter("@Intercooler", Intercooler));
                comando.Parameters.Add(new SqlParameter("@Puente_Delantero", PuenteDelantero));
                comando.Parameters.Add(new SqlParameter("@Alineamiento", Alineamiento));
                comando.Parameters.Add(new SqlParameter("@Sensores", Sensores ));
                comando.Parameters.Add(new SqlParameter("@Alternador", Alternador));
                comando.Parameters.Add(new SqlParameter("@Arrancador", Arrancador));
                comando.Parameters.Add(new SqlParameter("@Templador_Faja_Alternador", TemplFajaAlternador));
                comando.Parameters.Add(new SqlParameter("@Templador_Faja_Ventilador", TemplFajaVentilador));
                comando.Parameters.Add(new SqlParameter("@Templador_Faja_Bomba_de_Agua", TemplFajaBombaAgua));
                comando.Parameters.Add(new SqlParameter("@Aceite_Caja_de_Cambios", Aceitaja_de_Cambios));
                comando.Parameters.Add(new SqlParameter("@Aceite_Caja_de_Direccion", AceitCajaDirec));
                comando.Parameters.Add(new SqlParameter("@Mantenimiento_Bocamasa_Delantera", MantBocamasa));
                comando.Parameters.Add(new SqlParameter("@Aceite_Corona", AceitCorona));
                comando.Parameters.Add(new SqlParameter("@KMCambioAceiteMotor", KMCambioAceiteMotor));
                comando.Parameters.Add(new SqlParameter("@ViajesRestante", ViajesRestante));
                comando.Parameters.Add(new SqlParameter("@ActUnidadOperativa", ActUnidadOperativa));
                comando.Parameters.Add(new SqlParameter("@Marca", Marca));
                comando.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPlanTracto()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("Plan_Mantenimiento_Tractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataMantenimientos(int tipo, int mantenimiento, string tracto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand();
                switch (tipo)
                {
                    case 0: //Mantenimiento M1
                        #region Mantenimiento M1
                        switch (mantenimiento)
                        {
                            case 0: //ACEITE MOTOR
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Aceite_Motor", conexion);
                                break;

                            case 1: //FILTRO COMBUSTIBLE 
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Filtro_Combustible", conexion);
                                break;

                            case 2: //LIMPIEZA DE BOYAS
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Limpieza_de_Boyas", conexion);
                                break;

                            case 3: //MANTENIMIENTO DE BATERIAS
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Mantenimiento_de_Baterias", conexion);
                                break;
                        }
                        #endregion
                        break;

                    case 1: //Mantenimiento M2
                        #region Mantenimiento M2
                        switch (mantenimiento)
                        {
                            case 0: //CALIBRACION DE MOTOR
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Calibracion_de_Motor", conexion);
                                break;

                            case 1: //FILTRO DE AIRE 1°
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Filtro_de_Aire_1", conexion);
                                break;

                            case 2: //FILTRO DE AIRE 2°
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Filtro_de_Aire_2", conexion);
                                break;

                            case 3: //FILTRO SECADOR DE AIRE
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Filtro_Secado_de_Aire", conexion);
                                break;

                            case 4: //INTERCOOLER
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Intercooler", conexion);
                                break;

                            case 5: //PUENTE DELANTERO
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Puente_Delantero", conexion);
                                break;

                            case 6: //ALINEAMIENTO
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Alineamiento", conexion);
                                break;

                            case 7: //SENSORES
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Sensores", conexion);
                                break;

                            case 8: //ALTERNADOR 
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Alternador", conexion);
                                break;

                            case 9: //ARRANCADOR
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Arrancador", conexion);
                                break;

                            case 10: //TEMPLADOR FAJA ALTERNADOR 
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Templador_Faja_Alternador", conexion);
                                break;

                            case 11: //TEMPLADOR FAJA VENTILADOR 
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Templador_Faja_Ventilador", conexion);
                                break;

                            case 12: //TEMPLADOR FAJA BOMBA DE AGUA 
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Templador_Faja_Bomba_de_Agua", conexion);
                                break;
                        }
                        #endregion
                        break;

                    case 2: //Mantenimiento M3
                        #region Mantenimiento M3
                        switch (mantenimiento)
                        {
                            case 0: //ACEITE CAJA DE CAMBIOS 
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Aceite_Caja_de_Cambios", conexion);
                                break;

                            case 1: //ACEITE  CAJA DE  DIRECCION
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Aceite_Caja_de_Direccion", conexion);
                                break;

                            case 2: //MTTO BOCAMASA DELANTERA	
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Mantenimiento_Bocamasa_Delantera", conexion);
                                break;

                            case 3: //ACEITE CORONA
                                comando = new SqlCommand("ReportesApp_Mantenimiento_Mantenimiento_Aceite_Corona", conexion);
                                break;
                        }
                        #endregion
                        break;
                }
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRACTO", tracto));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetActividadxUnidadOperativa(string AceitMotor, string FiltComb, string Boyas, string Baterias, string CalibMotor, string Filtro1, string Filtro2, string FiltroAire, string Intercooler, string PuenteDelantero, string Alineamiento, string Sensores, string Alternador, string Arrancador, string TemplFajaAlternador, string TemplFajaVentilador, string TemplFajaBombaAgua, string Aceitaja_de_Cambios, string AceitCajaDirec, string MantBocamasa, string AceitCorona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("Actividad_por_Unidad_Operativa", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Aceite_Motor", AceitMotor));
                comando.Parameters.Add(new SqlParameter("@Filtro_Combustible", FiltComb));
                comando.Parameters.Add(new SqlParameter("@Limpieza_de_Boyas", Boyas));
                comando.Parameters.Add(new SqlParameter("@Mantenimiento_de_Baterias", Baterias));
                comando.Parameters.Add(new SqlParameter("@Calibracion_de_Motor", CalibMotor));
                comando.Parameters.Add(new SqlParameter("@Filtro_de_Aire_1", Filtro1));
                comando.Parameters.Add(new SqlParameter("@Filtro_de_Aire_2", Filtro2));
                comando.Parameters.Add(new SqlParameter("@Filtro_Secado_de_Aire", FiltroAire));
                comando.Parameters.Add(new SqlParameter("@Intercooler", Intercooler));
                comando.Parameters.Add(new SqlParameter("@Puente_Delantero", PuenteDelantero));
                comando.Parameters.Add(new SqlParameter("@Alineamiento", Alineamiento));
                comando.Parameters.Add(new SqlParameter("@Sensores", Sensores));
                comando.Parameters.Add(new SqlParameter("@Alternador", Alternador));
                comando.Parameters.Add(new SqlParameter("@Arrancador", Arrancador));
                comando.Parameters.Add(new SqlParameter("@Templador_Faja_Alternador", TemplFajaAlternador));
                comando.Parameters.Add(new SqlParameter("@Templador_Faja_Ventilador", TemplFajaVentilador));
                comando.Parameters.Add(new SqlParameter("@Templador_Faja_Bomba_de_Agua", TemplFajaBombaAgua));
                comando.Parameters.Add(new SqlParameter("@Aceite_Caja_de_Cambios", Aceitaja_de_Cambios));
                comando.Parameters.Add(new SqlParameter("@Aceite_Caja_de_Direccion", AceitCajaDirec));
                comando.Parameters.Add(new SqlParameter("@Mantenimiento_Bocamasa_Delantera", MantBocamasa));
                comando.Parameters.Add(new SqlParameter("@Aceite_Corona", AceitCorona));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                //SqlDataAdapter da = new SqlDataAdapter(comando);
                //da.Fill(dtTemp);
                //if (dtTemp.Rows.Count > 0)
                //{
                //    dtTemp.Rows[0]["ViajesRestantes"].ToString();
                //}
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_CategoriaRegistra(string Categoria)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_CategoriaRegistra", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Categoria", Categoria));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataMantenimiento_ControlHerramientas_CategoriaModificaElimina(int IDCategoria, string Categoria, int Opcion, string User)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_CategoriaModificaElimina", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IDCategoria", IDCategoria));
                comando.Parameters.Add(new SqlParameter("@Categoria", Categoria));
                comando.Parameters.Add(new SqlParameter("@User", User));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_CategoriaListar()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_CategoriaListar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_ItemsAlmacenListar(string Item)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_ItemsAlmacen_Listar", conexion);
                comando.Parameters.Add(new SqlParameter("@Item", Item));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        // GERARDO - 16/10/23
        public DataTable ReportesApp_Mantenimiento_ControlHerramientas_ListarHerramientasXAlmacen(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_ListarHerramientasXAlmacen", conexion);
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        // GERARDO - 16/10/23

        // GERARDO - 27/09/23
        public DataTable GetDataMantenimiento_ControlHerramientas_HerramientaRegistro(string Descripcion, string ItemAlmacen, string CodidoInterno, int EsDeAlmacen, int IDCategoria, string user)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_HerramientaRegistra", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@CodigoItemAlmacen", ItemAlmacen));
                comando.Parameters.Add(new SqlParameter("@CodigoInterno", CodidoInterno));
                comando.Parameters.Add(new SqlParameter("@EsDeAlmacen", EsDeAlmacen));
                comando.Parameters.Add(new SqlParameter("@IDCategoria", IDCategoria));
                comando.Parameters.Add(new SqlParameter("@UserRegistra", user));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        // GERARDO - 27/09/23

        public DataTable GetDataMantenimiento_ControlHerramientas_HerramientaElimina(int IDHerramienta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_HerramientaElimina", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDHerramienta", IDHerramienta));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        // GERARDO - 27/09/2023
        public DataTable GetDataMantenimiento_ControlHerramientas_Herramientas_Listar(int Opcion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_Herramientas_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        // GERARDO - 27/09/2023

        public DataTable GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(int IDHerramienta, int Persona, string User, int Opcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_HerramPersona_Vincular", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDHerramienta", IDHerramienta));
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@UserRegistra", User));
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        // GERARDO - 27/09/23
        public DataTable GetDataMantenimiento_ControlHerramientas_Herramientas_PersonaHta_Listar(int Persona, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_PersonaHta_Listar", conexion);
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        // GERARDO - 27/09/23

        public DataTable GetMantenimiento_BloqueoUnidades(int Accion,int IdBloqueo,int previaje,int tipopreviaje,int idunidad,string Motivo,string Area,
                                                                    string Descripcion, string Usuario,int predeterminado, string fechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_Unidades_BloquearDesbloquear", conexion);
                comando.Parameters.Add(new SqlParameter("@Accion", Accion));
                comando.Parameters.Add(new SqlParameter("@IdBloqueo", IdBloqueo));
                comando.Parameters.Add(new SqlParameter("@CodigoPreviaje", previaje));
                comando.Parameters.Add(new SqlParameter("@TipoProgramacion", tipopreviaje));
                comando.Parameters.Add(new SqlParameter("@IdUnidad", idunidad));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Area", Area));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@UsuarioBloquea", Usuario));
                comando.Parameters.Add(new SqlParameter("@Predeterminado", predeterminado));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        // GERARDO - 27/05/2023
        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarRegistro(int NroProgramacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarRegistro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        //GERARDO - 18/09/23
        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarSistema(string NuevoSistema)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarSistema", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NuevoSistema", NuevoSistema));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_Insertar(int NroTicket, int idTipoAuxilio, string TipoFalla, string Motivo, DateTime FechaInicio, DateTime HoraInicio, string Ubicacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_Insertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                comando.Parameters.Add(new SqlParameter("@idTipoAuxilio", idTipoAuxilio));
                comando.Parameters.Add(new SqlParameter("@TipoFalla", TipoFalla));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@HoraInicio", HoraInicio));
                comando.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ModificarFalla(int idFalla, int idTipoAuxilio, string TipoFalla, string Motivo, string Ubicacion, string UnidadAfectada, string Validacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ModificarFalla", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                comando.Parameters.Add(new SqlParameter("@idTipoAuxilio", idTipoAuxilio));
                comando.Parameters.Add(new SqlParameter("@TipoFalla", TipoFalla));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                comando.Parameters.Add(new SqlParameter("@UnidadAfectada", UnidadAfectada));
                comando.Parameters.Add(new SqlParameter("@Validacion", Validacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas(string NumeroPlaca, string FechaInicio, string FechaFin, int? idEstadoFalla, string Validacion, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", NumeroPlaca));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idEstadoFalla", idEstadoFalla));
                cmd.Parameters.Add(new SqlParameter("@Validacion", Validacion));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarFallaEditar(int idFalla)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarFallaEditar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_EliminarFalla(int idFalla)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_EliminarFalla", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion(int idFalla, string NombreTercero, string TelefonoTercero, int idTipoRecibo, string Comprobante, decimal MontoComprobante, string Tecnico, string idPlaca, decimal Galones, decimal PrecioUnitario, decimal PrecioTotal, decimal Monto, int idSistemaVehiculo, int idSubSistema, string Solucion, string Usuario, int MttoCorrectivo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                comando.Parameters.Add(new SqlParameter("@NombreTercero", NombreTercero));
                comando.Parameters.Add(new SqlParameter("@TelefonoTercero", TelefonoTercero));
                comando.Parameters.Add(new SqlParameter("@idTipoRecibo", idTipoRecibo));
                comando.Parameters.Add(new SqlParameter("@Comprobante", Comprobante));
                comando.Parameters.Add(new SqlParameter("@MontoComprobante", MontoComprobante));
                comando.Parameters.Add(new SqlParameter("@Tecnico", Tecnico));
                comando.Parameters.Add(new SqlParameter("@idPlaca", idPlaca));
                comando.Parameters.Add(new SqlParameter("@Galones", Galones));
                comando.Parameters.Add(new SqlParameter("@PrecioUnitario", PrecioUnitario));
                comando.Parameters.Add(new SqlParameter("@PrecioTotal", PrecioTotal));
                comando.Parameters.Add(new SqlParameter("@Monto", Monto));
                comando.Parameters.Add(new SqlParameter("@idSistemaVehiculo", idSistemaVehiculo));
                comando.Parameters.Add(new SqlParameter("@idSubSistema", idSubSistema));
                comando.Parameters.Add(new SqlParameter("@Solucion", Solucion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.Parameters.Add(new SqlParameter("@MttoCorrectivo", MttoCorrectivo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucion(int idFalla)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(string Filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Filtro", Filtro));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte(string NroPlaca)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroPlaca", NroPlaca));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte2(string NroPlaca)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte2", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroPlaca", NroPlaca));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarRecibos()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarRecibos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarLiquidacion()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarLiquidacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos(int idSistemaVehiculo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idSistemaVehiculo", idSistemaVehiculo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanica(int idFalla, int valorSalida, string EstadoAuxilio, DateTime Fecha, DateTime Hora, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                comando.Parameters.Add(new SqlParameter("@valorSalida", valorSalida));
                comando.Parameters.Add(new SqlParameter("@EstadoAuxilio", EstadoAuxilio));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@Hora", Hora));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanicaTerceros(int idFalla, DateTime FechaTermino, DateTime HoraTermino, DateTime FechaLlegada, DateTime HoraLlegada, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanicaTerceros", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                comando.Parameters.Add(new SqlParameter("@FechaTermino", FechaTermino));
                comando.Parameters.Add(new SqlParameter("@HoraTermino", HoraTermino));
                comando.Parameters.Add(new SqlParameter("@FechaLlegada", FechaLlegada));
                comando.Parameters.Add(new SqlParameter("@HoraLlegada", HoraLlegada));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_LiquidarFallaMecanica(int idFalla, DateTime FechaLiquidacion, int idEstadoLiquidacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_LiquidarFallaMecanica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                comando.Parameters.Add(new SqlParameter("@FechaLiquidacion", FechaLiquidacion));
                comando.Parameters.Add(new SqlParameter("@idEstadoLiquidacion", idEstadoLiquidacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarMontoDetalle(string Descripcion, decimal Gasto, int TipoRecibo, string Comprobante, DateTime FechaGasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarMontoDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Gasto", Gasto));
                comando.Parameters.Add(new SqlParameter("@TipoRecibo", TipoRecibo));
                comando.Parameters.Add(new SqlParameter("@Comprobante", Comprobante));
                comando.Parameters.Add(new SqlParameter("@FechaGasto", FechaGasto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarMontoDetalle(int idFalla)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarMontoDetalle", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_EliminarMontoDetalle(int idGastoDetalle)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_EliminarMontoDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idGastoDetalle", idGastoDetalle));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarMonto(int idFalla)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarMonto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPrecioPetroleo()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarPrecioPetroleo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }
        // 01/06/2023

        public DataTable ReportesApp_Mantenimiento_ListarRepuestosParaSegundoUso(string item)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ListarRepuestos", conexion);
                if (item.Length > 0)
                {
                    cmd.Parameters.AddWithValue("@NombreItem", item);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NombreItem", DBNull.Value);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ListarItemsInventarioSegundoUso(string item, string Sucursal, int verInactivos)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ListarItemsInventarioSegundoUso", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                if (item.Length > 0) { cmd.Parameters.AddWithValue("@NombreItem", item); }
                else { cmd.Parameters.AddWithValue("@NombreItem", DBNull.Value); }
                cmd.Parameters.AddWithValue("@Sucursal", Sucursal);
                cmd.Parameters.AddWithValue("@VerInactivos", verInactivos);
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Listar_Empleados_SegundoUso(string empleado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Listar_Empleados_SegundoUso", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                if (empleado.Length > 0)
                {
                    cmd.Parameters.AddWithValue("@Nombre", empleado);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Nombre", DBNull.Value);
                }

                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReportesApp_Mantenimiento_Quitar_Editar_ActivoSegundoUSo(int tipo, string idActivo, string Nombre, string unidadMedida, decimal cantidad)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Mantenimiento_Quitar_Editar_ActivoSegundoUSo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@TipoOperacion", tipo);
                comando.Parameters.AddWithValue("@idActivo", idActivo);
                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@unidadMedida", unidadMedida);
                comando.Parameters.AddWithValue("@cantidad", cantidad);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read()) { respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia); }
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public bool ReportesApp_Mantenimiento_RegistrarActivosSegundoUso(string nombre, string codigo, string cantidad, string unidadmedida, string Sucursal)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Mantenimiento_RegistrarActivosSegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@Unidadmedida", unidadmedida);
                comando.Parameters.AddWithValue("@Sucursal", Sucursal);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read()) { respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia); }
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public bool ReportesApp_Mantenimiento_AsignarItemsAlmacen_ActivoSegundoUso_Empleado(string xmlItemsAlmacen, int idActivoSegundoUso, int idEmpleado, string ot, string vale, decimal cantidadUso, string placa,bool consumible)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Mantenimiento_AsignarItemsAlmacen_ActivoSegundoUso_Empleado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                if (xmlItemsAlmacen == "")
                {
                    comando.Parameters.AddWithValue("@xmlItemsAlmacen", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@xmlItemsAlmacen", xmlItemsAlmacen);
                }
                comando.Parameters.AddWithValue("@idActivoSegundoUso", idActivoSegundoUso);
                comando.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                if (ot == "")
                {
                    comando.Parameters.AddWithValue("@ot", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@ot", ot);
                }
                
                comando.Parameters.AddWithValue("@vale", vale);
                comando.Parameters.AddWithValue("@cantidadUso", cantidadUso);
                comando.Parameters.AddWithValue("@placa", placa);
                comando.Parameters.AddWithValue("@Consumible", consumible);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);


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

        public DataTable ReportesApp_Mantenimiento_Listar_VinculoRepuesto_ActivoSegundoUso_Empleado(string FechaInicio, string FechaFin, string Sucursal, string Activo, string OT, string Operacion, string Empleado)
        {

            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Listar_VinculoRepuesto_ActivoSegundoUso_Empleado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.Parameters.AddWithValue("@Sucursal", Sucursal);
                cmd.Parameters.AddWithValue("@Activo", Activo);
                cmd.Parameters.AddWithValue("@OT", OT);
                cmd.Parameters.AddWithValue("@Operacion", Operacion);
                cmd.Parameters.AddWithValue("@Empleado", Empleado);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReportesApp_Mantenimiento_DesvincularActivoSegundoUso_Repuesto_Empleado(string idActivo, string codigoRepuesto, int idEmpleado, string Motivo, string ot,string tipooperacion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Mantenimiento_DesvincularActivoSegundoUso_Repuesto_Empleado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idActivo", idActivo);
                comando.Parameters.AddWithValue("@CodigoRepuesto", codigoRepuesto);
                comando.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                comando.Parameters.AddWithValue("@Motivo", Motivo);
                comando.Parameters.AddWithValue("@OT", ot);
                comando.Parameters.AddWithValue("@TipoOperacion", tipooperacion);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);


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

        public DataTable Reportesapp_Mantenimiento_ListarOrdenesTrabajo(string ot)
        {

            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ListarOrdenesTrabajo", conexion);
                cmd.Parameters.AddWithValue("@OT", ot);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Historial_SegundoUso_Desvinculados()
        {

            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Historial_SegundoUso_Desvinculados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        // GERARDO - 15/12
        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarUnidades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        /*
        public DataTable ReportesApp_Mantenimiento_Solicitudes_BuscarViajes(string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_BuscarViajes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }
        */
        // GERARDO - 15/12

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(int Opcion, int idComponente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarComponentes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idComponente", idComponente));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar(int idComponente, int idComponenteDetalle, int idPosicionLlanta, string Observacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idComponente", idComponente));
                comando.Parameters.Add(new SqlParameter("@idComponenteDetalle", idComponenteDetalle));
                comando.Parameters.Add(new SqlParameter("@idPosicionLlanta", idPosicionLlanta));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar2(int idSolicitud, int idComponente, int idComponenteDetalle, int idPosicionLlanta, string Observacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar2", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                comando.Parameters.Add(new SqlParameter("@idComponente", idComponente));
                comando.Parameters.Add(new SqlParameter("@idComponenteDetalle", idComponenteDetalle));
                comando.Parameters.Add(new SqlParameter("@idPosicionLlanta", idPosicionLlanta));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Modificar(int idSolicitud, int idSolicitudDetalle, int idComponente, int idComponenteDetalle, int idPosicionLlanta, string Observacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Modificar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                comando.Parameters.Add(new SqlParameter("@idSolicitudDetalle", idSolicitudDetalle));
                comando.Parameters.Add(new SqlParameter("@idComponente", idComponente));
                comando.Parameters.Add(new SqlParameter("@idComponenteDetalle", idComponenteDetalle));
                comando.Parameters.Add(new SqlParameter("@idPosicionLlanta", idPosicionLlanta));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_Listar(int Opcion, int idSolicitud, int idSolicitudDetalle)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_SolicitudDetalle_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                comando.Parameters.Add(new SqlParameter("@idSolicitudDetalle", idSolicitudDetalle));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_Editar(int Opcion, int idSolicitudDetalle, int IDC, string OT)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_SolicitudDetalle_Editar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idSolicitudDetalle", idSolicitudDetalle));
                comando.Parameters.Add(new SqlParameter("@idSolicitud", IDC));
                comando.Parameters.Add(new SqlParameter("@OT", OT));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_FiltrarOT(string Placa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_FiltrarOT", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Insertar(int idTracto, int TipoProgramacion, string TipoMtto, string TipoTrabajo, decimal Kilometraje,
                                                                        int idCisterna, string UnidadFalla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_Insertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                comando.Parameters.Add(new SqlParameter("@TipoOperacion", TipoProgramacion));
                comando.Parameters.Add(new SqlParameter("@TipoMtto", TipoMtto));
                comando.Parameters.Add(new SqlParameter("@TipoTrabajo", TipoTrabajo));
                comando.Parameters.Add(new SqlParameter("@Kilometraje", Kilometraje));
                comando.Parameters.Add(new SqlParameter("@idCisterna", idCisterna));
                comando.Parameters.Add(new SqlParameter("@UnidadFalla", UnidadFalla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Listar(string NumeroPlaca, string FechaInicio, string FechaFin, int idBase, string EstadoFalla, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", NumeroPlaca));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idBase", idBase));
                cmd.Parameters.Add(new SqlParameter("@EstadoFalla", EstadoFalla));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarSolicitud(int idSolicitud)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarSolicitud", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Modificar(int idSolicitud, int Opcion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_Modificar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado(string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", Placa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Programar(int Opcion, int idSolicitud, DateTime FechaProgramacion, string Motivo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_Programar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@FechaProgramacion", FechaProgramacion));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Pedidos(int Opcion, int idSolicitud, string Requerimiento, string Descripcion, DateTime FechaLlegadaP)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_Pedidos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@Requerimiento", Requerimiento));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@FechaLlegadaP", FechaLlegadaP));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarPedido(int idSolicitud)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarPedido", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_BuscarNotaIngreso(int Opcion, int idSolicitud, string Requerimiento)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_BuscarNotaIngreso", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@Requerimiento", Requerimiento));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarTalleres(string Taller)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarTalleres", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Taller", Taller));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(string Taller)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Taller", Taller));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarBases(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarBases", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarEstado(string NumeroPlaca, string FechaInicio, string FechaFin, int Ubicacion, string Estado, string EstadoProg)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarEstado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", NumeroPlaca));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@EstadoProg", EstadoProg));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarComboRubros(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarComboRubros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarContacto(int Opcion, int idContacto, string Nombre, string Contacto, string Ubicacion, int idRubro, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarContacto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idContacto", idContacto));
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@Contacto", Contacto));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@idRubro", idRubro));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarContactos(int idRubro, string Ubicacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarContactos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRubro", idRubro));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_BuscarContactos(int idContacto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_BuscarContactos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idContacto", idContacto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_EliminarContacto(int idContacto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_EliminarContacto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idContacto", idContacto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarFechaEstimada(int OpcionFecha, int idSolicitud, DateTime FechaEstimada, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_InsertarFechaEstimada", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionFecha", OpcionFecha));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@FechaEstimada", FechaEstimada));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarFechaRecepcion(int OpcionU, int idSolicitud, DateTime FechaRecepcion, int idBase, string UbicacionTaller, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_InsertarFechaRecepcion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionU", OpcionU));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@FechaRecepcion", FechaRecepcion));
                cmd.Parameters.Add(new SqlParameter("@idBase", idBase));
                cmd.Parameters.Add(new SqlParameter("@UbicacionTaller", UbicacionTaller));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public object ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(string ot,string codigoItem)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Ot", ot));
                cmd.Parameters.Add(new SqlParameter("@CodigoItem", codigoItem.Replace("-S", "").Replace("S", "")));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        //GERARDO - 30/11
        public DataTable ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos(string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_RegistrarModificarBaterias(int opcion, int idBateria, string CodBateria, string Marca, string Modelo, int idVehiculo,
                                                                                              DateTime FechaInicio, int Duracion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_RegistrarModificarBaterias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", opcion));
                cmd.Parameters.Add(new SqlParameter("@idBateria", idBateria));
                cmd.Parameters.Add(new SqlParameter("@CodBateria", CodBateria));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@DuracionBateria", Duracion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_ListarBaterias(int FiltroFechas, string TipoFecha, string CodBateria, string Placa, string FechaInicio, string FechaFin, int Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_ListarBaterias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FiltroFechas", FiltroFechas));
                cmd.Parameters.Add(new SqlParameter("@TipoFecha", TipoFecha));
                cmd.Parameters.Add(new SqlParameter("@CodBateria", CodBateria));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_GenerarInspeccion(int idBateria, string CodBateria, int Intervalo, DateTime FechaCambio, DateTime FechaInspeccion,
                                                                                     decimal NivelCarga, decimal EstadoB, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_GenerarInspeccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idBateria", idBateria));
                cmd.Parameters.Add(new SqlParameter("@CodBateria", CodBateria));
                cmd.Parameters.Add(new SqlParameter("@Intervalo", Intervalo));
                cmd.Parameters.Add(new SqlParameter("@FechaCambio", FechaCambio));
                cmd.Parameters.Add(new SqlParameter("@FechaInspeccion", FechaInspeccion));
                cmd.Parameters.Add(new SqlParameter("@NivelCarga", NivelCarga));
                cmd.Parameters.Add(new SqlParameter("@EstadoB", EstadoB));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria(int Opcion, int idBateria, string Motivo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idBateria", idBateria));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_FiltrarBaterias(int idBateria)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_FiltrarBaterias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idBateria", idBateria));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_RegistrarTraspaso(int idBateria, int idVehiculoAnt, int idVehiculoAct, string Motivo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_RegistrarTraspaso", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idBateria", idBateria));
                cmd.Parameters.Add(new SqlParameter("@idVehiculoAnt", idVehiculoAnt));
                cmd.Parameters.Add(new SqlParameter("@idVehiculoAct", idVehiculoAct));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_ListarTraspasos(int Opcion, string CodBateria, string Placa, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlBaterias_ListarTraspasos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@CodBateria", CodBateria));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_ListarDefectuosos(string Placa, string FechaInicio, string FechaFin, string EstadoFalla)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_SolicitudDetalle_ListarDefectuosos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@EstadoFalla", EstadoFalla));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Consumo_ListarGrupoTipoMaquina(int Opcion, int idGrupo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Consumo_ListarGrupoTipoMaquina", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarTractos(string Placa, int TipoUnidad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_BuscarTractos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto(string Aceite)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Aceite", Aceite));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarOperaciones(int idMttoOP)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarOperaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idMttoOP", idMttoOP));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMtto(int Opcion, int idRegistro, int idVehiculo, int idTipoVehiculo, string Aceite, int Frecuencia, string Ubigeo,
                                                                                       DateTime UltimaFecha, decimal UltimoKM, string TipoMantenimiento, int idMttoOP, int PS, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMtto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@idTipoVehiculo", idTipoVehiculo));
                cmd.Parameters.Add(new SqlParameter("@Aceite", Aceite));
                cmd.Parameters.Add(new SqlParameter("@Frecuencia", Frecuencia));
                cmd.Parameters.Add(new SqlParameter("@Ubigeo", Ubigeo));
                cmd.Parameters.Add(new SqlParameter("@UltimaFecha", UltimaFecha));
                cmd.Parameters.Add(new SqlParameter("@UltimoKM", UltimoKM));
                cmd.Parameters.Add(new SqlParameter("@TipoMantenimiento", TipoMantenimiento));
                cmd.Parameters.Add(new SqlParameter("@idMttoOP", idMttoOP));
                cmd.Parameters.Add(new SqlParameter("@PS", PS));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMttos(string Placa, int idTipoVehiculo, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarMttos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@idTipoVehiculo", idTipoVehiculo));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_FiltrarMttos(int Opcion, int idRegistro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_FiltrarMttos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMtto(string Placa, string FechaInicio, string FechaFin, int idTipoVehiculo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMtto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idTipoVehiculo", idTipoVehiculo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControl(int idVehiculo, int idAccesorio, decimal KMCambio, DateTime FechaCambio, decimal Intervalo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControl", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@idAccesorio", idAccesorio));
                cmd.Parameters.Add(new SqlParameter("@KMCambio", KMCambio));
                cmd.Parameters.Add(new SqlParameter("@FechaCambio", FechaCambio));
                cmd.Parameters.Add(new SqlParameter("@Intervalo", Intervalo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto(int Opcion, int idVehiculo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesos(int idVehiculo, int idAccesorio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@idAccesorio", idAccesorio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesos(int Opcion, int idProcesoMtto, int idVehiculo, decimal Kilometraje, decimal Intervalo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                cmd.Parameters.Add(new SqlParameter("@Kilometraje", Kilometraje));
                cmd.Parameters.Add(new SqlParameter("@Intervalo", Intervalo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_InsertarAccesorio(string Accesorio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_InsertarAccesorio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Accesorio", Accesorio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKM(string Placa, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKM", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMUnidad(int idVehiculo, DateTime Fecha, decimal UltKM)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMUnidad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@UltKM", UltKM));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_InsertarIncidencias(int idFalla, string EstadoUnidad, string Descripcion, string TipoDanio, string Danio, string DescripcionDanio,
                                                                                           string GPS, string Recursos, string Observacion, byte[] imagen, byte[] falla, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_InsertarIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                cmd.Parameters.Add(new SqlParameter("@EstadoUnidad", EstadoUnidad));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@TipoDanio", TipoDanio));
                cmd.Parameters.Add(new SqlParameter("@Danio", Danio));
                cmd.Parameters.Add(new SqlParameter("@DescripcionDanio", DescripcionDanio));
                cmd.Parameters.Add(new SqlParameter("@GPS", GPS));
                cmd.Parameters.Add(new SqlParameter("@Recursos", Recursos));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                if (imagen == null) { cmd.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@Imagen", imagen); }
                if (falla == null) { cmd.Parameters.AddWithValue("@Falla", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@Falla", falla); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ListarIncidencias(string Placa, int idOperacion, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_ListarIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(int Opcion, int idIncidenteC)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems(string Item)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Item", Item));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_RegistrarPresupuesto(int Opcion, int idIncidenteC, string TipoMaterial, string Material, string Unidad, decimal PrecioUnitario,
                                                                                            decimal Cantidad, decimal ImporteTotal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_RegistrarPresupuesto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                cmd.Parameters.Add(new SqlParameter("@TipoMaterial", TipoMaterial));
                cmd.Parameters.Add(new SqlParameter("@Material", Material));
                cmd.Parameters.Add(new SqlParameter("@Unidad", Unidad));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", PrecioUnitario));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@ImporteTotal", ImporteTotal));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_EliminarPresupuesto(int Opcion, int idIncidenteC, int idIncidenteD)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_EliminarPresupuesto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                cmd.Parameters.Add(new SqlParameter("@idIncidenteD", idIncidenteD));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ActualizarMontoSSOMAC(int idIncidenteC, decimal SubTotal, decimal IGV, decimal MontoTotal, string RutaLocal, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_ActualizarMontoSSOMAC", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                cmd.Parameters.Add(new SqlParameter("@SubTotal", SubTotal));
                cmd.Parameters.Add(new SqlParameter("@IGV", IGV));
                cmd.Parameters.Add(new SqlParameter("@MontoTotal", MontoTotal));
                cmd.Parameters.Add(new SqlParameter("@RutaLocal", RutaLocal));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ListarEstadoIncidencias(int idRegistroInc)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_ListarEstadoIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistroInc", idRegistroInc));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias(int Opcion, int idRegistroInc, string Estado, string Observacion,
                                                                                               byte[] imagen, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistroInc", idRegistroInc));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                if (imagen == null) { cmd.Parameters.AddWithValue("@ValeDcto", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ValeDcto", imagen); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_DesbloquearDescargo(int idIncidenteC, string RutaLocal, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_DesbloquearDescargo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                cmd.Parameters.Add(new SqlParameter("@RutaLocal", RutaLocal));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarMonto(int idFalla, decimal SubTotal, decimal IGV, decimal MontoTotal, string RutaLocal, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarMonto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                cmd.Parameters.Add(new SqlParameter("@SubTotal", SubTotal));
                cmd.Parameters.Add(new SqlParameter("@IGV", IGV));
                cmd.Parameters.Add(new SqlParameter("@MontoTotal", MontoTotal));
                cmd.Parameters.Add(new SqlParameter("@RutaLocal", RutaLocal));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarReporte(int idIncidenteC, string RutaLocal, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarReporte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                cmd.Parameters.Add(new SqlParameter("@RutaLocal", RutaLocal));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_InsertarKMUnidad(string xmlDetalleSinTildes, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_InsertarKMUnidad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@xmlDetalle", xmlDetalleSinTildes));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ModificarFrecuencia(int Opcion, int idVehiculo, string MaquinaCodigo, int Frecuencia)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ModificarFrecuencia", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@Frecuencia", Frecuencia));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarOTProgramadas(string Placa, string Descripcion, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarOTProgramadas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_InsertarMecanico(int Opcion, int Persona, string Codigo, string NombreCompleto, string Turno, string Compania)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_InsertarMecanico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@NombreCompleto", NombreCompleto));
                cmd.Parameters.Add(new SqlParameter("@Turno", Turno));
                cmd.Parameters.Add(new SqlParameter("@Compania", Compania));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(int Opcion, string NumeroOrden)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NumeroOrden", NumeroOrden));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico(int Opcion, int Persona, string NumeroOrden)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@NumeroOrden", NumeroOrden));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarHistorial(string Nombre, string FechaInicio, string FechaFin, int HorasExtra, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarHistorial", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@HorasExtra", HorasExtra));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_PausarOTMecanico(int Persona, string NumeroOrden, string MotivoPausa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_PausarOTMecanico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@NumeroOrden", NumeroOrden));
                cmd.Parameters.Add(new SqlParameter("@MotivoPausa", MotivoPausa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarTiempoExtra(int Persona, string NumeroOrden, DateTime FechaActual)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_AsignarTiempoExtra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@NumeroOrden", NumeroOrden));
                cmd.Parameters.Add(new SqlParameter("@FechaActual", FechaActual));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AprobarHorasExtra(int idHistorial, int Estado, string NFechaIni, string NHoraIni, string NFechaFin, string NHoraFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_AprobarHorasExtra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idHistorial", idHistorial));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@NFechaIni", NFechaIni));
                cmd.Parameters.Add(new SqlParameter("@NHoraIni", NHoraIni));
                cmd.Parameters.Add(new SqlParameter("@NFechaFin", NFechaFin));
                cmd.Parameters.Add(new SqlParameter("@NHoraFin", NHoraFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarHorasExtra(int Persona, string NumeroOrden, string Turno, string NFechaIni, string NHoraIni, string NFechaFin, string NHoraFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_AsignarHorasExtra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@NumeroOrden", NumeroOrden));
                cmd.Parameters.Add(new SqlParameter("@Turno", Turno));
                cmd.Parameters.Add(new SqlParameter("@NFechaIni", NFechaIni));
                cmd.Parameters.Add(new SqlParameter("@NHoraIni", NHoraIni));
                cmd.Parameters.Add(new SqlParameter("@NFechaFin", NFechaFin));
                cmd.Parameters.Add(new SqlParameter("@NHoraFin", NHoraFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataSet ReportesApp_Mantenimiento_AsignacionOT_ListarRegistros(int Anio)
        {
            try
            {
                DataSet dtTemp = new DataSet();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarRegistros", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dtTemp);
                return dtTemp;
            }
            catch { return new DataSet(); }
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarAuxilios(string Placa, string Descripcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarAuxilios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarAuxilio(int Opcion, int Persona, int idFalla, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_AsignarAuxilio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarMaquinas(int Opcion, string Placa, string TipoMaquina)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_BuscarMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMaquina(int Opcion, int idRegistroM, string MaquinaCodigo, string Aceite, int Frecuencia, string Dueno, string Ubicacion,
                                                                                       DateTime UltimaFecha, decimal UltimoKM, string TipoMantenimiento, int idMttoOP, int PS, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMaquina", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistroM", idRegistroM));
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@Aceite", Aceite));
                cmd.Parameters.Add(new SqlParameter("@Frecuencia", Frecuencia));
                cmd.Parameters.Add(new SqlParameter("@Dueno", Dueno));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@UltimaFecha", UltimaFecha));
                cmd.Parameters.Add(new SqlParameter("@UltimoKM", UltimoKM));
                cmd.Parameters.Add(new SqlParameter("@TipoMantenimiento", TipoMantenimiento));
                cmd.Parameters.Add(new SqlParameter("@idMttoOP", idMttoOP));
                cmd.Parameters.Add(new SqlParameter("@PS", PS));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosMaquinas(string Placa, string TipoMaquina, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKMMaquina(string Placa, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKMMaquina", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMMaquinas(string MaquinaCodigo, DateTime Fecha, decimal UltKM)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@UltKM", UltKM));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMaquinarias(string Placa, string FechaInicio, string FechaFin, string TipoMaquina)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMaquinarias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlMaquinas(string MaquinaCodigo, int idAccesorio, decimal KMCambio, DateTime FechaCambio, decimal Intervalo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@idAccesorio", idAccesorio));
                cmd.Parameters.Add(new SqlParameter("@KMCambio", KMCambio));
                cmd.Parameters.Add(new SqlParameter("@FechaCambio", FechaCambio));
                cmd.Parameters.Add(new SqlParameter("@Intervalo", Intervalo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoMaquinas(int Opcion, string MaquinaCodigo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosMaquinas(string MaquinaCodigo, int idAccesorio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@idAccesorio", idAccesorio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosMaquinas(int Opcion, int idProcesoMtto, string MaquinaCodigo, decimal Kilometraje, decimal Intervalo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                cmd.Parameters.Add(new SqlParameter("@Kilometraje", Kilometraje));
                cmd.Parameters.Add(new SqlParameter("@Intervalo", Intervalo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_BuscarMaquinas(string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_TicketsLavadero_BuscarMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket(string MaquinaCodigo, DateTime FechaProg, string Operacion, string TipoUsuario, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@FechaProg", FechaProg));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@TipoLavado", TipoUsuario));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_ListarTicket(string CodLavado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_TicketsLavadero_ListarTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodLavado", CodLavado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_Listar(string CodLavado, string Placa, string FechaInicio, string FechaFin, string Estado, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_TicketsLavadero_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodLavado", CodLavado));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_EliminarTicket(string CodLavado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_TicketsLavadero_EliminarTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodLavado", CodLavado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_ActualizarTicket(string CodLavado, DateTime FechaProg, string Estado, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_TicketsLavadero_ActualizarTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodLavado", CodLavado));
                cmd.Parameters.Add(new SqlParameter("@FechaProg", FechaProg));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_MarcarValidacion(string CodLavado, Byte Validacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_TicketsLavadero_MarcarValidacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodLavado", CodLavado));
                cmd.Parameters.Add(new SqlParameter("@Validacion", Validacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ListarHistorial_UnidadesBloqueadas(string fini, string ffin, string Placa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ListarHistorial_UnidadesBloqueadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));
                comando.Parameters.Add(new SqlParameter("@TRACTO", Placa));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarUbicacion(string Ubicacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_InsertarUbicacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto(string Periodo, string Placa, string Programacion, string TipoUnidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                comando.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursos(int Opcion, int idRecursoMtto, int idProcesoMtto, int idVehiculo,
                                                                                        string Item, string Descripcion, decimal Cantidad, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idRecursoMtto", idRecursoMtto));
                comando.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                comando.Parameters.Add(new SqlParameter("@Item", Item));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorio(int idProcesoMtto, int idVehiculo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorio", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursos(string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Actividad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                cmd.Parameters.Add(new SqlParameter("@Actividad", Actividad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursosMaquina(int Opcion, int idRecursoMtto, int idProcesoMtto, string MaquinaCodigo,
                                                                                               string Item, string Descripcion, decimal Cantidad, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursosMaquina", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idRecursoMtto", idRecursoMtto));
                comando.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                comando.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                comando.Parameters.Add(new SqlParameter("@Item", Item));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_RegistrarEliminarRecursos(int Opcion, int idRecursoMtto, int idProcesoMtto, int idVehiculo, string Placa,
                                                                                            string Item, string Descripcion, decimal Cantidad, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_RegistrarEliminarRecursos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idRecursoMtto", idRecursoMtto));
                comando.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Item", Item));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorioMaquina(int idProcesoMtto, string MaquinaCodigo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorioMaquina", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                comando.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarResumenRecursos(string FechaInicio, string FechaFin, string Item)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarResumenRecursos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Item", Item));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarHEMecanico(string Nombre)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarHEMecanico", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp(int Opcion, string CodigoComp, int Persona, string CFechaIni, string CHoraIni,
                                                                                     string CFechaFin, string CHoraFin, string TotalHE, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@CodigoComp", CodigoComp));
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@CFechaIni", CFechaIni));
                comando.Parameters.Add(new SqlParameter("@CHoraIni", CHoraIni));
                comando.Parameters.Add(new SqlParameter("@CFechaFin", CFechaFin));
                comando.Parameters.Add(new SqlParameter("@CHoraFin", CHoraFin));
                comando.Parameters.Add(new SqlParameter("@TotalHE", TotalHE));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarCompensaciones(int Persona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarCompensaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_IngresarEliminarDesbloqueo(int Opcion, int idDesbloqueo, int idVehiculo, int idRuta, DateTime FechaCompromiso, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_IngresarEliminarDesbloqueo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idDesbloqueo", idDesbloqueo));
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@FechaCompromiso", FechaCompromiso));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarDesbloqueoUnidades(string Placa, string Ruta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarDesbloqueoUnidades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_Herramientas_IngresarMaletas(int Opcion, string CodMaleta, string TipoMaleta, int idPersona, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_Herramientas_IngresarMaletas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@CodMaleta", CodMaleta));
                comando.Parameters.Add(new SqlParameter("@TipoMaleta", TipoMaleta));
                comando.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_Herramientas_ListarMaletas(int Opcion, string Empleado, int idMaletaC, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_Herramientas_ListarMaletas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                comando.Parameters.Add(new SqlParameter("@idMaletaC", idMaletaC));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta(int Opcion, int IDHerramienta, int idMaletaC, string CodMaleta, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IDHerramienta", IDHerramienta));
                comando.Parameters.Add(new SqlParameter("@idMaletaC", idMaletaC));
                comando.Parameters.Add(new SqlParameter("@CodMaleta", CodMaleta));
                comando.Parameters.Add(new SqlParameter("@UserRegistra", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_ControlHerramientas_AsignarDevolverMaleta(int Opcion, int idMaletaC, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_ControlHerramientas_AsignarDevolverMaleta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idMaletaC", idMaletaC));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarTolvas(string Tracto, string Carreta, DateTime FechaViaje, int PersonaC, int idRuta, int idTipoAuxilio, string TipoFalla,
                                                                                  string Motivo, DateTime FechaInicio, DateTime HoraInicio, string Ubicacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                comando.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                comando.Parameters.Add(new SqlParameter("@FechaViaje", FechaViaje));
                comando.Parameters.Add(new SqlParameter("@PersonaC", PersonaC));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@idTipoAuxilio", idTipoAuxilio));
                comando.Parameters.Add(new SqlParameter("@TipoFalla", TipoFalla));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@HoraInicio", HoraInicio));
                comando.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_BloquearConductorMtto(int idConductor, string Motivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_BloquearConductorMtto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_BuscarIncidencias(int idIncidenteC)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_BuscarIncidencias", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarIncidencias(int idIncidenteC, int idTipoAuxilio, string EstadoUnidad, string TipoFalla, DateTime FechaInicio, DateTime HoraInicio,
                                                                                             string Ubicacion, string Motivo, string Descripcion, string TipoDanio, string Danio, string GPS, string Recursos,
                                                                                             string Observacion, byte[] imagen, byte[] falla, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idIncidenteC", idIncidenteC));
                cmd.Parameters.Add(new SqlParameter("@idTipoAuxilio", idTipoAuxilio));
                cmd.Parameters.Add(new SqlParameter("@EstadoUnidad", EstadoUnidad));
                cmd.Parameters.Add(new SqlParameter("@TipoFalla", TipoFalla));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@HoraInicio", HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@TipoDanio", TipoDanio));
                cmd.Parameters.Add(new SqlParameter("@Danio", Danio));
                cmd.Parameters.Add(new SqlParameter("@GPS", GPS));
                cmd.Parameters.Add(new SqlParameter("@Recursos", Recursos));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                if (imagen == null) { cmd.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@Imagen", imagen); }
                if (falla == null) { cmd.Parameters.AddWithValue("@Falla", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@Falla", falla); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMtto(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMtto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMttoFecha(string FechaInicio, string FechaFin, string Placa, string Programacion, string TipoUnidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMttoFecha", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                comando.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMttoFechas(string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMttoFechas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarActividades(string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Actividad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                cmd.Parameters.Add(new SqlParameter("@Actividad", Actividad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems(string Item)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Item", Item));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion(string Placa, string Tipo, string SubTipo, string Operacion, string Marca, string Modelo, DateTime FechaProyectada, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Tipo", Tipo));
                cmd.Parameters.Add(new SqlParameter("@SubTipo", SubTipo));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                cmd.Parameters.Add(new SqlParameter("@FechaProyectada", FechaProyectada));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(int Opcion, string Placa, int idInspeccionC)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@idInspeccionC", idInspeccionC));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(int Opcion, int idInspeccionC, int idInspeccionD, string Estado, string Observacion, int Mecanico, int Electrico, int Neumatico, int Soldador,
                                                                                       DateTime FechaInicio, DateTime FechaFin, string Turno, string Sucursal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idInspeccionC", idInspeccionC));
                cmd.Parameters.Add(new SqlParameter("@idInspeccionD", idInspeccionD));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Mecanico", Mecanico));
                cmd.Parameters.Add(new SqlParameter("@Electrico", Electrico));
                cmd.Parameters.Add(new SqlParameter("@Neumatico", Neumatico));
                cmd.Parameters.Add(new SqlParameter("@Soldador", Soldador));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Turno", Turno));
                cmd.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones(int Opcion, string Fecha, string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Sucursal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                cmd.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarPedidosInspecciones(int idInspeccionC)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarPedidosInspecciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idInspeccionC", idInspeccionC));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_InsertarPedidosInspecciones(int Opcion, int idNroPedido, int idInspeccionC, string Item, string Descripcion,
                                                                                              decimal Cantidad, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_InsertarPedidosInspecciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idNroPedido", idNroPedido));
                cmd.Parameters.Add(new SqlParameter("@idInspeccionC", idInspeccionC));
                cmd.Parameters.Add(new SqlParameter("@Item", Item));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RequerimientoServicio_ListarCCosto(string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RequerimientoServicio_ListarCCosto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RequerimientoServicio_ListarReqServicio(string CentroCosto, string Descripcion, string Placa, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RequerimientoServicio_ListarReqServicio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_GenerarReporte(int Opcion, int FiltroFechas, string Periodo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RegistroIncidencias_GenerarReporte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@FiltroFechas", FiltroFechas));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarCanaletas(int Opcion, string Sucursal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_ListarCanaletas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarProgramacion(int Opcion, int idCanaletaProg, int idCanaleta, DateTime FechaProg, string Estado, string Observacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarProgramacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCanaletaProg", idCanaletaProg));
                cmd.Parameters.Add(new SqlParameter("@idCanaleta", idCanaleta));
                cmd.Parameters.Add(new SqlParameter("@FechaProg", FechaProg));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_EliminarProgramacion(int Opcion, int idCanaletaProg)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_EliminarProgramacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCanaletaProg", idCanaletaProg));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarProgramacion(string Sucursal, string Estado, string Canaletas, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_ListarProgramacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Canaleta", Canaletas));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarCambios(int Opcion, int idCCambio, int idCanaleta, DateTime FechaProg, string Observacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarCambios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCCambio", idCCambio));
                cmd.Parameters.Add(new SqlParameter("@idCanaleta", idCanaleta));
                cmd.Parameters.Add(new SqlParameter("@FechaProg", FechaProg));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarCambios(string Sucursal, string Canaleta, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_ListarCambios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                cmd.Parameters.Add(new SqlParameter("@Canaleta", Canaleta));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_RegistrarCambioDetalle(int Opcion, int idCCambio, int idCCambioDetalle, DateTime FechaCambio, decimal LongitudCambio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_RegistrarCambioDetalle", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCCambio", idCCambio));
                cmd.Parameters.Add(new SqlParameter("@idCCambioDetalle", idCCambioDetalle));
                cmd.Parameters.Add(new SqlParameter("@FechaCambio", FechaCambio));
                cmd.Parameters.Add(new SqlParameter("@LongitudCambio", LongitudCambio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarCambiosDetalle(int idCCambio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlCanaletas_ListarCambiosDetalle", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCCambio", idCCambio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarRegistros(string Placa, string Descripcion, string FechaInicio, string FechaFin, string Origen, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoCorrectivo_ListarRegistros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Origen", Origen));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto(int Opcion, int idMttoC, DateTime FechaProg, string OT, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idMttoC", idMttoC));
                cmd.Parameters.Add(new SqlParameter("@FechaProg", FechaProg));
                cmd.Parameters.Add(new SqlParameter("@OT", OT));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarOTProgramadas(string Placa, string OT)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoCorrectivo_ListarOTProgramadas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@OT", OT));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos(string NroReq)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroReq", NroReq));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_IngresarRequerimientos(string NroReq, string CentroCosto, string Proyecto, string Descripcion, DateTime FechaProgramada, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_IngresarRequerimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroReq", NroReq));
                cmd.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                cmd.Parameters.Add(new SqlParameter("@Proyecto", Proyecto));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@FechaProgramada", FechaProgramada));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarTiposMtto(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarTiposMtto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_ListarActividades(string Placa, string Actividad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_SolicitudDetalle_ListarActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Actividad", Actividad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarTarea(int Opcion, int idSolicitudDetalle, int idSolicitud, string CodTarea, DateTime FechaInicio, DateTime FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_InsertarTarea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idSolicitudDetalle", idSolicitudDetalle));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@CodTarea", CodTarea));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_RegistrarOrdenTrabajo(int idSolicitud, string Placa, string TipoMtto, string Clasificacion, string Ubicacion, string Descripcion,
                                                                                     DateTime FechaInicio, DateTime FechaFin, int Mecanico, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_RegistrarOrdenTrabajo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMtto", TipoMtto));
                cmd.Parameters.Add(new SqlParameter("@Clasificacion", Clasificacion));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Mecanico", Mecanico));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos(int Opcion, string Periodo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_MapearMecanicos(int IDPersona, string Periodo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_MapearMecanicos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_QuitarMecánicos(int IDPersona, string Periodo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_QuitarMecánicos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_ListarTablaMecanicos(string Periodo, string Nombre, string Puesto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_ListarTablaMecanicos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@Puesto", Puesto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_IngresarEstadoAsistencia(string Codigo, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_IngresarEstadoAsistencia", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_RegistrarAsistencia(string Periodo, string xmlAsistencia, string Codigo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_RegistrarAsistencia", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@xmlAsistencia", xmlAsistencia));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_EliminarAsistencia(string Periodo, string xmlAsistencia, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_EliminarAsistencia", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@xmlAsistencia", xmlAsistencia));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_ImportarAsistencia(string Periodo, string xmlAsistencia, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_RoosterProyectado_ImportarAsistencia", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@xmlDetalle", xmlAsistencia));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MovimientoC_RegistrarEditarComponentes(int Opcion, int idMovimientoC, int idSistemaVehiculo, int idSubSistema, string Descripcion,
                         int idUnidadProcedencia, int idUnidadDestino, DateTime FechaEjecucion, string Motivo, int PersonaAutorizada, string Requerimiento, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MovimientoC_RegistrarEditarComponentes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idMovimientoC", idMovimientoC));
                cmd.Parameters.Add(new SqlParameter("@idSistemaVehiculo", idSistemaVehiculo));
                cmd.Parameters.Add(new SqlParameter("@idSubSistema", idSubSistema));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@idUnidadProcedencia", idUnidadProcedencia));
                cmd.Parameters.Add(new SqlParameter("@idUnidadDestino", idUnidadDestino));
                cmd.Parameters.Add(new SqlParameter("@FechaEjecucion", FechaEjecucion));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@PersonaAutorizada", PersonaAutorizada));
                cmd.Parameters.Add(new SqlParameter("@Requerimiento", Requerimiento));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MovimientoC_ListarComponentes(int FiltroS, string NumeroPlaca, string FechaInicio, string FechaFin, int idSistema, int idSubSistema)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MovimientoC_ListarComponentes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FiltroS", FiltroS));
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", NumeroPlaca));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idSistema", idSistema));
                cmd.Parameters.Add(new SqlParameter("@idSubSistema", idSubSistema));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumplimiento(string Placa, string Operacion, string TipoUnidad, string Marca, string Ubigeo, string MttoPreventivo,
                         DateTime FechaProgramada, DateTime FCInicio, DateTime FCFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumplimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@Ubigeo", Ubigeo));
                cmd.Parameters.Add(new SqlParameter("@MttoPreventivo", MttoPreventivo));
                cmd.Parameters.Add(new SqlParameter("@FechaProgramada", FechaProgramada));
                cmd.Parameters.Add(new SqlParameter("@FCInicio", FCInicio));
                cmd.Parameters.Add(new SqlParameter("@FCFin", FCFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarCumplimiento(int Anio, int NroSemana, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarCumplimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(int Opcion, int Nro, string TipoMtto, DateTime FechaProgramada, string Estado, string Observacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Nro", Nro));
                cmd.Parameters.Add(new SqlParameter("@TipoMtto", TipoMtto));
                cmd.Parameters.Add(new SqlParameter("@FechaProgramada", FechaProgramada));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento(int Opcion, int Nro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Nro", Nro));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarMttosProgramados(int Anio, int NroSemana)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ContarMttosProgramados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarCalendarioMtto(string Periodo, string Placa, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoCorrectivo_ListarCalendarioMtto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarMttoProgramado(string Placa, DateTime FechaProg)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoCorrectivo_ListarMttoProgramado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaProg", FechaProg));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(int Opcion, int Anio, int NroSemana)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgruparOperacion(int Opcion, int Anio, int NroSemana)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_AgruparOperacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_GenerarSolicitud(int PSolicitud, DateTime FechaRequerida, string Direccion, string Detalle, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_GenerarSolicitud", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@PSolicitud", PSolicitud));
                cmd.Parameters.Add(new SqlParameter("@FechaRequerida", FechaRequerida));
                cmd.Parameters.Add(new SqlParameter("@Direccion", Direccion));
                cmd.Parameters.Add(new SqlParameter("@Detalle", Detalle));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_Listar(string FechaInicio, string FechaFin, string Ticket, string Conductor, string Area, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Ticket", Ticket));
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_CrearTicket(string xmlTicket, int PConductor, int Unidad, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_CrearTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@xmlTicket", xmlTicket));
                cmd.Parameters.Add(new SqlParameter("@PConductor", PConductor));
                cmd.Parameters.Add(new SqlParameter("@Unidad", Unidad));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_ListarTickets(string FechaInicio, string FechaFin, string Ticket, string Conductor)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_ListarTickets", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Ticket", Ticket));
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_IngresarKMs(int idTicketC, decimal KMSalida, decimal KMIngreso, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_IngresarKMs", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idTicketC", idTicketC));
                cmd.Parameters.Add(new SqlParameter("@KMSalida", KMSalida));
                cmd.Parameters.Add(new SqlParameter("@KMIngreso", KMIngreso));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_EliminarSolicitudes(int Opcion, string CodOperativo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_EliminarSolicitudes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@CodOperativo", CodOperativo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_AsignarConductor(string CodOperativo, int PConductor, int Unidad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_AsignarConductor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodOperativo", CodOperativo));
                cmd.Parameters.Add(new SqlParameter("@PConductor", PConductor));
                cmd.Parameters.Add(new SqlParameter("@Unidad", Unidad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_Filtrar(string CodOperativo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlOperativos_Filtrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodOperativo", CodOperativo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarUnidadTaller(string NumeroPlaca)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarUnidadTaller", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", NumeroPlaca));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ModificarUbicacion(int idSolicitud, int idBase, string Taller, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ModificarUbicacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@idBase", idBase));
                cmd.Parameters.Add(new SqlParameter("@Taller", Taller));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarEmpleados(string Empleado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpInspeccion(string Placa, string Operacion, string TipoUnidad, string Marca, DateTime ProxInspeccion,
                         DateTime FCInicio, DateTime FCFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpInspeccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@ProxInspeccion", ProxInspeccion));
                cmd.Parameters.Add(new SqlParameter("@FCInicio", FCInicio));
                cmd.Parameters.Add(new SqlParameter("@FCFin", FCFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpInspeccion(int Anio, int NroSemana, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpInspeccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpInspecciones(int Anio, int NroSemana)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpInspecciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpInspeccion(int Opcion, int Nro, DateTime FechaCump, string Estado, string Observacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpInspeccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Nro", Nro));
                cmd.Parameters.Add(new SqlParameter("@FechaCump", FechaCump));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpActividad(string Placa, string Operacion, string TipoUnidad, string Actividad, DateTime ProxFecha,
                         DateTime FCInicio, DateTime FCFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpActividad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                cmd.Parameters.Add(new SqlParameter("@Actividad", Actividad));
                cmd.Parameters.Add(new SqlParameter("@ProxFecha", ProxFecha));
                cmd.Parameters.Add(new SqlParameter("@FCInicio", FCInicio));
                cmd.Parameters.Add(new SqlParameter("@FCFin", FCFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpActividad(int Anio, int NroSemana, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpActividad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpActividades(int Anio, int NroSemana)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpActividades(int Opcion, int Nro, DateTime FechaCump, string Estado, string Observacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Nro", Nro));
                cmd.Parameters.Add(new SqlParameter("@FechaCump", FechaCump));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosH(string Periodo, string Placa, string TipoUnidad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosH", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ActualizarHorasDisp()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Disponibilidad_ActualizarHorasDisp", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosP(string Periodo, string Placa, string TipoUnidad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosP", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ActualizarPorcDisp()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Disponibilidad_ActualizarPorcDisp", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_PromedioMes(string Periodo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Disponibilidad_PromedioMes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlInventario_ListarSedeArea(int Opcion, string Sede)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlInventario_ListarSedeArea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Sede", Sede));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlInventario_ListarSillas(string Trabajador, string Sede, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlInventario_ListarSillas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Trabajador", Trabajador));
                cmd.Parameters.Add(new SqlParameter("@Sede", Sede));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlInventario_InsertarModificar(int Opcion, int idTicket, string TipoActivo, string Sede, string Area, int idPersona,
                                                                                       string NombrePersona, string Observacion, byte[] CodigoBarras, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlInventario_InsertarModificar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idTicket", idTicket));
                cmd.Parameters.Add(new SqlParameter("@TipoActivo", TipoActivo));
                cmd.Parameters.Add(new SqlParameter("@Sede", Sede));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                cmd.Parameters.Add(new SqlParameter("@NombrePersona", NombrePersona));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                if (CodigoBarras == null) { cmd.Parameters.AddWithValue("@CodigoBarras", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@CodigoBarras", CodigoBarras); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMO(int Opcion, int idManoObra, int idProcesoMtto, int idVehiculo,
                                                                                              string Especialidad, string Tiempo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMO", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idManoObra", idManoObra));
                cmd.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));
                cmd.Parameters.Add(new SqlParameter("@Tiempo", Tiempo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMOMaquinas(int Opcion, int idManoObra, int idProcesoMtto, string MaquinaCodigo,
                                                                                                      string Especialidad, string Tiempo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMOMaquinas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idManoObra", idManoObra));
                cmd.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                cmd.Parameters.Add(new SqlParameter("@MaquinaCodigo", MaquinaCodigo));
                cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));
                cmd.Parameters.Add(new SqlParameter("@Tiempo", Tiempo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ManoObra_ListarManoObra(string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Especialidad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ManoObra_ListarManoObra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_ListarResumen(string FechaInicio, string FechaFin, string Especialidad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_ListarResumen", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_CrearAnalisisFallas(int idFalla, string Tracto, string Carreta, string Operacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_CrearAnalisisFallas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                cmd.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                cmd.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas(int idFalla, int Contador, string Respuesta, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                cmd.Parameters.Add(new SqlParameter("@Contador", Contador));
                cmd.Parameters.Add(new SqlParameter("@Respuesta", Respuesta));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarAnalisisFallas(string NumeroPlaca, string Operacion, string FechaInicio, string FechaFin, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarAnalisisFallas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroPlaca", NumeroPlaca));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarEliminarRaizFalla(int Opcion, int idAnalisisFalla, string RaizFalla, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_InsertarEliminarRaizFalla", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idAnalisisFalla", idAnalisisFalla));
                cmd.Parameters.Add(new SqlParameter("@RaizFalla", RaizFalla));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucionAuxilio(int idFalla, string Servicio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucionAuxilio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idFalla", idFalla));
                cmd.Parameters.Add(new SqlParameter("@Servicio", Servicio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo(string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarEquipos(int Opcion, int idRegistro, string Codigo, int Periodo, string TipoMaquina, string EquipoNombre,
                                                                                          string Equipo, string Ubicacion, string Marca, string Modelo, DateTime UltimaFecha, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarEquipos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                cmd.Parameters.Add(new SqlParameter("@EquipoNombre", EquipoNombre));
                cmd.Parameters.Add(new SqlParameter("@Equipo", Equipo));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                cmd.Parameters.Add(new SqlParameter("@UltimaFecha", UltimaFecha));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosEquipos(string Equipo, string Tipo, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosEquipos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Equipo", Equipo));
                cmd.Parameters.Add(new SqlParameter("@Tipo", Tipo));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialEquipos(string Equipo, string FechaInicio, string FechaFin, string TipoMaquina)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialEquipos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Equipo", Equipo));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlEquipos(int idRegistro, int idAccesorio, DateTime FechaCambio, int Periodo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlEquipos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                cmd.Parameters.Add(new SqlParameter("@idAccesorio", idAccesorio));
                cmd.Parameters.Add(new SqlParameter("@FechaCambio", FechaCambio));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoEquipos(int idRegistro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoEquipos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosEquipos(int idRegistro, int idAccesorio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosEquipos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                cmd.Parameters.Add(new SqlParameter("@idAccesorio", idAccesorio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosEquipos(int Opcion, int idProcesoMtto, int idRegistro, int Periodo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosEquipos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idProcesoMtto", idProcesoMtto));
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema(int Opcion, int idTecnica)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idTecnica", idTecnica));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_RegistrarModificar(string Placa, int idTecnica, int idSistema, DateTime Fecha, string TipoAceite,
                         string Recomendacion, string Estado, string DirectorioPDF, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPredictivo_RegistrarModificar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@idTecnica", idTecnica));
                cmd.Parameters.Add(new SqlParameter("@idSistema", idSistema));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@TipoAceite", TipoAceite));
                cmd.Parameters.Add(new SqlParameter("@Recomendacion", Recomendacion));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@DirectorioPDF", DirectorioPDF));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_ListarRegistro(string Placa, int TipoUnidad, int idTecnica, int idSistema)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPredictivo_ListarRegistro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                cmd.Parameters.Add(new SqlParameter("@idTecnica", idTecnica));
                cmd.Parameters.Add(new SqlParameter("@idSistema", idSistema));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_ListarHistorial(string FechaInicio, string FechaFin, string Placa, int TipoUnidad, int idTecnica, int idSistema)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPredictivo_ListarHistorial", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                cmd.Parameters.Add(new SqlParameter("@idTecnica", idTecnica));
                cmd.Parameters.Add(new SqlParameter("@idSistema", idSistema));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_CompDesgaste_CrearComponente(int idPlaca, int idComponente, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_CompDesgaste_CrearComponente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idPlaca", idPlaca));
                cmd.Parameters.Add(new SqlParameter("@idComponente", idComponente));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataSet ReportesApp_Mantenimiento_CompDesgaste_ListarComponentes(int Opcion, string Placa, string Operacion, string FechaInicio, string FechaFin)
        {
            try
            {
                DataSet dtTemp = new DataSet();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_CompDesgaste_ListarComponentes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dtTemp);
                return dtTemp;
            }
            catch { return new DataSet(); }
        }

        public DataTable ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteC(int idPlaca, int idComponente, DateTime FechaAnterior, decimal Milimetro, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteC", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idPlaca", idPlaca));
                cmd.Parameters.Add(new SqlParameter("@idComponente", idComponente));
                cmd.Parameters.Add(new SqlParameter("@FechaAnterior", FechaAnterior));
                cmd.Parameters.Add(new SqlParameter("@Milimetro", Milimetro));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteT(int idPlaca, int idComponente, DateTime FechaAnterior, string Actividad, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteT", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idPlaca", idPlaca));
                cmd.Parameters.Add(new SqlParameter("@idComponente", idComponente));
                cmd.Parameters.Add(new SqlParameter("@FechaAnterior", FechaAnterior));
                cmd.Parameters.Add(new SqlParameter("@Actividad", Actividad));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarReprogramacion(string Placa, string Operacion, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarReprogramacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarUbicaciones(string Placa, string Operacion, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Solicitudes_ListarUbicaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorInspeccion(int Opcion, DateTime Fecha, string FechaInicio, string FechaFin, string Placa, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorInspeccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorUnidades(int Opcion, DateTime Fecha, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorUnidades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_RegistrarEditarNeumaticos(int Opcion, string Codigo, string DOT, string Marca, string Medida, string Modelo,
                         string Tipo, decimal NSK, decimal KM, decimal Precio, DateTime FechaInicio, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlNeumaticos_RegistrarEditarNeumaticos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@DOT", DOT));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@Medida", Medida));
                cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                cmd.Parameters.Add(new SqlParameter("@Tipo", Tipo));
                cmd.Parameters.Add(new SqlParameter("@NSK", NSK));
                cmd.Parameters.Add(new SqlParameter("@KM", KM));
                cmd.Parameters.Add(new SqlParameter("@Precio", Precio));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_ListarNeumaticos(string Codigo, string Marca, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlNeumaticos_ListarNeumaticos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_InstalarDesinstalar(int Opcion, int idMovimientoN, string Codigo, string Marca, int idVehiculo,
                         string Tipo, int Posicion, DateTime Fecha, decimal KM, decimal NSK, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlNeumaticos_InstalarDesinstalar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idMovimientoN", idMovimientoN));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                cmd.Parameters.Add(new SqlParameter("@Tipo", Tipo));
                cmd.Parameters.Add(new SqlParameter("@Posicion", Posicion));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@KM", KM));
                cmd.Parameters.Add(new SqlParameter("@NSK", NSK));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_ListarMovimientos(string FechaInicio, string FechaFin, string Codigo, string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ControlNeumaticos_ListarMovimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimiento(string Requerimiento)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Requerimiento", Requerimiento));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientoDetalle(string Requerimiento, string CodigoItem)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientoDetalle", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Requerimiento", Requerimiento));
                cmd.Parameters.Add(new SqlParameter("@CodigoItem", CodigoItem));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_RegistrarRequerimientos(string xmlItemsAlmacen, int idActivoSegundoUso, int idEmpleado, string Requerimiento, decimal cantidadUso, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ActivoSegundoUso_RegistrarRequerimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@xmlItemsAlmacen", xmlItemsAlmacen));
                cmd.Parameters.Add(new SqlParameter("@idActivoSegundoUso", idActivoSegundoUso));
                cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                cmd.Parameters.Add(new SqlParameter("@Requerimiento", Requerimiento));
                cmd.Parameters.Add(new SqlParameter("@cantidadUso", cantidadUso));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientos(string FechaInicio, string FechaFin, string Sucursal, string Activo, string Requerimiento, string Empleado)
        {

            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.Parameters.AddWithValue("@Sucursal", Sucursal);
                cmd.Parameters.AddWithValue("@Activo", Activo);
                cmd.Parameters.AddWithValue("@Requerimiento", Requerimiento);
                cmd.Parameters.AddWithValue("@Empleado", Empleado);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_DesvincularRequerimientos(int idActivo, int idEmpleado, string Requerimiento, string Motivo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ActivoSegundoUso_DesvincularRequerimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idActivo", idActivo));
                cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                cmd.Parameters.Add(new SqlParameter("@Requerimiento", Requerimiento));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_HistorialRequerimientos()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_ActivoSegundoUso_HistorialRequerimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(int Opcion, int idCapacitacionD, int idCapacitacionC, string Estado, int Persona, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCapacitacionD", idCapacitacionD));
                cmd.Parameters.Add(new SqlParameter("@idCapacitacionC", idCapacitacionC));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarAsistentes(int idCapacitacionC)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarAsistentes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCapacitacionC", idCapacitacionC));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_RegistrarCapacitacion(int Opcion, int idCapacitacionC, string Empresa, string Capacitador, 
                                                                                      string Tema, DateTime FechaInicio, DateTime FechaFin, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_RegistrarCapacitacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCapacitacionC", idCapacitacionC));
                cmd.Parameters.Add(new SqlParameter("@Empresa", Empresa));
                cmd.Parameters.Add(new SqlParameter("@Capacitador", Capacitador));
                cmd.Parameters.Add(new SqlParameter("@Tema", Tema));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataSet ReportesApp_Mantenimiento_AsignacionOT_ListarCapacitaciones(string FechaInicio, string FechaFin, string Estado, string Tema, string Asistente)
        {
            try
            {
                DataSet dtTemp = new DataSet();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Mantenimiento_AsignacionOT_ListarCapacitaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Tema", Tema));
                comando.Parameters.Add(new SqlParameter("@Asistente", Asistente));
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dtTemp);
                return dtTemp;
            }
            catch { return new DataSet(); }
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarDesviacion(int Anio, int NroSemana, string Operacion, string TipoMtto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarDesviacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@TipoMtto", TipoMtto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarSolicitudes(string NroPlaca)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_ListarSolicitudes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroPlaca", NroPlaca));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_VincularSolicitudes(int Opcion, int Nro, int idSolicitud)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_VincularSolicitudes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Nro", Nro));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }
    }
}
