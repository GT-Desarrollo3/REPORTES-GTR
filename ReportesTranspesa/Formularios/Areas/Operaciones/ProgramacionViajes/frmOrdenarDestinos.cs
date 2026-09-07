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
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
     
    public partial class frmOrdenarDestinos : Form
    {
        string valor;
        public string Operaciones;
        public int idOperacion;
        public frmOrdenarDestinos()
        {
            InitializeComponent();
        }

        private void frmOrdenarDestinos_Load(object sender, EventArgs e)
        {
            txtUsuario.Text = Utilitario.Instancia.SesionUsuario.usuario;
            txtOperacion.Text = Operaciones;

            DataTable dtListarOrdenDestinos = new DataTable();
            dtListarOrdenDestinos = clsOperacionesBL.Instancia.GetLista_Operaciones_DestinosListar(idOperacion);
            if (dtListarOrdenDestinos.Rows.Count > 0)
            {
                gridControl1.DataSource = dtListarOrdenDestinos;          
               
                gridView1.OptionsBehavior.Editable = true;


                RepositoryItemTextEdit formtoTEXTO = new RepositoryItemTextEdit();
                
               // formtoTEXTO.DisplayFormat.FormatString = "HH:mm:ss";
                gridControl1.RepositoryItems.Add(formtoTEXTO);
                gridView1.Columns["ORDEN"].ColumnEdit = formtoTEXTO;          
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Actualizar el Orden de los destinos?", "ORDEN DESTINOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gridView1.SelectAll();
                string cadena = "";
                System.Console.WriteLine(cadena);

                if (gridView1.RowCount > 0)
                {
                    int columnscount = 2;
                    cadena += "<r>";

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        cadena += "<d";
                        int j = 0;
                        for (j = 0; j < columnscount; j++)
                        {
                            string cabecera = gridView1.Columns[j].Name;
                           // if (j == 0) { valor = gridView2.GetRowCellValue(i, "RUTA").ToString(); cabecera = "RUTA"; }
                           // if (j == 1) { valor = gridView2.GetRowCellValue(i, "DESCRIPCION").ToString(); cabecera = "DESCRIPCION"; }
                            if (j == 0) { valor = gridView1.GetRowCellValue(i, "DESTINO").ToString(); cabecera = "DESTINO"; }
                            if (j == 1) { valor = gridView1.GetRowCellValue(i, "ORDEN").ToString(); cabecera = "ORDEN"; }                            

                            cadena += " " + cabecera;
                            cadena += "=\"" + valor.ToString();
                            cadena += "\"";
                        }

                        cadena += " />";
                    }

                    cadena += "</r>";


                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsOperacionesBL.Instancia.GetDataActualizarOrdenDestino(cadena,Utilitario.Instancia.SesionUsuario.usuario,idOperacion);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // this.Close();
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
