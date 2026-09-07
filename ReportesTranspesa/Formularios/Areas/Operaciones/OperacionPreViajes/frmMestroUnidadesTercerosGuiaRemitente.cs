using Comun;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmMestroUnidadesTercerosGuiaRemitente : Form
    {
        public frmMestroUnidadesTercerosGuiaRemitente()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || char.IsSeparator(e.KeyChar)) 
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                gtarjeta.Select();
                txtTarjetaCirculacion.Focus();
            }
        }

        private void txtTarjetaCirculacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtEmpresaCliente_Enter(object sender, EventArgs e)
        {
            txtEmpresaCliente.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtEmpresaCliente_Leave(object sender, EventArgs e)
        {
            txtEmpresaCliente.BackColor = Color.White;
        }

        private void txtEmpresaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEmpresaCliente, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {

                    if (txtEmpresaCliente.Tag == null)
                    {
                        txtEmpresaCliente.Clear();
                        txtEmpresaCliente.Enabled = false;
                    }
                    else
                    {
                        gPlaca.Select();
                        txtPlaca.Focus();
                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtTarjetaCirculacion_Enter(object sender, EventArgs e)
        {
            txtTarjetaCirculacion.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPlaca_Enter(object sender, EventArgs e)
        {
            txtPlaca.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            txtPlaca.BackColor = Color.White;
        }

        private void txtTarjetaCirculacion_Leave(object sender, EventArgs e)
        {
            txtTarjetaCirculacion.BackColor = Color.White;
        }

        private void txtEmpresaCliente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEmpresaCliente, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    if (txtEmpresaCliente.Tag == null)
                    {
                        txtEmpresaCliente.Clear();
                        txtEmpresaCliente.Enabled = false;
                    }
                    else
                    {
                        gPlaca.Select();
                        txtPlaca.Focus();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaDestinatario_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEmpresaCliente, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstEmpresaDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEmpresaCliente, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {

                    if (txtEmpresaCliente.Tag == null)
                    {
                        txtEmpresaCliente.Clear();
                    }


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPlaca.Text.Length < 6)
                {
                 MessageBox.Show("Usted ingreso datos incorrectos en la placa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
                }
                if (txtEmpresaCliente.Tag == null)
                {
                    MessageBox.Show("Usted no selecciono datos de cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (clsOperacionesBL.Instancia.ReportespApp_Operaciones_PlacaTercero_GuiaElectronica_RegistrarEliminar(Convert.ToInt32(txtEmpresaCliente.Tag), txtEmpresaCliente.Text, txtPlaca.Text, txtTarjetaCirculacion.Text, cbxTipoVehiculo.Text, Utilitario.TipoOperacion.Registrar))
                {
                    ListarPlacas();
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMestroUnidadesTercerosGuiaRemitente_Load(object sender, EventArgs e)
        {
            try
            {
                cbxTipoVehiculo.SelectedIndex = 0;
                ListarPlacas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ListarPlacas()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarUnidadesTerceros_GuiaElectronica();


            if (dt.Rows.Count > 0)
            {
                dgvUnidades.DataSource = dt;
                dgvUnidades.Columns["idCliente"].Visible = false;
                dgvUnidades.Columns["TipoVehiculo"].Visible = false;
            }
            else
            {
                dgvUnidades.DataSource = null;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsOperacionesBL.Instancia.ReportespApp_Operaciones_PlacaTercero_GuiaElectronica_RegistrarEliminar(0, "", dgvUnidades.CurrentRow.Cells["Placa"].Value.ToString(), "", "", Utilitario.TipoOperacion.Anular))
                {
                    ListarPlacas();
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}
