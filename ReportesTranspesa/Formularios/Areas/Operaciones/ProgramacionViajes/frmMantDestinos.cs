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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmMantDestinos : Form
    {
        int idot, OPCION;
        public int idruta = 0;
        public int idTipoProgramacion;
        public frmMantDestinos()
        {
            InitializeComponent();
        }
        public void envioOTdes(int var_idot)
        {
            idot = var_idot;          

        }
        private void frmMantDestinos_Load(object sender, EventArgs e)
        {
            if (idruta > 0)
            {          

                DataTable dtListaDestinos = new DataTable();
                dtListaDestinos = clsOperacionesBL.Instancia.GetLista_Operaciones_Destinos(idruta);

                if (dtListaDestinos.Rows.Count > 0)
                {
                    chbModificar.Checked = true;
                    lblOtMaes.Text = "Modificar Destino de la Ruta ";//+ idot.ToString();
                    OPCION = 2;

                  for (int i = 0; i < dtListaDestinos.Rows.Count; i++)
                    {
                        txtMaesDestino.Text =dtListaDestinos.Rows[i]["Destino"].ToString();
                        txtZona.Text=dtListaDestinos.Rows[i]["Zona"].ToString();
                        txtMaesKm.Text = dtListaDestinos.Rows[i]["Km"].ToString();
                        txtMaesdias.Text = dtListaDestinos.Rows[i]["CantidadDia"].ToString();
                        txtOrden.Text = dtListaDestinos.Rows[i]["Orden"].ToString();
                        idruta = Convert.ToInt32(dtListaDestinos.Rows[i]["IdRuta"].ToString());
                    }
                }
            }
            lblOtMaes.Text = "Agregar Destino a la Ruta ";//+ idot.ToString();
            OPCION = 1;
        }

        private void txtMaesKm_KeyPress(object sender, KeyPressEventArgs e)
        {         
          
        }

        private void txtMaesdias_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        string Respuesta;
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtDestino = new DataTable();
            
            dtDestino = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_GuardarDestino(OPCION,Convert.ToInt32(idot), txtMaesDestino.Text, Convert.ToDecimal(txtMaesKm.Text),
                                                                                                Convert.ToInt32(txtMaesdias.Text), txtZona.Text, idruta, idTipoProgramacion);
            Respuesta = Convert.ToString(dtDestino.Rows[0]["exito"]); ;
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();
        }

        private void txtMaesDestino_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbModificar_CheckedChanged(object sender, EventArgs e)
        {
            if (chbModificar.Checked == true)
            {
                lblOtMaes.Text = "Modificar Destino de la Ruta " ;//+ idot.ToString();
                OPCION = 2;
            }
            else
            {
                OPCION = 1;
                lblOtMaes.Text = "Agregar Destino a la Ruta ";
                   //+ idot.ToString();
            }
               
        }
    }
}
