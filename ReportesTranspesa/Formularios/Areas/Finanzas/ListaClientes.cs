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
    public partial class ListaClientes : MetroFramework.Forms.MetroForm
    {
        public ListaClientes()
        {
            InitializeComponent();
        }

        public static string cliente;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cliente = "";
            cliente = txtCliente.Text.Trim();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetClientes(cliente);
            if (dt.Rows.Count > 0)
            {
                dtgvListaClientes.DataSource = dt;
                dtgvViewListaClientes.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void dtgvListaClientes_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = dtgvViewListaClientes.GetDataRow(dtgvViewListaClientes.GetSelectedRows()[0]);
            cliente = row["Clientes"].ToString();
            DetalleCobranzas frm = new DetalleCobranzas();
            frm.txtClientes.Text = cliente.Trim();
            frm.Show();
            this.Close();
        }
    }
}
