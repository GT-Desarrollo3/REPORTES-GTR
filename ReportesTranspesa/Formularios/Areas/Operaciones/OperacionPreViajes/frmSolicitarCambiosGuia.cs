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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmSolicitarCambiosGuia : Form
    {
        DataTable dtConductor;
        public int TipoOperacion = -1;
        public string SerieGuia = "";
        public string NumeroGuia = "";
       
        public frmSolicitarCambiosGuia()
        {
            InitializeComponent();
        }


        private void btnBuscarGuia_Click(object sender, EventArgs e)
        {
            
            ListarGuiasElectronicas();
        }



        private void ListarGuiasElectronicas()
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable dtLista = clsOperacionesBL.Instancia.ReportesApp_Operaciones_BuscarGuiaIndividual("T", cbxSerieGuia.Text, txtNumeroGuia.Text);
            if (dtLista != null)
            {
                if (dtLista.Rows.Count > 0)
                {
                    
                    dtConductor = Utilitario.Instancia.ConvertirXMLaDatatable(dtLista.Rows[0]["xml_conductores"].ToString());
                    if (dtConductor.Rows.Count > 0)
                    {
                        txtConductor.Tag = dtConductor.Rows[0]["idConductor"].ToString();
                        txtConductor.Text = dtConductor.Rows[0]["Nombres_Conductor"].ToString() + " "+ dtConductor.Rows[0]["Apellidos_Conductor"].ToString();
                        txtTarjetaCirculacion.Text = dtConductor.Rows[0]["PlacaTarjetaCircula"].ToString();
                    }

                    SerieGuia = dtLista.Rows[0]["SerieGuia"].ToString();
                    NumeroGuia = dtLista.Rows[0]["NumeroGuia"].ToString();
                    txtGuia.Text = dtLista.Rows[0]["SerieGuia"].ToString() + "-" +dtLista.Rows[0]["NumeroGuia"].ToString();
                    txtPlaca.Tag = dtLista.Rows[0]["IdVehiculo"].ToString();
                    txtPlaca.Text = dtLista.Rows[0]["NumeroPlaca"].ToString();
                    txtCarreta.Tag = dtLista.Rows[0]["idCarreta"].ToString();
                    txtCarreta.Text = dtLista.Rows[0]["Carreta"].ToString();

                 
                    
           
                }
                else
                {
                   SerieGuia = "";
                   NumeroGuia = "";
                   txtGuia.Text = "";
                   txtPlaca.Tag = null;
                   txtPlaca.Text = "";
                   txtCarreta.Tag = null;
                   txtCarreta.Text = "";
                   txtConductor.Tag = null;
                   txtConductor.Text = "";
                   txtTarjetaCirculacion.Text = "";
                }
                this.Cursor = Cursors.Default;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtNumeroGuia_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbxSerieGuia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmSolicitarCambiosGuia_Load(object sender, EventArgs e)
        {
            CargarSeries();

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                p_solicitud.Enabled = true;
                p_MoficiarGuia.Enabled = false;
            }
            if (TipoOperacion == Utilitario.TipoOperacion.Editar)
            {
                p_solicitud.Enabled = false;
                p_MoficiarGuia.Enabled = true;
                if (cbxCampo.Text == "Tracto")
                {
                    txtConductor.Enabled = false;
                    txtTarjetaCirculacion.Enabled = false;
                    txtCarreta.Enabled = false;
                }
                if (cbxCampo.Text == "Carreta")
                {
                    txtConductor.Enabled = false;
                    txtTarjetaCirculacion.Enabled = false;
                    txtPlaca.Enabled = false;
                }
                if (cbxCampo.Text == "Conductor")
                {
                    txtPlaca.Enabled = false;
                    txtTarjetaCirculacion.Enabled = false;
                    txtCarreta.Enabled = false;
                }
                if (cbxCampo.Text == "Tarjeta Circulacion")
                {
                    txtPlaca.Enabled = false;
                    txtCarreta.Enabled = false;
                    txtConductor.Enabled = false;
                }

                cbxSerieGuia.Text = SerieGuia;
                btnBuscarGuia.PerformClick();

            }
 
        }


        private void CargarSeries()
        {
            DataTable dtSerieGuia = clsOperacionesBL.Instancia.ReportesApp_Listar_SerieGuiasElectronicas("T");


            if (dtSerieGuia.Rows.Count > 0)
            {
                cbxSerieGuia.DataSource = dtSerieGuia;
                cbxSerieGuia.DisplayMember = "SerieGuia";
                cbxSerieGuia.ValueMember = "SerieGuia";
                cbxSerieGuia.SelectedIndex = 0;

            }

        }

        private void txtTracto_Enter(object sender, EventArgs e)
        {
            txtPlaca.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtTracto_Leave(object sender, EventArgs e)
        {
            txtConductor.BackColor = Color.White;
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    
                    txtConductor.Focus();


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstPlaca.SelectedItems[0];
                    txtPlaca.Text = ItemActual.SubItems[1].Text.TrimEnd();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtConductor_Enter(object sender, EventArgs e)
        {
            txtConductor.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtConductor_Leave(object sender, EventArgs e)
        {
            txtConductor.BackColor = Color.White;
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    txtCarreta.Focus();
                   
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstConductor.SelectedItems[0];
                

                    if (txtConductor.Tag != null)
                    {
                        txtConductor.Text = ItemActual.SubItems[1].Text;
                    }
                    else
                    {
                        txtConductor.Text = "";
                    }


        
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstCarreta.SelectedItems[0];
                    txtCarreta.Text = ItemActual.SubItems[1].Text;
                   

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstCarreta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCarreta_Enter(object sender, EventArgs e)
        {
            txtCarreta.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtCarreta_Leave(object sender, EventArgs e)
        {
            txtCarreta.BackColor = Color.White;
        }

        private void btnSolicitarCambio_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxCampo.Text == "")
                {
                    MessageBox.Show("Nombre del campo a modificar no puede ser vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (cbxCampo.Text == "")
                {
                    MessageBox.Show("Nombre del campo a modificar no puede ser vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtNuevoValor.Text == "")
                {
                    MessageBox.Show("El nuevo valor no puede ser vacio.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtMotivoSolicitud.Text == "")
                {
                    MessageBox.Show("El motivo de solicitud no puede ser vacio.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string NroSolicitud = "";
                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_Registrar_SolicitudCambios_GuiaElectronica(ref NroSolicitud, SerieGuia, NumeroGuia, cbxCampo.Text, txtNuevoValor.Text, txtMotivoSolicitud.Text))
                {
                    txtNroSolicitud.Text = NroSolicitud;
                    MessageBox.Show(Utilitario.Instancia.Advertencia+ " Numero de Solicitud: "+ txtNroSolicitud.Text, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxSerieGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPlaca.Text == "")
                {
                    MessageBox.Show("Placa no puede estar vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtPlaca.Tag == null)
                {
                    MessageBox.Show("Placa no seleccionada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtConductor.Text == "")
                {
                    MessageBox.Show("Conductor no puede ser vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtConductor.Tag == null)
                {
                    MessageBox.Show("Conductor no seleccionado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtCarreta.Text == "")
                {
                    MessageBox.Show("Carreta no puede ser vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtCarreta.Tag == null)
                {
                    MessageBox.Show("Carreta no seleccionada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtTarjetaCirculacion.Text == "")
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ActualizarDatosGuia(txtNroSolicitud.Text,Convert.ToInt32(txtConductor.Tag),txtConductor.Text))
                {

                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
