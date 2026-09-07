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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class Operaciones : Form
    {
        int VerClientes=1;

        Boolean OPSelec = false;
        Boolean OTEntradaSelec = false;
        Boolean OTSalidaSelec = false;


        public Operaciones()
        {
            InitializeComponent();
        }

        private void Operaciones_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }
        private void CargarClientes()
        {
           
            DataTable dtClientes = new DataTable();

            dtClientes = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(1, 0, 0, 0,"");
            if (dtClientes.Rows.Count > 0)
            {
                dgvClientesAlmacen.DataSource = dtClientes;
                dgvClientesAlmacen.AutoResizeColumns();
                dgvClientesAlmacen.Columns["COD"].Width = 40;
                dgvClientesAlmacen.Columns["CLIENTE"].Width = 200;
                dgvClientesAlmacen.Columns["CTRs"].Width = 40;
                //dgvClientesAlmacen.Columns["FECHA INICIO"].DisplayFormat.FormatString = "g";
                //dgvClientesAlmacen.Columns["FECHA FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                //dgvClientesAlmacen.Columns["FECHA FIN"].DisplayFormat.FormatString = "g";
                //dgvClientesAlmacen.Columns["PESO PUERTO"].DisplayFormat.FormatType = FormatType.Numeric;
                //dgvClientesAlmacen.Columns["PESO PUERTO"].DisplayFormat.FormatString = "n2";
                //dgvClientesAlmacen.Columns["CANTIDAD INGRESO"].DisplayFormat.FormatType = FormatType.Numeric;
                //dgvClientesAlmacen.Columns["CANTIDAD INGRESO"].DisplayFormat.FormatString = "n2";
                //dgvClientesAlmacen.Columns["DIFERENCIA"].DisplayFormat.FormatType = FormatType.Numeric;
                //dgvClientesAlmacen.Columns["DIFERENCIA"].DisplayFormat.FormatString = "n2";
                //CalcularTotales(false);
                
            }
            else
            {
                //CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void dgvClientesAlmacen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvClientesAlmacen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                    
            DataTable dtContratos = new DataTable();

            int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString()); //dgvClientesAlmacen.Rows[e.RowIndex].Cells["COD"].Value; //dgvClientesAlmacen.Rows[e.RowIndex].Cells["COD"].Value.ToString();

            label3.Text = "OPERACIONES     > " + dgvClientesAlmacen.CurrentRow.Cells["CLIENTE"].Value.ToString();

            button1.Enabled = true;

            OPSelec = false;

            CargarOperaciones(IDCliente);

            dtContratos = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(2, IDCliente, 0, 0,"");
            if (dtContratos.Rows.Count > 0)
            {
                dgvContratos.DataSource = dtContratos;
                dgvContratos.AutoResizeColumns();
                //dgvClientesAlmacen.Columns["COD"].Width = 60;
                //dgvClientesAlmacen.Columns["CLIENTE"].Width = 200;
                //dgvClientesAlmacen.Columns["CTRs"].Width = 60;
                //dgvClientesAlmacen.Columns["FECHA INICIO"].DisplayFormat.FormatString = "g";
                //dgvClientesAlmacen.Columns["FECHA FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                //dgvClientesAlmacen.Columns["FECHA FIN"].DisplayFormat.FormatString = "g";
                //dgvClientesAlmacen.Columns["PESO PUERTO"].DisplayFormat.FormatType = FormatType.Numeric;
                //dgvClientesAlmacen.Columns["PESO PUERTO"].DisplayFormat.FormatString = "n2";
                //dgvClientesAlmacen.Columns["CANTIDAD INGRESO"].DisplayFormat.FormatType = FormatType.Numeric;
                //dgvClientesAlmacen.Columns["CANTIDAD INGRESO"].DisplayFormat.FormatString = "n2";
                //dgvClientesAlmacen.Columns["DIFERENCIA"].DisplayFormat.FormatType = FormatType.Numeric;
                //dgvClientesAlmacen.Columns["DIFERENCIA"].DisplayFormat.FormatString = "n2";
                //CalcularTotales(false);

            }
            else
            {
                //CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void dgvContratos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            splitContainer5.Panel1Collapsed = false;
            txtNuevoClienteOperacion.Tag = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
            txtNuevoClienteOperacion.Text = dgvClientesAlmacen.CurrentRow.Cells["CLIENTE"].Value.ToString();
            label8.Text = "NUEVA OPERACION: " + dgvClientesAlmacen.CurrentRow.Cells["CLIENTE"].Value.ToString();
            txtDescripcionOperacion.Text = "";
            txtMotonave.Text = "";
            txtDescripcionOperacion.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            splitContainer5.Panel1Collapsed = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string usuario = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            if(txtNuevoClienteOperacion.Tag == "0")
            {
                MessageBox.Show("Debe seleccionar antes a un cliente, cancele y vuelva intentar", "Aviso");
                return;
            }
   
            dtRespuesta = clsAlmacenBL.Instancia.GetData_Operaciones_GrabarOperacion(Convert.ToInt32(txtNuevoClienteOperacion.Tag), txtDescripcionOperacion.Text,cmbTipoOperacion.SelectedIndex + 1,txtMotonave.Text, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

            MessageBox.Show(Respuesta);

            splitContainer5.Panel1Collapsed = true;

            int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());

            CargarOperaciones(IDCliente);
        }
        private void CargarOperaciones(int IDCliente)
        {
            DataTable dtOperaciones = new DataTable();
            dtOperaciones = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(100, IDCliente, 0, 0,"");

            dgvOperacionesAlmacen.DataSource = null;
            dgvContratos.DataSource = null;
            dgvOTEntrada.DataSource = null;
            dgvOTSalida.DataSource = null;

            if (dtOperaciones.Rows.Count > 0)
            {
                dgvOperacionesAlmacen.DataSource = dtOperaciones;
                dgvOperacionesAlmacen.AutoResizeColumns();

                dgvOperacionesAlmacen.Columns[0].Visible = false;
                dgvOperacionesAlmacen.Columns[1].Visible = false;
                dgvOperacionesAlmacen.Columns[3].Visible = false;
                dgvOperacionesAlmacen.Columns["TIPO_OPERACION"].Width = 80;
                dgvOperacionesAlmacen.Columns["DESCRIPCION"].Width = 120;
                dgvOperacionesAlmacen.Columns["ESTADO"].Width = 70;

            }
            else
            {
                //CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                //m.ShowDialog();
            }
        }

        private void dgvContratos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtOTEntrada = new DataTable();
            int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
            int IDContrato = Convert.ToInt32(dgvContratos.CurrentRow.Cells["CONTRATO"].Value.ToString());

            dtOTEntrada = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(3, IDCliente, IDContrato, 0,"");

            OTSalidaSelec = false;

            dgvOTEntrada.DataSource = null;

            if (dtOTEntrada.Rows.Count > 0)
            {
                dgvOTEntrada.DataSource = dtOTEntrada;
                dgvOTEntrada.AutoResizeColumns();
                dgvOTEntrada.Columns[0].Visible = false;
                dgvOTEntrada.Columns[1].Visible = false;
                dgvOTEntrada.Columns["OT"].Width = 70;
                dgvOTEntrada.Columns["PERIODO"].Width = 60;
                dgvOTEntrada.Columns["FECHA_OT"].Width = 60;
                toolblCantOTEntrada.Text = "OT Encontradas: " + dtOTEntrada.Rows.Count.ToString();
            }
            else
            {
                //CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                //m.ShowDialog();
            }

           

            DataTable dtOTSalida = new DataTable();
           
            dtOTSalida = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(4, IDCliente, IDContrato, 0,"");

            dgvOTSalida.DataSource = null;

            if (dtOTSalida.Rows.Count > 0)
            {
                dgvOTSalida.DataSource = dtOTSalida;
                dgvOTSalida.AutoResizeColumns();
                dgvOTSalida.Columns[0].Visible = false;
                dgvOTSalida.Columns[1].Visible = false;
                dgvOTSalida.Columns["OT"].Width = 70;
                dgvOTSalida.Columns["PERIODO"].Width = 60;
                dgvOTSalida.Columns["FECHA_OT"].Width = 60;
                toolblCantOTSalida.Text = "OT Encontradas: " + dtOTSalida.Rows.Count.ToString();

            }
            else
            {
                //CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                //m.ShowDialog();
            }
        }

        private void dgvOTEntrada_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtOTEntradaInfo = new DataTable();

            OTEntradaSelec = true;

            int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
            int IDContrato = Convert.ToInt32(dgvContratos.CurrentRow.Cells["CONTRATO"].Value.ToString());
            int IDOT = Convert.ToInt32(dgvOTEntrada.CurrentRow.Cells["IDOT"].Value.ToString());
            string OT = dgvOTEntrada.CurrentRow.Cells["OT"].Value.ToString();

            dtOTEntradaInfo = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(5, IDCliente, IDContrato, IDOT,"");

            if (dtOTEntradaInfo.Rows.Count > 0)
            {
                lblOTEntradaSelec.Text = OT + " PESO TOTAL:" + dtOTEntradaInfo.Rows[0]["PESO"].ToString() + ", PESO PUERTO: " + dtOTEntradaInfo.Rows[0]["PESOPUERTO"].ToString() + ",  DIF.: " + dtOTEntradaInfo.Rows[0]["DIFERENCIA"].ToString();
            }
            else
            {
                //CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                //m.ShowDialog();
            }
        }

        private void dgvOTEntrada_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (VerClientes==1)
            {
                splitContainer2.Panel1Collapsed = true;
                button5.Text = "Ver Clientes >>";
                button5.Size = new Size(80, button5.Size.Height);
                VerClientes = 0;
            }
            else
            {
                splitContainer2.Panel1Collapsed = false;
                button5.Text = "<<";
                button5.Size = new Size(27, button5.Size.Height);
                VerClientes = 1;
            }
        }

        private void dgvOTEntrada_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOTSalida_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOTSalida_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtOTSalidaInfo = new DataTable();

            int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
            int IDContrato = Convert.ToInt32(dgvContratos.CurrentRow.Cells["CONTRATO"].Value.ToString());
            int IDOT = Convert.ToInt32(dgvOTSalida.CurrentRow.Cells["IDOT"].Value.ToString());
            string OT = dgvOTSalida.CurrentRow.Cells["OT"].Value.ToString();

            textBox1.Text = dgvOTSalida.CurrentRow.Cells["RetiroTN"].Value.ToString();

            OTSalidaSelec = true;

            dtOTSalidaInfo = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(6, IDCliente, IDContrato, IDOT,"");

            if (dtOTSalidaInfo.Rows.Count > 0)
            {
                lblOTSalidaSelec.Text = OT + " >  PESO TOTAL:" + dtOTSalidaInfo.Rows[0]["PESO"].ToString() + ", PESO PUERTO: " + dtOTSalidaInfo.Rows[0]["PESOPUERTO"].ToString() + ", DIFERENCIA: " + dtOTSalidaInfo.Rows[0]["DIFERENCIA"].ToString(); ;
            }
            else
            {
                //CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                //m.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (OTEntradaSelec == false)
            {
                MessageBox.Show("Debes seleccionar una OT Entrada", "Atención");
                return;
            }
            if (OPSelec == false)
            {
                MessageBox.Show("Debes seleccionar una Operacion", "Atención");
                return;
            }

            int AnioOP = Convert.ToInt32(dgvOperacionesAlmacen.CurrentRow.Cells["AnioOP"].Value.ToString());
            int NroOP = Convert.ToInt32(dgvOperacionesAlmacen.CurrentRow.Cells["NroOP"].Value.ToString());
            int IDOT = Convert.ToInt32(dgvOTEntrada.CurrentRow.Cells["IDOT"].Value.ToString());
            int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
            string OP = dgvOperacionesAlmacen.CurrentRow.Cells["OP"].Value.ToString();
            string OT = dgvOTEntrada.CurrentRow.Cells["OT"].Value.ToString();

            if (MessageBox.Show("Seguro de Enlazar la OT: " + OT + " a la Operacion: "+ OP, "Enlazar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string usuario = Utilitario.Instancia.SesionUsuario.usuario;
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsAlmacenBL.Instancia.GetData_Operaciones_GrabarEnlace_OP_OT(AnioOP, NroOP, IDOT, IDCliente,0, usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);
         
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (OTSalidaSelec==false)
            {
                MessageBox.Show("Debes seleccionar una OT Salida", "Atención");
                return;
            }
            if (OPSelec == false)
            {
                MessageBox.Show("Debes seleccionar una Operacion", "Atención");
                return;
            }
            if (Convert.ToDecimal(textBox1.Text.ToString())<=0)
            {
                MessageBox.Show("Debe colocar un valor a la OT Retiro", "Atención");
                return;
            }

            int AnioOP = Convert.ToInt32(dgvOperacionesAlmacen.CurrentRow.Cells["AnioOP"].Value.ToString());
            int NroOP = Convert.ToInt32(dgvOperacionesAlmacen.CurrentRow.Cells["NroOP"].Value.ToString());
            int IDOT = Convert.ToInt32(dgvOTSalida.CurrentRow.Cells["IDOT"].Value.ToString());
            int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
            string OP = dgvOperacionesAlmacen.CurrentRow.Cells["OP"].Value.ToString();
            string OT = dgvOTSalida.CurrentRow.Cells["OT"].Value.ToString();

            if (MessageBox.Show("Seguro de Enlazar la OT: " + OT + " a la Operacion: " + OP, "Enlazar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string usuario = Utilitario.Instancia.SesionUsuario.usuario;
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsAlmacenBL.Instancia.GetData_Operaciones_GrabarEnlace_OP_OT(AnioOP, NroOP, IDOT, IDCliente,Convert.ToDecimal (textBox1.Text.ToString()), usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                DataTable dtOTSalida = new DataTable();

                int IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
                int IDContrato = Convert.ToInt32(dgvContratos.CurrentRow.Cells["CONTRATO"].Value.ToString());
                string TextoBusca = textBox2.Text.ToString();

                if (cbxFiltro.SelectedIndex == 0)
                {
                    dtOTSalida = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(7, IDCliente, IDContrato, 0, TextoBusca);
                }
                else if (cbxFiltro.SelectedIndex == 1)
                {
                    dtOTSalida = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(8, IDCliente, IDContrato, 0, TextoBusca);
                }
                else if (cbxFiltro.SelectedIndex == 2)
                {
                    dtOTSalida = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(9, IDCliente, IDContrato, 0, TextoBusca);
                }
                else if (cbxFiltro.SelectedIndex == 3)
                {
                    dtOTSalida = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(10, IDCliente, IDContrato, 0, TextoBusca);
                }
                else if (cbxFiltro.SelectedIndex == 4)
                {
                    dtOTSalida = clsAlmacenBL.Instancia.GetData_Operaciones_Consultas(11, IDCliente, IDContrato, 0, TextoBusca);
                }

                OTSalidaSelec = false;
               
                dgvOTSalida.DataSource = null;

                if (dtOTSalida.Rows.Count > 0)
                {
                    dgvOTSalida.DataSource = dtOTSalida;
                    dgvOTSalida.AutoResizeColumns();
                    dgvOTSalida.Columns[0].Visible = false;
                    dgvOTSalida.Columns[1].Visible = false;
                    dgvOTSalida.Columns["OT"].Width = 70;
                    dgvOTSalida.Columns["PERIODO"].Width = 60;
                    dgvOTSalida.Columns["FECHA_OT"].Width = 60;
                    toolblCantOTSalida.Text = "CANT. OT: " + dtOTSalida.Rows.Count.ToString();

                }
                else
                {
                    //CalcularTotales(true);
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hubo resultados";
                    //m.ShowDialog();
                }
            }

        }

        private void dgvOTEntrada_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOTSalida_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
           (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dgvOperacionesAlmacen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOperacionesAlmacen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OPSelec = true;
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (OPSelec == true)
            {
                Reportes.ReporteXOperacion frmReporteOP = new Reportes.ReporteXOperacion();
                frmReporteOP.IDCliente = Convert.ToInt32(dgvClientesAlmacen.CurrentRow.Cells["COD"].Value.ToString());
                frmReporteOP.AnioOP = Convert.ToInt32(dgvOperacionesAlmacen.CurrentRow.Cells["AnioOP"].Value.ToString());
                frmReporteOP.NroOP = Convert.ToInt32(dgvOperacionesAlmacen.CurrentRow.Cells["NroOP"].Value.ToString());
                frmReporteOP.OP = dgvOperacionesAlmacen.CurrentRow.Cells["OP"].Value.ToString();
                frmReporteOP.NombreCliente = dgvClientesAlmacen.CurrentRow.Cells["CLIENTE"].Value.ToString();
                frmReporteOP.Show();
            }
            else
            {
                MessageBox.Show("Debe primero seleccinar una operaciones", "Aviso");
                return;
            }
        }

        private void btnAmpliarOTE_Click(object sender, EventArgs e)
        {
            splitContainer6.Panel2Collapsed = true;
            btnReducirOTE.Visible = true;
            btnAmpliarOTE.Visible = false;
        }

        private void btnReducirOTE_Click(object sender, EventArgs e)
        {
            splitContainer6.Panel2Collapsed = false;
            btnReducirOTE.Visible = false;
            btnAmpliarOTE.Visible = true;
        }

        private void btnAmpliarOTS_Click(object sender, EventArgs e)
        {
            splitContainer6.Panel1Collapsed = true;
            btnAmpliarOTS.Visible = false;
            btnReducirOTS.Visible = true;
        }

        private void btnReducirOTS_Click(object sender, EventArgs e)
        {
            splitContainer6.Panel1Collapsed = false;
            btnAmpliarOTS.Visible = true;
            btnReducirOTS.Visible = false;
        }

       

    }
}
