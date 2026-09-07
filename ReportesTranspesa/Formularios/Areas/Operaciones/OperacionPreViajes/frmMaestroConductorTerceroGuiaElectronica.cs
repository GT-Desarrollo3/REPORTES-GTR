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
    public partial class frmMaestroConductorTerceroGuiaElectronica : Form
    {
        public frmMaestroConductorTerceroGuiaElectronica()
        {
            InitializeComponent();
        }

        private void frmMaestroConductorTerceroGuiaElectronica_Load(object sender, EventArgs e)
        {
            try
            {
                CargarTipoDocumentos();
                CargarConductoresTerceros();
                txtEmpresaCliente.Text = "GRUPO TRANSPESA S.A.C";
                txtEmpresaCliente.Tag = "1553";
                gNombres.Select();
                txtNombres.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }

        private void CargarConductoresTerceros()
        {
          DataTable dt =  clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarConductoresTercerosGuiaElectronica(-1);
          if (dt.Rows.Count > 0)
          {
              dgvConductores.DataSource = dt;
              dgvConductores.Columns["idCliente"].Visible = false;
              dgvConductores.Columns["CodTipoDocIdentidad_Conductor"].Visible = false;
              dgvConductores.Columns["Nombres"].Visible = false;
              dgvConductores.Columns["Apellidos"].Visible = false;
          }
          else
          {
              dgvConductores.DataSource = null;
          }

        }

        private void CargarTipoDocumentos()
        {
            DataTable dtTipoDocFiscal = clsOperacionesBL.Instancia.ReportesApp_Listar_TipoDocumentoIdentidad_GuiaElectronica();
            if (dtTipoDocFiscal.Rows.Count > 0)
            {
                cbxTipoDocumento.DataSource = dtTipoDocFiscal;
                cbxTipoDocumento.DisplayMember = "Descripcion";
                cbxTipoDocumento.ValueMember = "Codigo";
                cbxTipoDocumento.SelectedIndex = 1;

            }
            else
            {
                MessageBox.Show("Combobox de TipoProducto no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void txtConductor_Enter(object sender, EventArgs e)
        {
            txtNombres.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtConductor_Leave(object sender, EventArgs e)
        {
            txtNombres.BackColor = Color.White;
        }

        private void txtLicenciaConducir_Enter(object sender, EventArgs e)
        {
            txtLicenciaConducir.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtLicenciaConducir_Leave(object sender, EventArgs e)
        {
            txtLicenciaConducir.BackColor = Color.White;
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
                        gNombres.Select();
                        txtNombres.Focus();
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
                        gNombres.Select();
                        txtNombres.Focus();
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
            if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEmpresaCliente, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
            {

                if (txtEmpresaCliente.Tag == null)
                {
                    txtEmpresaCliente.Clear();
                }
                gNombres.Select();
                txtNombres.Focus();

            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gApellidos.Select();
                txtApellidos.Focus();
            }
            
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gDocumento.Select();
                txtDocumento.Focus();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpresaCliente.Tag == null || txtNombres.Text.Length == 0 || txtApellidos.Text.Length == 0 || txtDocumento.Text.Length == 0 || txtLicenciaConducir.Text.Length == 0)
                {
                    MessageBox.Show("Usted no llenó uno o varios dato obligatorios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (clsOperacionesBL.Instancia.ReportesAPP_RegistrarAnular_ConductoresTerceros(Convert.ToInt32(txtEmpresaCliente.Tag), txtNombres.Text, txtApellidos.Text, Convert.ToInt32(cbxTipoDocumento.SelectedValue), cbxTipoDocumento.Text, txtDocumento.Text, txtLicenciaConducir.Text, Utilitario.TipoOperacion.Registrar))
                {
                    CargarConductoresTerceros();
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

        private void ListarConductores()
        {
            throw new NotImplementedException();
        }

        private void cbxTipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDocumento_Enter(object sender, EventArgs e)
        {
            txtDocumento.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDocumento_Leave(object sender, EventArgs e)
        {
            txtDocumento.BackColor = Color.White;
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gLicencia.Select();
                txtLicenciaConducir.Focus();
            }
        }

        private void txtApellidos_Enter(object sender, EventArgs e)
        {
            txtApellidos.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtApellidos_Leave(object sender, EventArgs e)
        {
            txtApellidos.BackColor = Color.White;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsOperacionesBL.Instancia.ReportesAPP_RegistrarAnular_ConductoresTerceros(0, "", "", 0, "", dgvConductores.CurrentRow.Cells["NumeroDocIdentidad_Conductor"].Value.ToString(), txtLicenciaConducir.Text, Utilitario.TipoOperacion.Anular))
                {
                    CargarConductoresTerceros();
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
