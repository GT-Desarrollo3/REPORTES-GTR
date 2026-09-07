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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmRegistroKM : Form
    {
        public frmListaMantenimiento frmListaMantenimiento = new frmListaMantenimiento();
        int xClick = 0, yClick = 0;
        public int idVehiculo = 0;
        String CarpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Downloads");
        DataTable dtListaKM;
        String xmlKilometrajes;
        DataTable dtPermisos = new DataTable();  

        public frmRegistroKM()
        {
            InitializeComponent();
        }

        private void frmRegistroKM_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaMantenimiento");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    btnActualizar.Enabled = true;
                    btnAgregar.Enabled = true;
                    btnGuardar.Enabled = true;
                }
                else
                {
                    btnActualizar.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnGuardar.Enabled = false;
                }
            }
            
            btnGenerar.Enabled = false;
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
            dtpFecha.Value = DateTime.Now;
            ListarRegistroKM();
        }


        public void ListarRegistroKM()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtListaKM = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKM(txtPlaca.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgRegistroKM.DataSource = dtListaKM;
                if (dtListaKM.Rows.Count > 0)
                {
                    dgvRegistroKMVista.Columns["IdVehiculo"].Visible = false;
                    dgvRegistroKMVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroKMVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvRegistroKMVista.BestFitColumns();
                }
            }
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
                        txtRuta.Text = op.FileName;
                        btnGenerar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRegistroKM(); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRegistroKM(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRegistroKM(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarRegistroKM(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgRegistroKM.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DIARIO DE KM - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgRegistroKM.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            pActualizar.Visible = true;
            pActualizar.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pActualizar.Visible = false;
            pActualizar.SendToBack();
            txtBuscarPlaca.Clear();
            idVehiculo = 0;
            dtpFecha.Value = DateTime.Now;
        }

        private void pActualizar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pActualizar.Left = pActualizar.Left + (e.X - xClick);
                pActualizar.Top = pActualizar.Top + (e.Y - yClick);
            }
        }

        private void txtBuscarPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos(txtBuscarPlaca.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 100;
            lstPlaca.Columns[2].Width = 100;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                txtBuscarPlaca.Focus();
                lstPlaca.SendToBack();
                idVehiculo = 0;
            }
        }

        private void txtBuscarPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            if (!lstPlaca.Items.Count.Equals(0)) { lstPlaca.Items[0].Selected = true; }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlaca.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlaca.SelectedItems[0];

                idVehiculo = Int32.Parse(ItemActual.Text);
                txtBuscarPlaca.Text = ItemActual.SubItems[1].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                dtpFecha.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                txtBuscarPlaca.Focus();
                idVehiculo = 0;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idVehiculo = Int32.Parse(ItemActual.Text);
            txtBuscarPlaca.Text = ItemActual.SubItems[1].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            dtpFecha.Focus();
        }

        private void txtUltKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtBuscarPlaca.Text.Length == 0 || txtUltKM.Text.Length == 0)
            {
                if (txtBuscarPlaca.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese la placa de la unidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscarPlaca.Focus();
                }
                else
                {
                    MessageBox.Show("El KM de la unidad no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUltKM.Focus();
                }
                return;
            }
            else
            {
                DataTable dtActualizar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtActualizar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMUnidad(idVehiculo, dtpFecha.Value, Convert.ToDecimal(txtUltKM.Text));
                respta = Convert.ToString(dtActualizar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pictureBox1_Click(sender, e);
                    ListarRegistroKM();
                    frmListaMantenimiento.ListarMantenimientos();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgRegistroKM_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                idVehiculo = Convert.ToInt32(dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "IdVehiculo"));
                txtBuscarPlaca.Text = dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "PLACA").ToString();
                dtpFecha.Value = Convert.ToDateTime(dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "FECHA"));
                txtUltKM.Text = dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "KM_REAL").ToString();
                pActualizar.Visible = true;
                pActualizar.BringToFront();
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pImportarKM.Visible = false;
            pImportarKM.SendToBack();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pImportarKM.Visible = true;
            pImportarKM.BringToFront();
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivo();

                if (System.IO.File.Exists(txtRuta.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtRuta.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "IMPORTAR");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaKM = dataSetDetalle.Tables[0];

                    for (int i = dtListaKM.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaKM.Rows[i]["Placa"] == "0") { dtListaKM.Rows[i].Delete(); }
                    }
                }

                if (dtListaKM.Rows.Count > 0)
                {
                    xmlKilometrajes = "";
                    dgvKilometraje.DataSource = dtListaKM;
                    xmlKilometrajes = Comun.Utilitario.Instancia.DatatableToXml(dtListaKM);
                }
                else
                { dgvKilometraje.DataSource = null; }
            }
            catch (Exception ex)
            {
                btnGenerar.Enabled = false;
                dgvKilometraje.DataSource = null;
                MessageBox.Show("El archivo seleccionado no es el correcto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta = "";

            try
            {
                string xmlDetalleSinTildes = Utilitario.Instancia.QuitarTildes(xmlKilometrajes);
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_InsertarKMUnidad(xmlDetalleSinTildes, Usuario);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    txtRuta.Clear();
                    dgvKilometraje.DataSource = null;
                    btnGenerar.Enabled = false;
                    ListarRegistroKM();
                    btnCerrar2_Click(sender, e);
                    frmListaMantenimiento.ListarMantenimientos();
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
