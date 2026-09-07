using Comun;
using DevExpress.XtraGrid.Columns;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmReporteVacaciones : Form
    {
        public string TipoPermiso;
        public DataTable dtPermisoFormulario;
        public frmReporteVacaciones()
        {
            InitializeComponent();
        }

        private void gridControl1_EmbeddedNavigator_StyleChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {



                if (checkConductores.Checked && checkVerTodos.Checked)
                {
                    TipoPermiso = "VER TODOS";
                }
                if (checkConductores.Checked && checkVerTodos.Checked == false)
                {
                    TipoPermiso = "VER CONDUCTORES";
                }
                if (checkVerTodos.Checked && checkConductores.Checked == false)
                {
                    TipoPermiso = "VER TODOS";
                }

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            DataTable DTReportexCompensar;
            DTReportexCompensar = clsOperacionesBL.Instancia.GetDataReporteAsistenciaVacaciones(TipoPermiso);

            if (DTReportexCompensar.Rows.Count > 0)
            {
                label3.Text = "Total: " + DTReportexCompensar.Rows.Count.ToString();

                gridControl1.DataSource = DTReportexCompensar;

                gridView1.Columns["PK"].Visible = false;

                gridView1.Columns["IDPersona"].Fixed = FixedStyle.Left;
                gridView1.Columns["Trabajador"].Fixed = FixedStyle.Left;

                gridView1.Columns["IDPersona"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "IDPersona", "{0}");
                gridView1.Columns["TOTAL_PENDIENTES"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL_PENDIENTES", "{0}");
               
                gridView1.BestFitColumns();
            }
            else
            {
                MessageBox.Show("No datos para mostrar", "AVISO");
                label3.Text = "Total: 0";
            }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReporteVacaciones_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmReporteVacaciones");
                if (dtPermisos != null)
                {
                    dtPermisoFormulario = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());


                    if (dtPermisoFormulario.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPermisoFormulario.Rows.Count; i++)
                        {
                            if (dtPermisoFormulario.Rows[i]["NombrePermiso"].ToString().Equals("VER CONDUCTORES"))
                            {
                                checkConductores.Checked = true;

                            }
                            if (dtPermisoFormulario.Rows[i]["NombrePermiso"].ToString().Equals("VER TODOS"))
                            {
                                checkVerTodos.Checked = true;
                                checkVerTodos.Enabled = true;
                                checkConductores.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        checkConductores.Checked = true;
                        checkVerTodos.Enabled = false;
                        checkConductores.Enabled = false;
                    }
                }
                else {
                    MessageBox.Show("Usted no tiene permiso para esta opción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button4.Enabled = false;
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

            if (gridView1.DataSource == null)
            {
                MessageBox.Show("No hay data para exportar", "AVISO");
            }
            else
            {

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Vacaciones  del " + dateTimePicker1.Value.ToString("dd_MM_yyyy") + " al " + dateTimePicker2.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gridView1.ExportToXlsx(nombre);
                System.Diagnostics.Process.Start(nombre);
            }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
