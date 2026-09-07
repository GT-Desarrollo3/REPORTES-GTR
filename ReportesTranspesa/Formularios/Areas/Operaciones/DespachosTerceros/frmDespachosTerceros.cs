using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using System.Xml;
using System.IO;
using ReportesTranspesa.Formularios.Areas.Logistica;
using Comun;


namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmDespachosTerceros : Form
    {
        public DataTable DtCompania;
        public DataTable DtProveedor;
        public DataTable DtAccesos;
        DataTable dtListaD;
        public int xClick = 0, yClick = 0;

        public int AccionTipoDoc = 0;

        public frmDespachosTerceros()
        {
            InitializeComponent();
        }

        private void frmControlDocumentos_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 58;

            CargarControles();
        }

        private void CargarControles()
        {
            DataTable dtControl = new DataTable();
            dtControl.Clear();
            dtControl = clsDespachosTercerosBL.Instancia.getDespachosTerceros_LlenarControlesDespachosTerceros(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtControl == null)
            { MessageBox.Show("No se cargaron controles", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                if (dtControl.Rows.Count > 0)
                {
                    if(dtControl.Rows[0][0].ToString() != "") { DtProveedor = ConvertirXmlToDataTable(dtControl.Rows[0][0].ToString()); }
                    if(dtControl.Rows[0][1].ToString() != "") { DtAccesos = ConvertirXmlToDataTable(dtControl.Rows[0][1].ToString()); }
                    
                    tsBtnNuevo.Visible = false;
                    actualizarToolStripMenuItem.Visible = false;
                    anularToolStripMenuItem.Visible = false;

                    if (DtAccesos != null)
                    {
                        if (DtAccesos.Rows.Count > 0)
                        {
                            int Registrar = Convert.ToInt32(DtAccesos.Rows[0][2].ToString());
                            int Actualizar = Convert.ToInt32(DtAccesos.Rows[0][3].ToString());
                            int Anular = Convert.ToInt32(DtAccesos.Rows[0][4].ToString());

                            tsBtnNuevo.Visible = Convert.ToBoolean(Registrar);
                            actualizarToolStripMenuItem.Visible = Convert.ToBoolean(Actualizar);
                            anularToolStripMenuItem.Visible = Convert.ToBoolean(Anular);
                        }
                    }
                }
            }
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
                        txtRuta.Text = op.FileName;
                        btnGenerar.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private DataTable ConvertirXmlToDataTable(string pXml)
        {
            string xml = pXml;
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(xml));
            DataTable Dt = new DataTable(Name);

            try
            {
                XmlNode NodoEstructura = doc.FirstChild.FirstChild;
                //  Table structure (columns definition) 
                foreach (XmlNode columna in NodoEstructura.ChildNodes)
                { Dt.Columns.Add(columna.Name, typeof(String)); }

                XmlNode Filas = doc.FirstChild;
                //  Data Rows 
                foreach (XmlNode Fila in Filas.ChildNodes)
                {
                    List<string> Valores = new List<string>();
                    foreach (XmlNode Columna in Fila.ChildNodes)
                    { Valores.Add(Columna.InnerText); }

                    Dt.Rows.Add(Valores.ToArray());
                }
            }
            catch (Exception) { }

            return Dt;
        }


        private void ListarDespachosTerceros()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            string periodo = dtpPeriodo.Text;
            dt = clsDespachosTercerosBL.Instancia.getDespachosTerceros_Listar(periodo);

            if (dt.Rows.Count > 0)
            {
                dgvTicketsTerceros.DataSource = dt;

                //dgvTicketsTercerosView.Columns["IDDOCUMENTO"].Visible = false;
                //dgvTicketsTercerosView.Columns["SUCURSAL_DESCRIPCION"].Visible = false;
                //dgvTicketsTercerosView.Columns["IdRelacion"].Visible = false;
                //dgvTicketsTercerosView.Columns["TIPO_RELACION"].Visible = false;
                //dgvTicketsTercerosView.Columns["RELACION_CODIGO"].Visible = false;
                //dgvTicketsTercerosView.Columns["TipoDocumento"].Visible = false;
                //dgvTicketsTercerosView.Columns["TIENE_VENCIMIENTO"].Visible = false;
                //dgvTicketsTercerosView.Columns["Marca"].Visible = false;
                //dgvTicketsTercerosView.Columns["TipoVehiculo"].Visible = false;
                //dgvTicketsTercerosView.Columns["Compania"].Visible = false;
                //dgvTicketsTercerosView.Columns["Sucursal"].Visible = false;
                //dgvTicketsTercerosView.Columns["MONEDA"].Visible = false;

                dgvTicketsTercerosView.Columns["IdVehiculo"].Visible = false;
                dgvTicketsTercerosView.Columns["Precio"].Visible = false;

                dgvTicketsTercerosView.Columns["PrecioUnitario"].DisplayFormat.FormatType = FormatType.Numeric;
                dgvTicketsTercerosView.Columns["PrecioUnitario"].DisplayFormat.FormatString = "n2";

                dgvTicketsTercerosView.BestFitColumns();
            }
            else
            {
                dgvTicketsTerceros.DataSource = null;
                MessageBox.Show("No hay data para mostrar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarDespachosTerceros(); }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            frmMantenedorDespachosTerceros frm = new frmMantenedorDespachosTerceros();

            frm._tipo = 1;
            frm.DtProveedor = DtProveedor;
            frm._ticket = 0;
            frm.ShowDialog();

            if (frm.NrRPTA == "0") { ListarDespachosTerceros(); } 
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) { this.Close(); }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Ticket = 0;
            int codigo = 0; string placa = ""; 
            string fechaDespacho = "";
            string hora = ""; 
            decimal cantidad = 0; 
            decimal precio = 0; 
            string chofer = "";
            int notaSalida = 0;
            string Producto = "";
            string Proveedor = "", Lugar = "";
            string usuarioAnula = Utilitario.Instancia.SesionUsuario.usuario;

            foreach(var i in dgvTicketsTercerosView.GetSelectedRows())
            {
                Ticket = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Ticket"].ToString());
                placa = dgvTicketsTercerosView.GetDataRow(i)["Placa"].ToString();
                codigo = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Codigo"].ToString());
                fechaDespacho = dgvTicketsTercerosView.GetDataRow(i)["FechaDespacho"].ToString();
                hora = dgvTicketsTercerosView.GetDataRow(i)["Hora"].ToString();
                cantidad = Convert.ToDecimal(dgvTicketsTercerosView.GetDataRow(i)["Cantidad"].ToString());
                precio = Convert.ToDecimal(dgvTicketsTercerosView.GetDataRow(i)["Precio"].ToString());
                chofer = dgvTicketsTercerosView.GetDataRow(i)["Conductor"].ToString();
                notaSalida = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["NotaSalida"].ToString());
                Producto = dgvTicketsTercerosView.GetDataRow(i)["Producto"].ToString();
                Proveedor = dgvTicketsTercerosView.GetDataRow(i)["Proveedor"].ToString();
                Lugar = dgvTicketsTercerosView.GetDataRow(i)["Lugar"].ToString();
            }

            if (Ticket == 0)
            {
                MessageBox.Show("No se seleccionó ningún ticket", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Desea anular documento?", "ANULAR TICKET TERCERO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string Rpta;
                DataTable dtRpta = clsDespachosTercerosBL.Instancia.getDespachosTerceros_Anular(Ticket, codigo, placa, fechaDespacho, hora, cantidad, precio, chofer, notaSalida, Producto, Proveedor, Lugar, usuarioAnula);
                Rpta = Convert.ToString(dtRpta.Rows[0]["exito"]);
                string NrRPTA = Rpta.Substring(0, 1);

                if (NrRPTA == "0")
                {
                    ListarDespachosTerceros();
                    MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }  
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMantenedorDespachosTerceros frm = new frmMantenedorDespachosTerceros();

            int Ticket = 0;
            frm.DtProveedor = DtProveedor;
            frm._tipo = 2;

            foreach (var i in dgvTicketsTercerosView.GetSelectedRows())
            {
                Ticket = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Ticket"].ToString());
                frm._ticket = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Ticket"].ToString());
                frm._Placa = dgvTicketsTercerosView.GetDataRow(i)["Placa"].ToString();
                frm._idPlaca = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["IdVehiculo"].ToString());
                frm.txtPlaca.Text = dgvTicketsTercerosView.GetDataRow(i)["Placa"].ToString();
                frm.txtCodigo.Text = dgvTicketsTercerosView.GetDataRow(i)["Codigo"].ToString();
                frm.cbxProducto.Text = dgvTicketsTercerosView.GetDataRow(i)["Producto"].ToString();
                frm.cbxProveedor.Text = dgvTicketsTercerosView.GetDataRow(i)["Proveedor"].ToString();
                frm._Proveedor = dgvTicketsTercerosView.GetDataRow(i)["Proveedor"].ToString();

                frm.txtTotalizador.Text = dgvTicketsTercerosView.GetDataRow(i)["Contometro"].ToString();
                frm.dtpFechaDespacho.Text = dgvTicketsTercerosView.GetDataRow(i)["FechaDespacho"].ToString();
                frm.dtpHora.Text = dgvTicketsTercerosView.GetDataRow(i)["Hora"].ToString();
                frm._cantidad = Convert.ToDecimal(dgvTicketsTercerosView.GetDataRow(i)["Cantidad"].ToString());
                frm.txtPrecio.Text = dgvTicketsTercerosView.GetDataRow(i)["Precio"].ToString();
                frm.txtKilometraje.Text = dgvTicketsTercerosView.GetDataRow(i)["Kilometraje"].ToString();
                frm.txtDni.Text = dgvTicketsTercerosView.GetDataRow(i)["Dni"].ToString();
                frm.txtConductor.Text = dgvTicketsTercerosView.GetDataRow(i)["Conductor"].ToString();
   
            }

            if (Ticket == 0)
            {
                MessageBox.Show("No se seleccionó ningún ticket", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frm.ShowDialog();

            if (frm.NrRPTA == "0") { ListarDespachosTerceros(); }
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMantenedorDespachosTerceros frm = new frmMantenedorDespachosTerceros();
            int Ticket = 0;
            frm._tipo = 0;

            frm.DtProveedor = DtProveedor;
            foreach (var i in dgvTicketsTercerosView.GetSelectedRows())
            {
                Ticket = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Ticket"].ToString());
                frm._ticket = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Ticket"].ToString());
                frm._Placa = dgvTicketsTercerosView.GetDataRow(i)["Placa"].ToString();
                frm._idPlaca = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["IdVehiculo"].ToString());
                frm.txtPlaca.Text = dgvTicketsTercerosView.GetDataRow(i)["Placa"].ToString();
                frm.txtCodigo.Text = dgvTicketsTercerosView.GetDataRow(i)["Codigo"].ToString();
                frm.cbxProducto.Text = dgvTicketsTercerosView.GetDataRow(i)["Producto"].ToString();
                frm.cbxProveedor.Text = dgvTicketsTercerosView.GetDataRow(i)["Proveedor"].ToString();
                frm._Proveedor = dgvTicketsTercerosView.GetDataRow(i)["Proveedor"].ToString();
                frm.txtTotalizador.Text = dgvTicketsTercerosView.GetDataRow(i)["Contometro"].ToString();
                frm.dtpFechaDespacho.Text = dgvTicketsTercerosView.GetDataRow(i)["FechaDespacho"].ToString();
                frm.dtpHora.Text = dgvTicketsTercerosView.GetDataRow(i)["Hora"].ToString();
                frm._cantidad = Convert.ToDecimal(dgvTicketsTercerosView.GetDataRow(i)["Cantidad"].ToString());
                frm.txtPrecio.Text = dgvTicketsTercerosView.GetDataRow(i)["Precio"].ToString();
                frm.txtKilometraje.Text = dgvTicketsTercerosView.GetDataRow(i)["Kilometraje"].ToString();
                frm.txtDni.Text = dgvTicketsTercerosView.GetDataRow(i)["Dni"].ToString();
                frm.txtConductor.Text = dgvTicketsTercerosView.GetDataRow(i)["Conductor"].ToString();
            }

            if (Ticket == 0)
            {
                MessageBox.Show("No se seleccionó ningún ticket", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frm.ShowDialog();
        }

        private void dgvTicketsTerceros_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmMantenedorDespachosTerceros frm = new frmMantenedorDespachosTerceros();
            int Ticket = 0;
            frm._tipo = 0;

            frm.DtProveedor = DtProveedor;
            foreach (var i in dgvTicketsTercerosView.GetSelectedRows())
            {
                Ticket = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Ticket"].ToString());
                frm._ticket = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["Ticket"].ToString());
                frm._Placa = dgvTicketsTercerosView.GetDataRow(i)["Placa"].ToString();
                frm._idPlaca = Convert.ToInt32(dgvTicketsTercerosView.GetDataRow(i)["IdVehiculo"].ToString());
                frm.txtPlaca.Text = dgvTicketsTercerosView.GetDataRow(i)["Placa"].ToString();
                frm.txtCodigo.Text = dgvTicketsTercerosView.GetDataRow(i)["Codigo"].ToString();
                frm.cbxProducto.Text = dgvTicketsTercerosView.GetDataRow(i)["Producto"].ToString();
                frm.cbxProveedor.Text = dgvTicketsTercerosView.GetDataRow(i)["Proveedor"].ToString();
                frm._Proveedor = dgvTicketsTercerosView.GetDataRow(i)["Proveedor"].ToString();
                frm._Lugar = dgvTicketsTercerosView.GetDataRow(i)["Lugar"].ToString();

                frm.txtTotalizador.Text = dgvTicketsTercerosView.GetDataRow(i)["Contometro"].ToString();
                frm.dtpFechaDespacho.Text = dgvTicketsTercerosView.GetDataRow(i)["FechaDespacho"].ToString();
                frm.dtpHora.Text = dgvTicketsTercerosView.GetDataRow(i)["Hora"].ToString();
                frm._cantidad = Convert.ToDecimal(dgvTicketsTercerosView.GetDataRow(i)["Cantidad"].ToString());
                frm.txtPrecio.Text = dgvTicketsTercerosView.GetDataRow(i)["Precio"].ToString();
                frm.txtKilometraje.Text = dgvTicketsTercerosView.GetDataRow(i)["Kilometraje"].ToString();
                frm.txtDni.Text = dgvTicketsTercerosView.GetDataRow(i)["Dni"].ToString();
                frm.txtConductor.Text = dgvTicketsTercerosView.GetDataRow(i)["Conductor"].ToString();
            }

            if (Ticket == 0)
            {
                MessageBox.Show("No se seleccionó ningún ticket", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPrecioCombustibleRuta frmPrecioRuta = new frmPrecioCombustibleRuta();
            frmPrecioRuta.Show();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvTicketsTerceros.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Despachos de Tickets Terceros " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dgvTicketsTerceros.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            btnGenerar.Enabled = false;
            pImportarDespachos.Location = new System.Drawing.Point(446, 122);
            pImportarDespachos.Visible = true;
            pImportarDespachos.BringToFront();
        }

        private void pImportarDespachos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pImportarDespachos.Left = pImportarDespachos.Left + (e.X - xClick);
                pImportarDespachos.Top = pImportarDespachos.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            txtRuta.Clear();
            dgvDespachos.DataSource = null;
            pImportarDespachos.Visible = false;
            pImportarDespachos.SendToBack();
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
                    string queryDetalle = String.Format("select * from [{0}$]", "DESPACHOS");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaD = dataSetDetalle.Tables[0];

                    for (int i = dtListaD.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaD.Rows[i]["Placa"] == "" || dtListaD.Rows[i]["IdProveedor"].ToString() == "" || dtListaD.Rows[i]["Proveedor"].ToString() == "" ||
                        dtListaD.Rows[i]["Codigo"].ToString() == "")
                        { dtListaD.Rows[i].Delete(); }
                    }
                }

                if (dtListaD.Rows.Count > 0) { dgvDespachos.DataSource = dtListaD; }
                else { dgvDespachos.DataSource = null; }
            }
            catch (Exception ex)
            {
                btnGenerar.Enabled = false;
                dgvDespachos.DataSource = null;
                txtRuta.Clear();
                MessageBox.Show("El archivo seleccionado no es el correcto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtListaD.Rows.Count > 0)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta = "";
                    int Correcto = 0;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    if (MessageBox.Show("¿Desea registrar estos despachos?", "IMPORTAR DESPACHOS DE TICKETS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dtListaD.Rows.Count; i++)
                        {
                            int IdEmpresa = Convert.ToInt32(dtListaD.Rows[i]["IdProveedor"]);
                            string empresa = Convert.ToString(dtListaD.Rows[i]["Proveedor"]);
                            string producto = Convert.ToString(dtListaD.Rows[i]["Producto"]);
                            string lugar = Convert.ToString(dtListaD.Rows[i]["Lugar"]);
                            string dni = Convert.ToString(dtListaD.Rows[i]["DNI"]);
                            string Conductor = Convert.ToString(dtListaD.Rows[i]["Conductor"]);
                            string FechaDespacho = Convert.ToString(dtListaD.Rows[i]["FechaDespacho"]);
                            string hora = Convert.ToString(dtListaD.Rows[i]["HoraDespacho"]);
                            string placa = Convert.ToString(dtListaD.Rows[i]["Placa"]);
                            string codigo = Convert.ToString(dtListaD.Rows[i]["Codigo"]);
                            decimal kilometraje = Convert.ToDecimal(dtListaD.Rows[i]["Kilometraje"]);
                            decimal cantidad = Convert.ToDecimal(dtListaD.Rows[i]["Cantidad"]);

                            dtRespuesta = clsDespachosTercerosBL.Instancia.getDespachosTerceros_Registrar(codigo, placa, FechaDespacho, hora, cantidad, 0.00M, producto,
                                                                                                          lugar, IdEmpresa, empresa, dni, Conductor, kilometraje, 0, Usuario, 0);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRspta = Respuesta.Substring(0, 1);
                            if (NroRspta == "0") { Correcto = Correcto + 1; }
                            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }

                        if (Correcto == dtListaD.Rows.Count) { MessageBox.Show("0 = Los despachos han sido registrados correctamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        btnCerrar_Click(sender, e);
                        ListarDespachosTerceros();
                    }
                }
                else { MessageBox.Show("El archivo seleccionado no contiene ninguna información.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("Se produjo un error al registrar los despachos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
