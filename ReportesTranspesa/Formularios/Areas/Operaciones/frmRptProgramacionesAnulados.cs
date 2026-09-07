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

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class frmRptProgramacionesAnulados : MetroFramework.Forms.MetroForm
    {
        public frmRptProgramacionesAnulados()
        {
            InitializeComponent();
        }

        private void CargaCombustible_Load(object sender, EventArgs e)
        {

            splitContainer1.SplitterDistance = 79;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();

                return;
            }

            dgvProgramaciones.DataSource = null;
            dgvProgramacionesView.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Clear();

            dt = clsOperacionesBL.Instancia.Obtener_Lista_ProgramacionesAnulados(fechin, fechfin);
            
            
            if (dt.Rows.Count > 0)
            {
                
                dgvProgramaciones.DataSource = dt;

                dgvProgramacionesView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

                if (dgvProgramaciones.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Analisis Diario " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvProgramaciones.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            

        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvProgramaciones.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dgvProgramaciones.ShowPrintPreview();
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }


        private void verInformacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInformacionDespacho frm = new FrmInformacionDespacho();

            //dtgvDespachosDiariosView.GetSelectedRows;

            
            string Placa = "";
            DateTime FechaProgramacion = DateTime.Now;

            foreach(var i in dgvProgramacionesView.GetSelectedRows())
            {
                Placa = dgvProgramacionesView.GetDataRow(i)["PLACA"].ToString();
                FechaProgramacion = Convert.ToDateTime(dgvProgramacionesView.GetDataRow(i)["FECHAPROG_VIAJE"].ToString());
            }

            frm.Placa = Placa;
            frm.FechaProgramacion = FechaProgramacion;
            frm.ShowDialog();

        }
    }
}
