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


namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmImportarProgramaciones : Form
    {
        public frmImportarProgramaciones()
        {
            InitializeComponent();
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

        private void frmImportarProgramaciones_Load(object sender, EventArgs e)
        {
            DataTable DTCodigos = new DataTable();
            DTCodigos = clsOperacionesBL.Instancia.GetOperaciones_ListarCodigosVicncular();

            comboBox1.DisplayMember = "codigo";
            comboBox1.DataSource = DTCodigos;


        }

       

        private void button6_Click_1(object sender, EventArgs e)
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (txtCodProg.Text.Length == 0)
            {
                MessageBox.Show("No ha registrado el codigo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_ImportarProgramaciones(cadena, Utilitario.Instancia.SesionUsuario.usuario, txtCodProg.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //buscarImportados();
                    frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                    f1.Val_Respuesta = "1";
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dtbuscarViajes = new DataTable();
            dtbuscarViajes = clsOperacionesBL.Instancia.GetOperaciones_Operaciones_ListarViajesAltra(comboBox1.Text);

            if (dtbuscarViajes.Rows.Count > 0)
            {
                dataGridView2.DataSource = dtbuscarViajes;
                int totalprograma = Convert.ToInt32(dtbuscarViajes.Rows.Count.ToString());
                label5.Text = "TOTAL PROGRAMACIONES: " + dtbuscarViajes.Rows.Count.ToString();
            }
            else 
            {
                dataGridView2.DataSource = null;
                MessageBox.Show("No hay Programaciones para vincular", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                label5.Text = "TOTAL PROGRAMACIONES: 0";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Seleccionar codigo.."))
            {
                MessageBox.Show("No ha seleccionado el codigo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataGridView2.DataSource == null)
            {
                MessageBox.Show("No hay Codigos para enlazar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            string cadena = "";
            System.Console.WriteLine(cadena);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (dataGridView2.Rows.Count > 0)
            {

                int rowscount = dataGridView2.Rows.Count;
                int columnscount = dataGridView2.Columns.Count;

                cadena += "<r>";

                for (int i = 0; i < rowscount; i++)
                {
                    cadena += "<d";

                    for (int j = 0; j < columnscount; j++)
                    {
                        string cabecera = dataGridView2.Columns[j].HeaderText;
                        string valor = dataGridView2.Rows[i].Cells[j].Value.ToString();
                        cadena += " " + cabecera.ToString();
                        cadena += "=\"" + valor.ToString();
                        cadena += "\"";
                    }

                    cadena += " />";
                }

                cadena += "</r>";

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_Operaciones_AnexarViajes(cadena, Utilitario.Instancia.SesionUsuario.usuario, comboBox1.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //buscarImportados();
                    frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                    f1.Val_Respuesta = "1";
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                return;
            }
        }

        private void frmImportarProgramaciones_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
            f1.Val_Respuesta = "0";
            this.Close();
        }
    }
}
