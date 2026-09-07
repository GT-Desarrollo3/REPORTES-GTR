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

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class ListaBancos : MetroFramework.Forms.MetroForm
    {
        public ListaBancos()
        {
            InitializeComponent();
        }

        public static string banco;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string banco = "";
            banco = txtBanco.Text.Trim();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetBancos(banco);
            if (dt.Rows.Count > 0)
            {
                dtgvListaBancos.DataSource = dt;
                dtgvViewListaBancos.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void dtgvListaBancos_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = dtgvViewListaBancos.GetDataRow(dtgvViewListaBancos.GetSelectedRows()[0]);
            banco = row["DescripcionCorta"].ToString();
            DetalleCobranzas frm = new DetalleCobranzas();
            frm.rbBancos.Checked = true;
            frm.txtClientes.Text = banco.Trim();
            frm.Show();
            this.Close();
        }
    }
}
