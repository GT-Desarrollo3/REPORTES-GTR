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
using ReportesTranspesa.Sistema;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    
    public partial class frmRegularizarTickets : Form
    {
        public int tipo,codigopreviaje,ticket;
        public string dni, conductor,placa,fechaTicket,horaTicket;
        decimal odometro;
        public frmRegularizarTickets()
        {
            InitializeComponent();
        }

        private void frmRegularizarTickets_Load(object sender, EventArgs e)
        {
           // txtTicket.Text = ticket.ToString();
            label7.Text ="Fecha Despacho: " + fechaTicket;
            txtCodigoPreviaje.Text = codigopreviaje.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                grbCodigoPreviaje.Enabled = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
            else {
                grbCodigoPreviaje.Enabled = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                grbPlaca.Enabled = true;
                radioButton1.Checked = false;
                radioButton3.Checked = false;
            }
            else {
                grbPlaca.Enabled = false;
                radioButton1.Checked = false;
                radioButton3.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                tipo = 1;
            }

            if (radioButton2.Checked == true)
            {
                tipo = 2;
            }

            if (radioButton3.Checked == true)
            {
                tipo = 3;
            }

            if (radioButton1.Checked == true && txtCodigoPreviaje.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Codigo Previaje","ADVERTENCIA");
                return;
            }

            if (radioButton2.Checked == true && txtPlaca.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Placa", "ADVERTENCIA");
                return;
            }
            if (radioButton2.Checked == true && txtOdometro.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Odometro", "ADVERTENCIA");
                return;
            }
            if (radioButton2.Checked == true && txtConductor.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Conductor", "ADVERTENCIA");
                return;
            }
            if (radioButton2.Checked == true && txtDni.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Dni", "ADVERTENCIA");
                return;
            }

            ticket = Convert.ToInt32(txtTicket.Text);
            if (txtOdometro.Text.Length > 0)
            {
                odometro = Convert.ToDecimal(txtOdometro.Text);
            }

            codigopreviaje =Convert.ToInt32(txtCodigoPreviaje.Text);
            string rpta;
            DataTable dtGurdarDatos = new DataTable();
            dtGurdarDatos = clsCombustibleBL.Instancia.GetCombustible_RegularizarTicktesalKardex(tipo, ticket, codigopreviaje, txtPlaca.Text, txtDni.Text,
                                                                                                    txtConductor.Text, odometro, Utilitario.Instancia.SesionUsuario.usuario);
            rpta = Convert.ToString(dtGurdarDatos.Rows[0]["exito"]);
            string NrRPTA = rpta.Substring(0, 1);
            if (NrRPTA == "0")
            {
                MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (lstFiltroPlacas.SelectedIndices() == true)
                //{
                //    MessageBox.Show("seleccionar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPlaca.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstPlaca.SelectedItems[0];

                    lstPlaca.Visible = false;

                    txtPlaca.Text = ItemActual.SubItems[1].Text;
                    txtPlaca.Tag = ItemActual.SubItems[0].Text;
                    CargarOdometro();
                    txtOdometro.Focus();
                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lstPlaca.Visible = false;
                    txtPlaca.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CargarOdometro()
        {

            DataTable dt = new DataTable();

            dt = clsDespachosTercerosBL.Instancia.getDespachosTerceros_ObtenerOdometroPlaca(txtPlaca.Text, fechaTicket.Substring(0, 8), fechaTicket.Substring(9, 8));

            if (dt != null)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    MessageBox.Show("No se encontró Odometro para esta placa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    txtOdometro.Text = dt.Rows[0][1].ToString();
                    txtDni.Text = dt.Rows[0][4].ToString();
                    txtConductor.Text = dt.Rows[0][3].ToString();
                }
            }
            else
            {
                MessageBox.Show("No se encontró Odometro para esta placa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPlaca.Text.Length > 0)
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {

                    clsVisuales.Instancia.LlenarLw(lstPlaca, clsDespachosTercerosBL.Instancia.getDocumento_BuscarRelacion(0, txtPlaca.Text), true, false, false);
                    
                    if (lstPlaca.Items.Count.Equals(0))
                    {
                        txtPlaca.Focus();
                    }
                    else
                    {
                        lstPlaca.Columns[0].Width = 0;
                        lstPlaca.Columns[1].Width = 240;
                        lstPlaca.Columns[2].Width = 0;

                        lstPlaca.Size = new System.Drawing.Size(130, 103);

                        lstPlaca.BringToFront();
                        lstPlaca.Visible = true;

                        txtOdometro.Focus();
                        lstPlaca.Items[0].Selected = true;
                    }
                }
            }
            else
            {
                lstPlaca.Visible = false;
                txtPlaca.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPlaca.Visible = false;
                txtPlaca.Focus();
            }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            if (!lstPlaca.Items.Count.Equals(0))
            {
                lstPlaca.Items[0].Selected = true;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {   
                    ListViewItem ItemActual;

                    ItemActual = lstPlaca.SelectedItems[0];

                    lstPlaca.Visible = false;

                    txtPlaca.Text = ItemActual.SubItems[1].Text;
                    txtPlaca.Tag = ItemActual.SubItems[0].Text;
                    CargarOdometro();
                    txtOdometro.Focus(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                grbPlaca.Enabled = false;
                grbCodigoPreviaje.Enabled = false;
                radioButton1.Checked = false;
                 radioButton2.Checked = false;
            }           
        }
    }
}
