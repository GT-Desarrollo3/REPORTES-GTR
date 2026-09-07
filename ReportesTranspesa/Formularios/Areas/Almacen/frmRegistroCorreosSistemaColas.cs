using Negocio;
using ReportesTranspesa.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using System.Text.RegularExpressions;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class frmRegistroCorreosSistemaColas : Form
    {
        public frmRegistroCorreosSistemaColas()
        {
            InitializeComponent();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //CARGADO DEL CLIENTE
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {

                    clsVisuales.Instancia.LlenarLw(lstCliente, clsAlmacenBL.Instancia.ReportesApp_Almacen_ListarClientesAlmacen(txtCliente.Text), true, false, false);

                    lstCliente.Columns[0].Width = 0;
                    lstCliente.Columns[1].Width = 300;


                    lstCliente.Size = new System.Drawing.Size(lstCliente.Size.Width, 103);

                    lstCliente.BringToFront();
                    lstCliente.Visible = true;
                    lstCliente.Focus();

                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lstCliente.Visible = false;
                    txtCliente.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstCliente.SelectedItems[0];
                txtCliente.Tag = Convert.ToInt32(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;

                lstCliente.Visible = false;
         
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstCliente.Visible = false;
                lstCliente.Tag = 0;
                txtCliente.Focus();
            }
        }

        private void lstCliente_Enter(object sender, EventArgs e)
        {
            if (!lstCliente.Items.Count.Equals(0))
            {
                lstCliente.Items[0].Selected = true;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCliente.Tag == null)
                {
                    MessageBox.Show("No a seleccionado el Cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }



                if (txtCorreo1.Text.Length == 0)
                {
                    MessageBox.Show("Es necesario ingresar un correo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {

                    if (!Regex.IsMatch(txtCorreo1.Text, "@"))
                    {
                        MessageBox.Show("El correo debe contener un @", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (txtCorreo2.Text.Length > 0)
                {
                    if (!Regex.IsMatch(txtCorreo2.Text, "@"))
                    {
                        MessageBox.Show("El correo debe contener un @", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                   
                }
  
            

                if (clsAlmacenBL.Instancia.ReportesApp_Almacen_Registrar_Correos(txtCliente.Text,Convert.ToInt16(txtCliente.Tag),txtCorreo1.Text,txtCorreo2.Text))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    DataTable dt = clsAlmacenBL.Instancia.ReportesApp_Almacen_Registrar_ListarCorreos();
                    dgvListar.DataSource = dt;


                    this.Close();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia,"Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsAlmacenBL.Instancia.ReportesApp_Almacen_Eliminar_Correos(Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "idCorreo"))))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = clsAlmacenBL.Instancia.ReportesApp_Almacen_Registrar_ListarCorreos();
                    dgvListar.DataSource = dt;
                  
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia,"Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmRegistroCorreosSistemaColas_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsAlmacenBL.Instancia.ReportesApp_Almacen_Registrar_ListarCorreos();
                dgvListar.DataSource = dt;
                gridView1.Columns["idCorreo"].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

    
    }
}
