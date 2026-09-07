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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using ReportesTranspesa.Sistema;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    
    public partial class frmStatusUnidadesGEOTAB : Form
    {
        int second = 0;
         TimeSpan time;
         Timer timer = new Timer();
        public frmStatusUnidadesGEOTAB()
        {
            InitializeComponent();

        }

        private void frmStatusUnidadesGEOTAB_Load(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = true;

            CargarStatus();

            time = TimeSpan.Parse("00:01:00");

            
            timer.Interval = 1000;

            timer.Tick += (a, b) =>
            {
                time = time.Subtract(new TimeSpan(0, 0, 1));
                label2.Text = time.ToString();

                if (time.Seconds == 0)
                {
                    timer.Stop();
                    CargarStatus();
                    time = TimeSpan.Parse("00:01:00");
                    timer.Start();
                }
            };

            timer.Start();
        }

        void CargarStatus()
        {
            DataTable dt = new DataTable();
            dt = clsCombustibleBL.Instancia.GetCombustible_StatusUnidades("JROJAS");

            dtgvData.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                //dgvDataView.Columns["UbicacionGEOTAB"].Visible = false;
                //dgvDataView.Columns["UbicacionGOOGLE"].Visible = false;
                dgvDataView.BestFitColumns();
               
              //  MessageBox.Show("Se Recargo la data", "Aviso");
                timer1.Start();
            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
        }
        string geotab,google;
        private void dtgvData_Click(object sender, EventArgs e)
        {
          
        }

        private void dgvDataView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int[] filas = dgvDataView.GetSelectedRows();
            string datoseleccionado = dgvDataView.GetFocusedValue().ToString();
            
            for (int i = 0; i < filas.Length; i++)
            {
                geotab = dgvDataView.GetRowCellValue(filas[i], "UbicacionGPS").ToString();
                google = dgvDataView.GetRowCellValue(filas[i], "GoogleMps").ToString();
                //string validar = datos.Substring(0,13);
                
                if (datoseleccionado.Equals(geotab))
                {
                    geotab = dgvDataView.GetRowCellValue(filas[i], "UbicacionGEOTAB").ToString();
                    textUrl.Text = geotab;
                    button1.PerformClick();
                    //System.Diagnostics.Process.Start(geotab);
                }          

                else if (datoseleccionado.Equals(google))
                {
                    google = dgvDataView.GetRowCellValue(filas[i], "UbicacionGOOGLE").ToString();
                    textUrl.Text = google;
                    button1.PerformClick();
                    //System.Diagnostics.Process.Start(google);
                }
            }                  
           
        }

        private void dgvDataView_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
           
        }

        private void dtgvData_DoubleClick(object sender, EventArgs e)
        {
           // dgvDataView.GetRowCellValue(, "").ToString();
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Ubicacion" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            second = second + 1;
            if (second >= 300)
            {
                timer1.Stop();
                CargarStatus();
                second = 0;

            }
        }

        private void AdelanteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void GoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Perez987ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void VisualStudioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void YahooToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void PararLaCargaDeLaPáginaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RecargarLaPáginaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CodeHTMLToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {

        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            webView1.Navigate(textUrl.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = false;
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = true;
            button5.Visible = false;
            webView1.Navigate("about:blank");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
