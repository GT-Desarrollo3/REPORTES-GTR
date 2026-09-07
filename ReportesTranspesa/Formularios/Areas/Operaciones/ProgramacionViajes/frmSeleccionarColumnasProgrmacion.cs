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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmSeleccionarColumnasProgrmacion : Form
    {
        string _Usuario;
        string valor;
        public void envioVariables(/*string sucursal,string programacion,*/ string usuario)
        {
            _Usuario = usuario;
        }
        public frmSeleccionarColumnasProgrmacion()
        {
            InitializeComponent();
        }

        private void frmSeleccionarColumnasProgrmacion_Load(object sender, EventArgs e)
        {
            label3.Text = _Usuario;
            DataTable dtColumnas = new DataTable();
            dtColumnas = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarColumnas(Utilitario.Instancia.SesionUsuario.usuario);
            if (dtColumnas.Rows.Count > 0)
            {
                gridControl2.DataSource = dtColumnas;
                gridView2.Columns["CODIGO"].Visible = false;
                gridView2.Columns["Estado"].Visible = false;

                for (int i = 0; i < gridView2.DataRowCount - 1; i++)
                {
                    if (Convert.ToInt32(gridView2.GetRowCellValue(i, "Estado")) == 1)
                    {
                        gridView2.SelectRow(i);
                    }                  
                }
            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int[] filas = gridView2.GetSelectedRows();
            
            string cadena = "";
            System.Console.WriteLine(cadena);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            if (filas.Length > 0)
            {

                int[] rowscount = gridView2.GetSelectedRows();
                int columnscount = 2;

                cadena += "<r>";
                
                for (int i = 0; i < rowscount.Length; i++)
                {
                    cadena += "<d";

                    for (int j = 0; j < columnscount; j++)
                    {
                        string cabecera = gridView2.Columns[j].Name;
                        if (j == 0)
                        {
                             valor = gridView2.GetRowCellValue(filas[i], "CODIGO").ToString();
                        }
                        if (j == 1)
                        {
                             valor = gridView2.GetRowCellValue(filas[i], "Descripcion").ToString();
                        }
                        cadena += " " + cabecera.Substring(3, 6).ToString();
                        cadena += "=\"" + valor.ToString();
                        cadena += "\"";
                    }

                    cadena += " />";
                }

                cadena += "</r>";


                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_Previajes_RegistroFiltroColumnas(cadena, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
              
    }
}
