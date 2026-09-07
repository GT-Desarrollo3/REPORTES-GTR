using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

using System.Xml;
using System.IO;


namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmRptConductoresSinEMO : Form
    {
        public DataTable DtCompania;

        public frmRptConductoresSinEMO()
        {
            InitializeComponent();
        }

        private void frmRptConductoresSinEMO_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 54;

            //Llenado de Empresas
            cbxCompania.DataSource = DtCompania;
            cbxCompania.DisplayMember = "Compania";
            cbxCompania.ValueMember = "Codigo";
            cbxCompania.SelectedIndex = 0;

            txtFiltro.Text = "";
        }



        private void ListarDocumentos()
        {
            DataTable dt = new DataTable();

            dt.Clear();

            string filtro = "";

            filtro = txtFiltro.Text;

            dt = clsControlDocumentosBL.Instancia.getDocumentos_ReporteConductoresSinEMO(cbxCompania.SelectedValue.ToString(), filtro);

            if (dt.Rows.Count > 0)
            {

                dgvDocumentos.DataSource = dt;

                //dgvDocumentosView.Columns["IDDOCUMENTO"].Visible = false;

                dgvDocumentosView.BestFitColumns();
            }
            else
            {
                dgvDocumentos.DataSource = null;
                MessageBox.Show("No hay data para mostrar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarDocumentos();
        }

      
        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvDocumentos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte Conductores Sin EMO " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dgvDocumentos.ExportToXlsx(nombre);
                Process.Start(nombre);

            }
        }



    }
}
