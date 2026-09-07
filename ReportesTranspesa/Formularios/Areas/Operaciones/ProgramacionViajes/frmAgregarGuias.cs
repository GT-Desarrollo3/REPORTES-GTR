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
using ReportesTranspesa.Sistema;
using System.Globalization;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmAgregarGuias : Form
    {
        int _codProgramacion;
        int _Accion;
        string _anio;
        public int idCliente;
        public int tipoprogramacion;
        public int ot;
        public string DireccionPartida;
        public string DireccionDestino;
     
        public frmAgregarGuias()
        {
            InitializeComponent();
        }
        public void envioviaje(int var_codProgramacion, int Accion,string anio)
        {
            _codProgramacion = var_codProgramacion;
            _Accion = Accion;
            _anio = anio;

        }
        private void frmAgregarGuias_Load(object sender, EventArgs e)
        {
            try
            {
                txtPesoCarga.Text = "1";
                txtPesoCliente.Text = "1";
                txtPesoCombustible.Text = "1";

                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                cbxDireccionPartida.Visible = false;
                cbxDireccionDestino.Visible = false;
                txtOT.Visible = false;

                dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;

                if (_Accion == 2)
                {
                    gridControl1.Visible = true;

                    gridControl1.Size = new System.Drawing.Size(315, 262);

                    DataTable DT = new DataTable();
                    DT = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarConductores();

                    if (DT.Rows.Count > 0)
                    {
                        gridControl1.DataSource = DT;
                        gridControl1.Focus();
                    }
                    gridView1.Columns["IdConductor"].Visible = false;
                    gridView1.OptionsBehavior.Editable = false;

                    //listado de los consolidados 
                    DataTable dtComboConsolidados = new DataTable();
                    dtComboConsolidados = clsOperacionesBL.Instancia.GetOperaciones_ListarPorCliente(0, Convert.ToInt32(_anio), Convert.ToInt32(_codProgramacion));
                    comboBox1.DisplayMember = "DATOS";
                    comboBox1.ValueMember = "IdProducto";
                    comboBox1.DataSource = dtComboConsolidados;
                }

                if (tipoprogramacion == 2 || tipoprogramacion == 4 || tipoprogramacion == 10)
                {
                    label9.Visible = true;
                    label10.Visible = true;
                    cbxDireccionPartida.Visible = true;
                    cbxDireccionDestino.Visible = true;
                    txtOT.Text = ot.ToString();
                    label11.Visible = true;
                    txtOT.Visible = true;
                    txtOT.ReadOnly = true;
                    comboBox1.Visible = false;
                    label8.Visible = false;
                    pCargaCliente.Visible = false;
                    pCargaCliente.SendToBack();
                    pCargaDescarga.Visible = true;
                    pCargaDescarga.BringToFront();

                    DataTable dtDireccionesPartida = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarDireccionesCliente(idCliente);
                    DataTable dtDireccionesCliente = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarDireccionesCliente(idCliente);

                    cbxDireccionPartida.DisplayMember = "Direccion";
                    cbxDireccionPartida.ValueMember = "Secuencia";
                    cbxDireccionPartida.DataSource = dtDireccionesPartida;
                    cbxDireccionPartida.SelectedIndex = 0;

                    cbxDireccionDestino.DisplayMember = "Direccion";
                    cbxDireccionDestino.ValueMember = "Secuencia";
                    cbxDireccionDestino.DataSource = dtDireccionesCliente;
                    cbxDireccionDestino.SelectedIndex = 0;

                    label3.Text = "Guias Otros";
                }

                if (tipoprogramacion == 3)
                {
                    pCargaCliente.Visible = true;
                    pCargaCliente.BringToFront(); 
                    pCargaDescarga.Visible = false;
                    pCargaDescarga.SendToBack();
                }

                if (tipoprogramacion == 1 || tipoprogramacion == 11 || tipoprogramacion == 13)
                {
                    pCargaCliente.Visible = true;
                    pCargaCliente.BringToFront();
                    pCargaDescarga.Visible = false;
                    pCargaDescarga.SendToBack();

                    label11.Visible = true;
                    txtOT.Visible = true;
                    txtOT.ReadOnly = true;
                    txtOT.Text = ot.ToString();
                    comboBox1.Visible = false;
                    label8.Visible = false;

                    label9.Visible = true;
                    label10.Visible = true;
                    cbxDireccionPartida.Visible = true;
                    cbxDireccionDestino.Visible = true;
                }

                chkRetorno.Checked = false;
                chkTercero_CheckedChanged(sender, e);
            }
            catch (Exception ex) { MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTransportista2.Text.Length > 0)
            {
                if (!txtTransportista2.Text.Contains("-"))
                {
                    MessageBox.Show("Formato de guia incorrecto, ejemplo (011-10545)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtTransportista2.Text.Length <= 4)
                {
                    MessageBox.Show("Formato de guia incorrecto, ejemplo (011-10545)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

                DataTable dtResp = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaGuias(_codProgramacion, textBox1.Text, textBox2.Text, textBox3.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                   _anio, ot, tipoprogramacion, Convert.ToInt32(cbxDireccionPartida.SelectedValue), Convert.ToInt32(cbxDireccionDestino.SelectedValue), Convert.ToDecimal(txtPesoCarga.Text),
                                   Convert.ToDecimal(txtPesoCliente.Text), Convert.ToDecimal(txtPesoCombustible.Text), txtTransportista2.Text, chkRetorno.Checked);

                string Rpta = Convert.ToString(dtResp.Rows[0]["exito"]);
                string NrRPTA = Rpta.Substring(0, 1);

                if (NrRPTA == "0")
                {
                    MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                    f1.Val_Respuesta = "1";
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int[] filas = gridView1.GetSelectedRows();
                string datoseleccionado = gridView1.GetFocusedValue().ToString();

                for (int i = 0; i < filas.Length; i++)
                {
                    string OTselec = gridView1.GetRowCellValue(filas[i], "IdConductor").ToString();
                    string nombre = gridView1.GetRowCellValue(filas[i], "Conductor").ToString();

                    if (MessageBox.Show("¿Desea agregar el conductor "+ nombre+" a la programación?", "AGREGAR CONDUCTOR APOYO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRptaViaje = new DataTable();
                        string Rpta;
                        dtRptaViaje = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(8, _codProgramacion, 0, OTselec,_anio);
                        Rpta = Convert.ToString(dtRptaViaje.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);
                       
                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                            f1.Val_Respuesta = "1";
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                        f1.Val_Respuesta = "0";
                        this.Close();
                    }
                }
            }
            catch (Exception) { }
        }

        private void button2_Click(object sender, EventArgs e) { this.Close(); }

        private void chkTercero_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetorno.Checked == true)
            {
                label4.Text = "P. Descarga (KG): ";
                cbxDireccionPartida.Text = DireccionDestino;
                cbxDireccionDestino.Text = DireccionPartida;
            }

            if (chkRetorno.Checked == false)
            {
                label4.Text = "P. Carga (KG): ";
                cbxDireccionPartida.Text = DireccionPartida;
                cbxDireccionDestino.Text = DireccionDestino;
            }
        }
    }
}





