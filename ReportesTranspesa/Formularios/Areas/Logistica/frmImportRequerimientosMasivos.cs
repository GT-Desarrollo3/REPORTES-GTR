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


namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmImportRequerimientosMasivos : Form
    {

        DataTable dtView;
        DataTable dtViewDetalle;
        DataTable dtViewCotizacion;
        String xml;
        String xmlDetalle;
        String xmlCotizacion;
        String CarpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        DataTable igv = new DataTable();  

        public frmImportRequerimientosMasivos()
        {
            InitializeComponent();
        }

        private void frmImportRequerimientosMasivos_Load(object sender, EventArgs e)
        {
            igv.Columns.Add("ID", typeof(String));
            igv.Columns.Add("IGV", typeof(String));
            igv.Rows.Add(1,"18%");
            igv.Rows.Add(2,"10%");

            cbxIgv.DataSource = igv;
            cbxIgv.ValueMember = "ID";
            cbxIgv.DisplayMember = "IGV";

            cbxTipo.SelectedIndex = 1;
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
                        
                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {

        try 
	    {	  
      
            cargarArchivo();


            if (System.IO.File.Exists(txtRuta.Text))
            {

                //Cabecera
                string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtRuta.Text);
                OleDbConnection conexion_OleDb = new OleDbConnection(connectionString);
                string query = String.Format("select * from [{0}$]", "GenerarDataCabecera");
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, conexion_OleDb);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataSet.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                dataSet.Tables[0].AcceptChanges();
                dtView = dataSet.Tables[0];

                //Detalle
                string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtRuta.Text);
                OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                string queryDetalle = String.Format("select * from [{0}$]", "GenerarDataDetalle");
                OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                DataSet dataSetDetalle = new DataSet();
                dataAdapterDetalle.Fill(dataSetDetalle);
                dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                dataSetDetalle.Tables[0].AcceptChanges();
                dtViewDetalle = dataSetDetalle.Tables[0];

                for (int i = dtViewDetalle.Rows.Count - 1; i >= 0; i--)
                {
                    if (dtViewDetalle.Rows[i]["CompaniaSocio"] == "#REF!")
                    {
                        dtViewDetalle.Rows[i].Delete();
                    }
                }


                //Detalle
                string connectionStringCotizacion = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtRuta.Text);
                OleDbConnection conexion_OleDbCotizacion = new OleDbConnection(connectionStringCotizacion);
                string queryCotizacion = String.Format("select * from [{0}$]", "GenerarDataCotizar");
                OleDbDataAdapter dataAdapterCotizacion = new OleDbDataAdapter(queryCotizacion, conexion_OleDbCotizacion);
                DataSet dataSetCotizacion = new DataSet();
                dataAdapterCotizacion.Fill(dataSetCotizacion);
                dataSetCotizacion.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                dataSetCotizacion.Tables[0].AcceptChanges();
                dtViewCotizacion = dataSetCotizacion.Tables[0];


                for (int i = dtViewCotizacion.Rows.Count - 1; i >= 0; i--)
                {
                    if (dtViewCotizacion.Rows[i]["CompaniaSocio"] == "#REF!")
                    {
                        dtViewCotizacion.Rows[i].Delete();
                    }
                }

                if (dtView.Rows.Count > 0)
                {
                    xml = "";
                    dgvRequerimientos.DataSource = dtView;
                    xml = Comun.Utilitario.Instancia.DatatableToXml(dtView);
                    //MessageBox.Show(xml);
                    
                }
                else
                {
                    dgvRequerimientos.DataSource = null;
                }
		
	            



                if (dtViewDetalle.Rows.Count > 0)
                {
                    xmlDetalle = "";
                    dgvRequerimientosDetalle.DataSource = dtViewDetalle;
                    xmlDetalle = Comun.Utilitario.Instancia.DatatableToXml(dtViewDetalle);
                    //MessageBox.Show(xml);

                }
                else
                {
                    dgvRequerimientosDetalle.DataSource = null;
                }




                if (dtViewCotizacion.Rows.Count > 0)
                {
                    xmlCotizacion = "";
                    dgvCotizacion.DataSource = dtViewCotizacion;
                    xmlCotizacion = Comun.Utilitario.Instancia.DatatableToXml(dtViewCotizacion);
                    //MessageBox.Show(xml);

                }
                else
                {
                    dgvCotizacion.DataSource = null;
                }




                conexion_OleDb.Close();

            }}
	        catch (Exception ex)
	        {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
	        }
           

}



        
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Boolean respuesta = false;
         
            try
            {
                string igv = "";
                if (cbxIgv.Text == "18%")
                {
                    igv = "1.18";
                }
                else
                {
                    igv = "1.1";
                }



                String xmlDetalleSinTildes = Utilitario.Instancia.QuitarTildes(xmlDetalle);
                respuesta = clsLogisticaBL.Instancia.ReportesApp_Reporte_Importe_Masivo_Requerimientos(xml, xmlDetalleSinTildes, xmlCotizacion, igv,cbxTipo.Text);

                if (respuesta)
                {
                    txtRuta.Text = "";
                    dgvRequerimientos.DataSource = null;
                    dgvRequerimientosDetalle.DataSource = null;
                    dgvCotizacion.DataSource = null;
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void btnEjemplo_Click(object sender, EventArgs e)
        {
            String plantillaExcel1=  @"\\192.168.4.237\ReportesTranspesa2\imagen\IMPORTAR_MASIVO-COMMODITY-1-A MUCHOS.xlsx";
            String plantillaExcel2 = @"\\192.168.4.237\ReportesTranspesa2\imagen\IMPORTAR_MASIVO-COMMODITY-1A1.xlsx";



            try
            {

                if (System.IO.File.Exists(plantillaExcel1))
                {
                    System.Diagnostics.Process.Start(plantillaExcel1);

                }

                if (System.IO.File.Exists(plantillaExcel2))
                {
                     System.Diagnostics.Process.Start(plantillaExcel2);
                }


            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           


            // Use Path class to manipulate file and directory paths.
           // string destFile = System.IO.Path.Combine(targetPath, fileName);

            try
            {
                if (System.IO.Directory.Exists(CarpetaDestino))
                {


                    OpenFileDialog op = new OpenFileDialog();
                    op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                    op.Title = "Archivo.xlsx";
                    op.InitialDirectory =  @"C:\Users\" + Environment.UserName + @"\Desktop";
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        if (op.FileName != "")
                        {
                            System.IO.File.Copy(op.FileName, @"\\192.168.4.237\ReportesTranspesa2\ImportExcel\"+op.SafeFileName, true);
                            MessageBox.Show("*ARCHIVO COPIADO CORRECTAMENTE*", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                

                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


