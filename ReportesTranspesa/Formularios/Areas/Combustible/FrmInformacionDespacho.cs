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

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class FrmInformacionDespacho : Form
    {
        public string Placa;
        public DateTime FechaProgramacion;
        public FrmInformacionDespacho()
        {
            InitializeComponent();
        }

        private void FrmInformacionDespacho_Load(object sender, EventArgs e)
        {
            txtPlaca.Text = Placa;
            dtpFechaIni.Value = FechaProgramacion;
            dtpFechaFin.Value = FechaProgramacion;

            ListarProgramacionesPorPlaca();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarProgramacionesPorPlaca();
        }

        public void ListarProgramacionesPorPlaca()
        {
            DataTable dt;

            dt = clsCombustibleBL.Instancia.GetDataProgramacionPorPlaca(txtPlaca.Text, dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString());

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvProgramacion.DataSource = dt;
            }
            
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
