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
    public partial class CambiarBD : MetroFramework.Forms.MetroForm
    {
        public CambiarBD()
        {
            InitializeComponent();
        }

        public void BasedeDatos()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = cboDataSource.Text;
            builder.InitialCatalog = cboCatalagoBD.Text;
            builder.UserID = txtUserID.Text;
            builder.Password = txtPaswword.Text;


         
            //builder.ApplicationName = "MyApp";
            Console.WriteLine(builder.ConnectionString);
            clsDBBL.Instancia.IP_Servidor(builder, txtServidor);
            MessageBox.Show("Probando conexion exitosa a la Instancia: "+txtServidor.Text);
            Comun.Utilitario.Instancia.TextoMenuServidor = "Servidor: " + cboDataSource.Text + " - Base Datos: " + cboCatalagoBD.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
            //Application.Restart();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                BasedeDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void CambiarBD_Load(object sender, EventArgs e)
        {
          



            cboCatalagoBD.SelectedIndex = 0;
            cboDataSource.SelectedIndex = 0;
            txtUserID.Text = "sa";

            if (cboDataSource.SelectedItem.ToString() == "192.168.4.234")
            {
                cboCatalagoBD.SelectedIndex = 3;
                txtPaswword.Text = "s!stema5";
                
            }

            if (cboDataSource.SelectedItem.ToString() == "192.168.4.237")
            {
                cboCatalagoBD.SelectedIndex = 2;
                txtPaswword.Text = "diego090991?";

            }

            if (cboDataSource.SelectedItem.ToString() == "192.168.4.15")
            {
                cboCatalagoBD.SelectedIndex = 3;
                txtPaswword.Text = "s!stema5";

            }

            
        }

        private void cboDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboDataSource.SelectedItem.ToString() == "192.168.4.234")
            {
                cboCatalagoBD.SelectedIndex = 3;
                txtPaswword.Text = "s!stema5";

            }

            if (cboDataSource.SelectedItem.ToString() == "192.168.4.237")
            {
                cboCatalagoBD.SelectedIndex = 2;
                txtPaswword.Text = "diego090991?";

            }

            if (cboDataSource.SelectedItem.ToString() == "192.168.4.15")
            {
                cboCatalagoBD.SelectedIndex = 3;
                txtPaswword.Text = "s!stema5";

            }
        }
    }
}
