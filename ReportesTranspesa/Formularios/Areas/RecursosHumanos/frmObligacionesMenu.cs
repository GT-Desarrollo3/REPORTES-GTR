using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Negocio;
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Properties;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmObligacionesMenu : Form
    {
        DataTable dtViewDetalle;
        String xmlDetalle;
        String CarpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Downloads");

        public frmObligacionesMenu()
        {
            InitializeComponent();
        }

        private void frmObligacionesMenu_Load(object sender, EventArgs e)
        {
            btnGenerar.Enabled = false;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            if (Usuario == "EPLACENCIA" || Usuario == "SCHAVEZ" || Usuario == "GREYES")
            {
                chkCTS.Visible = true;
            }
            else { chkCTS.Visible = false; }
        }

        private void frmObligacionesMenu_Shown(object sender, EventArgs e)
        {
            btnBuscarArchivo.Focus();
        }


        public void cargarArchivo()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = CarpetaDestino;
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtRuta.Text = op.FileName;
                        btnGenerar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }


        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                cargarArchivo();

                if (System.IO.File.Exists(txtRuta.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtRuta.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "Importar");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtViewDetalle = dataSetDetalle.Tables[0];

                    for (int i = dtViewDetalle.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtViewDetalle.Rows[i]["Codigo"] == "#REF!")
                        { dtViewDetalle.Rows[i].Delete(); }
                    }
                }

                if (dtViewDetalle.Rows.Count > 0)
                {
                    xmlDetalle = "";
                    dgvMenuDetalle.DataSource = dtViewDetalle;
                    xmlDetalle = Comun.Utilitario.Instancia.DatatableToXml(dtViewDetalle);

                }
                else
                { dgvMenuDetalle.DataSource = null; }
            }
            catch (Exception ex)
	        {
                btnGenerar.Enabled = false;
                dgvMenuDetalle.DataSource = null;
                MessageBox.Show("El archivo seleccionado no es el correcto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta = "";

            try
            {
                string xmlDetalleSinTildes = Utilitario.Instancia.QuitarTildes(xmlDetalle);
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

        
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Obligaciones_Menu_Insertar(xmlDetalleSinTildes, Usuario,chkCTS.Checked);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    txtRuta.Clear();
                    dgvMenuDetalle.DataSource = null;
                    btnGenerar.Enabled = false;
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex)
            { MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.Directory.Exists(CarpetaDestino))
                {
                    OpenFileDialog op = new OpenFileDialog();
                    op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                    op.Title = "Archivo.xlsx";
                    op.InitialDirectory = @"C:\Users\" + Environment.UserName + @"\Desktop";
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        if (op.FileName != "")
                        {
                            System.IO.File.Copy(op.FileName, @"\\192.168.4.237\ReportesTranspesa2\ImportExcel\" + op.SafeFileName, true);
                            MessageBox.Show("*ARCHIVO COPIADO CORRECTAMENTE.*", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
