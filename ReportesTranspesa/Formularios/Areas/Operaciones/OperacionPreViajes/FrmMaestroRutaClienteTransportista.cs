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
    public partial class FrmMaestroRutaClienteTransportista : Form
    {
        string ubigeoPartida = string.Empty;
        string direccionUbigeoPartida = string.Empty;
        string ubigeoDestino = string.Empty;
        string direccionUbigeoDestino = string.Empty;
        public int idRuta = 0;
        DataTable dtDireccionesRutaOrigen;
        DataTable dtDireccionesRutaDestino;
        int SecuenciaOrigen = 0;
        int SecuenciaFin = 0;
        public int idCliente = 0 ;
        public string remitente = string.Empty;
        public string destinatario = string.Empty;
        public string ruta = string.Empty;
        public FrmMaestroRutaClienteTransportista()
        {
            InitializeComponent();
        }

        private void lstEmpresaDestinatario_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCliente, ref lstEmpresaCliente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }



        private void lstEmpresaCliente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCliente, ref lstEmpresaCliente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCliente, ref  lstEmpresaCliente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {


                    txtRemitente.Select();
                    txtRemitente.Focus();
                    dtDireccionesRutaOrigen = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtCliente.Tag));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCliente_Enter(object sender, EventArgs e)
        {
            txtCliente.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            txtCliente.BackColor = Color.White;
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtCliente, ref  lstEmpresaCliente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    txtDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtCliente, ref lstEmpresaCliente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    txtDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMaestroRutaClienteTransportista_Load(object sender, EventArgs e)
        {
            try
            {

                CargarMaestro();
               // CargarRemitente();
              //  CargarRuta();
               

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRuta()
        {
            txtRuta.Text = ruta;
            txtRuta_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstRuta.Select();
            lstRuta_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstRuta_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
        }


        private void CargarRemitente()
        {
            txtCliente.Text = remitente;
            txtCliente_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstEmpresaCliente.Select();
            txtCliente_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstEmpresaCliente_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
     
        }
        private void CargarMaestro()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarMaestroClienteDestinatarioRuta(txtBuscarRemitente.Text,txtBuscarRuta.Text);
            if (dt.Rows.Count > 0)
            {
                dgvMaestro.DataSource = dt;
                dgvMaestro.Columns["idCliente"].Visible = false;
                dgvMaestro.Columns["idRuta"].Visible = false;
                dgvMaestro.Columns["idDestinatario"].Visible = false;
                dgvMaestro.Columns["idRemitente"].Visible = false;
            }
            else
            {
                dgvMaestro.DataSource = null;
            }
        }

        private void txtRuta_Enter(object sender, EventArgs e)
        {
            txtRuta.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRuta_Leave(object sender, EventArgs e)
        {
            txtRuta.BackColor = Color.White;
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRuta, ref  lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {

                    btnAgregar.Select();
                    btnAgregar.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {
                    btnAgregar.Select();
                    btnAgregar.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRuta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRuta, ref  lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {

                    //ubigeoPartida = txtRuta.Tag.ToString(); // ubigeo partida
                    //direccionUbigeoPartida = lstRuta.SelectedItems[0].SubItems[3].Text;

                    //ubigeoDestino = lstRuta.SelectedItems[0].SubItems[2].Text; // ubigeo destino
                    //direccionUbigeoDestino = lstRuta.SelectedItems[0].SubItems[4].Text;

                    idRuta = Convert.ToInt32(lstRuta.SelectedItems[0].SubItems[5].Text);

                    btnAgregar.Select();
                    btnAgregar.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRutaOrigen))
                {
                   
                    txtDestinatario.Select();
                    txtDestinatario.Focus();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }

   */
        }

        private void txtDireccionPartida_Enter(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionPartida_Leave(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.White;
        }

        private void txtDireccionDestino_Enter(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionDestino_Leave(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.White;
        }

        private void txtDireccionDestino_KeyPress(object sender, KeyPressEventArgs e)
        {

            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionDestino , ref  lstDireccionDestino, null, dtDireccionesRutaDestino))
                {
                    
                   
                    txtRuta.Select();
                    txtRuta.Focus();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (SecuenciaOrigen == 0 || SecuenciaFin == 0)
                {
                    MessageBox.Show("Secuencia no obtenida, seleccione la direccion correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToInt32(txtDireccionPartida.Tag) == 0 || Convert.ToInt32(txtDireccionPartida.Tag) == 0)
                {
                    MessageBox.Show("Ubigeo no existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int i = 0; i < dgvMaestro.Rows.Count; i++)
                {
                    if (txtCliente.Tag.ToString() == dgvMaestro.Rows[i].Cells["idCliente"].Value.ToString() && idRuta == Convert.ToInt32(dgvMaestro.Rows[i].Cells["idRuta"].Value) && txtDestinatario.Tag.ToString() == dgvMaestro.Rows[i].Cells["idDestinatario"].Value.ToString() && txtRemitente.Tag.ToString() == dgvMaestro.Rows[i].Cells["Remitente"].Value.ToString())
                    {
                        MessageBox.Show("Cliente, Remitente y Ruta ya estan registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (clsOperacionesBL.Instancia.ReportesaApp_Operaciones_MaestroClienteRuta_GuiaElectronica(txtCliente.Tag.ToString(), txtCliente.Text, txtRemitente.Tag.ToString() ,txtRemitente.Text, txtDestinatario.Tag.ToString(), txtDestinatario.Text, Convert.ToString(idRuta), txtRuta.Text, txtDireccionPartida.Text, txtDireccionDestino.Text, ubigeoPartida, ubigeoDestino, SecuenciaOrigen, SecuenciaFin, Utilitario.TipoOperacion.Registrar))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMaestro();
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

        private void txtDestinatario_Enter(object sender, EventArgs e)
        {
            txtDestinatario.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDestinatario_Leave(object sender, EventArgs e)
        {
            txtDestinatario.BackColor = Color.White;
        }

        private void txtDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {


                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDestinatario, ref  lstDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    
                    txtDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtDestinatario_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDestinatario, ref lstDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    txtDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDestinatario_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDestinatario, ref lstDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDestinatario, ref  lstDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    txtDireccionDestino.Text = "";
                    txtDireccionDestino.Tag = null;

                    dtDireccionesRutaDestino = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtCliente.Tag));
                    txtDireccionDestino.Select();
                    txtDireccionDestino.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDestinatario_KeyUp(object sender, KeyEventArgs e)
        {
                    
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDestinatario, ref lstDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscarRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            CargarMaestro();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsOperacionesBL.Instancia.ReportesaApp_Operaciones_MaestroClienteRuta_GuiaElectronica(dgvMaestro.CurrentRow.Cells["idCliente"].Value.ToString(), dgvMaestro.CurrentRow.Cells["Cliente"].Value.ToString(), dgvMaestro.CurrentRow.Cells["idRemitente"].Value.ToString(), dgvMaestro.CurrentRow.Cells["Remitente"].Value.ToString(), dgvMaestro.CurrentRow.Cells["idDestinatario"].Value.ToString(), dgvMaestro.CurrentRow.Cells["Destinatario"].Value.ToString(), dgvMaestro.CurrentRow.Cells["idRuta"].Value.ToString(), "", "", "", "", "", Convert.ToInt32(dgvMaestro.CurrentRow.Cells["SecuenciaPartida"].Value), Convert.ToInt32(dgvMaestro.CurrentRow.Cells["SecuenciaDestino"].Value), Utilitario.TipoOperacion.Anular))
               {
                   MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   CargarMaestro();
               }
               else
               {
                   MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
            catch (Exception ex )
            {
                 MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionPartida_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRutaOrigen);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRutaOrigen))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstDireccionPartida.SelectedItems[0];
                    SecuenciaOrigen = Convert.ToInt32(ItemActual.SubItems[2].Text);
                    ubigeoPartida = ItemActual.SubItems[3].Text;
                    direccionUbigeoPartida= ItemActual.SubItems[4].Text;

                    txtDestinatario.Select();
                    txtDestinatario.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

    
        }

        private void lstDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRutaOrigen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRutaOrigen))
                {
                    txtDestinatario.Select();
                    txtDestinatario.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestino))
                {
                    txtRuta.Select();
                    txtRuta.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRutaDestino))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstDireccionDestino.SelectedItems[0];
                    SecuenciaFin = Convert.ToInt32(ItemActual.SubItems[2].Text);
                    ubigeoDestino = ItemActual.SubItems[3].Text;
                    direccionUbigeoDestino = ItemActual.SubItems[4].Text;

                    txtRuta.Select();
                    txtRuta.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestino);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionDestino_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestino);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtRemitente_Enter(object sender, EventArgs e)
        {
            txtCliente.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRemitente_Leave(object sender, EventArgs e)
        {
            txtCliente.BackColor = Color.White;
        }

        private void txtRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
          /*  try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRemitente, ref  lstRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    txtDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRemitente, ref lstRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    txtDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRemitente_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRemitente, ref lstRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRemitente, ref  lstRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    txtDireccionPartida.Text = "";
                    txtDireccionPartida.Tag = null;

                    txtDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                    dtDireccionesRutaOrigen = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtCliente.Tag));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRemitente, ref lstRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscarRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CargarMaestro();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
