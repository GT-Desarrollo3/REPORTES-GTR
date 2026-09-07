using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
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
using Negocio;
using System.Xml;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmPrecioCombustibleRuta : Form
    {
        public DataTable DtProveedor, dtBuscar, dtLugarXproveedor, dtLugarXproveedor2;

        public frmPrecioCombustibleRuta()
        {
            InitializeComponent();
        }

        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdProveedor = Convert.ToInt32(cbxProveedor2.SelectedValue);
                dtLugarXproveedor = clsDespachosTercerosBL.Instancia.getDocumento_ListarLugarXproveedor(IdProveedor);
                cbxLugar2.DataSource = dtLugarXproveedor;
                cbxLugar2.DisplayMember = "Lugar";
                cbxLugar2.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        private void cbxProveedor1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdProveedor = Convert.ToInt32(cbxProveedor1.SelectedValue);
                dtLugarXproveedor2 = clsDespachosTercerosBL.Instancia.getDocumento_ListarLugarXproveedor(IdProveedor);
                cbxLugar1.DataSource = dtLugarXproveedor2;
                cbxLugar1.DisplayMember = "Lugar";
                cbxLugar1.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        private void frmPrecioCombustibleRuta_Load(object sender, EventArgs e)
        {
            CargarControles();

            //Llenado de Empresas
            cbxProveedor2.DataSource = DtProveedor;
            cbxProveedor2.DisplayMember = "Proveedor";
            cbxProveedor2.ValueMember = "IdProveedor";
            cbxProveedor2.SelectedIndex = 0;

            cbxProveedor1.DataSource = dtBuscar;
            cbxProveedor1.DisplayMember = "Proveedor";
            cbxProveedor1.ValueMember = "IdProveedor";
            cbxProveedor1.SelectedIndex = 0;

            dtLugarXproveedor = clsDespachosTercerosBL.Instancia.getDocumento_ListarLugarXproveedor(Convert.ToInt32(cbxProveedor1.SelectedValue));
            dtLugarXproveedor2 = clsDespachosTercerosBL.Instancia.getDocumento_ListarLugarXproveedor(Convert.ToInt32(cbxProveedor2.SelectedValue));

            cbxLugar2.DataSource = dtLugarXproveedor2;
            cbxLugar2.DisplayMember = "Lugar";
            cbxLugar2.SelectedIndex = 0;

            cbxLugar1.DataSource = dtLugarXproveedor;
            cbxLugar1.DisplayMember = "Lugar";
            cbxLugar1.SelectedIndex = 0;

            cbxProducto.Text = "PETROLEO";
        }


        private void CargarControles()
        {
            DataTable dtControl = new DataTable();
            dtControl.Clear();
            dtControl = clsDespachosTercerosBL.Instancia.getDespachosTerceros_LlenarControlesDespachosTerceros(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtControl == null)
            {
                MessageBox.Show("No se cargaron controles.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (dtControl.Rows.Count > 0)
                {
                    DtProveedor = ConvertirXmlToDataTable(dtControl.Rows[0][0].ToString());
                    dtBuscar = ConvertirXmlToDataTable(dtControl.Rows[0][0].ToString());
                }
            }
        }

        void ListarPrecios() 
        {
            DataTable dtPrecio = new DataTable();

            dtPrecio = clsLogisticaBL.Instancia.GetListarPreciosCombustibleTerceros(Convert.ToInt32(cbxProveedor2.SelectedValue),cbxLugar2.Text);
            if (dtPrecio.Rows.Count > 0)
            {
                gridControl1.DataSource = dtPrecio;

                gridView1.Columns["IdCliente"].Visible = false;

                gridView1.Columns["Precio_IGV"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView1.Columns["Precio_IGV"].DisplayFormat.FormatString = "n2";

                gridView1.BestFitColumns();
            }
            else 
            {
                gridControl1.DataSource = null;
            }
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
                {
                    Dt.Columns.Add(columna.Name, typeof(String));
                }

                XmlNode Filas = doc.FirstChild;
                //  Data Rows 
                foreach (XmlNode Fila in Filas.ChildNodes)
                {
                    List<string> Valores = new List<string>();
                    foreach (XmlNode Columna in Fila.ChildNodes)
                    {
                        Valores.Add(Columna.InnerText);
                    }
                    Dt.Rows.Add(Valores.ToArray());
                }
            }
            catch (Exception) { }
            return Dt;
        }

        private void Eliminar()
        {
            int IdCliente = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IdCliente"));
            DateTime IdFecha = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IdFecha"));
            string Lugar = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Lugar"));
            string Producto = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Producto"));

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsLogisticaBL.Instancia.GetListarPreciosCombustibleTerceros_Registrar(3, IdFecha.ToString(), IdCliente, 0, Lugar, Producto);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarPrecios();
            }
            else
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (btnModificar.Visible == false)
                {
                    btnNuevo_Click(sender, e);
                }
                else
                {
                    button2_Click(sender, e);
                }
            }
        }

        private void dtpFechaRegistro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPrecio.Focus();
            }
        }

        private void cbxProveedor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void cbxLugar2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarPrecios();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (txtPrecio.Text.Length == 0 || cbxLugar1.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPrecio.Text.Length == 0)
                {
                    txtPrecio.Focus();
                }
                else
                {
                    cbxLugar1.Focus();
                }
                return;
            }

            decimal precio;
            precio = Convert.ToDecimal(txtPrecio.Text);

            string Rpta;
            string NrRPTA;
            DataTable dt = new DataTable();
            dt = clsLogisticaBL.Instancia.GetListarPreciosCombustibleTerceros_Registrar(1, dtpFechaRegistro.Text, Convert.ToInt32(cbxProveedor1.SelectedValue), precio, cbxLugar1.Text, cbxProducto.Text);
            Rpta = Convert.ToString(dt.Rows[0]["exito"]);
            NrRPTA = Rpta.Substring(0, 1);

            if (NrRPTA == "0")
            {                
                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxProveedor2.SelectedValue = cbxProveedor1.SelectedValue;
                cbxLugar2.Text = cbxLugar1.Text;
                ListarPrecios();
            }
            else
            {
                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPrecio.Text.Length == 0 || cbxLugar1.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPrecio.Text.Length == 0)
                {
                    txtPrecio.Focus();
                }
                else
                {
                    cbxLugar1.Focus();
                }
                return;
            }

            decimal precio;
            precio = Convert.ToDecimal(txtPrecio.Text);

            string Rpta;
            string NrRPTA;
            DataTable dt = new DataTable();
            dt = clsLogisticaBL.Instancia.GetListarPreciosCombustibleTerceros_Registrar(2, dtpFechaRegistro.Text, Convert.ToInt32(cbxProveedor1.SelectedValue), precio, cbxLugar1.Text, cbxProducto.Text);
            Rpta = Convert.ToString(dt.Rows[0]["exito"]);
            NrRPTA = Rpta.Substring(0, 1);

            if (NrRPTA == "0")
            {
                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxProveedor2.SelectedValue = cbxProveedor1.SelectedValue;
                cbxLugar2.Text = cbxLugar1.Text;
                ListarPrecios();
                btnModificar.Visible = false;
                cbxProveedor1.SelectedIndex = 0;
                cbxLugar1.SelectedIndex = 0;
                dtpFechaRegistro.Value = DateTime.Now;
                txtPrecio.Clear();
                modificarToolStripMenuItem.Text = "Modificar";
            }
            else
            {
                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnModificar.Visible = false;
                cbxProveedor1.SelectedIndex = 0;
                cbxLugar1.SelectedIndex = 0;
                dtpFechaRegistro.Value = DateTime.Now;
                txtPrecio.Clear();
                modificarToolStripMenuItem.Text = "Modificar";
                return;
            }
        }

        private void modificarToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (btnModificar.Visible == false)
                {
                    modificarToolStripMenuItem.Text = "Insertar";
                    btnModificar.Visible = true;
                    cbxProveedor1.SelectedValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IdCliente").ToString();
                    cbxLugar1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Lugar").ToString();
                    dtpFechaRegistro.Value = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IdFecha"));
                    txtPrecio.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PrecioUnitario").ToString();

                }
                else
                {
                    modificarToolStripMenuItem.Text = "Modificar";
                    btnModificar.Visible = false;
                    cbxProveedor1.SelectedIndex = 0;
                    cbxLugar1.SelectedIndex = 0;
                    dtpFechaRegistro.Value = DateTime.Now;
                    txtPrecio.Clear();
                }
            }
            catch
            {
                MessageBox.Show("El registro seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                modificarToolStripMenuItem.Text = "Modificar";
                btnModificar.Visible = false;
                cbxProveedor1.SelectedIndex = 0;
                cbxLugar1.SelectedIndex = 0;
                dtpFechaRegistro.Value = DateTime.Now;
                txtPrecio.Clear();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Eliminar();
            }
            catch
            {
                MessageBox.Show("El registro seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
