using Comun;
using Negocio;
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

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class frmImportargasboy : Form
    {

        DataTable dtListaTarifas;
        string xmlgasboy = string.Empty;
        public frmImportargasboy()
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivo();

                if (System.IO.File.Exists(txtUbicacion.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtUbicacion.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "Hoja1");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaTarifas = dataSetDetalle.Tables[0];


                    if (!dtListaTarifas.Columns.Contains("Fecha") || !dtListaTarifas.Columns.Contains("Hora") || !dtListaTarifas.Columns.Contains("Vehiculo") || !dtListaTarifas.Columns.Contains("Producto") || !dtListaTarifas.Columns.Contains("Cantidad") || !dtListaTarifas.Columns.Contains("Precio") || !dtListaTarifas.Columns.Contains("Totalizador") || !dtListaTarifas.Columns.Contains("TicketPreviaje"))
                    {
                        MessageBox.Show("Excel no contiene alguna de las Columnas necesarias como Fecha, Hora ,Vehiculo, Producto, Cantidad , Precio , TicketPreviaje , Totalizador.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    for (int i = dtListaTarifas.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaTarifas.Rows[i]["Vehiculo"].ToString() == "")
                        {
                            dtListaTarifas.Rows[i].Delete();
                        }
                    }
                }

                if (dtListaTarifas.Rows.Count > 0)
                {
                    xmlgasboy = "";
                    dtgvData.DataSource = dtListaTarifas;
                    dtgvDataView.BestFitColumns();
                    btnActualizar.Enabled = true;
                    xmlgasboy = Comun.Utilitario.Instancia.DatatableToXml(dtListaTarifas);
                }
                else
                { dtgvData.DataSource = null; btnActualizar.Enabled = false; }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string RUTA = @"\\192.168.4.237\ReportesTranspesa2\Resources\plantilla_gasboy.xlsx";

                if (System.IO.File.Exists(RUTA))
                {
                    System.Diagnostics.Process.Start(RUTA);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarArchivo()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                //op.InitialDirectory = CarpetaDestino;
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtUbicacion.Text = op.FileName;
                        btnActualizar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                if (xmlgasboy.Length > 0)
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Combustible_CargarGasboy(xmlgasboy))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos importados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
