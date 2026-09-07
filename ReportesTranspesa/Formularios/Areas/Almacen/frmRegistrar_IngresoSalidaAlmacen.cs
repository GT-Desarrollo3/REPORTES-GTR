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
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using System.Drawing.Printing;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class frmRegistrar_IngresoSalidaAlmacen : Form
    {
        public frmRegistrar_IngresoSalidaAlmacen()
        {
            InitializeComponent();
        }

        private void lstCliente_Enter(object sender, EventArgs e)
        {
            if (!lstCliente.Items.Count.Equals(0))
            {
                lstCliente.Items[0].Selected = true;
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
                txtConductor.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstCliente.Visible = false;
                lstCliente.Tag = 0;
                txtCliente.Focus();
            }
        }

        private void txtOrdenCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) )
            {
                txtCliente.Focus();
                
            }

        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];
                txtConductor.Tag = Convert.ToInt32(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;

                lstConductor.Visible = false;
                txtVehiculo.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstConductor.Visible = false;
                txtConductor.Tag = 0;
                txtConductor.Focus();
            }
        }

        private void lstTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstTracto.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTracto.SelectedItems[0];
                txtVehiculo.Tag = Convert.ToInt32(ItemActual.Text);
                txtVehiculo.Text = ItemActual.SubItems[1].Text;

                lstTracto.Visible = false;
                lstTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstTracto.Visible = false;
                txtVehiculo.Tag = 0;
                btnGuardar.Focus();
                

            }
        }


        private void lstConductor_Enter(object sender, EventArgs e)
        {
            if (!lstConductor.Items.Count.Equals(0))
            {
                lstConductor.Items[0].Selected = true;
            }
        }

        private void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0))
            {
                lstTracto.Items[0].Selected = true;
            }
        }

        private void txtConductor_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //CARGADO DEL CONDUCTOR
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
  
                    clsVisuales.Instancia.LlenarLw(lstConductor, clsAlmacenBL.Instancia.ReportesApp_Almacen_ListarConductoresAlmacen(txtConductor.Text), true, false, false);

                    lstConductor.Columns[0].Width = 0;
                    lstConductor.Columns[1].Width = 206;
                    lstConductor.Columns[2].Width = 0;
                    lstConductor.Columns[3].Width = 110;

                    lstConductor.Size = new System.Drawing.Size(lstCliente.Size.Width, 103);

                    lstConductor.BringToFront();
                    lstConductor.Visible = true;
                    lstConductor.Focus();
                
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstConductor.Visible = false;
                txtConductor.Focus();
            }
        }

        private void frmRegistrar_IngresoSalidaAlmacen_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
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
                    lstCliente.Columns[1].Width = 206;


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

        private void txtVehiculo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //CARGADO DEL VEHICULO
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {

                    clsVisuales.Instancia.LlenarLw(lstTracto, clsAlmacenBL.Instancia.ReportesApp_Almacen_ListarVehiculoAlmacen(txtVehiculo.Text), true, false, false);

                    lstTracto.Columns[0].Width = 0;
                    lstTracto.Columns[1].Width = 206;


                    lstTracto.Size = new System.Drawing.Size(lstTracto.Size.Width, 103);

                    lstTracto.BringToFront();
                    lstTracto.Visible = true;
                    lstTracto.Focus();

                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lstTracto.Visible = false;
                    txtCliente.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (txtOrdenCliente.TextLength == 0)
                {
                    MessageBox.Show("La orden no puede estar vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (checkCliente.Checked )
                {
                    if (txtCliente.Tag.ToString() == "0" || txtCliente.Tag.ToString() == "")
                    {
                        MessageBox.Show("El Cliente no puede estar vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (checkConductor.Checked )
                {
                    if (txtConductor.Tag.ToString() == "0" || txtConductor.Tag.ToString() == "")
                    {
                        MessageBox.Show("El Conductor no puede estar vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if(checkVehiculo.Checked )
                {
                    if (txtVehiculo.Tag.ToString() == "0" || txtVehiculo.Tag.ToString() == "")
                    {
                        MessageBox.Show("El Vehiculo no puede estar vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (txtVehiculo.Tag.ToString() == "" || txtVehiculo.Text.ToString().Length < 7)
                {
                    MessageBox.Show("El Vehiculo no puede estar vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
    
                string codigo = "";
                string transportista = "";
                string nroOrden = "";
                
                string grupo = "";

                if (rbtGranel.Checked)
                {
                    grupo = "GRANEL";
                }
                if(rbtEnsacado.Checked)
                {
                    grupo = "ENSACADO";
                }



                if (clsAlmacenBL.Instancia.ReportesApp_Almacen_Registrar_FechaIngreso(txtOrdenCliente.Text, txtCliente.Tag == null ? 0 : Convert.ToInt32(txtCliente.Tag), txtCliente.Text, txtConductor.Tag == null ? 0 : Convert.ToInt32(txtConductor.Tag), txtConductor.Text, txtVehiculo.Tag == null ? 0 : Convert.ToInt32(txtVehiculo.Tag), txtVehiculo.Text, Convert.ToDateTime(lblFecha.Text).ToString("dd/MM/yyyy HH:mm:ss"), txtPesoGuia.Text.Trim(), grupo, ref  nroOrden, ref codigo, ref transportista))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    imprimitTicket(codigo,transportista,grupo,nroOrden);


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

        private void imprimitTicket(string codigo,string transportista,string grupo , string nroOrden)
        {
            PrintDocument printDoc = new PrintDocument();
            Ticket ticket = new Ticket();
            ticket.MaxChar = 40;
            ticket.MaxCharDescription = 40;
            ticket.AddHeaderLine("GRUPO: " + grupo + "                          ");
            ticket.AddSubHeaderLine2("GRUPO TRANSPESA S.A.C.");
            ticket.FontCodigo = 12;
            ticket.AddHeaderLine("        CONTROL DE INGRESO / SALIDA" + "                                                               ");
            ticket.AddSubHeaderLine2("NRO ORDEN: " + nroOrden + "                          ");
            ticket.FontSize = 8;
            ticket.AddSubHeaderLine("CODIGO: " + codigo + "                          ");
            ticket.AddSubHeaderLine("ORDEN CLIENTE: " + txtOrdenCliente.Text + "                          ");
            ticket.AddSubHeaderLine("CLIENTE: " + txtCliente.Text + "                          ");
            ticket.AddSubHeaderLine("CONDUCTOR: " + txtConductor.Text);
            ticket.AddSubHeaderLine("VEHICULO" + txtVehiculo.Text + "                    ");
            ticket.AddSubHeaderLine("                                         ");
            ticket.AddSubHeaderLine("TRANSPORTISTA: " + transportista + "                    ");
            ticket.AddSubHeaderLine("FECHA LLEGADA: " + lblFecha.Text + "                    ");
            ticket.AddSubHeaderLine("                                         ");


            /* for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
             {
                 ticket.AddItem(dgvProductosGuia.Rows[i].Cells["Cantidad"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Codigo"].Value.ToString());
                       
             }*/

        
            ticket.AddFooterLine("                                   ");




            int Encontrado = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {        
                if (printer == "POS-80-Series")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                }

                if (printer == "POS-80-Series (1)")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                }

                if (printer == "POS-80-Series (2)")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                }
            }

            if (Encontrado == 0)
            {
                ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
            }

        }



    
    }
}
