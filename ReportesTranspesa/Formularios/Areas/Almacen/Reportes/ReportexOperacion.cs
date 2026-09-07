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
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

namespace ReportesTranspesa.Formularios.Areas.Almacen.Reportes
{
    public partial class ReporteXOperacion : Form
    {
        public int IDCliente, AnioOP, NroOP;
        public string NombreCliente, OP;
        public ReporteXOperacion()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReporteXOperacion_Load(object sender, EventArgs e)
        {
            lblCliente.Text = NombreCliente;
            lblOperacion.Text = OP;
            try
            {

                verReportesIngresos();
              //  verReportesOrdenesRetiro();
               // verReportesDespachos();
                verReportesDespachosSacos();
                verReportesSaldos();

                /////////
                //verReporteSaldos();
                //verReportesDespachoSacos();
                verReportesDespacho();
                verReportesOrdenesRetiros();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error Informe", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void verReportesIngresos()
        {
            //ConnectionInfo cn = new ConnectionInfo();
            //cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            //cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            //cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            //cn.Type = ConnectionInfoType.SQL;


            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPIngresos.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.ServerName = host;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName = catalog;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.UserID = user;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.Password = pass;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.IntegratedSecurity = true;
            CrystalDecisions.Shared.TableLogOnInfo tLogonInfo = new CrystalDecisions.Shared.TableLogOnInfo();
            tLogonInfo.ConnectionInfo = r.Database.Tables[0].LogOnInfo.ConnectionInfo;
            r.Database.Tables[0].ApplyLogOnInfo(tLogonInfo);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            //r.DataSourceConnections[0].IntegratedSecurity = true;
            r.SetParameterValue("@TipoConsulta", 1);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);
            
            crvIngresos.ReportSource = r;           

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
        private void verReportesOrdenesRetiro()
        {
           ConnectionInfo cn = new ConnectionInfo();
            cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            cn.Type = ConnectionInfoType.SQL;

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPOrdenRetiro.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@TipoConsulta", 2);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);
            crvOrdenesRetiro.ReportSource = r;
             
            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
        private void verReportesDespachos()
        {
            ConnectionInfo cn = new ConnectionInfo();
            cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            cn.Type = ConnectionInfoType.SQL;

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPDespachos.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, true);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@TipoConsulta", 3);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);
            crvDespachos.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
        private void verReportesDespachosSacos()
        {
            ConnectionInfo cn = new ConnectionInfo();
            cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            cn.Type = ConnectionInfoType.SQL;

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPDespachosSacos.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@TipoConsulta", 4);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);
            crvDespachosSacos.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
        private void verReportesSaldos()
        {
            ConnectionInfo cn = new ConnectionInfo();
            cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            cn.Type = ConnectionInfoType.SQL;

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPSaldos.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@TipoConsulta", 5);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);
            crvSaldos.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
        /******nuevos******/
        private void verReportesOrdenesRetiros()
        {
            //ConnectionInfo cn = new ConnectionInfo();
            //cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            //cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            //cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            //cn.Type = ConnectionInfoType.SQL;


            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPOrdenRetiro.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.ServerName = host;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName = catalog;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.UserID = user;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.Password = pass;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.IntegratedSecurity = true;
            CrystalDecisions.Shared.TableLogOnInfo tLogonInfo = new CrystalDecisions.Shared.TableLogOnInfo();
            tLogonInfo.ConnectionInfo = r.Database.Tables[0].LogOnInfo.ConnectionInfo;
            r.Database.Tables[0].ApplyLogOnInfo(tLogonInfo);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            //r.DataSourceConnections[0].IntegratedSecurity = true;
            r.SetParameterValue("@TipoConsulta", 2);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);

            crvOrdenesRetiro.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }

        private void verReportesDespacho()
        {
            //ConnectionInfo cn = new ConnectionInfo();
            //cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            //cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            //cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            //cn.Type = ConnectionInfoType.SQL;


            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPDespachos.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.ServerName = host;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName = catalog;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.UserID = user;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.Password = pass;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.IntegratedSecurity = true;
            CrystalDecisions.Shared.TableLogOnInfo tLogonInfo = new CrystalDecisions.Shared.TableLogOnInfo();
            tLogonInfo.ConnectionInfo = r.Database.Tables[0].LogOnInfo.ConnectionInfo;
            r.Database.Tables[0].ApplyLogOnInfo(tLogonInfo);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            //r.DataSourceConnections[0].IntegratedSecurity = true;
            r.SetParameterValue("@TipoConsulta", 3);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);

            crvDespachos.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
        private void verReportesDespachoSacos()
        {
            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPDespachosSacos.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.ServerName = host;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName = catalog;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.UserID = user;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.Password = pass;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.IntegratedSecurity = true;
            CrystalDecisions.Shared.TableLogOnInfo tLogonInfo = new CrystalDecisions.Shared.TableLogOnInfo();
            tLogonInfo.ConnectionInfo = r.Database.Tables[0].LogOnInfo.ConnectionInfo;
            r.Database.Tables[0].ApplyLogOnInfo(tLogonInfo);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            //r.DataSourceConnections[0].IntegratedSecurity = true;
            r.SetParameterValue("@TipoConsulta", 4);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);

            crvDespachosSacos.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

        }
        private void verReporteSaldos()
        {
            //ConnectionInfo cn = new ConnectionInfo();
            //cn.ServerName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            //cn.DatabaseName = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //cn.UserID = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            //cn.Password = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            //cn.Type = ConnectionInfoType.SQL;


            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Almacen\Reportes\";
            string rpt = "crvReporteOPSaldos.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            //r.SetDatabaseLogon(cn.UserID, cn.Password, cn.ServerName, cn.DatabaseName, false);
            //r.SetDatabaseLogon(user, pass, host, catalog);
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.ServerName = host;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName = catalog;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.UserID = user;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.Password = pass;
            r.Database.Tables[0].LogOnInfo.ConnectionInfo.IntegratedSecurity = true;
            CrystalDecisions.Shared.TableLogOnInfo tLogonInfo = new CrystalDecisions.Shared.TableLogOnInfo();
            tLogonInfo.ConnectionInfo = r.Database.Tables[0].LogOnInfo.ConnectionInfo;
            r.Database.Tables[0].ApplyLogOnInfo(tLogonInfo);
            //r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            //r.DataSourceConnections[0].IntegratedSecurity = true;
            r.SetParameterValue("@TipoConsulta", 5);
            r.SetParameterValue("@AnioOP", AnioOP);
            r.SetParameterValue("@NroOP", NroOP);
            r.SetParameterValue("@IDCliente", IDCliente);

            crvSaldos.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
    }
}
