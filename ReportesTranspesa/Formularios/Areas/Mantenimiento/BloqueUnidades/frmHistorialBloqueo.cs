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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.BloqueUnidades
{
    public partial class frmHistorialBloqueo : Form
    {
        public frmHistorialBloqueo()
        {
            InitializeComponent();
        }

        private void frmHistorialBloqueo_Shown(object sender, EventArgs e)
        {
            txtPlaca.Focus();
        }

        private void frmHistorialBloqueo_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            btnBuscar_Click(sender, e);
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha de Inicio debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarHistorial_UnidadesBloqueadas(dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString(), txtPlaca.Text);
            if (dt.Rows.Count > 0)
            {
                dtgData.DataSource = dt;
                dgvExpressVista.Columns["FECHA_BLOQUEO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvExpressVista.Columns["FECHA_BLOQUEO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvExpressVista.Columns["FECHA_DESBLOQUEO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvExpressVista.Columns["FECHA_DESBLOQUEO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvExpressVista.BestFitColumns();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgData.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Historial de Unidades Bloqueadas " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }
    }
}
