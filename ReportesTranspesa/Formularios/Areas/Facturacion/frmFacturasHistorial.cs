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

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class frmFacturasHistorial : Form
    {
        public frmFacturasHistorial()
        {
            InitializeComponent();
        }

        private void frmFacturasHistorial_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dtHistoricoFac = new DataTable();
            dtHistoricoFac = clsFinanzasBL.Instancia.GetListarHistoricoFacturas(dateTimePicker1.Text +" 00:00:00.00",dateTimePicker2.Text+" 23:59:59.999");

            if (dtHistoricoFac.Rows.Count > 0)
            {

                gridControl1.DataSource = dtHistoricoFac;
            }
            else 
            {
                gridControl1.DataSource = null;
            }
        }
    }
}
