using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Negocio;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmImportarGuias : Form
    {
        public bool esElectronico = false;
        public int AnioProgramacion = 0;
        public int idProgramacion = 0;
        public FrmImportarGuias()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buscarImportados();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {               
                Filter = "Excel | *.xls;*.xlsx;",
                Title = "Seleccionar Archivo"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = openFileDialog.FileName.ToString();
                dataGridView1.DataSource = ImportarDatos(openFileDialog.FileName);
            
            }
        }

        DataView ImportarDatos(string nombrearchivo) 
        {
           
            string conexion = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = 'Excel 12.0;'", nombrearchivo);

            OleDbConnection conector = new OleDbConnection(conexion);
            conector.Open();

            OleDbCommand consulta = new OleDbCommand("select * from [Hoja1$]", conector);
            OleDbDataAdapter adaptador = new OleDbDataAdapter
            {
                SelectCommand = consulta
            };

            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            conector.Close();

            return ds.Tables[0].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (esElectronico)
            {
                if (idProgramacion == 0 || AnioProgramacion == 0)
                {
                    MessageBox.Show("Anio o ID Programacion incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
 

            string cadena = "";
            System.Console.WriteLine(cadena);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (dataGridView1.Rows.Count > 0)
            {

                int rowscount = dataGridView1.Rows.Count;
                int columnscount = dataGridView1.Columns.Count;

                cadena += "<r>";

                for (int i = 0; i < rowscount; i++)
                {
                    cadena += "<d";

                    for (int j = 0; j < columnscount; j++)
                    {
                        string cabecera = dataGridView1.Columns[j].HeaderText;
                        string valor = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        cadena += " " + cabecera.ToString();
                        cadena += "=\"" + valor.ToString();
                        cadena += "\"";
                    }

                    cadena += " />";
                }

                cadena += "</r>";

                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (esElectronico)
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ImportarGuiasTerceros(cadena, Utilitario.Instancia.SesionUsuario.usuario,idProgramacion,AnioProgramacion))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_ImportarGuiasExcel01(cadena, Utilitario.Instancia.SesionUsuario.usuario, "");
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        buscarImportados();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }   
                }
              
                         

            }
            else
            {
                return;
            }

        }
        private void buscarImportados()
        {
            while (dataGridView1.RowCount > 1)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }

            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_Guias_ver(1);
            dataGridView1.DataSource = dtRespuesta;
        
        }

        private void FrmImportarGuias_Load(object sender, EventArgs e)
        {
            if (esElectronico)
            {
                txtModulo.Visible = true;
            }
        }

        private void FrmImportarGuias_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

    }
}
