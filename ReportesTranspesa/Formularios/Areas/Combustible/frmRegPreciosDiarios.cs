using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class frmRegPreciosDiarios : Form
    {
        string  _Fecha;
        int _Opcion;
        public decimal _Precio = 12.120000m;
        public frmRegPreciosDiarios()
        {
            InitializeComponent();
        }

        public void pasarDato(int crea_modifica, string fecha, decimal precio)
        {
            _Opcion = crea_modifica;
            _Fecha =fecha;
            _Precio = precio;
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dtpFecha.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            dtpFecha.Format = DateTimePickerFormat.Custom;

            if (txtPrecio.Text.Equals(""))
            {
                MessageBox.Show("Ingresar el Precio...!");
            }
            else
            {
                string rpta;
                DataTable dtGuardar = new DataTable();
                dtGuardar = clsCombustibleBL.Instancia.GetCombustible_PreciosDiarios_Nuevo_Modifa_elimina(_Opcion, dtpFecha.Text, Convert.ToDecimal(txtPrecio.Text), Utilitario.Instancia.SesionUsuario.usuario);
                rpta = Convert.ToString(dtGuardar.Rows[0]["exito"]);
                string NrRPTA = rpta.Substring(0, 1);
           
                if (NrRPTA == "0")
                {
                    MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }            

        private void frmRegPreciosDiarios_Load(object sender, EventArgs e)
        {
            if (_Opcion == 2)
            {
                dtpFecha.Text = _Fecha;
                txtPrecio.Text = Convert.ToString(_Precio);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
