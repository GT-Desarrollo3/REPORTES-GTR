using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Negocio;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;



namespace ReportesTranspesa.Formularios.Areas.Operaciones.CumplimientoViajes
{
    public partial class frmConsolidado_pesos_operaciones : Form
    {
        public frmConsolidado_pesos_operaciones()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt =    clsOperacionesBL.Instancia.ReportesApp_Operaciones_Consolidado_Pesos_Operacion(dtpFechaInicio.Text,dtpFechaFin.Text);
                if (dt.Rows.Count > 0)
                {
                    grvreporte.DataSource = dt;

                    GridView gridView = grvreporte.FocusedView as GridView;

                    gridView2.Columns["PesoViaje"].Summary.Clear();
                    gridView2.Columns["PesoViaje"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PesoViaje", "Cantidad = {0:N2}");
                 

                    gridView2.BestFitColumns();
                }
                else
                {
                    grvreporte.DataSource = null;
                }
                

            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de COnsolidado de Pesos - " + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gridView2.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            catch (Exception ex) 
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmConsolidado_pesos_operaciones_Load(object sender, EventArgs e)
        {

        }
    }
}
