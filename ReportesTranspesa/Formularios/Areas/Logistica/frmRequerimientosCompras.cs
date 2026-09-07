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
using System.IO;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmRequerimientosCompras : MetroFramework.Forms.MetroForm
    {
        public frmRequerimientosCompras()
        {
            InitializeComponent();
        }

        private void frmRequerimientosCompras_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
        }


        public void ListarRequerimientos(bool checkResumen)
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dt = clsLogisticaBL.Instancia.ReportesApp_Mantenimiento_Reporte_RequerimientosALogistica(dtpFechaIni.Text, dtpFechaFin.Text, Usuario , checkResumen);



                dtgRequerimientosCompras.DataSource = null;
                dgvRequerimientosComprasVista.Columns.Clear(); 

                if (dt.Rows.Count > 0)
                {
                    dtgRequerimientosCompras.DataSource = dt;
                    dgvRequerimientosComprasVista.OptionsView.ShowGroupedColumns = true;

                    if (checkResumen)
                    {
                        dgvRequerimientosComprasVista.Columns["Periodo"].Group();
                        dgvRequerimientosComprasVista.ExpandAllGroups();
                    }
       
                    dgvRequerimientosComprasVista.BestFitColumns();


                }
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e) { ListarRequerimientos(chkResumen.Checked); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgRequerimientosCompras.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REQUERIMIENTO DE COMPRAS DE MTTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgRequerimientosCompras.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRequerimientos(chkResumen.Checked); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRequerimientos(chkResumen.Checked); }
        }

        private void dgvRequerimientosComprasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "FechaRequerida")
            {
                
                 e.Appearance.BackColor = Color.FromArgb(0, 213, 255); 


            }



            if (e.Column.FieldName == "OC_FechaEntrega")
            {

              
                 e.Appearance.BackColor = Color.FromArgb(31, 255, 0); 

            }


            if (e.Column.FieldName == "Dias Atraso")
            {


                e.Appearance.BackColor = Color.FromArgb(230, 55, 55);

            }
         

 
        }
    }
}
