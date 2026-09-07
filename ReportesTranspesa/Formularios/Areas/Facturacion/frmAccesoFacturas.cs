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

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class frmAccesoFacturas : Form
    {
        public string Factura, serie, compania;
        int Descenlace=0, ModificarMonto=0, EliminarFactura=0;
        public frmAccesoFacturas()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //obtenemos los valores
            if (cbhDescenlace.Checked == true)
            {
                Descenlace = 1;
            }
            if (cbhModifcaMontos.Checked == true)
            {
                ModificarMonto = 1;
            }
            if (cbhEliminarFacturas.Checked == true)
            {
                EliminarFactura = 1;
            }


            string Respuesta;
            DataTable dtGuardarAcceso = new DataTable();
            dtGuardarAcceso = clsFinanzasBL.Instancia.GetRegistrarAcesosxFactura(Factura, serie, compania, Utilitario.Instancia.SesionUsuario.usuario, Descenlace, ModificarMonto,
                                                     EliminarFactura);
            Respuesta = Convert.ToString(dtGuardarAcceso.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAccesoFacturas_Load(object sender, EventArgs e)
        {
            lblDatoFactura.Text = serie +" "+ Factura;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
