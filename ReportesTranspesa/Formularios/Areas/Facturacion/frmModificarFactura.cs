using DevExpress.Utils;
using Negocio;
using ReportesTranspesa.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class frmModificarFactura : Form
    {

        string idviaje;
        string guia1;
        string _factura;
        string _compania;
        string _tipodocumento;
        string _CompaniaDescripcion;

        bool descenlace,ModificarMonto,Eliminar;
        public int Ver=0;

        public frmModificarFactura()
        {
            InitializeComponent();
        }

        private void tabModi_Click(object sender, EventArgs e)
        {

        }

        private void buscar()
        {

            GridCFactura1.DataSource = null;
            EnlazaryDesenlazar.Columns.Clear();
            EnlazaryDesenlazar.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetDESENLAZARRFACTURAS(_factura);
            if (dt.Rows.Count > 0)
            {
                GridCFactura1.DataSource = dt;
                EnlazaryDesenlazar.OptionsBehavior.Editable = false;
                EnlazaryDesenlazar.Columns["IDVIAJE"].Visible = false;
                /* EnlazaryDesenlazar.Columns[0].Width = 50;
                 EnlazaryDesenlazar.Columns["SituacionFacturado"].Width = 300;
                 EnlazaryDesenlazar.Columns["SerieDocumentoRelacion"].Width = 190;
                 EnlazaryDesenlazar.Columns["DocumentoRelacion"].Width = 190;
                 EnlazaryDesenlazar.Columns["TipoRelacion"].Width = 100;
                 EnlazaryDesenlazar.Columns["Codigo"].Width = 100;
                 EnlazaryDesenlazar.Columns["Guia1"].Width = 200;*/
                EnlazaryDesenlazar.BestFitColumns();
            }
            else
            {
                GridCFactura1.DataSource = null;
            }
        }
        private void buscarFechas()
        {

            GridCFactura2.DataSource = null;
            GridCFactura3.DataSource = null;
            gridView3.GroupSummary.Clear();
            gridView2.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetListarFacturasFechas(_compania, _factura, _tipodocumento);
            if (dt.Rows.Count > 0)
            {
                GridCFactura2.DataSource = dt;
                GridCFactura3.DataSource = dt;
                gridView3.OptionsBehavior.Editable = false;
                gridView2.Columns["MontoPendientePago"].Visible = false;
                gridView3.Columns["FechaVencimiento"].Visible = false;
                gridView3.Columns["FechaVencimientoOriginal"].Visible = false;
                gridView3.Columns["MontoPendientePago"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView3.Columns["MontoPendientePago"].DisplayFormat.FormatString = "F";

                /*EnlazaryDesenlazar.Columns[0].Width = 50;
                 EnlazaryDesenlazar.Columns["SituacionFacturado"].Width = 300;
                 EnlazaryDesenlazar.Columns["SerieDocumentoRelacion"].Width = 190;
                 EnlazaryDesenlazar.Columns["DocumentoRelacion"].Width = 190;
                 EnlazaryDesenlazar.Columns["TipoRelacion"].Width = 100;
                 EnlazaryDesenlazar.Columns["Codigo"].Width = 100;
                 EnlazaryDesenlazar.Columns["Guia1"].Width = 200;*/

                gridView2.BestFitColumns();
                gridView3.BestFitColumns();
            }

            else
            {
                GridCFactura2.DataSource = null;
            }
        }

        public void setearvariable(string factura, string compania, string CompaniaDescripcion, string tipodocumento)
        {
            _factura = factura;
            _compania = compania;
            _CompaniaDescripcion = CompaniaDescripcion;
            _tipodocumento = tipodocumento;

        }
        private void frmModificarFactura_Load(object sender, EventArgs e)
        {
            TxtCompania.Text = _CompaniaDescripcion;
            label2.Text = "Factura a Modificar:" + _factura;
            txtTipoDocumento.Text = _tipodocumento;
            txtCompSocio.Text = _CompaniaDescripcion;
            txtTipDoc.Text = _tipodocumento;
            txtTipoDocR.Text = _tipodocumento; //"FC";
            txtSituacionF.Text = "FC";
            buscarFechas();
            buscar();

            //BUSCAMOS LOS ACCESOS DE LAS FACTURAS
            DataTable dtVerAccesosXfactura = new DataTable();
            dtVerAccesosXfactura = clsFinanzasBL.Instancia.GetAccesosxFacturas(2,_factura, _compania, _tipodocumento,Utilitario.Instancia.SesionUsuario.usuario);
            if (dtVerAccesosXfactura.Rows.Count > 0)
            {
                for (int i = 0; i < dtVerAccesosXfactura.Rows.Count; i++)
                {
                  descenlace =  Convert.ToBoolean(dtVerAccesosXfactura.Rows[i]["AccesoDescenlace"].ToString());
                  ModificarMonto =  Convert.ToBoolean(dtVerAccesosXfactura.Rows[i]["AccesoModifcarMontoNeto"].ToString());
                  Eliminar = Convert.ToBoolean(dtVerAccesosXfactura.Rows[i]["AccesoEliminarFactura"].ToString());
                }
            }

            //ACTIVAMOS LOS BOTONES SEGUN EL ACCESO
            if (descenlace == true)
            {
                btnQuitar.Enabled = true;
            }
            if (ModificarMonto == true)
            {
                btnAceptar.Enabled = true;
            }
            if (Eliminar == true)
            {
                btnEliminar.Enabled = true;
               // btnRestablecerCO.Enabled = true;
            }

            if (Ver == 1)
            {
                btnEnlazar.Enabled = false;
                btnBuscar.Enabled = false;
                btnQuitar.Enabled = false;
                btnGuardar.Enabled = false;
                btnAceptar.Enabled = false;
                btnEliminar.Enabled = false;
                btnRestablecerCO.Enabled = false;
            }
        }

        private void EnlaFac_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        ////////// OPCION  1 DESENLAZAR //////////////
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            
            int[] filass = EnlazaryDesenlazar.GetSelectedRows();
            string datoseleccionado = EnlazaryDesenlazar.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                idviaje = EnlazaryDesenlazar.GetRowCellValue(filass[i], "IDVIAJE").ToString();
                guia1 = EnlazaryDesenlazar.GetRowCellValue(filass[i], "GUIA TRANSPORTE").ToString();
            }
            if (MessageBox.Show("Esta seguro de desenlazar la factura?", "Desenlazar Factura", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string input = "";
                 if (ShowInputDialogBox(ref input, "Agregar Comentario", "INGRESAR OBSERVACION", 300, 200) == DialogResult.OK)
                 {
                     string rpta;
                     DataTable dt = new DataTable();
                     dt = clsFinanzasBL.Instancia.GetModificarFactura(1, _factura, idviaje, guia1, "0", DateTime.Now.ToString(), DateTime.Now.ToString(), 0, 
                                                                        _compania, _tipodocumento,input,"","","","",0);
                     rpta = Convert.ToString(dt.Rows[0]["exito"]);
                     string NrRPTA = rpta.Substring(0, 1);
                     if (NrRPTA == "0")
                     {
                         MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     }
                     else
                     {
                         MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
            }
            buscar();
        }

        //////////OPCION 2 ENLAZAR /////////////////

        private void button1_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            string Respuesta;

            //SI LA GUIA1 ACTUAL ES VACIO REEMPLAZA POR 0
            if (txtGuia1.Text.Length == 0)
            {
                MessageBox.Show("Ingresar  Guia", "Mensaje");
                txtGuia1.Focus();
                return;
            }

            if (txtCodViaje.Text.Length == 0)
            {
                MessageBox.Show("Ingresar  codigo viaje", "Mensaje");
                txtCodViaje.Focus();
                return;
            }

            dt = clsFinanzasBL.Instancia.GetModificarFactura(2, _factura, "0", txtGuia1.Text, txtCodViaje.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), 
                                                            0, "0", "0","","","","","",0);


            Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buscar();
                buscarFechas();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ///OPCION 3 MODIFICAR FECHA/////////////////
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            string Respuesta;
            dt = clsFinanzasBL.Instancia.GetModificarFactura(3, _factura, "0", "0", "0", dtpFechaDocumento.Text, dtpFVencimientoOr.Text, 0, _compania,
                                                            _tipodocumento,"","","","","",0);
            Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buscarFechas();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //////////OPCION 4 PERMITE MODIFICAR MONTO PENDIENTE PAGO///////////
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea guardar Cambios?", "Crear Cambios", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dt = new DataTable();
                string Respuesta;
                decimal IdentificarMontoPendientePago;
                //SI MONTO ACTUAL ES VACIO REEMPLAZA POR 0
                if (txtCompSocio.Text.Length == 0)
                {

                    IdentificarMontoPendientePago = 0;
                }

                // SI EL MONTO INGRESADO ES VACIO AVISAR CON MENSAJE
                if (txtMonPenPago.Text.Length == 0)
                {
                    MessageBox.Show("Ingresar un monto a modificar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMonPenPago.Focus();

                }

                dt = clsFinanzasBL.Instancia.GetModificarFactura(4, _factura, "0", "0", "0", DateTime.Now.ToString(), DateTime.Now.ToString(),
                                                                    Convert.ToDecimal(txtMonPenPago.Text), _compania, _tipodocumento, "", "", "", "", "", Convert.ToDecimal(txtMontoDetraccion.Text));
                Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh automativo
                    buscarFechas();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }

        }

        //////////OPCION 5 PERMITE ELIMINAR FACTURAS///////////
        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Esta seguro de eliminar la factura?", "Eliminar Factura", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string input = "";
                if (ShowInputDialogBox(ref input, "Agregar Comentario", "INGRESAR OBSERVACION", 300, 200) == DialogResult.OK)
                {
                    string rpta;
                    DataTable dt = new DataTable();
                    dt = clsFinanzasBL.Instancia.GetModificarFactura(5, _factura, "0", "0", "0", DateTime.Now.ToString(), DateTime.Now.ToString(), 0, _compania,
                                                                    _tipodocumento, input, "", "", "","",0);
                    rpta = Convert.ToString(dt.Rows[0]["exito"]);
                    string NrRPTA = rpta.Substring(0, 1);
                    if (NrRPTA == "0")
                    {
                        MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        ///////////////////// OPCION 6 PERMITE RESTABLECER EL CORRELATIVO ///////////////////// /////////////////////
        private void button1_Click_1(object sender, EventArgs e)
        {
            string rpta;
            DataTable dt = new DataTable(); //CREA EL ALIAS dt  
            dt = clsFinanzasBL.Instancia.GetModificarFactura(6, _factura, "0", "0", "0", DateTime.Now.ToString(), DateTime.Now.ToString(), 0, _compania,
                                                                _tipodocumento, "", "", "", "", "",0); //Aqui se mostraran las variables
            rpta = Convert.ToString(dt.Rows[0]["exito"]);
            string NrRPTA = rpta.Substring(0, 1);//INGRESAMOS LA VARIABLE  NrPTA y lo colocamos un substring que toma la respuesta desde la posicion 0 
            if (NrRPTA == "0")//Colocamos una  condicional  
            {
                txtCorrelativo.Text = rpta.Substring(27, 5);//hace un subtring que toma desde la posicion 27  y tomara 5 digitos de adelante
                MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);// muestra el mensaje operacion exitosa
                txtCorrelativo.Focus();

            }
            else
            {
                MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//MUESTRA EL MENSAJE  ERROR
            }
            buscar();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void txtCodViaje_KeyPress(object sender, KeyPressEventArgs e)
        {
          
       }

        private static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Anular Viajes", int width = 100, int height = 200)
        {
            Size size = new Size(width, height);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Height = 150;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.Text = title;

            //Create a new label to hold the prompt
            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(5, 5);
            label.Width = size.Width - 10;
            label.Margin = new System.Windows.Forms.Padding(3, 25, 2, 35);
            inputBox.Controls.Add(label);

            //Create a textbox to accept the user's input
            TextBox textBox = new TextBox();
            textBox.Size = new Size(260, 23);
            textBox.Location = new Point(20, label.Location.Y + 20);
            textBox.Text = input.ToUpper();
            textBox.Location = new Point(20, 40);
            inputBox.Controls.Add(textBox);

            //Create an OK Button 
            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new Point(size.Width - 80 - 80, 100 - 30);
            inputBox.Controls.Add(okButton);

            //Create a Cancel Button
            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new Point(size.Width - 80, 100 - 30);
            inputBox.Controls.Add(cancelButton);

            //Set the input box's buttons to the created OK and Cancel Buttons respectively so the window appropriately behaves with the button clicks
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            //Show the window dialog box 
            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            string obser = textBox.Text;
            return result;
        }

        private void txtDirpartida_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {           
            if (MessageBox.Show("Esta seguro de moficar la Factura?", "Ubigeo Factura", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string rpta;
                DataTable dt = new DataTable();
                dt = clsFinanzasBL.Instancia.GetModificarFactura(7, _factura, "0", "0", "0", DateTime.Now.ToString(), DateTime.Now.ToString(), 0, _compania, _tipodocumento, "",
                                                                txtPartida.Text,txtLLegada.Text,txtDirpartida.Text,txtDireLlegada.Text,0);
                rpta = Convert.ToString(dt.Rows[0]["exito"]);
                string NrRPTA = rpta.Substring(0, 1);
                if (NrRPTA == "0")
                {
                    MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de liberar la Factura para enviar al portal?", "Liberar Factura", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string rpta;
                DataTable dt = new DataTable();
                dt = clsFinanzasBL.Instancia.GetModificarFactura(8, _factura, "0", "0", "0", DateTime.Now.ToString(), DateTime.Now.ToString(), 0, _compania, _tipodocumento, "",
                                                                txtPartida.Text, txtLLegada.Text, txtDirpartida.Text, txtDireLlegada.Text,0);
                rpta = Convert.ToString(dt.Rows[0]["exito"]);
                string NrRPTA = rpta.Substring(0, 1);
                if (NrRPTA == "0")
                {
                    MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void txtMonPenPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
               // e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
               // e.Handled = true;
            }
        }
    
    }
}







