using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmListaTarifasOT : Form
    {
        int xClick = 0, yClick = 0;
        DataTable dtListaTarifas;
        String xmlTarifas;
        String CarpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        DataTable dtPermisos = new DataTable();  

        public frmListaTarifasOT()
        {
            InitializeComponent();
        }

        private void frmListaTarifasOT_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaTarifasOT");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnImportar.Enabled = true; }
                else { btnImportar.Enabled = false; }
            }

            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            cbxCliente.Text = "LINDLEY";
            ListarTarifas();
        }


        public void CargarArchivo()
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
                        txtDirectorio.Text = op.FileName;
                        btnGenerar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        public void ListarTarifas()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtListaTarifas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlTarifas_ListarTarifas(txtBuscarRuta.Text, dtpFechaInicio.Text, dtpFechaFin.Text, cbxCliente.Text);
                dtgTarifaOT.DataSource = dtListaTarifas;
                if (dtListaTarifas.Rows.Count > 0)
                {
                    //dgvTarifaOTVista.Columns["IdVehiculo"].Visible = false;
                    dgvTarifaOTVista.Columns["FECHA_CREACION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvTarifaOTVista.Columns["FECHA_CREACION"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvTarifaOTVista.Columns["TARIFA"].Summary.Clear();
                    dgvTarifaOTVista.Columns["TARIFA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TARIFA", "Total = {0}");

                    dgvTarifaOTVista.BestFitColumns();
                }
            }
        }


        private void btnImportar_Click(object sender, EventArgs e)
        {
            pNuevaTarifa.Visible = true;
            cbxMoneda.Text = "SOLES";
            pNuevaTarifa.BringToFront();
            btnGenerar.Enabled = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pNuevaTarifa.Visible = false;
            pNuevaTarifa.SendToBack();
            txtDirectorio.Clear();
            dgvTarifasRuta.DataSource = null;
            btnGenerar.Enabled = false;
        }

        private void pNuevaTarifa_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevaTarifa.Left = pNuevaTarifa.Left + (e.X - xClick);
                pNuevaTarifa.Top = pNuevaTarifa.Top + (e.Y - yClick);
            }
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivo();

                if (System.IO.File.Exists(txtDirectorio.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtDirectorio.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "Tarifas");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaTarifas = dataSetDetalle.Tables[0];

                    for (int i = dtListaTarifas.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaTarifas.Rows[i]["Ruta"] == "") { dtListaTarifas.Rows[i].Delete(); }
                    }
                }

                if (dtListaTarifas.Rows.Count > 0)
                {
                    xmlTarifas = "";
                    dgvTarifasRuta.DataSource = dtListaTarifas;
                    xmlTarifas = Comun.Utilitario.Instancia.DatatableToXml(dtListaTarifas);
                }
                else { dgvTarifasRuta.DataSource = null; }
            }
            catch (Exception ex)
            {
                btnGenerar.Enabled = false;
                dgvTarifasRuta.DataSource = null;
                MessageBox.Show("El archivo seleccionado no es el correcto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta = "";

            try
            {
                string xmlDetalleSinTildes = Utilitario.Instancia.QuitarTildes(xmlTarifas);
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlTarifas_InsertarTarifas(xmlDetalleSinTildes, cbxMoneda.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    txtDirectorio.Clear();
                    dgvTarifasRuta.DataSource = null;
                    btnGenerar.Enabled = false;
                    btnCerrar_Click(sender, e);
                    ListarTarifas();
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex)
            { MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarTarifas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgTarifaOT.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE TARIFAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgTarifaOT.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void txtBuscarRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTarifas(); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTarifas(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTarifas(); }
        }

        private void cbxCliente_DropDownClosed(object sender, EventArgs e) { ListarTarifas(); }
    }
}
