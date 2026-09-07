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

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmNuevoGastoReten : Form
    {
        public string esVALE;
        public frmNuevoGastoReten()
        {
            InitializeComponent();
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            try
            {


                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtNombrePersona, ref lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_BuscarPersonal);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtNombrePersona_Enter(object sender, EventArgs e)
        {
            txtNombrePersona.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtNombrePersona_Leave(object sender, EventArgs e)
        {
            txtNombrePersona.BackColor = Color.White;
        }

        private void txtNroPlantilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && e.KeyChar != (char)Keys.Enter)
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }

            if(e.KeyChar == (char)Keys.Enter){
                lstDestino.Select();
                gImporte.Select();
                txtImporte.Focus();
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Enter)
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {

                gDestino.Select();
                txtDestino.Focus();
            }
        }

        private void txtDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (esVALE == "NO")
                {
                    if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDestino, ref  lstDestino, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                    {

                        gObservacion.Select();
                        txtObservacion.Focus();

                    }
                }
                else
                {
                    lstDestino.Visible = false;

                    if (e.KeyChar == (char)Keys.Enter)
                    {

                        gObservacion.Select();
                        txtObservacion.Focus();
                    }

                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombrePersona_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtNombrePersona, ref lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_BuscarPersonal);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombrePersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtNombrePersona, ref  lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_BuscarPersonal))
                {

                    if (esVALE == "SI")
                    {
                        gImporte.Select();
                        txtImporte.Focus();
                    }
                    else
                    {
                        gNumeroPlantilla.Select();
                        txtNroPlantilla.Focus();
                    }
      

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtNombrePersona, ref  lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_BuscarPersonal))
                {
                    if (esVALE == "SI")
                    {
                        gImporte.Select();
                        txtImporte.Focus();
                    }
                    else
                    {
                        gNumeroPlantilla.Select();
                        txtNroPlantilla.Focus();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstPersona_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtNombrePersona, ref lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_BuscarPersonal);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (esVALE == "NO")
                {
                    if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDestino, ref  lstDestino, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                    {
                        gObservacion.Select();
                        txtObservacion.Focus();
                    }
                }
                else
                {
                    if (e.KeyChar == (char)Keys.Enter)
                    {
                        lstDestino.Visible = false;
                        gObservacion.Select();
                        txtObservacion.Focus();

                    }


                }


                




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDestino, ref lstDestino, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDestino, ref lstDestino, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(txtNombrePersona.Tag) == 0 || txtNombrePersona.Tag.ToString().Length == 0)
            {
                MessageBox.Show("Usted no a seleccionado Empleado, si no encuentra el empleado comunicarse con TI", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                                                                                    
            Boolean respuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_Registrar_Elimina_EditarGastoReten(0,txtNombrePersona.Text, Convert.ToInt32(txtNombrePersona.Tag), txtNroPlantilla.Text,Convert.ToDateTime(dtpNuevaFecha.Value), Convert.ToDecimal(txtImporte.Text), txtDestino.Text, txtObservacion.Text ,Utilitario.TipoOperacion.Registrar,esVALE);
            if (respuesta)
            {
      
                 MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 this.DialogResult = System.Windows.Forms.DialogResult.OK;
                 this.Close();
                
        
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDestino_Enter(object sender, EventArgs e)
        {
            txtDestino.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDestino_Leave(object sender, EventArgs e)
        {
            txtDestino.BackColor = Color.White;
        }

        private void txtNroPlantilla_Enter(object sender, EventArgs e)
        {
            txtNroPlantilla.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtNroPlantilla_Leave(object sender, EventArgs e)
        {
            txtNroPlantilla.BackColor = Color.White;
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            txtNombrePersona.BackColor = Color.White;
        }

        private void txtImporte_Enter(object sender, EventArgs e)
        {
            txtNroPlantilla.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtObservacion_Enter(object sender, EventArgs e)
        {
            txtObservacion.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtObservacion_Leave(object sender, EventArgs e)
        {
            txtObservacion.BackColor = Color.White;
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                btnGuardar.Focus();
               
            }
        }

        private void frmNuevoGastoReten_Load(object sender, EventArgs e)
        {
            if (esVALE == "SI")
            {
                
                gNumeroPlantilla.Visible = false;
                lstDestino.Visible = false;
                gDestino.Text = "Area";

            }


        }
    }
}
