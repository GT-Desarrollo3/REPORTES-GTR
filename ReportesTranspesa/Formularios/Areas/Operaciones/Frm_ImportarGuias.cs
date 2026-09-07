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
    public partial class Frm_ImportarGuias : Form
    {
        public Frm_ImportarGuias()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                //DE ESTA MANERA FILTRAMOS TODOS LOS ARCHIVOS EXCEL EN EL NAVEGADOR DE ARCHIVOS
                Filter = "Excel | *.xls;*.xlsx;",

                //AQUÍ INDICAMOS QUE NOMBRE TENDRÁ EL NAVEGADOR DE ARCHIVOS COMO TITULO
                Title = "Seleccionar Archivo"
            };

            //EN CASO DE SELECCIONAR EL ARCHIVO, ENTONCES PROCEDEMOS A ABRIR EL ARCHIVO CORRESPONDIENTE
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = openFileDialog.FileName.ToString();
                dataGridView1.DataSource = ImportarDatos(openFileDialog.FileName);
                //  splitContainer3.Panel2Collapsed = true;
                // splitContainer3.Panel1Collapsed = false;
                // splitContainer4.Panel2Collapsed = true;
                // splitContainer4.Panel1Collapsed = false;
            }
        }

        DataView ImportarDatos(string nombrearchivo) //COMO PARAMETROS OBTENEMOS EL NOMBRE DEL ARCHIVO A IMPORTAR
        {
            //UTILIZAMOS 12.0 DEPENDIENDO DE LA VERSION DEL EXCEL, EN CASO DE QUE LA VERSIÓN QUE TIENES ES INFERIOR AL DEL 2013, CAMBIAR A EXCEL 8.0 Y EN VEZ DE
            //ACE.OLEDB.12.0 UTILIZAR LO SIGUIENTE (Jet.Oledb.4.0)
            string conexion = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = 'Excel 12.0;'", nombrearchivo);

            OleDbConnection conector = new OleDbConnection(conexion);

            conector.Open();

            //DEPENDIENDO DEL NOMBRE QUE TIENE LA PESTAÑA EN TU ARCHIVO EXCEL COLOCAR DENTRO DE LOS []
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
            if (txtArchivo.Text.Length == 0)
            {
                MessageBox.Show("No ha seleccionado ningun archivo a importar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                dtRespuesta = clsContabilidadBL.Instancia.GetPeajes_ImportarDataExcel01(cadena, Utilitario.Instancia.SesionUsuario.usuario);
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


                //PASA TODO EL DETALLE DE UNA TABLA A XML  ////////////////////////////////// JOEL ROJAS
                //--------------------------------------------------------------------------------------

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<r>");

                //for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                //{
                //    sb.Append("<d");

                //    for (int col = 0; col < dataGridView1.Rows[fila].Cells.Count; col++)
                //    {
                //        string cabecera = dataGridView1.Rows[0].Cells[col].Value.ToString();
                //        string valor = dataGridView1.Rows[fila].Cells[col].Value.ToString();

                //        sb.Append(" " + cabecera.ToString());
                //        sb.Append("=\"" + valor.ToString());
                //        sb.Append("\"");
                //    }

                //    sb.Append(" />");
                //}

                //sb.Append("</r>");

                //MessageBox.Show(sb.ToString(), "Selected Cells");

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
            dtRespuesta = clsContabilidadBL.Instancia.GetPeajes_Ver(1, "", "", "");
            dataGridView1.DataSource = dtRespuesta;
            /// splitContainer5.Panel1Collapsed = false;
            //splitContainer5.Panel2Collapsed = true;
            //  splitContainer3.Panel1Collapsed = false;
            // splitContainer3.Panel2Collapsed = true;
        }

        private void Frm_ImportarGuias_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
