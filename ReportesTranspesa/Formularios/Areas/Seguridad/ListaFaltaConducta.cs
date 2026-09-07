using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Negocio;
using ReportesTranspesa.Sistema;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class ListaFaltaConducta : MetroFramework.Forms.MetroForm
    {
        public ListaFaltaConducta()
        {
            InitializeComponent();
        }

        private void ListaFaltaConducta_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dtgvData.DataSource = null;
            dt = clsSeguridadBL.Instancia.GetListaFaltaConducta();
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string nombre = "";
            nombre = txtConductor.Text;
            dtgvData.DataSource = null;
            dt = clsSeguridadBL.Instancia.GetDataListaFaltaConducta(nombre);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
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
                string nombre = System.IO.Path.Combine(desktop, "Listado Falta de Conducta de los Conductores " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvData.ShowPrintPreview();
            }
        }

        private void txtConductor_TextChanged(object sender, EventArgs e)
        {
            txtConductor.CharacterCasing = CharacterCasing.Upper;
        }

    }
}
