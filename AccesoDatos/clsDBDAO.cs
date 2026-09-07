using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Comun;
using System.Configuration;

namespace AccesoDatos
{
    public class clsDBDAO
    {
        private clsDBDAO()
        {

        }

        private readonly static clsDBDAO instancia = new clsDBDAO();

        public static clsDBDAO Instancia
        {
            get { return instancia; }
        }

        public void IP_Servidor(SqlConnectionStringBuilder CadenaConexion, TextBox txtServidor)
        {

            SqlConnection con = new SqlConnection(CadenaConexion.ConnectionString);
            con.Open();
            SqlCommand comando = new SqlCommand("SELECT c.local_net_address AS Servidor FROM sys.dm_exec_connections AS c WHERE c.session_id = @@SPID", con);
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read() == true)
            {

                Utilitario.Instancia.ipServidor = CadenaConexion.DataSource;
                Utilitario.Instancia.BaseDatos = CadenaConexion.InitialCatalog;
                Utilitario.Instancia.UsuarioConexion = CadenaConexion.UserID;
                Utilitario.Instancia.ClaveConexion = CadenaConexion.Password;
                /*clsConexion.Funciones.valorServidor = CadenaConexion.DataSource;
                clsConexion.Funciones.valorBaseDatos = CadenaConexion.InitialCatalog;
                clsConexion.Funciones.valorUsuario = CadenaConexion.UserID;
                clsConexion.Funciones.valorClave = CadenaConexion.Password;*/

                txtServidor.Text = leer["Servidor"].ToString();
            }
            leer.Close();
            con.Close();

        }

        public void IP_Servidor_CambiarPublico(SqlConnectionStringBuilder CadenaConexion,ref TextBox txtServidor)
        {
            if (CadenaConexion.TransactionBinding == "local")
            {

                txtServidor.Text = "172.16.0.11";
    
           
            }
            else
            {

                txtServidor.Text = "190.116.64.132,29692";
             
           
            }

        }


        public void Nombre_DB(TextBox txtNombreDB)
        {
            SqlConnection con = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            con.Open();
            SqlCommand comando = new SqlCommand("SELECT DB_NAME() AS NombreDB", con);
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read() == true)
            {
                txtNombreDB.Text = leer["NombreDB"].ToString();
            }
            leer.Close();
            con.Close();

        }

        public void UsuariosDB(TextBox txtID, TextBox txtUsuario)
        {
            string usuarios = "";
            usuarios = Utilitario.Instancia.SesionUsuario.usuario;
            SqlConnection con = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            con.Open();
            SqlCommand comando = new SqlCommand("Select Empleado,CodigoUsuario from EmpleadoMast Where CodigoUsuario='" + usuarios + "'", con);
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read() == true)
            {
                txtID.Text = leer["Empleado"].ToString();
                txtUsuario.Text = leer["CodigoUsuario"].ToString();
            }
            leer.Close();
            con.Close();
        }
    }
}
