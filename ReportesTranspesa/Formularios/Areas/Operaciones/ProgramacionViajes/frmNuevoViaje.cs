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
namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmNuevoViaje : Form
    {
        int _Viaje, _codProgramacion;
        string _anio;

        int idcliente;
        int idruta;
        int idproducto;
   
        public frmNuevoViaje()
        {
            InitializeComponent();
        }
        public void envioviaje(int var_Viaje, int var_codProgramacion,string anio)
        {            
            _Viaje = var_Viaje;
            _codProgramacion = var_codProgramacion;
            _anio = anio;

        }
        private void lblCodViaje_Click(object sender, EventArgs e)
        {
        }

        private void txtOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                if (!string.IsNullOrEmpty(txtOT.Text))
                {
                    int otbuscar;
                    otbuscar = int.Parse(txtOT.Text);
                  //  DataTable dt = new DataTable();
                    //DataTable dt1 = new DataTable();
                    DataTable datosOT = new DataTable();

                    datosOT = clsOperacionesBL.Instancia.GetOperaciones_ListarDatosOts(otbuscar);

                    if (datosOT.Rows.Count > 0)
                    {
                        for (int i = 0; i < datosOT.Rows.Count; i++)
                        {
                            txtCliente.Text = datosOT.Rows[i]["BUSQUEDA"].ToString();
                            idcliente = Convert.ToInt32(datosOT.Rows[i]["IdClienteFacturacion"].ToString());
                            idruta = Convert.ToInt32(datosOT.Rows[i]["IdRuta"].ToString());
                            idproducto = Convert.ToInt32(datosOT.Rows[i]["Producto"].ToString());
                            txtProducto.Text = datosOT.Rows[i]["Nombre"].ToString();
                            txtRuta.Text = datosOT.Rows[i]["DESCRIPCION"].ToString();
                            lbMedida.Text = datosOT.Rows[i]["UMUso"].ToString();
                        }
                    }
                    else {
                        MessageBox.Show("La OT no existe..!");
                    }
                    /*dt = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(otbuscar);
                    dt1 = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(otbuscar);
                    
                    cboPartida.DisplayMember = "Direccion";
                    cboPartida.ValueMember = "Secuencia";
                    cboPartida.DataSource = dt;

                    cboLlegada.DisplayMember = "Direccion";
                    cboLlegada.ValueMember = "Secuencia";
                    cboLlegada.DataSource = dt1;*/

                    
                }
            }
        }

        private void frmNuevoViaje_Load(object sender, EventArgs e)
        {
            txtViaje.Text = Convert.ToString(_Viaje);
        }

        private void btnAgregarOt_Click(object sender, EventArgs e)
        {
            if (txtOT.Text.Equals(""))
            {
                MessageBox.Show("Ingresar una OT", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtOT.Focus();
            }
            else if (txtCantidad.Text.Equals(""))
            {
                MessageBox.Show("Ingresar cantidad", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCantidad.Focus();
            }
            else
            {
                decimal Peso;
                 if (txtCantidad.Text.Equals(""))
                {
                    Peso = 1;
                }
                else
                {
                    Peso = Convert.ToDecimal(txtCantidad.Text);
                }
               //  string IDPARTIDA = cboPartida.SelectedValue.ToString();
                // string IDLLEGADA = cboLlegada.SelectedValue.ToString();

                DataTable dtRptaViaje = new DataTable();
                DataTable dtConsolidado = new DataTable();
                string Rpta;

                string Rpta1;
                dtConsolidado = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_GenerarConsolidados(Convert.ToInt32(txtViaje.Text), _codProgramacion, Convert.ToInt32(txtOT.Text),
                                                                                                  1, 1, Utilitario.Instancia.SesionUsuario.usuario, Peso, "", "", "", idproducto, idcliente, lbMedida.Text, Convert.ToInt32(_anio));
                Rpta1 = Convert.ToString(dtConsolidado.Rows[0]["exito"]);
                string NrRPTA1 = Rpta1.Substring(0, 1);

               
               

                if (NrRPTA1 == "0")
                {

                    txtOT.Text = "";
                    txtCliente.Text = "";
                    txtProducto.Text = "";
                    txtRuta.Text = "";
                    txtCantidad.Text = "";

                   /* dtRptaViaje = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_GuardarConsolidados(Convert.ToInt32(txtViaje.Text), _codProgramacion, Convert.ToInt32(txtOT.Text), idruta,
                                                                                                          idproducto, idcliente, lbMedida.Text, Utilitario.Instancia.SesionUsuario.usuario, Peso,_anio);

                    Rpta = Convert.ToString(dtRptaViaje.Rows[0]["exito"]);*/

                   /* string NrRPTA = Rpta.Substring(0, 1);
                    if (NrRPTA == "0")
                    {
                        MessageBox.Show(Rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOT.Text = "";
                        txtCliente.Text = "";
                        txtProducto.Text = "";
                        txtRuta.Text = "";
                        txtCantidad.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(Rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }*/
                }
                else
                {
                    MessageBox.Show(Rpta1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                 

                }
            }
        }

        private void txtOT_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
