using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmBuscaPreviajes : Form
    {
        public DataTable dtProgramacionViaje;
        public int NroViaje;
        public string Ruta;
        public string Placa;

        public FrmBuscaPreviajes()
        {
            InitializeComponent();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBuscaPreviajes_Load(object sender, EventArgs e)
        {
            CargarPreviajes();
        }

        private void CargarPreviajes()
        {

            if (dtProgramacionViaje != null || dtProgramacionViaje.Rows.Count > 0)
            {
                dgvViajes.DataSource = dtProgramacionViaje;

                if (dgvViajes.Rows.Count > 0)
                {
                    dgvViajes.Columns["CODIGOVIAJE"].Width = 100;
                    dgvViajes.Columns["ESTADO"].Width = 90;
                    dgvViajes.Columns["FECHAVIAJE"].Width = 130;
                    dgvViajes.Columns["PLACA"].Width = 80;

                    dgvViajes.Rows[0].Selected = true;
                    dgvViajes.CurrentCell = dgvViajes.Rows[0].Cells[1];


                }
                else
                {
                    dgvViajes.DataSource = null;

                }


            }
            else
            {
                dgvViajes.DataSource = null;

            }
        }

        private void dgvViajes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dgvViajes.CurrentRow.Selected)
            {

                NroViaje = Convert.ToInt32(dgvViajes.CurrentRow.Cells["CODIGOVIAJE"].Value.ToString());
                Ruta =   dgvViajes.CurrentRow.Cells["RUTA"].Value.ToString();
                Placa = dgvViajes.CurrentRow.Cells["PLACA"].Value.ToString();
                this.Close();
            }

        }


    }
}
