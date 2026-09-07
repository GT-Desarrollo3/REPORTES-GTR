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
using System.Globalization;
using System.Diagnostics;
using Comun;
namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmProgramacionesxAtender : Form
    {
        int OPCION = 1;
        public frmProgramacionesxAtender()
        {
            InitializeComponent();
        }

        private void frmProgramacionesxAtender_Load(object sender, EventArgs e)
        {
            btnExcel.Visible = false;
            DataTable dtProgramacionexCompletar = new DataTable();
            dtProgramacionexCompletar = clsOperacionesBL.Instancia.GetLista_Operaciones_Previajes_PorCompletar(OPCION,Utilitario.Instancia.SesionUsuario.usuario);

            if (dtProgramacionexCompletar.Rows.Count > 0)
            {
                gridControl1.DataSource = dtProgramacionexCompletar;

                gridView1.Columns["TIPO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TIPO", "TOTAL:");
                gridView1.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "{0}");
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                OPCION = 0;

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                DataTable dtProgramacionexCompletar = new DataTable();
                dtProgramacionexCompletar = clsOperacionesBL.Instancia.GetLista_Operaciones_Previajes_PorCompletar(OPCION, Utilitario.Instancia.SesionUsuario.usuario);

                if (dtProgramacionexCompletar.Rows.Count > 0)
                {
                    gridControl1.DataSource = dtProgramacionexCompletar;
                    btnExcel.Visible = true;
                }
            }
            else
            {
                btnExcel.Visible = false;
                OPCION = 1;
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                DataTable dtProgramacionexCompletar = new DataTable();
                dtProgramacionexCompletar = clsOperacionesBL.Instancia.GetLista_Operaciones_Previajes_PorCompletar(OPCION, Utilitario.Instancia.SesionUsuario.usuario);

                if (dtProgramacionexCompletar.Rows.Count > 0)
                {
                    gridControl1.DataSource = dtProgramacionexCompletar;

                    gridView1.Columns["TIPO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TIPO", "TOTAL:");
                    gridView1.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "{0}");
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gridControl1.DataSource == null)
            {
                
                MessageBox.Show("No hay data para exportar","AVISO");
          
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Previajes por Atender " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gridControl1.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
