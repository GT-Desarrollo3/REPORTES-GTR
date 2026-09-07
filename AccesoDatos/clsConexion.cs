using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Comun;
using System.Configuration;


namespace AccesoDatos
{
    public class clsConexion
    {
        public clsConexion()
        {
        
        }

        private readonly static clsConexion instancia = new clsConexion();

        public static clsConexion Instancia
        {
            get { return instancia; }
        }

      

    
      

        public string cadenaConexionLocal()
        {
            string CadenaConexion = "";
   
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string tipoConexion = config.AppSettings.Settings["tipoConexion"].Value;
            
            if (tipoConexion == "publico")
            {
               // CadenaConexion = "Data Source=172.16.0.11;initial catalog=spring;Persist Security Info=True; User ID=sa; Password='" + "Ma5@iñb0t$" + "'";
                CadenaConexion = "Data Source=190.116.64.132,29692;initial catalog=spring;Persist Security Info=True; User ID=sa; Password='" + "Ma5@iñb0t$" + "'";
                clsConexion.Funciones.valorBaseDatos = "spring";
                clsConexion.Funciones.valorClave = "Ma5@iñb0t$";
                clsConexion.Funciones.valorServidor = "190.116.64.132,29692";
                //clsConexion.Funciones.valorServidor = "172.16.0.11";
                clsConexion.Funciones.valorUsuario = "sa";
            }
            else
            {
                CadenaConexion = "Data Source=172.16.0.11;initial catalog=spring;Persist Security Info=True; User ID=sa; Password='" + "Ma5@iñb0t$" + "'";
                clsConexion.Funciones.valorBaseDatos = "spring";
                clsConexion.Funciones.valorClave = "Ma5@iñb0t$";
                clsConexion.Funciones.valorServidor = "172.16.0.11";
                clsConexion.Funciones.valorUsuario = "sa";
            }
            return CadenaConexion;

            //"Data Source=" + direccionIp + ";initial catalog=" + baseDatos + ";Persist Security Info=True; User ID=" + usuario + "; Password='" + "Ma5@iñb0t$" + "'";
        }

        public static class Funciones
        {
            private static string _valorUsuario;
            private static string _valorClave;
            private static string _valorBaseDatos;
            private static string _valorServidor;

            public static string valorUsuario
            {
                get { return _valorUsuario; }
                set { _valorUsuario = value; }
            }
            public static string valorClave 
            {
                get { return _valorClave; }
                set { _valorClave = value; }
            }
            public static string valorBaseDatos
            {
                get { return _valorBaseDatos; }
                set { _valorBaseDatos = value; }
            }
            public static string valorServidor{

                get { return _valorServidor; }
                set { _valorServidor = value; }
            }
           
             
            
        }      
    }
}

