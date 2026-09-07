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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class ListaProveedores : MetroFramework.Forms.MetroForm
    {
        public ListaProveedores()
        {
            InitializeComponent();
        }

        private void ListaProveedores_Load(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            DataTable dt = new DataTable();

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dt1 = new DataTable();

            dt = clsFinanzasBL.Instancia.GetListaProveedores();
            dt1 = clsFinanzasBL.Instancia.GetListaClientes();

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();

                gridControl1.DataSource = dt1;
                gridView1.BestFitColumns();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            string tipo="";
            if (dtgvData1.SelectedIndex == 0)
            {
                tipo = "Proveedores";
                if (dtgvData.DataSource == null)
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para exportar";
                    m.ShowDialog();
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Lista de Proveedores - Cuentas Bancarias " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            else
            {
                tipo = "Clientes";
                if (gridControl1.DataSource == null)
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para exportar";
                    m.ShowDialog();
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Lista de Clientes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gridControl1.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }

           
        }
    }
}
