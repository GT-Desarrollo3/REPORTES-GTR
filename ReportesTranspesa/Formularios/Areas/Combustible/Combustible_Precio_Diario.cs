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
    public partial class Combustible_Precio_Diario : Form
    {
        string var_fecha;
        decimal var_precio = 12.120000m;
        public Combustible_Precio_Diario()
        {
            InitializeComponent();
        }

        private void Combustible_Precio_Diario_Load(object sender, EventArgs e)
        {
            CargaPrecios();
            dgvPrecios.RowHeadersVisible = false;
        }
        private void CargaPrecios()
        {
            DataTable dtPrecios = new DataTable();
            dtPrecios = clsCombustibleBL.Instancia.GetCombustible_PreciosDiarios("");

            dgvPrecios.DataSource = dtPrecios;
        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRegPreciosDiarios frmRegPrecios = new frmRegPreciosDiarios();
            frmRegPrecios.pasarDato(1, "", var_precio);
            frmRegPrecios.ShowDialog();
            CargaPrecios();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string cont = dgvPrecios.SelectedRows.Count.ToString();
            int pos = Convert.ToInt32(dgvPrecios.CurrentRow.Index.ToString());

            if (dgvPrecios.CurrentRow.Selected == true)
            {
                var_fecha = dgvPrecios.Rows[pos].Cells["FECHA"].Value.ToString();
                var_precio = Convert.ToDecimal(dgvPrecios.Rows[pos].Cells["PRECIO"].Value.ToString());

                frmRegPreciosDiarios frmRegPrecios = new frmRegPreciosDiarios();
                frmRegPrecios.pasarDato(2, var_fecha, var_precio);
                frmRegPrecios.ShowDialog();
                CargaPrecios();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cont = dgvPrecios.SelectedRows.Count.ToString();
            int pos = Convert.ToInt32(dgvPrecios.CurrentRow.Index.ToString());

            if (dgvPrecios.CurrentRow.Selected == true)
            {


                if (MessageBox.Show("Esta seguro de eliminar el Precio?", "Eliminar Precio", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var_fecha = dgvPrecios.Rows[pos].Cells["FECHA"].Value.ToString();
                    var_precio = Convert.ToDecimal(dgvPrecios.Rows[pos].Cells["PRECIO"].Value.ToString());

                    string rpta;
                    DataTable dtGuardar = new DataTable();
                    dtGuardar = clsCombustibleBL.Instancia.GetCombustible_PreciosDiarios_Nuevo_Modifa_elimina(3, var_fecha, var_precio, Utilitario.Instancia.SesionUsuario.usuario);
                    rpta = Convert.ToString(dtGuardar.Rows[0]["exito"]);
                    string NrRPTA = rpta.Substring(0, 1);

                    if (NrRPTA == "0")
                    {
                        MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaPrecios();
                    }
                    else
                    {
                        MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   // MessageBox.Show("Pago realizado");
                }
                else
                {
                  //  MessageBox.Show("Pago NO realizado");
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dtPrecios = new DataTable();
            dtPrecios = clsCombustibleBL.Instancia.GetCombustible_PreciosDiarios(dtpBuscar.Text);

            dgvPrecios.DataSource = dtPrecios;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargaPrecios();
        }
    }
}
