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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmEditarGuias : Form
    {
        public string guiaregistrada ="";
        public int idticket = 0, OpcionBusca = 0, ID, importado,Ot;
        string Ticket, Compania, Placa, Carreta, Ruc, DniConductor, Serie, Numero, Gr, CodProgram,
            FhTicket, UNM, Observacion;
        decimal PesoPuerto,CantidadBase;

        public frmEditarGuias()
        {
            InitializeComponent();
        }

        private void frmEditarGuias_Load(object sender, EventArgs e)
        {
            if (!guiaregistrada.Equals(""))
            {
                txtGuiaregistrada.Text = guiaregistrada;
                BuscarGuia(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuscarGuia();
        }

        private void BuscarGuia()
        {
            if (OpcionBusca ==1)
            {
                if (txtGuiaregistrada.Text.Length > 0)
                {
                    guiaregistrada = txtGuiaregistrada.Text;
                }
                if (txtId.Text.Length > 0)
                {
                    idticket = Convert.ToInt32(txtId.Text);
                }            
            }
            
           
            DataTable dtBuscaGuia = new DataTable();
            dtBuscaGuia = clsOperacionesBL.Instancia.GetDataBuscarGuiasModificar(0,guiaregistrada, idticket, Utilitario.Instancia.SesionUsuario.usuario);

            if(dtBuscaGuia.Rows.Count>0)
            {
              
                    for (int i = 0; i < dtBuscaGuia.Rows.Count; i++)
                    {
                        // txtGuiaregistrada.Text = dtBuscaGuia.Rows[i]["GuiaRegistrada"].ToString();				  
                        txtId.Text = dtBuscaGuia.Rows[i]["ID"].ToString();
                        txtcompanianumero.Text = dtBuscaGuia.Rows[i]["Compania"].ToString();
                        cboCompania.Text = dtBuscaGuia.Rows[i]["Descripcion"].ToString();
                        txtPlaca.Text = dtBuscaGuia.Rows[i]["Placa"].ToString();
                        txtCarreta.Text = dtBuscaGuia.Rows[i]["Carreta"].ToString();
                        txtCliente.Text = dtBuscaGuia.Rows[i]["Cliente"].ToString();
                        txtRuc.Text = dtBuscaGuia.Rows[i]["RucCliente"].ToString();
                        txtConductor.Text = dtBuscaGuia.Rows[i]["Conductor"].ToString();
                        txtDni.Text = dtBuscaGuia.Rows[i]["DNIConductor"].ToString();
                        txtImportado.Text = dtBuscaGuia.Rows[i]["IMPORTADO"].ToString();
                        txtSerie.Text = dtBuscaGuia.Rows[i]["Serie"].ToString();
                        txtNumero.Text = dtBuscaGuia.Rows[i]["Numero"].ToString();
                        txtGuiaRemision.Text = dtBuscaGuia.Rows[i]["GUIAREMISION"].ToString();
                        txtCodigoPrograma.Text = dtBuscaGuia.Rows[i]["CODPROG"].ToString();
                        dtFechaGuia.Text = dtBuscaGuia.Rows[i]["Fecha"].ToString();
                        txtTicket.Text = dtBuscaGuia.Rows[i]["Ticket"].ToString();
                        TxtPesoPuerto.Text = dtBuscaGuia.Rows[i]["PesoPuerto"].ToString();
                        txtUniMedida.Text = dtBuscaGuia.Rows[i]["UMBase"].ToString();
                        TxtCantidadBase.Text = dtBuscaGuia.Rows[i]["CantidadBase"].ToString();
                        txtObservacion.Text = dtBuscaGuia.Rows[i]["OBSERVACION"].ToString();
                        txtOt.Text = dtBuscaGuia.Rows[i]["OT"].ToString();
                        txtTipo.Text = dtBuscaGuia.Rows[i]["Tipo"].ToString();

                    }
         

            }
            else 
            {
                MessageBox.Show("La guia no existe o esta mal ingresada", "Error");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            ID = Convert.ToInt32(txtId.Text);
            guiaregistrada = txtGuiaregistrada.Text;
            Compania = txtcompanianumero.Text;
            Placa = txtPlaca.Text;
            Carreta = txtCarreta.Text;
            Ruc = txtRuc.Text;
            DniConductor = txtDni.Text; 
            if(txtImportado.Text.Equals("EXCEL"))
            {
                importado = 1; 
            }else
            {
                importado = 1;
            }
            Serie = txtSerie.Text;
            Numero = txtNumero.Text;
            Gr = txtGuiaRemision.Text;
            CodProgram = txtCodigoPrograma.Text;
            Ticket = txtTicket.Text;
            FhTicket = dtFechaGuia.Text;
            PesoPuerto = Convert.ToDecimal(TxtPesoPuerto.Text);
            UNM = txtUniMedida.Text;
            CantidadBase = Convert.ToDecimal(TxtCantidadBase.Text);
            Observacion = txtObservacion.Text;
            Ot = Convert.ToInt32(txtOt.Text);


            string rpta = "";
            DataTable dtGuardarGuias = new DataTable();
            dtGuardarGuias = clsOperacionesBL.GetDataGuardarGuia(ID,guiaregistrada, Compania, Placa, Carreta, Ruc, DniConductor, importado, Serie, Numero, Gr, CodProgram,Ticket,
                                                                FhTicket, PesoPuerto,UNM, CantidadBase, Observacion, Ot, Utilitario.Instancia.SesionUsuario.usuario);
            rpta = Convert.ToString(dtGuardarGuias.Rows[0]["exito"]);
            string NroRPTA = rpta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                DataTable ValidaDatoCliente = new DataTable();
                ValidaDatoCliente = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_ValidardatoGuias(txtRuc.Text,"CL");

                if (ValidaDatoCliente.Rows.Count > 0)
                {
                    for(int i=0;i<ValidaDatoCliente.Rows.Count;i++)
                    {
                        txtCliente.Text = ValidaDatoCliente.Rows[0]["DATOCORRECTO"].ToString();
                    }                    
                }
                else
                {
                    MessageBox.Show("El RUC ingresado es incorrecto o no esta registrado.");
                    txtCliente.Text = "";
                    txtRuc.Focus();
                }

            }

            if (e.KeyChar == (char)Keys.Escape)
            {                
                txtRuc.Focus();
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                DataTable ValidaDatosGuia = new DataTable();
                ValidaDatosGuia = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_ValidardatoGuias(txtDni.Text, "CD");

                if (ValidaDatosGuia.Rows.Count > 0)
                {
                    for (int i = 0; i < ValidaDatosGuia.Rows.Count; i++)
                    {
                        txtConductor.Text = ValidaDatosGuia.Rows[0]["DATOCORRECTO"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("El DNI ingresado es incorrecto o no esta registrado.");
                    txtConductor.Text = "";
                    txtDni.Focus();
                }

            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                txtDni.Focus();
            }
        }

        private void btnBuscarTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionBusca == 1)
                {
                    if (txtBuscarTicket.Text.Length > 0)
                    {
                        guiaregistrada = txtBuscarTicket.Text;
                    }
                    if (txtId.Text.Length > 0)
                    {
                        idticket = Convert.ToInt32(txtId.Text);
                    }
                }


                DataTable dtBuscaGuia = new DataTable();
                dtBuscaGuia = clsOperacionesBL.Instancia.GetDataBuscarGuiasModificar(1,guiaregistrada, idticket, Utilitario.Instancia.SesionUsuario.usuario);

                if (dtBuscaGuia.Rows.Count > 0)
                {

                    for (int i = 0; i < dtBuscaGuia.Rows.Count; i++)
                    {
                        // txtGuiaregistrada.Text = dtBuscaGuia.Rows[i]["GuiaRegistrada"].ToString();				  
                        txtId.Text = dtBuscaGuia.Rows[i]["ID"].ToString();
                        txtcompanianumero.Text = dtBuscaGuia.Rows[i]["Compania"].ToString();
                        cboCompania.Text = dtBuscaGuia.Rows[i]["Descripcion"].ToString();
                        txtPlaca.Text = dtBuscaGuia.Rows[i]["Placa"].ToString();
                        txtCarreta.Text = dtBuscaGuia.Rows[i]["Carreta"].ToString();
                        txtCliente.Text = dtBuscaGuia.Rows[i]["Cliente"].ToString();
                        txtRuc.Text = dtBuscaGuia.Rows[i]["RucCliente"].ToString();
                        txtConductor.Text = dtBuscaGuia.Rows[i]["Conductor"].ToString();
                        txtDni.Text = dtBuscaGuia.Rows[i]["DNIConductor"].ToString();
                        txtImportado.Text = dtBuscaGuia.Rows[i]["IMPORTADO"].ToString();
                        txtSerie.Text = dtBuscaGuia.Rows[i]["Serie"].ToString();
                        txtNumero.Text = dtBuscaGuia.Rows[i]["Numero"].ToString();
                        txtGuiaRemision.Text = dtBuscaGuia.Rows[i]["GUIAREMISION"].ToString();
                        txtCodigoPrograma.Text = dtBuscaGuia.Rows[i]["CODPROG"].ToString();
                        dtFechaGuia.Text = dtBuscaGuia.Rows[i]["Fecha"].ToString();
                        txtTicket.Text = dtBuscaGuia.Rows[i]["Ticket"].ToString();
                        TxtPesoPuerto.Text = dtBuscaGuia.Rows[i]["PesoPuerto"].ToString();
                        txtUniMedida.Text = dtBuscaGuia.Rows[i]["UMBase"].ToString();
                        TxtCantidadBase.Text = dtBuscaGuia.Rows[i]["CantidadBase"].ToString();
                        txtObservacion.Text = dtBuscaGuia.Rows[i]["OBSERVACION"].ToString();
                        txtOt.Text = dtBuscaGuia.Rows[i]["OT"].ToString();
                        txtTipo.Text = dtBuscaGuia.Rows[i]["Tipo"].ToString();

                    }


                }
                else
                {
                    MessageBox.Show("La guia no existe o esta mal ingresada", "Error");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
