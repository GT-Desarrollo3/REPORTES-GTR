using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Negocio
{
    public class clsDBBL
    {
        private clsDBBL()
        {

        }

        private readonly static clsDBBL instancia = new clsDBBL();

        public static clsDBBL Instancia
        {
            get { return instancia; }
        }

        public void IP_Servidor(SqlConnectionStringBuilder CadenaConexion, TextBox Servidor)
        {
            clsDBDAO.Instancia.IP_Servidor(CadenaConexion, Servidor);
        }

        public void Nombre_DB(TextBox NombreDB)
        {
            clsDBDAO.Instancia.Nombre_DB(NombreDB);
        }

        public void UsuariosDB(TextBox ID, TextBox Usuario)
        {
            clsDBDAO.Instancia.UsuariosDB(ID, Usuario);
        }

        public void IP_Servidor_CambiarPublico(SqlConnectionStringBuilder builder,ref TextBox servidor)
        {
            clsDBDAO.Instancia.IP_Servidor_CambiarPublico(builder,ref servidor);
        }
    }

}
