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
    public partial class frmUnidadesBloqueadas : Form
    {
        int count = 1;
        int ContaActivas = 0;
        int ContarBloque = 0;
        public frmUnidadesBloqueadas()
        {
            InitializeComponent();
        }

        private void frmUnidadesBloqueadas_Load(object sender, EventArgs e)
        {            
                tcUnidades.Visible = true;
                dgvUnidadesDisponibles.Visible = true;

                /*************Vidaulizar Unidades Disponilbes********************/
                DataTable dtUnidadActivas = new DataTable();
                dtUnidadActivas = clsOperacionesBL.Instancia.GetOperaciones_ListarUnidadesActivas();
                if (dtUnidadActivas.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUnidadActivas.Rows.Count; i++)
                    {
                        dgvUnidadesDisponibles.DataSource = dtUnidadActivas;
                        gridView2.Columns["UBICACIONGPS"].Visible = false;
                        ContaActivas =  dtUnidadActivas.Rows.Count;
                        lblContador.Text = "TOTAL: " + ContaActivas.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Por momento no se encuntran unidades activas", "Aviso");
                }

                /************Visualizar Unidades Bloqueadas*****************/
                DataTable dtBloqueadas = new DataTable();
                dtBloqueadas = clsOperacionesBL.Instancia.GetOperaciones_ListarUnidadesBloqueadas("", "", "");
                if (dtBloqueadas.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBloqueadas.Rows.Count; i++)
                    {
                        dgvUnidadesBloqueadas.DataSource = dtBloqueadas;
                        ContarBloque = dtBloqueadas.Rows.Count;                      
                    }
                }
                else
                {
                    MessageBox.Show("Por momento no se encuntran unidades bloqueadas", "Aviso");
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webView2.Visible = false;
            button4.Visible = false;
        }
            

        private void tcUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (count == 1)
            {               
                count = 0;
                  lblContador.Text = "TOTAL: " + ContarBloque.ToString();
            }
            else
            {               
                count = 1;
              lblContador.Text = "TOTAL: " + ContaActivas.ToString();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (count == 1) 
            {
                if (dgvUnidadesDisponibles.DataSource == null)
                {
                    MessageBox.Show("No hay data para exportar", "Error");
                }
                else
                {
                   CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte de Unidades Disponibles por " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvUnidadesDisponibles.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            else 
            
            {
                if (dgvUnidadesBloqueadas.DataSource == null)
                {
                   MessageBox.Show("No hay data para exportar","Error");                    
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte de Unidades Bloqueadas por " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvUnidadesBloqueadas.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int[] filas = gridView2.GetSelectedRows();
            string datoseleccionado = gridView2.GetFocusedValue().ToString();

            for (int i = 0; i < filas.Length; i++)       
            {
                string dato = gridView2.GetRowCellValue(filas[i], "GPS").ToString();
                string Ubicacion = gridView2.GetRowCellValue(filas[i], "UBICACIONGPS").ToString();

                if (datoseleccionado.Equals(dato))
                {
                    webView2.Visible = true;
                    button4.Visible = true;
                    webView2.Navigate(Ubicacion);
                }               
            }               
        }        
    }
}
