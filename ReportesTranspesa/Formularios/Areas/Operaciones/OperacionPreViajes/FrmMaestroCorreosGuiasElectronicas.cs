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
    public partial class FrmMaestroCorreosGuiasElectronicas : Form
    {
        public int TipOperacion;

        public string Cliente;
        public string idCliente;
        public int idCorreoPrincipal;
        public string direccionDestino = "";
 


        public FrmMaestroCorreosGuiasElectronicas()
        {
            InitializeComponent();
        }

        private void txtEmpresaRemitente_Enter(object sender, EventArgs e)
        {

            txtEmpresaDestinatario.BackColor = Color.FromArgb(192, 255, 192); 
            
        }

        private void txtEmpresaRemitente_Leave(object sender, EventArgs e)
        {
            txtEmpresaDestinatario.BackColor = Color.White;
        }

        private void txtEmpresaRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEmpresaDestinatario, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtEmpresaRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEmpresaDestinatario, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaRemitente_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEmpresaDestinatario, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        public void CargarListaCorreosEmpresa()
        {
                
                DataTable dt = clsOperacionesBL.Instancia.ReportesApp_ListarCorreos_Master(Convert.ToInt32(txtEmpresaDestinatario.Tag),txtDireccionDestino.Text);
                dgvCorreos.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvCorreos.Rows.Add(dt.Rows[i]["idCliente"], dt.Rows[i]["idCorreo"], dt.Rows[i]["Correo"], dt.Rows[i]["Principal"]);
                    //dgvCorreos.Rows[i].ReadOnly = true;
                }

        }
        private void lstEmpresaRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEmpresaDestinatario, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    CargarListaCorreosEmpresa();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEmpresaDestinatario, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMaestroCorreosGuiasElectronicas_Load(object sender, EventArgs e)
        {
            try
            {

                if (Utilitario.TipoOperacion.Lectura == TipOperacion)
                {
                    txtEmpresaDestinatario.Text = Cliente;
                    txtEmpresaDestinatario.Tag = idCliente;
                    txtEmpresaDestinatario.Enabled = false;
                    txtDireccionDestino.Text = direccionDestino;
                    btnAgregar.Visible = true;
                    btnActualizar.Enabled = true;
                    eliminarToolStripMenuItem.Enabled = true;

                    CargarListaCorreosEmpresa();
                    
                }

                if (Utilitario.TipoOperacion.Editar == TipOperacion)
                {
                    //txtEmpresaDestinatario.Text = Cliente;
                    //txtEmpresaDestinatario.Tag = idCliente;
                    txtEmpresaDestinatario.Enabled = true;
                    btnAgregar.Visible = false;
                    eliminarToolStripMenuItem.Enabled = false;

                    CargarListaCorreosEmpresa();

                }

                if (Utilitario.TipoOperacion.Anular == TipOperacion)
                {
                    //txtEmpresaDestinatario.Text = Cliente;
                    //txtEmpresaDestinatario.Tag = idCliente;
                    txtEmpresaDestinatario.Enabled = true;
                    btnActualizar.Enabled = false;
                    btnAgregar.Visible = false;
             
                    CargarListaCorreosEmpresa();

                }

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCorreos.Rows.Count > 0)
                {
                    dgvCorreos.Rows.Add("", "", "", false);
                }
                else
                {
                    dgvCorreos.Rows.Add("", "", "", true);
                }
               

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void dgvCorreos_Enter(object sender, EventArgs e)
        {

        }

        private void dgvCorreos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
           
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmMaestroCorreosGuiasElectronicas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
  
            
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCorreos.CurrentRow.Cells["idCorreo"].Value != "")
                {

                    if (clsOperacionesBL.Instancia.ReportesApp_Nuevo_Editar_Anular_Correo_Empresa_GuiasElectronicas(Convert.ToInt32(txtEmpresaDestinatario.Tag), 
                                                                                                     dgvCorreos.CurrentRow.Cells["Correo"].Value.ToString(), 
                                                                                                     Utilitario.TipoOperacion.Anular, txtDireccionDestino.Text,
                                                                                                     false, Convert.ToInt32(dgvCorreos.CurrentRow.Cells["idCorreo"].Value)))

                    {
                        CargarListaCorreosEmpresa();
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Correo no se ha creado favor presione enter luego de digitar el correo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMaestroCorreosGuiasElectronicas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgvCorreos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void dgvCorreos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {


                if (dgvCorreos.Columns[e.ColumnIndex].Name == "Principal")
                    {

                        for (int i = 0; i < dgvCorreos.Rows.Count; i++)
                        {
                            dgvCorreos.Rows[i].Cells["Principal"].Value = false;
                        }

                        dgvCorreos.Rows[e.RowIndex].Cells["Principal"].Value = true;


                      /*  if (dgvCorreos.CurrentRow.Cells["idCorreo"].Value.ToString() != "")
                        {
     
                            if (clsOperacionesBL.Instancia.ReportesApp_Nuevo_Editar_Anular_Correo_Empresa_GuiasElectronicas(Convert.ToInt32(txtEmpresaDestinatario.Tag),
                                                                                                            dgvCorreos.CurrentRow.Cells["Correo"].Value.ToString(),
                                                                                                            4, txtDireccionDestino.Text,//opcion asignar principal
                                                                                                            Convert.ToInt32(dgvCorreos.CurrentRow.Cells["idCorreo"].Value)))
                            {
                                idCorreoPrincipal = Convert.ToInt32(dgvCorreos.CurrentRow.Cells["idCorreo"].Value);
                                CargarListaCorreosEmpresa();
                                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("correo aun no fue registrado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    */
       

                    }
                }

            
            catch (Exception ex)
            {

                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void FrmMaestroCorreosGuiasElectronicas_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsOperacionesBL.Instancia.ReportesApp_Nuevo_Editar_Anular_Correo_Empresa_GuiasElectronicas(Convert.ToInt32(txtEmpresaDestinatario.Tag),
                                                                                                 dgvCorreos.CurrentRow.Cells["Correo"].Value.ToString(),
                                                                                                 Utilitario.TipoOperacion.Editar, txtDireccionDestino.Text,
                                                                                                 Convert.ToBoolean(dgvCorreos.CurrentRow.Cells["Principal"].Value), Convert.ToInt32(dgvCorreos.CurrentRow.Cells["idCorreo"].Value)))
                {
                    dgvCorreos.CurrentRow.ReadOnly = false;
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

  


                    if (clsOperacionesBL.Instancia.ReportesApp_Nuevo_Editar_Anular_Correo_Empresa_GuiasElectronicas(Convert.ToInt32(txtEmpresaDestinatario.Tag), dgvCorreos.CurrentRow.Cells["Correo"].Value.ToString(), Utilitario.TipoOperacion.Registrar,txtDireccionDestino.Text,false))
                    {

                        CargarListaCorreosEmpresa();
                        //dgvCorreos.CurrentRow.ReadOnly = true;

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
