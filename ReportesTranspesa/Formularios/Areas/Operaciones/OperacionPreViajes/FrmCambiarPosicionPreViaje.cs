using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmCambiarPosicionPreViaje : Form
    {

        public string rango;
        public int Resp = 0;
        public int ItemActual;
        public int ItemNuevo;
        public int ItemInicial;
        public int ItemFinal;
        


        public FrmCambiarPosicionPreViaje()
        {
            InitializeComponent();
        }

        private void FrmCambiarPosicionPreViaje_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Resp = 0;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPosicion.Text == "")
            {
                MessageBox.Show("Ingrese una posición.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPosicion.Focus();
                return;
            }

            ItemNuevo = Convert.ToInt32(txtPosicion.Text);


            if (ItemNuevo == ItemActual)
            {
                MessageBox.Show("No debe ser la misma posición del Item", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPosicion.Focus();
                return;

            }
            else
            {
                if (ItemNuevo >= ItemInicial && ItemNuevo <= ItemFinal)
                {

                    Resp = 1;
                }
                else
                {
                    MessageBox.Show("Item a cambiar debe de estar entre el rango " + ItemInicial.ToString() + " a " + ItemFinal.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPosicion.Focus();
                    return;
                }
            }

            Resp = 1;
            this.Close();

        }

        private void txtPosicion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPosicion_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (txtPosicion.Text.Equals(""))
            {
                return;
            }*/
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
    }
}
