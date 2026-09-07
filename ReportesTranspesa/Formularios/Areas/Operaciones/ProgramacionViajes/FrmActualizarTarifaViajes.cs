using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Entidades;
using Negocio;
using System.Drawing.Printing;
using ReportesTranspesa.Properties;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class FrmActualizarTarifaViajes : Form
    {
        DataTable dtListaTarifas;
        string xmlTarifas = string.Empty;

        public FrmActualizarTarifaViajes()
        {
            InitializeComponent();
        }

        private void FrmActualizarTarifaViajes_Load(object sender, EventArgs e)
        {

        }


        public void CargarArchivo()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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

                    if (!dtListaTarifas.Columns.Contains("VIAJE") || !dtListaTarifas.Columns.Contains("OT") || !dtListaTarifas.Columns.Contains("NUEVOPESO") || !dtListaTarifas.Columns.Contains("NUEVAOT"))
                    {
                        MessageBox.Show("Excel no contiene las columnas necesarias.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int i = dtListaTarifas.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaTarifas.Rows[i]["VIAJE"].ToString() == "")  { dtListaTarifas.Rows[i].Delete(); }
                    }
                }

                if (dtListaTarifas.Rows.Count > 0)
                {
                    xmlTarifas = "";
                    dtgvData.DataSource = dtListaTarifas;
                    dtgvDataView.BestFitColumns();
                    btnActualizar.Enabled = true;
                    xmlTarifas = Comun.Utilitario.Instancia.DatatableToXml(dtListaTarifas);
                }
                else { dtgvData.DataSource = null; btnActualizar.Enabled = false; }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (xmlTarifas.Length > 0)
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarDatosViajesPorFecha_Viaje(xmlTarifas))
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
                else { MessageBox.Show("No hay datos importados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string RUTA = @"\\192.168.4.200\temporal\FormatoActualizarTarifa.xlsx";

                if (System.IO.File.Exists(RUTA)) { System.Diagnostics.Process.Start(RUTA); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsignarGrupo_Click(object sender, EventArgs e)
        {
            frmActualizarDatosViajes frmActualizarDatosViajes = new frmActualizarDatosViajes();
            frmActualizarDatosViajes.ShowDialog();
        }
    }
}
