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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmGuias_ImpresionDesemparque : Form
    {
        public frmGuias_ImpresionDesemparque()
        {
            InitializeComponent();
        }

        private void frmGuias_ImpresionDesemparque_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetImpresion_Desembarque_SerieGuias(1,"");
            
            if (dt.Rows.Count > 0)
            {
                cbxSeries.DisplayMember = "Serie";
                cbxSeries.ValueMember = "Serie";
                cbxSeries.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No se cargaron las Series", "Aviso");
            }

            CargarInfoSerie(cbxSeries.Text);

            this.WindowState = FormWindowState.Maximized;

            button1.Focus();

        }
        private void LimpiarCampos()
        {
            txtRazonRemite.Text = "";
            txtDireccionRemite.Text = "";
            txtRucRemite.Text = "";
            txtDistritoRemite.Text = "";
            txtProvinciaRemite.Text = "";
            txtDepartamentoRemite.Text = "";

            txtRazonDestino.Text = "";
            txtDireccionDestino.Text = "";
            txtRucDestino.Text ="";
            txtDistritoDestino.Text = "";
            txtProvinciaDestino.Text = "";
            txtDepartamentoDestino.Text = "";

            txtCodVehiculo.Text = "";
            txtMotonave.Text = "";
            txtProducto.Text = "";
        }

        private void CargarInfoSerie(string Serie)
        {
            //MessageBox.Show(Serie, "Aviso");
            LimpiarCampos();

            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetImpresion_Desembarque_SerieGuias(2, Serie);

            if (dt.Rows.Count > 0)
            {
                txtRazonRemite.Text = dt.Rows[0]["remitenterazonsocial"].ToString();
                txtDireccionRemite.Text = dt.Rows[0]["direccioncarga"].ToString();
                txtRucRemite.Text = dt.Rows[0]["remitenteruc"].ToString();
                txtDistritoRemite.Text = dt.Rows[0]["distrito"].ToString();
                txtProvinciaRemite.Text = dt.Rows[0]["provincia"].ToString();
                txtDepartamentoRemite.Text = dt.Rows[0]["depa"].ToString();

                txtRazonDestino.Text = dt.Rows[0]["destinatariorazonsocial"].ToString();
                txtDireccionDestino.Text = dt.Rows[0]["ptollegada"].ToString();
                txtRucDestino.Text = dt.Rows[0]["destinatarioruc"].ToString();
                txtDistritoDestino.Text = dt.Rows[0]["distritollegada"].ToString();
                txtProvinciaDestino.Text = dt.Rows[0]["provinciallegada"].ToString();
                txtDepartamentoDestino.Text = dt.Rows[0]["depallegada"].ToString();

                txtCodVehiculo.Text = dt.Rows[0]["codconfivehicular"].ToString();
                txtMotonave.Text = dt.Rows[0]["motonave"].ToString();
                txtProducto.Text = dt.Rows[0]["descripcionproducto"].ToString();
                
            }
            else
            {
                MessageBox.Show("No hay información para la Serie:" + Serie);
                verReporte();
            }

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarInfoSerie(cbxSeries.Text.Trim());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                verReporte();
               

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error Informe", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void verReporte()
        {
            CrystalReportViewer rv = new CrystalReportViewer();

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Operaciones\Reportes\";
            string rpt = "crvImpresionGuiaDesembarqueMasivo.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@SerieGuia", cbxSeries.Text.Trim());

            crvReporte.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }

        private void txtRazonRemite_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void txtRazonRemite_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtRucRemite.Focus();
            }       
            
        }

        private void txtRucRemite_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtRucRemite_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                string Razon = "";
                string Direc = "";
                string Depa = "";
                string Prov = "";
                string Dist = "";
                DataTable dtRespuesta = new DataTable();

                dtRespuesta = clsOperacionesBL.Instancia.GetImpresion_Desembarque_ConsultaRUC(txtRucRemite.Text);
                Razon = Convert.ToString(dtRespuesta.Rows[0]["RAZON"]);
                Direc = Convert.ToString(dtRespuesta.Rows[0]["DIRECCION"]);
                Depa = Convert.ToString(dtRespuesta.Rows[0]["DEPARTAMENTO"]);
                Prov = Convert.ToString(dtRespuesta.Rows[0]["PROVINCIA"]);
                Dist = Convert.ToString(dtRespuesta.Rows[0]["DISTRITO"]);
                txtRazonRemite.Text = Razon;
                txtDireccionRemite.Text = Direc;
                txtDepartamentoRemite.Text = Depa;
                txtProvinciaRemite.Text = Prov;
                txtDistritoRemite.Text = Dist;

                txtDireccionRemite.Focus();
            }

        }
        private void txtDireccionRemite_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDistritoRemite.Focus();
            }

        }
        private void txtDistritoRemite_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtProvinciaRemite.Focus();
            }

        }
        private void txtProvinciaRemite_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDepartamentoRemite.Focus();
            }

        }
        private void txtDepartamentoRemite_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtRazonDestino.Focus();
            }

        }
        private void txtRazonDestino_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtRucDestino.Focus();
            }

        }
        private void txtRucDestino_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                string Razon = "";
                string Direc = "";
                string Depa = "";
                string Prov = "";
                string Dist = "";
                DataTable dtRespuesta = new DataTable();

                dtRespuesta = clsOperacionesBL.Instancia.GetImpresion_Desembarque_ConsultaRUC(txtRucDestino.Text);
                Razon = Convert.ToString(dtRespuesta.Rows[0]["RAZON"]);
                Direc = Convert.ToString(dtRespuesta.Rows[0]["DIRECCION"]);
                Depa = Convert.ToString(dtRespuesta.Rows[0]["DEPARTAMENTO"]);
                Prov = Convert.ToString(dtRespuesta.Rows[0]["PROVINCIA"]);
                Dist = Convert.ToString(dtRespuesta.Rows[0]["DISTRITO"]);
                txtRazonDestino.Text = Razon;
                txtDireccionDestino.Text = Direc;
                txtDepartamentoDestino.Text = Depa;
                txtProvinciaDestino.Text = Prov;
                txtDistritoDestino.Text = Dist;

                txtDireccionDestino.Focus();
            }

        }
        private void txtDireccionDestino_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDistritoDestino.Focus();
            }

        }
        private void txtDistritoDestino_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtProvinciaDestino.Focus();
            }

        }
        private void txtProvinciaDestino_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtDepartamentoDestino.Focus();
            }

        }
        private void txtDepartamentoDestino_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {   

                if (Int32.Parse(txtCopias.Text)>1)
                {
                    if
                    (MessageBox.Show("Ha colocado el papel contínuo?", "Impresión Contínua", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < Int32.Parse(txtCopias.Text); i++)
                        {
                            ImprimirContinuo();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Int32.Parse(txtCopias.Text); i++)
                    {
                        ImprimirContinuo();
                    }
                }

               
                
            }
            catch (Exception eeee) { }
        }

        private void ImprimirContinuo()
        {
            //Pasamos a variables String los campos:
            string remite_razon = txtRazonRemite.Text;//.Substring(0, 30).ToString();
            string remite_ruc = txtRucRemite.Text;
            string remite_direccion = txtDireccionRemite.Text;
            string remite_distrito = txtDistritoRemite.Text;
            string remite_provincia = txtProvinciaRemite.Text;
            string remite_Departamento = txtDepartamentoRemite.Text;

            string Destino_razon = txtRazonDestino.Text;//.Substring(0, 30).ToString();
            string Destino_ruc = txtRucDestino.Text;
            string Destino_direccion = txtDireccionDestino.Text;
            string Destino_distrito = txtDistritoDestino.Text;
            string Destino_provincia = txtProvinciaDestino.Text;
            string Destino_Departamento = txtDepartamentoDestino.Text;

            string confvehiculo = txtCodVehiculo.Text;
            string producto = txtProducto.Text;
            string nave = txtMotonave.Text;

            int nroEspacios = 8;

            remite_direccion = Cadena(28, remite_direccion); //Cadena (Caracteres Maximo, Texto a Encuadrar)
            remite_distrito = Cadena(9, remite_distrito);
            remite_provincia = Cadena(9, remite_provincia);
            remite_Departamento = Cadena(12, remite_Departamento);
            remite_razon = Cadena(25, remite_razon);
            remite_ruc = Cadena(30, remite_ruc);

            Destino_direccion = Cadena(34, Destino_direccion); //Cadena (Caracteres Maximo, Texto a Encuadrar)
            Destino_distrito = Cadena(9, Destino_distrito);
            Destino_provincia = Cadena(8, Destino_provincia);
            Destino_Departamento = Cadena(12, Destino_Departamento);
            Destino_razon = Cadena(25, Destino_razon);

            producto = Cadena(25, producto);

            ImpresionDirecta.CrearImpresion Guia = new ImpresionDirecta.CrearImpresion();
            for (int i = 0; i < nroEspacios; i++)
            {
                Guia.TextoIzquierda(" ");
            }

            //DISTRIBUCION GUIA DE DESEMBARQUE
            Guia.TextoIzquierda("       " + remite_direccion + "           ");
            Guia.TextoIzquierda("     " + remite_distrito + "  " + remite_provincia + "  " + remite_Departamento + "       " + Destino_direccion);
            Guia.TextoIzquierda("                                              " + Destino_distrito + "  " + Destino_provincia + "  " + Destino_Departamento);
            Guia.TextoIzquierda("           " + remite_razon + "               "+ Destino_razon);
            Guia.TextoIzquierda("     " + remite_ruc + "          " + Destino_ruc);
            Guia.TextoIzquierda(" ");
            Guia.TextoIzquierda(" ");
            Guia.TextoIzquierda("                 " + confvehiculo);
            Guia.TextoIzquierda(" ");
            Guia.TextoIzquierda(" ");
            Guia.TextoIzquierda("           " + producto);
            //FIN

            if (Int32.Parse(txtCopias.Text) == 1)
            {
                nroEspacios = 22;
            }
            else 
            {
                nroEspacios = 11;
            }
                    
            
            for (int i = 0; i < nroEspacios; i++)
            {
                Guia.TextoIzquierda(" ");
            }
            Guia.TextoIzquierda(" "); //Puede ir guiones para marcar donde termina.
            Guia.CortaDocumento();
            Guia.ImprimirDocumento("EPSONFX-890_OK");
        }

        public static String Cadena(int CaracMax, string Texto)
        {

            string espaciosFaltan = "";
            for (int i = 0; i < (CaracMax - Texto.Length); i++)
            {
                espaciosFaltan += " ";//Agrega espacios para alinear a la derecha
            }
            Texto = Texto + espaciosFaltan;

            if (Texto.Length > CaracMax)
            {
                Texto = Texto.Substring(0, CaracMax);
            }

            return Texto;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Visible = false;
            btnGrabar.Visible = true;
            activarCampos();
            txtRazonRemite.Focus();
        }

        private void activarCampos()
        {
            txtRazonRemite.Enabled = true;
            txtDireccionRemite.Enabled = true;
            txtRucRemite.Enabled = true;
            txtDistritoRemite.Enabled = true;
            txtProvinciaRemite.Enabled = true;
            txtDepartamentoRemite.Enabled = true;

            txtRazonDestino.Enabled = true;
            txtDireccionDestino.Enabled = true;
            txtRucDestino.Enabled = true;
            txtDistritoDestino.Enabled = true;
            txtProvinciaDestino.Enabled = true;
            txtDepartamentoDestino.Enabled = true;

            txtCodVehiculo.Enabled = true;
            txtMotonave.Enabled = true;
            txtProducto.Enabled = true;
        }

        private void DesactivarCampos()
        {
            txtRazonRemite.Enabled = false;
            txtDireccionRemite.Enabled = false;
            txtRucRemite.Enabled = false;
            txtDistritoRemite.Enabled = false;
            txtProvinciaRemite.Enabled = false;
            txtDepartamentoRemite.Enabled = false;

            txtRazonDestino.Enabled = false;
            txtDireccionDestino.Enabled = false;
            txtRucDestino.Enabled = false;
            txtDistritoDestino.Enabled = false;
            txtProvinciaDestino.Enabled = false;
            txtDepartamentoDestino.Enabled = false;

            txtCodVehiculo.Enabled = false;
            txtMotonave.Enabled = false;
            txtProducto.Enabled = false;
        }

        private void txtCopias_KeyPress(object sender,
          KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            //Pasamos a variables String los campos:
            string serie = cbxSeries.Text.Trim();

            string remite_razon = txtRazonRemite.Text; //.Substring(0, 30).ToString();
            string remite_ruc = txtRucRemite.Text;
            string remite_direccion = txtDireccionRemite.Text;
            string remite_distrito = txtDistritoRemite.Text;
            string remite_provincia = txtProvinciaRemite.Text;
            string remite_Departamento = txtDepartamentoRemite.Text;

            string Destino_razon = txtRazonDestino.Text; //.Substring(0, 30).ToString();
            string Destino_ruc = txtRucDestino.Text;
            string Destino_direccion = txtDireccionDestino.Text;
            string Destino_distrito = txtDistritoDestino.Text;
            string Destino_provincia = txtProvinciaDestino.Text;
            string Destino_Departamento = txtDepartamentoDestino.Text;

            string confvehiculo = txtCodVehiculo.Text;
            string producto = txtProducto.Text;
            string nave = txtMotonave.Text;

            string usuario = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            dtRespuesta = clsOperacionesBL.Instancia.GetImpresion_Desembarque_GuardaGuiaMasiva(serie, remite_razon, remite_ruc, remite_direccion, remite_distrito, remite_provincia,
                                                                    remite_Departamento, Destino_razon, Destino_ruc, Destino_direccion, Destino_distrito
                                                                   , Destino_provincia, Destino_Departamento, confvehiculo, producto, nave);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

            MessageBox.Show(Respuesta);
            btnGrabar.Visible = false;
            btnEditar.Visible = true;
            DesactivarCampos();
            verReporte();

        }

        private void txtRucDestino_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        
         
    }
}
