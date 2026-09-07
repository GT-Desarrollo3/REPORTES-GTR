using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Sistemas
{

    public partial class frmAccesosSpring : MetroFramework.Forms.MetroForm
    {
        public frmAccesosSpring()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListadoAccesos();
        }

        public void ListadoAccesos()
        {
            dtgvData.DataSource = null;
            string nombres = "";
            nombres = txtNombres.Text;
            string usuario = "";
            usuario = txtUsuario.Text;
            char estado;
            if (rbActivo.Checked == true)
            {
                estado = 'A';
            }
            else
            {
                estado = 'I';
            }
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetListaAccesoSpring(usuario, nombres, estado);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
            }

            splitContainer2.Panel1Collapsed = true;
            splitContainer2.Panel2Collapsed = false;

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
                string nombre = System.IO.Path.Combine(desktop, "Listado de Accesos al Sistema SPRING " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (e.KeyChar.ToString()).ToUpper().ToCharArray()[0];
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (e.KeyChar.ToString()).ToUpper().ToCharArray()[0];
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {

            CrystalReportViewer rv = new CrystalReportViewer();

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm =  FolderDegug + @"\Formularios\Areas\Sistemas\Reportes\";
            string rpt = "crvReporteAccesosSpring.rpt";
                             
            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@USUARIO", txtUsuario.Text);
            r.SetParameterValue("@NOMBRES", txtNombres.Text);
            r.SetParameterValue("@ESTADO", "A");
            r.SetParameterValue("p_Usuario", txtNombres.Text=="" ? "TODOS" : txtNombres.Text);
            r.SetParameterValue("p_Estado", rbActivo.Checked ==true ? "Activos":"Inactivos");

                crvReporte.ReportSource = r;

            splitContainer2.Panel1Collapsed = false;
            splitContainer2.Panel2Collapsed = true;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error Informe", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            
            
          }

        private void frmAccesosSpring_Load(object sender, EventArgs e)
        {
            
        }

        private void txtNombres_Click(object sender, EventArgs e)
        {

        }
       
           
    }
}
