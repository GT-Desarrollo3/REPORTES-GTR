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
    public partial class frmGuiasElectronicas_HsitorialEstadoSUNAT : Form
    {
        public DataTable dt;
        public frmGuiasElectronicas_HsitorialEstadoSUNAT()
        {
            InitializeComponent();
        }

        private void frmGuiasElectronicas_HsitorialEstadoSUNAT_Load(object sender, EventArgs e)
        {
            try
            {
                if (dt != null)
                {
                    dtgLista.DataSource = dt;
                    dgvListaExpressVista.Columns["idEmpresaGrupo"].Visible = false;
                    dgvListaExpressVista.Columns["idCliente"].Visible = false;
                    dgvListaExpressVista.Columns["idOT"].Visible = false;
                    dgvListaExpressVista.Columns["idGuiaElectronica"].Visible = false;
                    dgvListaExpressVista.Columns["CodigoMensaje"].Visible = false;
                    dgvListaExpressVista.Columns["idRespuestaSunat"].Visible = false;
                    dgvListaExpressVista.BestFitColumns();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
