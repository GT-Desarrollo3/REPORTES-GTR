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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroBaterias
{
    public partial class frmHistorialTraspasos : Form
    {
        public int Opcion;

        public frmHistorialTraspasos()
        {
            InitializeComponent();
        }

        private void frmHistorialTraspasos_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            if (Opcion == 1) { ListarTraspasos(); }
            if (Opcion == 2) { ListarInspecciones(); }
        }


        public void ListarTraspasos()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtgTraspasos.DataSource = null;
                dgvTraspasosVista.Columns.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_ListarTraspasos(1, txtCodigo.Text, txtPlaca.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                if (dt.Rows.Count > 0)
                {
                    dtgTraspasos.DataSource = dt;

                    dgvTraspasosVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvTraspasosVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvTraspasosVista.BestFitColumns();
                }
            }
        }

        public void ListarInspecciones()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtgTraspasos.DataSource = null;
                dgvTraspasosVista.Columns.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_ListarTraspasos(2, txtCodigo.Text, txtPlaca.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                if (dt.Rows.Count > 0)
                {
                    dtgTraspasos.DataSource = dt;

                    dgvTraspasosVista.Columns["idInspeccionB"].Visible = false;
                    dgvTraspasosVista.Columns["idBateria"].Visible = false;

                    dgvTraspasosVista.Columns["FECHA_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvTraspasosVista.Columns["FECHA_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvTraspasosVista.BestFitColumns();
                }
            }
        }


        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarTraspasos(); }
                if (Opcion == 2) { ListarInspecciones(); }
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarTraspasos(); }
                if (Opcion == 2) { ListarInspecciones(); }
            }
        }

        private void FechaProgIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarTraspasos(); }
                if (Opcion == 2) { ListarInspecciones(); }
            }
        }

        private void FechaProgFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarTraspasos(); }
                if (Opcion == 2) { ListarInspecciones(); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarTraspasos(); }
            if (Opcion == 2) { ListarInspecciones(); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgTraspasos.DataSource == null)
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
                string nombre = "";
                if (Opcion == 1)
                { nombre = System.IO.Path.Combine(desktop, "Reporte de Traspasos de Baterias - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx"); }
                if (Opcion == 2)
                { nombre = System.IO.Path.Combine(desktop, "Registro de Inspecciones de Baterias - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx"); }
                dtgTraspasos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
