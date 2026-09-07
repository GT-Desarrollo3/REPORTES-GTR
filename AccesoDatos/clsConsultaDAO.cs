using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using Comun;
//comentario

namespace AccesoDatos
{
    public class clsConsultaDAO
    {
        private readonly static clsConsultaDAO instancia = new clsConsultaDAO();

        public static clsConsultaDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataCompañias()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT '' AS 'IDCOMP','TODOS' AS 'COMPDESC' " +
                "UNION ALL " +
                "SELECT " +
                "companyowner.companyowner  AS 'IDCOMP', " +
                "companyowner.description AS 'COMPDESC' " +
                "FROM companyowner , " +
                "CompaniaMast " +
                "WHERE ( companyowner.company = CompaniaMast.CompaniaCodigo ) and " +
                "(CompaniaMast.Estado = 'A' )";
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

        public DataTable GetDataUnidadesNegocio()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT '' AS 'IDNEG','TODOS' AS 'NEGDESC' " +
                "UNION ALL " +
                "SELECT  " +
                "MA_UnidadNegocio.UnidadNegocio AS 'IDNEG', " +
                "MA_UnidadNegocio.DescripcionLocal AS 'NEGDESC' " +
                "FROM MA_UnidadNegocio " +
                "WHERE " +
                "(MA_UnidadNegocio.Estado = 'A' )";
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

        public DataTable GetDataUnidadesReplicacion()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = " SELECT ''AS 'IDREP','TODOS' AS 'REPDESC' " +
                "UNION ALL " +
                "SELECT  " +
                "SY_UnidadReplicacion.UnidadReplicacion AS 'IDREP', " +
                "SY_UnidadReplicacion.DescripcionLocal AS 'REPDESC' " +
                "FROM SY_UnidadReplicacion " +
                "WHERE ( SY_UnidadReplicacion.Estado = 'A' ) ";
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

        public DataTable GetDataConceptos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT '' AS 'IDCONCEP','TODOS' AS 'CONCEPDESC' " +
                "UNION ALL " +
                "SELECT " +
                "AP_ConceptoGasto.ConceptoGasto AS 'IDCONCEP', " +
                "AP_ConceptoGasto.DescripcionLocal AS 'CONCEPDESC' " +
                "FROM AP_ConceptoGasto " +
                "WHERE ( AP_ConceptoGasto.Estado = 'A' ); ";
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

        public DataTable GetDataPersona(string nombre)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "  SELECT  " +
                "PersonaMast.Persona AS 'ID', " +
                "LTRIM(RTRIM(PersonaMast.Busqueda)) AS 'PERSONA',  " +
                "PersonaMast.Documento AS 'DOCUMENTO'" +
                "FROM PersonaMast " +
                "WHERE ( PersonaMast.Estado = 'A' )     AND " +
                "(EsEmpleado = 'S' or EsProveedor = 'S' or EsOtro = 'S' or EsCliente = 'S')  AND " +
                "Busqueda LIKE '" + nombre + "%' ORDER BY Busqueda  ";
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

        public DataTable GetDataPersonaOperaciones(string nombre)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "  SELECT  " +
                "PersonaMast.Persona AS 'ID', " +
                "LTRIM(RTRIM(PersonaMast.Busqueda)) AS 'PERSONA',  " +
                "PersonaMast.Documento AS 'DOCUMENTO'" +
                "FROM PersonaMast " +
                "WHERE ( PersonaMast.Estado = 'A' )     AND " +
                "(EsEmpleado = 'S' or EsProveedor = 'S' or EsOtro = 'S' or EsCliente = 'S')  AND " +
                "Busqueda LIKE '%" + nombre + "%' ORDER BY Busqueda  ";
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

        public DataTable GetDataPersona2(string IDPersona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();//  



                comando.CommandText = "  SELECT   PersonaMast.Persona,     PersonaMast.apellidopaterno, PersonaMast.apellidomaterno," +
                "PersonaMast.Nombres , PersonaMast.tipodocumento , " +
                "PersonaMast.documento  " +
                "FROM   PersonaMast " +
                "INNER JOIN EmpleadoMast  	ON (PersonaMast.Persona = EmpleadoMast.Empleado)  " +
                "WHERE  ( PersonaMast.EsEmpleado = 'S' ) AND " +
                "PersonaMast.Persona = '" + IDPersona + "' ORDER BY Busqueda  ";
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

        public DataTable GetDataPersona3(string IDPersona)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();//  



                comando.CommandText = "  SELECT   PersonaMast.Persona,     PersonaMast.apellidopaterno, PersonaMast.apellidomaterno," +
                "PersonaMast.Nombres , PersonaMast.tipodocumento , " +
                "PersonaMast.documento, HR_PersonaNoGrata.Motivo  " +
                "FROM   PersonaMast " +
                "INNER JOIN EmpleadoMast  	ON (PersonaMast.Persona = EmpleadoMast.Empleado)  " +
                "LEFT JOIN HR_PersonaNoGrata ON (PersonaMast.Persona = HR_PersonaNoGrata.CodigoEmpleado)" +
                "WHERE  ( PersonaMast.EsEmpleado = 'S' ) AND " +
                "PersonaMast.Persona = '" + IDPersona + "' ORDER BY Busqueda  ";
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

        public DataTable GetDataPersona4(string DNI)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "  SELECT  " +
                "PersonaMast.Persona AS 'ID', " +
                "LTRIM(RTRIM(PersonaMast.Busqueda)) AS 'PERSONA',  " +
                "PersonaMast.Documento AS 'DOCUMENTO'" +
                "FROM PersonaMast    WHERE PersonaMast.Documento = '" + DNI + " '  ";
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

        public DataTable GetDataPersona5(string DNI)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "  SELECT  " +
                "PersonaMast.Persona AS 'ID', " +
                "LTRIM(RTRIM(PersonaMast.Busqueda)) AS 'PERSONA',  " +
                "PersonaMast.Documento AS 'DOCUMENTO'" +
                "FROM PersonaMast    WHERE PersonaMast.Busqueda like '%" + DNI + "% '  ";
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

        public DataTable GetDataCentroCostos(string nombre)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT " +
                "AC_CostCenterMst.CostCenter AS 'C. DE COSTOS' , " +
                "LTRIM(RTRIM(AC_CostCenterMst.LocalName)) AS 'DESCRIPCION' " +
                "FROM " +
                "AC_CostCenterMst " +
                "WHERE " +
                "AC_CostCenterMst.Status = 'A' AND " +
                "AC_CostCenterMst.LocalName LIKE '" + nombre + "%' ";
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

        public DataTable GetDataConductores(string nombre)
        {

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando = new SqlCommand("ReportesApp_Operaciones_ConductoresActivos", conexion);
                //comando.CommandText = "SELECT IdConductor AS 'ID',Nombre AS 'PERSONA',Documento AS 'DOCUMENTO' from OP_TR_Conductor  C INNER JOIN EmpleadoMast E ON C.IdPersona = E.Empleado  Where E.Estado='A' AND C.Nombre LIKE '" + nombre + "%' ORDER BY Nombre";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Conductor", nombre));
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

        public DataTable GetUnidades(string Placa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select distinct IdVehiculo as ID , numeroplaca AS UNIDAD, IndTercero as ORIGEN," +
                "CASE WHEN SubTipoVehiculo = '1'THEN 'TRACTO'" +
                "WHEN SubTipoVehiculo ='2' THEN 'PLATAFORMA'	" +
                "WHEN SubTipoVehiculo ='3' THEN 'TOLVA' " +
                "WHEN SubTipoVehiculo ='4' THEN 'FURGON' WHEN SubTipoVehiculo ='5' THEN 'CORTINERA'" +
                "WHEN SubTipoVehiculo ='6' THEN 'TERMOKING'" +
                "WHEN SubTipoVehiculo ='7' THEN 'CAMIONETA'" +
                "WHEN SubTipoVehiculo ='8' THEN 'CAMION'" +
                "WHEN SubTipoVehiculo ='9' THEN 'CISTERNA'" +
                "WHEN SubTipoVehiculo IS NULL THEN ''" +
                "END AS 'TIPO' from OP_TR_Vehiculo where Estado='2' AND numeroplaca LIKE'" + Placa + "%' ORDER BY numeroplaca";
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

        public DataTable GetUnidadesMaquinarias(string Placa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select distinct IdVehiculo as ID , numeroplaca AS UNIDAD, IndTercero as ORIGEN," +
                "CASE WHEN SubTipoVehiculo = '1'THEN 'TRACTO'" +
                "WHEN SubTipoVehiculo ='2' THEN 'PLATAFORMA'	" +
                "WHEN SubTipoVehiculo ='3' THEN 'TOLVA' " +
                "WHEN SubTipoVehiculo ='4' THEN 'FURGON' WHEN SubTipoVehiculo ='5' THEN 'CORTINERA'" +
                "WHEN SubTipoVehiculo ='6' THEN 'TERMOKING'" +
                "WHEN SubTipoVehiculo ='7' THEN 'CAMIONETA'" +
                "WHEN SubTipoVehiculo ='8' THEN 'CAMION'" +
                "WHEN SubTipoVehiculo ='9' THEN 'CISTERNA'" +
                "WHEN SubTipoVehiculo IS NULL THEN ''" +
                "END AS 'TIPO' from OP_TR_Vehiculo where Estado='2' and iNdtercero in ('P','C') AND numeroplaca LIKE'" + Placa + "%' ORDER BY numeroplaca";
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
        public DataTable GetProductos(string Descripcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT IDPRODUCTO AS ID ,CODIGO,Nombre AS DESCRIPCION  FROM OP_AL_Producto WHERE Nombre LIKE '" + Descripcion + "%' ORDER BY DESCRIPCION";
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

        public DataTable GetRutas(string Descripcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECt IdRuta ID, DESCRIPCION  FROM OP_TR_Ruta WHERE Estado = 2 AND DESCRIPCION LIKE '" + Descripcion + "%' ORDER BY DESCRIPCION"; // estado 2 es activo
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

        public DataTable GetRazonSocial(string Razonsocial)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = " SELECT LTRIM (RTRIM (Busqueda)) AS RazonSocial ,Persona as Codigo,DocumentoFiscal FROM PersonaMast WHERE Busqueda LIKE'%" + Razonsocial + "%' AND PersonaMast.Estado = 'A'  ORDER BY Busqueda ASC";
 
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

        public DataTable GetUnidadesActivas(string Unidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Listar_UnidadesActivas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Unidad", Unidad);
                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);


                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }



        public DataTable GetRutasActivas(string Descripcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Listar_RutasActivas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Descripcion", Descripcion);
                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);


                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataEmpleado(string nombre)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT TOP(25) P.Persona AS 'ID', LTRIM(RTRIM(P.Busqueda)) AS 'PERSONA',  P.Documento AS 'DOCUMENTO' FROM PersonaMast p INNER JOIN EmpleadoMast E ON E.Empleado=P.Persona WHERE ( P.Estado = 'A' )  AND (P.EsEmpleado = 'S')  AND P.Busqueda LIKE '%" + nombre + "%' AND E.Estado='A'ORDER BY P.Busqueda  ";
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



        public bool ReportesApp_Permisos_CopiarPermiso(string Nombre, string UsuarioOrigen, int idPersonaOrigen, string NombrePersonaNuevo, int idPersonaNuevo)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Permisos_CopiarPermiso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@NombresOrigen", Nombre);
                comando.Parameters.AddWithValue("@UsuarioOrigen", UsuarioOrigen);
                comando.Parameters.AddWithValue("@idPersonaOrigen", idPersonaOrigen);
                comando.Parameters.AddWithValue("@NombrePersonaNuevo", NombrePersonaNuevo);
                comando.Parameters.AddWithValue("@idPersonaNuevo", idPersonaNuevo);


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

        public DataTable ReportesApp_CargarPersonalSinUsuario()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_CargarPersonalSinUsuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
    
                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);


                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
