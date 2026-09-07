using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Comun;

namespace ReportesTranspesa.Sistema
{
    public partial class InformacionDB : MetroFramework.Forms.MetroForm
    {
        public InformacionDB()
        {
            InitializeComponent();
        }

        private void InformacionDB_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = Utilitario.Instancia.ipServidor; 
            builder.InitialCatalog = Utilitario.Instancia.BaseDatos;
            builder.UserID = Utilitario.Instancia.UsuarioConexion;
            builder.Password = Utilitario.Instancia.ClaveConexion;

            clsDBBL.Instancia.UsuariosDB(txtID, txtUsuario);
            clsDBBL.Instancia.Nombre_DB(txtNombreDB);
            clsDBBL.Instancia.IP_Servidor(builder, txtServidor);
            BasedeDatos();
        }

        public void BasedeDatos()
        {
            if (txtServidor.Text == "192.168.4.234")
            {
                txtBD.Text = "Producción";
            }
            else
            {
                txtBD.Text = "Pruebas";
            }
        }

    }
}
