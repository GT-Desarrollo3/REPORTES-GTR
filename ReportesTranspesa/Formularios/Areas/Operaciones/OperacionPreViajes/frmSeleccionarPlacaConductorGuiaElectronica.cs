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


namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmSeleccionarPlacaConductorGuiaElectronica : Form
    {
        public string placaTercero = string.Empty;
        public string tarjetaCirculacion = string.Empty;
        public string nombretipovehiculo = string.Empty;
        public int idtipovehiculo = 0;

        public int idConductor = 0;
        public string nombres = string.Empty;
        public string apellidos = string.Empty;
        public string nombresCompletos = string.Empty;
        public string licencia = string.Empty;
        public string codigoDocumento = string.Empty;
        public string tipoDocumento = string.Empty;
        public string documento = string.Empty;

        

        public frmSeleccionarPlacaConductorGuiaElectronica()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmSeleccionarPlacaConductorGuiaElectronica_Load(object sender, EventArgs e)
        {
            try
            {
                ListarPlacasTercero();
                CargarConductoresTerceros();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void CargarConductoresTerceros()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarConductoresTercerosGuiaElectronica(-1);
            if (dt.Rows.Count > 0)
            {
                dgvConductores.DataSource = dt;
                dgvConductores.Columns["idCliente"].Visible = false;
                dgvConductores.Columns["CodTipoDocIdentidad_Conductor"].Visible = false;
                dgvConductores.Columns["Nombres"].Visible = false;
                dgvConductores.Columns["Apellidos"].Visible = false;
                dgvConductores.ClearSelection();
            }
            else
            {
                dgvConductores.DataSource = null;
            }

        }
        private void ListarPlacasTercero()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarPlacasTercero_porCliente(Convert.ToInt32(txtxEmpresaCliente.Tag));

            if (dt.Rows.Count > 0)
            {
                dgvPlacas.DataSource = dt;
                dgvPlacas.Columns["idCliente"].Visible = false;
                dgvPlacas.Columns["TipoVehiculo"].Visible = false;
                dgvPlacas.ClearSelection();
            }
            else
            { 
                dgvPlacas.DataSource = null; 
            }

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlacas.CurrentRow == null || dgvConductores.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una placa y conductor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                placaTercero = dgvPlacas.CurrentRow.Cells["Placa"].Value.ToString();
                tarjetaCirculacion = dgvPlacas.CurrentRow.Cells["TarjetaCirculacion"].Value.ToString();
                idtipovehiculo = Convert.ToInt32(dgvPlacas.CurrentRow.Cells["TipoVehiculo"].Value.ToString());
                nombretipovehiculo = dgvPlacas.CurrentRow.Cells["NombreTipoVehiculo"].Value.ToString();

                idConductor = 0; Convert.ToInt32(dgvConductores.CurrentRow.Cells["idCliente"].Value);
                nombres = dgvConductores.CurrentRow.Cells["Nombres"].Value.ToString();
                apellidos = dgvConductores.CurrentRow.Cells["Apellidos"].Value.ToString();
                nombresCompletos = dgvConductores.CurrentRow.Cells["NombresCompleto"].Value.ToString();
                licencia = dgvConductores.CurrentRow.Cells["Licencia_Conducir"].Value.ToString();
                codigoDocumento = dgvConductores.CurrentRow.Cells["CodTipoDocIdentidad_Conductor"].Value.ToString(); 
                tipoDocumento = dgvConductores.CurrentRow.Cells["TipoDocIdentidad_Conductor"].Value.ToString();  
                documento = dgvConductores.CurrentRow.Cells["NumeroDocIdentidad_Conductor"].Value.ToString();


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
