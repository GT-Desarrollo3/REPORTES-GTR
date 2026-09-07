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
    public partial class frmMaestroNumerosMTCxEmpresa : Form
    {
        public frmMaestroNumerosMTCxEmpresa()
        {
            InitializeComponent();
        }

        private void txtEmpresaCliente_Enter(object sender, EventArgs e)
        {
            txtEmpresaCliente.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtEmpresaCliente_Leave(object sender, EventArgs e)
        {
            txtEmpresaCliente.BackColor = Color.White;
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

        private void lstEmpresaDestinatario_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEmpresaCliente, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
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

        private void txtEmpresaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
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
                        gMTC.Select();
                        txtMTC.Focus();
                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
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
                        gMTC.Select();
                        txtMTC.Focus();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarEmpresaxCodigoMTC(Convert.ToInt32(txtEmpresaCliente.Tag),txtEmpresaCliente.Text,txtMTC.Text,Utilitario.TipoOperacion.Registrar))
                {
                    ListarCodigosMTC();
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

        private void frmMaestroNumerosMTCxEmpresa_Load(object sender, EventArgs e)
        {
            ListarCodigosMTC();
        }

        private void ListarCodigosMTC()
        {
            DataTable dt =  clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarEmpresaxCodigoMTC();
            if (dt.Rows.Count > 0)
            {
                dgvMTC.DataSource = dt;
                dgvMTC.Columns["idCliente"].Visible = false;
            }
            else
            {
                dgvMTC.DataSource = null;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarEmpresaxCodigoMTC(Convert.ToInt32(dgvMTC.CurrentRow.Cells["idCliente"].Value), "", dgvMTC.CurrentRow.Cells["NumeroMTC"].Value.ToString(), Utilitario.TipoOperacion.Anular))
                {
                    ListarCodigosMTC();
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
