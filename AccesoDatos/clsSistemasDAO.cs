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
    public class clsSistemasDAO
    {
        private clsSistemasDAO()
        {

        }

        private readonly static clsSistemasDAO instancia = new clsSistemasDAO();

        public static clsSistemasDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetCelulares(int opcion, int persona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Sistemas_Celular_Reporte_Consultas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));
                comando.Parameters.Add(new SqlParameter("@Persona", persona));
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public string InsertCelular(string marca,string modelo,string imei,string observaciones)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "INSERT INTO Celular VALUES ('"+marca+"','"+modelo+"','"+imei+"','LIBRE','"+observaciones+"',GETDATE())";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                int rowsaffected = comando.ExecuteNonQuery();
                conexion.Close();
                if (rowsaffected > 0)
                {
                    return "Celular registrado";
                }
                else
                {
                    return "Error al insertar";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string UpdateCelular(string id, string marca, string modelo, string imei, string observaciones)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "UPDATE Celular SET "+
                "Marca = '" + marca + "',Modelo = '" + modelo + "',IMEI = '" + imei + "', Observaciones = '" +
                observaciones + "', Ultima_modif = " + "GETDATE() WHERE Id = "+ id;
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                int rowsaffected = comando.ExecuteNonQuery();
                conexion.Close();
                if (rowsaffected > 0)
                {
                    return "Celular actualizado";
                }
                else
                {
                    return "Error al actualizar";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string BajaCelular(string id, string observaciones)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "UPDATE Celular SET Estado = 'BAJA'," +
                "Observaciones = '"+observaciones + "', Ultima_modif = " + "GETDATE() WHERE Id = " + id;
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                int rowsaffected = comando.ExecuteNonQuery();
                conexion.Close();
                if (rowsaffected > 0)
                {
                    return "Baja exitosa";
                }
                else
                {
                    return "Error al actualizar";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public DataTable GetAsignacionesCel(string usuario, int persona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Sistemas_Celular_Reporte_Asignacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@USUARIO", usuario));
                comando.Parameters.Add(new SqlParameter("@PERSONA", persona));
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;
                //SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                //SqlCommand comando = new SqlCommand();

                //comando.CommandText = "SELECT A.Id,P.NombreCompleto,P.Persona,A.Numero,A.Telefono,C.Marca,"+
                //"C.Modelo,C.IMEI,A.Fecha_Asignacion as 'Fecha Asignación',A.Estado,A.Observaciones,CASE WHEN C.Estado='INACTIVA' THEN '3' WHEN C.Estado='ACTIVA' THEN '2' ELSE '1' END AS CONTADOR " +
                //"FROM Celular_Asignacion A "+
                //"LEFT JOIN Celular C ON A.Telefono = C.Id "+
                //"LEFT JOIN Personamast P ON A.Empleado = P.Persona ";
                //comando.CommandType = CommandType.Text;
                //comando.Connection = conexion;
                //conexion.Open();

                //dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        
        }

        public DataTable GetAsignacionesCel_Consultas(int opcion, int persona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Sistemas_Celular_Reporte_Consultas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@OPCION", opcion));
                comando.Parameters.Add(new SqlParameter("@PERSONA", persona));
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;
             
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAsignacionesCel_Update(int opcion, int persona, int codigo,string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Sistemas_Celular_Reporte_Update", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));//1=Equipo Vincular, 2=Equipo Desvincular, 3=Linea Vincular, 4=Linea Desvincular
                comando.Parameters.Add(new SqlParameter("@Persona", persona));
                comando.Parameters.Add(new SqlParameter("@Codigo", codigo));
                comando.Parameters.Add(new SqlParameter("@User", usuario));
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;

                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAsignacionesCel_Equipo_Update(int opcion, int IDEquipo, int codigo, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Sistemas_Celular_Reporte_Update", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));//1=Equipo Vincular, 2=Equipo Desvincular, 3=Linea Vincular, 4=Linea Desvincular
                comando.Parameters.Add(new SqlParameter("@Persona", codigo));
                comando.Parameters.Add(new SqlParameter("@Codigo", codigo));
                comando.Parameters.Add(new SqlParameter("@User", usuario));
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;

                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAsignacionesActivas()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT A.Id,P.NombreCompleto,P.Persona,A.Numero,A.Telefono,C.Marca," +
                "C.Modelo,C.IMEI,A.Fecha_Asignacion as 'Fecha Asignación',A.Estado,A.Observaciones " +
                "FROM Celular_Asignacion A " +
                "LEFT JOIN Celular C ON A.Telefono = C.Id " +
                "LEFT JOIN Personamast P ON A.Empleado = P.Persona WHERE A.Estado = 'ACTIVA' ";
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

        public string ValidaEmpleado(int empleado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT * FROM Celular_Asignacion WHERE Empleado = "+empleado+" AND Estado = 'ACTIVA' AND Empleado NOT IN (45,42)";
                //45,42 = MPN Y EPN
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                if (dtTemp.Rows.Count == 0)
                {
                    return "OK";
                }
                else
                {
                    return "Ya existe una asignación para ese empleado";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string ValidaNumero(string numero)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT * FROM Celular_Asignacion WHERE Numero='" + numero + "' AND Estado = 'ACTIVA' AND Numero <> ''";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                if (dtTemp.Rows.Count == 0)
                {
                    return "OK";
                }
                else
                {
                    return "Ya existe una asignación para ese número";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string ValidaTelefono(string imei)
        {
            try
            {

                string resultado;
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT * FROM Celular_Asignacion A INNER JOIN Celular C ON "+
                "A.Telefono = C.Id WHERE C.IMEI='"+imei+"' AND A.Estado = 'ACTIVA'";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                dtTemp.Load(comando.ExecuteReader());
                if (dtTemp.Rows.Count > 0)
                {
                    resultado = "Ya existe una asignación para ese equipo(IMEI).";
                }
                else
                {
                    resultado = "OK";
                }
                //************************************************************
                if (resultado == "OK")
                {
                    DataTable dtTemp2 = new DataTable();
                    SqlCommand comando2 = new SqlCommand();
                    comando2.CommandText = "SELECT * FROM Celular WHERE IMEI='" + imei + "' AND Estado = 'LIBRE'";
                    comando2.CommandType = CommandType.Text;
                    comando2.Connection = conexion;
                    //conexion.Open();
                    dtTemp2.Load(comando2.ExecuteReader());
                    if (dtTemp2.Rows.Count == 0)
                    {
                        resultado = "El IMEI ingresado no existe o el equipo se encuentra malogrado.";
                    }
                }
                conexion.Close();
                return resultado;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public DataTable GetTelefono(string imei)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT Id,Marca,Modelo FROM Celular WHERE IMEI='"+imei+"' AND Estado = 'LIBRE' ";
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

        public string InsertAsignacion(int idempleado,string numero, int idtelefono,string observaciones,string fechaasignacion)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "INSERT INTO Celular_Asignacion VALUES (" + idempleado + ",'" + numero + "'," +
                idtelefono + ",'ACTIVA','" + fechaasignacion + "','"+observaciones+"',GETDATE())";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    SqlCommand comando2 = new SqlCommand();
                    comando2.CommandText = "UPDATE Celular SET Estado='ASIGNADO',Ultima_modif=GETDATE() WHERE Id=" + idtelefono;
                    comando2.CommandType = CommandType.Text;
                    comando2.Connection = conexion;
                    int rowsaffected2 = comando2.ExecuteNonQuery();
                    conexion.Close();
                    if (rowsaffected > 0)
                    {
                        return "Asignacion registrada";
                    }
                    else
                    {
                        return "Error al actualizar el estado del celular";
                    }
                }
                else
                {
                    return "Error al insertar la asignación";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string ModificarAsignacion(int idasignacion,int idtelefant, int idempleado, string numero, int idtelefono, 
            string observaciones, string fechaasignacion)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "UPDATE Celular_Asignacion SET Estado = 'INACTIVA',Ultima_modif = GETDATE() WHERE Id = "+ idasignacion;
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    SqlCommand comando2 = new SqlCommand();
                    comando2.CommandText = "INSERT INTO Celular_Asignacion VALUES (" + idempleado + ",'" + numero + "'," +
                    idtelefono + ",'ACTIVA','" + fechaasignacion + "','" + observaciones + "',GETDATE())";
                    comando2.CommandType = CommandType.Text;
                    comando2.Connection = conexion;
                    int rowsaffected2 = comando2.ExecuteNonQuery();
                    if (rowsaffected2 > 0)
                    {
                        if (idtelefono == idtelefant)
                        {
                            //no cambio de equipo, no se actualiza nada
                            return "Asignación actualizada";
                        }
                        else //cambio equipo
                        {
                            if (idtelefono == 0)// no hay nuevo equipo
                            {
                                //telefono antiguo liberar
                                SqlCommand comando3 = new SqlCommand();
                                comando3.CommandText = "UPDATE Celular SET Estado='LIBRE',Ultima_modif=GETDATE() WHERE Id = " + idtelefant;
                                comando3.CommandType = CommandType.Text;
                                comando3.Connection = conexion;
                                int rowsaffected3 = comando3.ExecuteNonQuery();
                                conexion.Close();
                                if (rowsaffected3 > 0)
                                {
                                    return "Asignación actualizada";
                                }
                                else
                                {
                                    return "Error al actualizar teléfono anterior";
                                }
                            }
                            else
                            {
                                if (idtelefant != 0)
                                {
                                    // libera el antiguo y actualizar el nuevo
                                    SqlCommand comando3 = new SqlCommand();
                                    comando3.CommandText = "UPDATE Celular SET Estado='LIBRE',Ultima_modif=GETDATE() WHERE Id = " + idtelefant;
                                    comando3.CommandType = CommandType.Text;
                                    comando3.Connection = conexion;
                                    int rowsaffected3 = comando3.ExecuteNonQuery();
                                    if (rowsaffected3 > 0)
                                    {
                                        SqlCommand comando4 = new SqlCommand();
                                        comando4.CommandText = "UPDATE Celular SET Estado='ASIGNADO',Ultima_modif=GETDATE() WHERE Id = " + idtelefono;
                                        comando4.CommandType = CommandType.Text;
                                        comando4.Connection = conexion;
                                        int rowsaffected4 = comando4.ExecuteNonQuery();
                                        conexion.Close();
                                        if (rowsaffected4 > 0)
                                        {
                                            return "Asignación actualizada";
                                        }
                                        else
                                        {
                                            return "Error al actualizar teléfono anterior";
                                        }
                                    }
                                    else
                                    {
                                        return "Error al actualizar teléfono anterior";
                                    }
                                }
                                else
                                {
                                    SqlCommand comando4 = new SqlCommand();
                                    comando4.CommandText = "UPDATE Celular SET Estado='ASIGNADO',Ultima_modif=GETDATE() WHERE Id = " + idtelefono;
                                    comando4.CommandType = CommandType.Text;
                                    comando4.Connection = conexion;
                                    int rowsaffected4 = comando4.ExecuteNonQuery();
                                    conexion.Close();
                                    if (rowsaffected4 > 0)
                                    {
                                        return "Asignación actualizada";
                                    }
                                    else
                                    {
                                        return "Error al actualizar teléfono anterior";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return "Error al insertar la nueva asignación";
                    }
                }
                else
                {
                    return "Error al actualizar la asignación anterior";
                }
                
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string EliminarAsignacion(int idasignacion, int idtelefono)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "UPDATE Celular_Asignacion SET Estado = 'INACTIVA',Ultima_modif = GETDATE() WHERE Id = " + idasignacion;
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    //telefono liberar
                    SqlCommand comando2 = new SqlCommand();
                    comando2.CommandText = "UPDATE Celular SET Estado='LIBRE',Ultima_modif=GETDATE() WHERE Id = " + idtelefono;
                    comando2.CommandType = CommandType.Text;
                    comando2.Connection = conexion;
                    int rowsaffected2 = comando2.ExecuteNonQuery();
                    conexion.Close();
                    if (rowsaffected2 > 0)
                    {
                        return "Asignación eliminada";
                    }
                    else
                    {
                        return "Error al liberar el teléfono";
                    }
                }
                else
                {
                    return "Error al actualizar la asignación";
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public DataTable ListaRPC(string compañia)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "Select CASE WHEN CompañiaSocio='10000000' THEN 'TRANSPESA' WHEN CompañiaSocio='60000000' THEN 'AMT' WHEN CompañiaSocio='70000000' THEN 'DISOR' END AS Compañia,* from Lineas_RPC Where CompañiaSocio like '%" + compañia + "%'";
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

        public DataTable GetListaAccesoSpring(string usuario,string nombres, char estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Sistemas_Accesos_Spring", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@USUARIO", usuario));
                comando.Parameters.Add(new SqlParameter("@NOMBRES", nombres));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetGestionEquipos_CategoriasListar()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Sistemas_Equipos_CategoriaListar", conexion);
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
        public DataTable GetGestionEquipos_Registra_Modifica_Elimina(int Opcion,int IDEquipo, int IDCategoria,string Descripcion,string Marca,string Modelo,string Procesador, 
               string  Placa,string Memoria,string Disco,string  Kase,string  Lector,string Monitor,string  Teclado,string Mouse,string  Otros,string IP, string Hostname, string Mac,
               string SistemaOP, string UsuarioWin, string ClaveWin, string AnyDesk_Nro, string AnyDesk_Pass, int Persona, string Area, string User)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Sistemas_Equipos_Nuevo_Modifica_Elimina", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IDEquipo", IDEquipo));
                comando.Parameters.Add(new SqlParameter("@IDCategoria", IDCategoria));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Marca", Marca));
                comando.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                comando.Parameters.Add(new SqlParameter("@Procesador", Procesador));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Memoria", Memoria));
                comando.Parameters.Add(new SqlParameter("@Disco", Disco));
                comando.Parameters.Add(new SqlParameter("@Case", Kase));
                comando.Parameters.Add(new SqlParameter("@Lector", Lector));
                comando.Parameters.Add(new SqlParameter("@Monitor", Monitor));
                comando.Parameters.Add(new SqlParameter("@Teclado", Teclado));
                comando.Parameters.Add(new SqlParameter("@Mouse", Mouse));
                comando.Parameters.Add(new SqlParameter("@Otros", Otros));
                comando.Parameters.Add(new SqlParameter("@IP", IP));
                comando.Parameters.Add(new SqlParameter("@Hostname", Hostname));
                comando.Parameters.Add(new SqlParameter("@Mac", Mac));
                comando.Parameters.Add(new SqlParameter("@SistemaOP", SistemaOP));
                comando.Parameters.Add(new SqlParameter("@Usuario", UsuarioWin));
                comando.Parameters.Add(new SqlParameter("@Clave", ClaveWin));
                comando.Parameters.Add(new SqlParameter("@AnyDesk_Nro", AnyDesk_Nro));
                comando.Parameters.Add(new SqlParameter("@AnyDesk_Pass", AnyDesk_Pass));
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@Area", Area));
                comando.Parameters.Add(new SqlParameter("@UserRegistra", User));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
            public DataTable GetGestionEquipos_Listar()
            {
                try
                {
                    DataTable dtTemp = new DataTable();
                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("ReportesApp_Sistemas_Equipos_Listar", conexion);
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
            public DataSet ListarEquipos_Sala()
            {
                DataSet ds = new DataSet();

                try
                {


                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("ReportesApp_ListarCombo_AgendarReunion", conexion);

                    SqlDataAdapter da = new SqlDataAdapter(comando);

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandTimeout = 0;
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        ds.Tables[0].TableName = "Oficina";
                        ds.Tables[1].TableName = "Equipo";
                        ds.Tables[2].TableName = "Sala";
                    }


                }
                catch
                {
                    //return new DataTable();
                }

                return ds;
            }


            public Boolean ReportesApp_Registrar_AgendarReuninon(string idUsuario, string fechaInicio, string horaInicio, string horaFin, int idOficina, int numeroSala, string linkReunion, string xmlEquipo)
            {

                Boolean respuesta = false;
                SqlCommand comando = null;
                try
                {


                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    conexion.Open();
                    comando = new SqlCommand("ReportesApp_Registrar_AgendarReuninon", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    //comando.Parameters.AddWithValue("@FechaFin", fechaFin);
                    comando.Parameters.AddWithValue("@HoraInicio", horaInicio);
                    comando.Parameters.AddWithValue("@HoraFin", horaFin);
                    comando.Parameters.AddWithValue("@idOficina", idOficina);
                    comando.Parameters.AddWithValue("@NumeroSala", numeroSala);
                    comando.Parameters.AddWithValue("@LinkReunion", linkReunion);
                    if (xmlEquipo == null)
                    {
                        comando.Parameters.AddWithValue("@xmlEquipo", DBNull.Value);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@xmlEquipo", xmlEquipo);
                    }


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

            public Boolean ReportesApp_Actualizar_AgendarReuninon(int idAgendarReunion, string idUsuario, string fechaInicio, string horaInicio, string horaFin, int idOficina, int numeroSala, string linkReunion, string xmlEquipo)
            {

                Boolean respuesta = false;
                SqlCommand comando = null;
                try
                {


                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    conexion.Open();
                    comando = new SqlCommand("ReportesApp_Actualizar_AgendarReuninon", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idAgendarReunion", idAgendarReunion);
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@HoraInicio", horaInicio);
                    comando.Parameters.AddWithValue("@HoraFin", horaFin);
                    comando.Parameters.AddWithValue("@idOficina", idOficina);
                    comando.Parameters.AddWithValue("@NumeroSala", numeroSala);
                    comando.Parameters.AddWithValue("@LinkReunion", linkReunion);
                    if (xmlEquipo == null)
                    {
                        comando.Parameters.AddWithValue("@xmlEquipo", DBNull.Value);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@xmlEquipo", xmlEquipo);
                    }


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


            public DataTable sp_ReportesApp_ListarReunionesAgendadas(string fechaInicio, string fechaFin, string estadoReunion, int idOficina, int idSala)
            {
                SqlCommand cmd = null;
                DataTable dt = new DataTable();

                try
                {
                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    cmd = new SqlCommand("ReportesApp_ListarReunionesAgendadas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                    if (idOficina == -1)
                    {
                        cmd.Parameters.AddWithValue("@idOficina", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@idOficina", idOficina);
                    }

                    if (idSala == -1)
                    {
                        cmd.Parameters.AddWithValue("@idSala", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@idSala", idSala);
                    }


                    if (estadoReunion == null)
                    {

                        cmd.Parameters.AddWithValue("@EstadoReunion", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@EstadoReunion", estadoReunion);
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



            public Boolean ReportesApp_Autorizar_Rechazar_Finalizar_SolicitudReunion(string idUsuarioAutoriza, int idAgendarReunion, string Autorizar_Rechazar_Finalizar)
            {

                Boolean respuesta = false;
                SqlCommand comando = null;
                try
                {


                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    conexion.Open();
                    comando = new SqlCommand("ReportesApp_Autorizar_Rechazar_Finalizar_SolicitudReunion", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idUsuarioAutoriza", idUsuarioAutoriza);
                    comando.Parameters.AddWithValue("@idAgendarReunion", idAgendarReunion);
                    comando.Parameters.AddWithValue("@Autorizar_Rechazar_Finalizar", Autorizar_Rechazar_Finalizar);

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
    }
}
