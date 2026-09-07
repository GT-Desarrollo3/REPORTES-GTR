using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class frmKpiTransportes : Form
    {
        public frmKpiTransportes()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            dgvResumen.DataSource = null;
             System.Data.DataTable dtKpi = new System.Data.DataTable();
             dtKpi = clsContabilidadBL.Instancia.GetLista_KpiTransporte(dtpAnio.Text);
             if (dtKpi.Rows.Count > 0)
             {
                 dgvResumen.DataSource = dtKpi;
                 dgvResumen.Rows[0].DefaultCellStyle.BackColor = Color.DodgerBlue;
                 dgvResumen.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                 dgvResumen.AutoResizeColumns();
                 dgvResumen.Columns["PK"].Visible = false;
                 dgvResumen.Columns["NROMES"].Visible = false;
             }
             else
             {
                 MessageBox.Show("No hay datos...!");
             }

        
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvResumen.DataSource == null)
            {               
                MessageBox.Show("No hay data para exportar");               
            }
            else
            {
                try
                {                    
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); // Instancia a la libreria de Microsoft Office
                    excel.Application.Workbooks.Add(true); //Con esto añadimos una hoja en el Excel para exportar los archivos
                    int IndiceColumna = 0;
                    foreach (DataGridViewColumn columna in dgvResumen.Columns) //Aquí empezamos a leer las columnas del listado a exportar
                    {
                        if (columna.Index>1 )//ocultamos la columna 0 y 1
                        {
                            IndiceColumna++;
                            excel.Cells[1, IndiceColumna] = columna.Name;
                        }
                    }
                    int IndiceFila = 0;
                    foreach (DataGridViewRow fila in dgvResumen.Rows) //Aquí leemos las filas de las columnas leídas
                    {
                       
                            IndiceFila++;
                            IndiceColumna = 0;
                            foreach (DataGridViewColumn columna in dgvResumen.Columns)
                            {
                                if (columna.Index > 1)//ocultamos la columna 0 y 1 ademas de las filas
                                {
                                    IndiceColumna++;
                                    //excel.Cells[IndiceFila + 1, IndiceColumna] = fila.Cells[columna.Name].Value; // Con el +1 sale cabecera
                                    excel.Cells[IndiceFila, IndiceColumna] = fila.Cells[columna.Name].Value; // Sin el +1 No sale cabecera
                                }
                            }                        
                    }
                    excel.Visible = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("No hay Registros a Exportar.");
                }
            }
        }

        private void frmKpiTransportes_Load(object sender, EventArgs e)
        {

        }
    }
}
