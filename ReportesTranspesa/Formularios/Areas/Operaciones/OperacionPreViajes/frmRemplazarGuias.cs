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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmRemplazarGuias : Form
    {

        public int idViaje = 0;
        public string Viaje = "";
        public string Serie = "";
        public string Numero = "";

      

        public frmRemplazarGuias()
        {
            InitializeComponent();
        }

        private void frmRemplazarGuias_Load(object sender, EventArgs e)
        {
            try
            {
                txtViaje.Text = Viaje;
                ListarGuias();
                ListarDirecciones();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ListarDirecciones()
        {
            cbxDireccionPartida.Visible = true;
            cbxDireccionDestino.Visible = true;


            DataTable dtDireccionesPartida = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarDireccionesCliente(Convert.ToInt32(dgvGuiasVista.GetRowCellValue(dgvGuiasVista.FocusedRowHandle, "idCliente")));
            DataTable dtDireccionesCliente = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarDireccionesCliente(Convert.ToInt32(dgvGuiasVista.GetRowCellValue(dgvGuiasVista.FocusedRowHandle, "idCliente")));

            cbxDireccionPartida.DisplayMember = "Direccion";
            cbxDireccionPartida.ValueMember = "Secuencia";
            cbxDireccionPartida.DataSource = dtDireccionesPartida;
            cbxDireccionPartida.SelectedIndex = 0;


            cbxDireccionDestino.DisplayMember = "Direccion";
            cbxDireccionDestino.ValueMember = "Secuencia";
            cbxDireccionDestino.DataSource = dtDireccionesCliente;
            cbxDireccionDestino.SelectedIndex = 0;
        }

        private void ListarGuias()
        {
            try
            {
                DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarGuiasViaje(idViaje,Serie,Numero);
                if (dt.Rows.Count > 0)
                {
                    dtgGuias.DataSource = dt;
                    dgvGuiasVista.Columns["IdGuia"].Visible = false;
                    dgvGuiasVista.Columns["IdViaje"].Visible = false;
                    dgvGuiasVista.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRemplazar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxDireccionPartida.Items.Count > 0)
                {
      

                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ReemplzarGuias(dgvGuiasVista.GetRowCellValue(dgvGuiasVista.FocusedRowHandle, "IdGuia").ToString(), dgvGuiasVista.GetRowCellValue(dgvGuiasVista.FocusedRowHandle, "Serie").ToString(), dgvGuiasVista.GetRowCellValue(dgvGuiasVista.FocusedRowHandle, "Numero").ToString(), txtSerie.Text, txtNumero.Text, idViaje, txtRemitente.Text, txtGuiaOtros.Text, dgvGuiasVista.GetRowCellValue(dgvGuiasVista.FocusedRowHandle, "IdOT").ToString(), Convert.ToInt32(cbxDireccionPartida.SelectedValue), Convert.ToInt32(cbxDireccionDestino.SelectedValue), Convert.ToInt32(dgvGuiasVista.GetRowCellValue(dgvGuiasVista.FocusedRowHandle, "LineaOT"))))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos en las direcciones", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

        
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtNumero.TextLength > 2)
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConsultarGuiaExiste(txtSerie.Text, txtNumero.Text))
                    {
                        txtRemitente.ReadOnly = false;
                        txtGuiaOtros.ReadOnly = false;
                        btnRemplazar.Enabled = true;
                    }
                    else
                    {
                        btnRemplazar.Enabled = false;
                        txtRemitente.ReadOnly = true;
                        txtGuiaOtros.ReadOnly = true;
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
