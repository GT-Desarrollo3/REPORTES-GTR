using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Neumaticos
{
    public partial class frmRegistroKMUnidades : Form
    {
        public string TipoVehiculo;
        DataTable dtListaKM = new DataTable();
        DataTable dtInsertarKM = new DataTable();

        public frmRegistroKMUnidades()
        {
            InitializeComponent();
        }

        private void frmRegistroKMUnidades_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
            rbTracto.Checked = true;
            rbTracto_CheckedChanged(sender, e);
        }


        public void ListarKMRegistro()
        {
            dtInsertarKM = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_InsertarKMUnidades(txtPlaca.Text, dtpFechaInicio.Text, dtpFechaFin.Text, TipoVehiculo);
            dtListaKM = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ListarKMUnidades();

            dtgRegistroKM.DataSource = dtListaKM;
            if (dtListaKM.Rows.Count > 0)
            {
                dgvRegistroKMVista.Columns["#"].Visible = false;
                dgvRegistroKMVista.Columns["IdVehiculo"].Visible = false;

                dgvRegistroKMVista.BestFitColumns();
            }
        }


        private void rbTracto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTracto.Checked == true) { TipoVehiculo = "T"; }
        }

        private void rbCarreta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCarreta.Checked == true) { TipoVehiculo = "C"; }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKMRegistro(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarKMRegistro(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgRegistroKM.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Kilometrajes de Unidades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgRegistroKM.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
