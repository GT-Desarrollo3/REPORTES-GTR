using Comun;
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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmAgregarGuiasFisicas : Form
    {

        DataTable ultimoCorrelativo;
        public frmAgregarGuiasFisicas()
        {
            InitializeComponent();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void frmAgregarGuiasFisicas_Load(object sender, EventArgs e)
        {
            cbxCompania.SelectedIndex = 0;
            cbxSerie.SelectedIndex = 0;
        }

        private void cbxSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbxSerie_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                 ultimoCorrelativo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ObtenerUltimoCorrelativoGuiaTransportista(cbxSerie.Text);
                 if (ultimoCorrelativo.Rows.Count > 0)
                 {
                     txtDesde.Text = Convert.ToString(Convert.ToInt32(ultimoCorrelativo.Rows[0]["UltimoCorrelativo"]) + 1);
                 }
                 else
                 {
                     txtDesde.Clear();
                     txtHasta.Clear();
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ultimoCorrelativo.Rows.Count > 0)
                {
                    txtHasta.Text = Convert.ToString(Convert.ToInt32(txtDesde.Text) + Convert.ToInt32(txtCantidad.Text));
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
