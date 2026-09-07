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
    public partial class FrmListarPreviajesLibres : Form
    {
        DataTable dtTipoProg;
        public string NuevoNroTicket = string.Empty;
        public int idTipoProgramacion = -1;
        public string Respuesta = string.Empty;
        public FrmListarPreviajesLibres()
        {
            InitializeComponent();
        }

        private void FrmListarPreviajesLibrescs_Load(object sender, EventArgs e)
        {
            dtTipoProg = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOperaciones(Utilitario.Instancia.SesionUsuario.usuario);
            //dtSucursal = clsOperacionesBL.Instancia.GetOperaciones_ListarSucursalPreviajes(Utilitario.Instancia.SesionUsuario.usuario);

            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
            cbxOperacion.DataSource = dtTipoProg;
            cbxOperacion.SelectedValue = idTipoProgramacion;
            chkFecha.Checked = true;
            g_fecha.Enabled = true;
            btnBuscar.PerformClick();

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFecha.Checked)
            {
                g_fecha.Enabled = true;
            }
            else
            {
                g_fecha.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(chkFecha.Checked)
                {
                     DataTable dt = clsOperacionesBL.Instancia.ReportesApp_BuscarProgramacionPreviajeLibre(Convert.ToInt32(cbxOperacion.SelectedValue) , dtpFechaInicio.Text,dtpFechaFin.Text,txtPreviaje.Text);
                     dtgListaGuiasTransportista.DataSource = dt;
                     dgvListaGuiaTraspExpressVista.Columns["IdProgramacion"].Visible = false;
                     dgvListaGuiaTraspExpressVista.Columns["Anio"].Visible = false;
                }
                else
                {
                    DataTable dt = clsOperacionesBL.Instancia.ReportesApp_BuscarProgramacionPreviajeLibre(Convert.ToInt32(cbxOperacion.SelectedValue), "", "", txtPreviaje.Text);
                    dtgListaGuiasTransportista.DataSource = dt; 
                }
            
               
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show( ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListaGuiaTraspExpressVista_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void dgvListaGuiaTraspExpressVista_DoubleClick(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Se Asignara Previaje a la guia Seleccionada y Generará Viaje, ¿Desea Continuar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if (cbxOperacion.Text == "LIMAGAS")
                {
                    Respuesta = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Peso", "Peso Limagas");
                }
                else
                {
                    Respuesta = "1.000";
                }

                int numericValue;
                bool esNumero = int.TryParse(Respuesta, out numericValue);

                if (esNumero == false)
                {
                    MessageBox.Show("Dato ingresado no es numero.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                    NuevoNroTicket = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion"));
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                


            }
            catch (Exception ex)
            {
                
                MessageBox.Show( ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vincularYGenerarViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Se Asignara Previaje a la guia Seleccionada y Generará Viaje, ¿Desea Continuar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                NuevoNroTicket = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicket"));
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
