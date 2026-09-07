using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class clsReporteDAO
    {
        private clsReporteDAO()
        {

        }

        private readonly static clsReporteDAO instancia = new clsReporteDAO();

        public static clsReporteDAO Instancia
        {
            get { return instancia; }
        }

        public String registrar_Reporte(clsReporte obj)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Reporte_Registrar", conexion);

                comando.CommandType = CommandType.StoredProcedure;
                SqlParameter IdReporte = new SqlParameter("@idReporte", SqlDbType.Int);
                IdReporte.Direction = ParameterDirection.Output;

                comando.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = obj.nombre;                
                comando.Parameters.Add("@descripcion", SqlDbType.NVarChar, 200).Value = obj.descripcion;
                comando.Parameters.Add("@idArea", SqlDbType.Char,3).Value = obj.objArea.idArea;
                comando.Parameters.Add("@idFormulario", SqlDbType.Int).Value = obj.objFormulario.idFormulario;
                comando.Parameters.Add(IdReporte);
                comando.Parameters.Add("@formula", SqlDbType.NVarChar, 100).Value = obj.formula;

                comando.ExecuteNonQuery();

                conexion.Close();

                obj.idReporte = Convert.ToInt32(IdReporte.Value);

                SqlCommand comando_CodigoReporte = new SqlCommand("ReportesApp_Reporte_SeleccionarCodigo", conexion);

                comando_CodigoReporte.CommandType = CommandType.StoredProcedure;

                comando_CodigoReporte.Parameters.AddWithValue("@idReporte", obj.idReporte);

                comando_CodigoReporte.Connection.Open();

                SqlDataReader dr = comando_CodigoReporte.ExecuteReader();

                while (dr.Read())
                {
                    obj.codigo = dr[0].ToString().Trim();
                }

                comando_CodigoReporte.Connection.Close();         

                conexion.Dispose();

                return "Registrado";
            }
            catch (Exception e)
            {
                return "ERROR: " + e.Message + e.StackTrace;
            }
        }

        public String modificar_Reporte(clsReporte obj)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Reporte_Modificar", conexion);

                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = obj.idReporte;
                comando.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = obj.nombre;                
                comando.Parameters.Add("@descripcion", SqlDbType.NVarChar, 200).Value = obj.descripcion;
                comando.Parameters.Add("@idArea", SqlDbType.Char, 3).Value = obj.objArea.idArea;
                comando.Parameters.Add("@formula", SqlDbType.NVarChar, 100).Value = obj.formula;

                comando.ExecuteNonQuery();

                conexion.Close();
                conexion.Dispose();

                return "Modificado";
            }
            catch (Exception e)
            {
                return "ERROR: " + e.Message + e.StackTrace;
            }
        }

        public List<clsReporte> consulta_Reporte_Por_IdArea(String idArea)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Reporte_ConsultaPorIdArea", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idArea", SqlDbType.NVarChar, 20).Value = idArea;                

                SqlDataReader lector = comando.ExecuteReader();
                List<clsReporte> coleccion = new List<clsReporte>();

                while (lector.Read())
                {
                    clsReporte objclsReporte = new clsReporte();

                    objclsReporte.codigo = lector.GetString(0).ToString();
                    objclsReporte.descripcion = lector.GetString(1).ToString();
                    objclsReporte.idReporte = lector.GetInt32(2);
                    objclsReporte.nombre = lector.GetString(3).ToString();
                    objclsReporte.objArea.idArea = lector.GetString(4).ToString();
                   
                    coleccion.Add(objclsReporte);
                }

                conexion.Close();
                conexion.Dispose();

                if (coleccion.Count <= 0)
                    return null;

                return coleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<clsReporte> consulta_Reporte_Por_Codigo_Nombre(String codigoReporte, String nombreReporte)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Reporte_Consulta_Por_Codigo_Nombre", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@codigo", SqlDbType.NVarChar, 20).Value = codigoReporte;
                comando.Parameters.Add("@nombre", SqlDbType.NVarChar, 20).Value = nombreReporte;                

                SqlDataReader lector = comando.ExecuteReader();
                List<clsReporte> coleccion = new List<clsReporte>();

                while (lector.Read())
                {
                    clsReporte objclsReporte = new clsReporte();

                    objclsReporte.codigo = lector.GetString(0).Trim();
                    objclsReporte.descripcion = lector.GetString(1).Trim();
                    objclsReporte.idReporte = lector.GetInt32(2);
                    objclsReporte.nombre = lector.GetString(3).Trim();
                    objclsReporte.objArea.idArea = lector.GetString(4).Trim();
                    objclsReporte.formula = lector.GetString(5).Trim();
                    objclsReporte.objArea.descripcion = lector.GetString(6).Trim();

                    coleccion.Add(objclsReporte);
                }

                conexion.Close();
                conexion.Dispose();

                if (coleccion.Count <= 0)
                    return null;

                return coleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Registros(string IdReporte, string usuario, string formulario,string area)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            SqlCommand comando = new SqlCommand("ReportesApp_Insert_Registros", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@IDREPORTE", IdReporte));
            comando.Parameters.Add(new SqlParameter("@USUARIO", usuario)); ;
            comando.Parameters.Add(new SqlParameter("@FORMULARIO", formulario));
            comando.Parameters.Add(new SqlParameter("@AREA", area)); ;
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void GuardaRegistrosForms(string usuario, string formulario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            SqlCommand comando = new SqlCommand("ReportesApp_Guarda_Registros_Forms", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@USUARIO", usuario)); ;
            comando.Parameters.Add(new SqlParameter("@FORMULARIO", formulario));            
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable ListaReporteForms()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                //SqlCommand comando = new SqlCommand("ReportesApp_Guarda_Registros_Forms", conexion);
                SqlCommand comando = new SqlCommand();
                //comando.CommandText = "SELECT * FROM ReportesApp_RegistroFormularios";
                comando.CommandText = "Select Usuario,FechaRegistro,RIGHT(CONVERT(CHAR(20), FechaRegistro, 22), 11) AS 'Hora',FormularioNombre from ReportesApp_RegistroFormularios EXCEPT Select Usuario,FechaRegistro,RIGHT(CONVERT(CHAR(20), FechaRegistro, 22), 11) AS 'Hora',FormularioNombre from ReportesApp_RegistroFormularios Where FormularioNombre='' ORDER BY FechaRegistro DESC";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ListaReporteFormsUsuario()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Lista_Registros_Forms", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Connection = conexion;
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable IdReporte()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Guarda_Registros_Forms", conexion);
                //SqlCommand comando = new SqlCommand();
                //comando.CommandText = "";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public void Registrar_Nuevo_Reporte(string formulario, string reporte, string areas, string idarea,string DescripcionReporte)
        {
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            SqlCommand comando = new SqlCommand("ReportesApp_Reporte_Nuevos_Formularios", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@Formulario", formulario)); ;
            comando.Parameters.Add(new SqlParameter("@Reporte ", reporte));
            comando.Parameters.Add(new SqlParameter("@Area", areas));
            comando.Parameters.Add(new SqlParameter("@IdArea", idarea));
            comando.Parameters.Add(new SqlParameter("@Descripcion", DescripcionReporte));
            comando.Connection = conexion;
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable ListaPeriodos(string reporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Lista_Periodos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Reporte ", reporte));
                comando.Connection = conexion;
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable(); 
            }
        }

        public DataTable ComboPeriodos(string formulario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand();
                //comando.CommandText = "Select Descripcion AS Periodo from ReportesApp_Periodos Where Reporte_Formulario LIKE '%" + formulario + "%'";
                comando.CommandText = "Select Descripcion AS Periodo from ReportesApp_Periodos Where Reporte_Formulario='" + formulario + "'";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ComboPorPeriodos(string formulario,string periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand();
                //comando.CommandText = "Select ROW_NUMBER()OVER(ORDER BY ID) AS 'Nº',* from ReportesApp_Periodos Where Reporte_Formulario LIKE '%" + formulario + "%' AND Descripcion='" + periodo + "'";
                comando.CommandText = "Select ROW_NUMBER()OVER(ORDER BY ID) AS 'Nº',* from ReportesApp_Periodos Where Reporte_Formulario='" + formulario + "' AND Descripcion='" + periodo + "'";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public void Registrar_Nuevo_Periodo(string formulario, string fechaini, string fechafin)
        {
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Inserta_Periodo", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@FORMULARIO", formulario)); ;
            comando.Parameters.Add(new SqlParameter("@FECHAINI ", fechaini));
            comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
            comando.Connection = conexion;
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable ListaReportes(string usuario, string nombres, char estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Reporte_Lista", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@USUARIO ", usuario));
                comando.Parameters.Add(new SqlParameter("@NOMBRES ", nombres));
                comando.Parameters.Add(new SqlParameter("@ESTADO ", estado));
                comando.Connection = conexion;
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        //public DataTable Areas(string area)
        //{
        //    DataTable dtTemp = new DataTable();
        //    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //    conexion.Open();
        //    SqlCommand comando = new SqlCommand("ReportesApp_Insert_Registros", conexion);
        //    comando.CommandType = CommandType.StoredProcedure;
        //    comando.Parameters.Add(new SqlParameter("@AREA", area)); ;
        //    comando.ExecuteNonQuery();
        //    conexion.Close();
        //    return dtTemp;
        //}
    }
}
