using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using Entidades;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmMantenedorDespachosTerceros : Form
    {
        public DataTable DtProveedor;

        public int _ticket;
        public string _compania;
        public string _sucursal;
        public int _idPlaca;
        public string _Placa;
        public string _fechaDespacho;
        public int _estado;
        public string _usuarioCrea;
        public decimal _cantidad;
        public string _Proveedor, _Lugar;

        public int _tipo;

        public string NrRPTA;

        bool permitir = true;

        public frmMantenedorDespachosTerceros()
        {
            InitializeComponent();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) { this.Close(); }

        private void frmRegistroDocumentos_Load(object sender, EventArgs e)
        {
            //Llenado de Empresas
            cbxProveedor.DataSource = DtProveedor;
            cbxProveedor.DisplayMember = "Proveedor";
            cbxProveedor.ValueMember = "IdProveedor";

            DataTable dtLugarXproveedor = new DataTable();
            dtLugarXproveedor = clsDespachosTercerosBL.Instancia.getDocumento_ListarLugarXproveedor(Convert.ToInt32(cbxProveedor.SelectedValue));
            cbxLugar.DataSource = dtLugarXproveedor;
            cbxLugar.DisplayMember = "Lugar";
            // cbxLugar.ValueMember = "idlugar";
            cbxLugar.SelectedIndex = 0;
            cbxProducto.SelectedIndex = 0;

            ///CargarControles();
            txtPlaca.Text = "";
            txtPlaca.Tag = 0;
            txtCantidad.Text = "0.00";

            DateTime fecha = DateTime.Now;
            dtpFechaDespacho.Value = fecha;

            if (_ticket > 0)
            {
                cargarDatosDocumento();

                if (_tipo == 0)
                {
                    cbxProveedor.Text = _Proveedor;
                    cbxLugar.Text = _Lugar;
                    tsBtnGuardar.Visible = false;
                    cbxProveedor.Enabled = false;
                    cbxProducto.Enabled = false;
                    txtPlaca.Enabled = false;
                    txtCodigo.Enabled = false;
                    txtConductor.Enabled = false;
                    dtpFechaDespacho.Enabled = false;
                    dtpHora.Enabled = false;
                    //txtKilometraje.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtPrecio.Enabled = false;
                    cbxLugar.Enabled = false;
                    txtTotalizador.Enabled = false;
                }
                else { cbxProveedor.SelectedIndex = 0; }     
            }            
        }

        public void cargarDatosDocumento()
        {
            txtPlaca.Tag = _idPlaca;
            txtPlaca.Text = _Placa;
            //txtCodigo.Text = _codigo;
            dtpFechaDespacho.Text = _fechaDespacho;
            txtCantidad.Text = _cantidad.ToString();

            cbxProveedor.Text = _Proveedor;
        }


        private void tsBtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length <= 0)
            {
                MessageBox.Show("Debe ingresar Placa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPlaca.Focus();
                return;
            }

            if(txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar codigo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Focus();
                return;
            }

            if (txtConductor.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar conductor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConductor.Focus();
                return;
            }

            if (txtKilometraje.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar Kilometraje.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtKilometraje.Focus();
                return;
            }

            if (Convert.ToDecimal(txtKilometraje.Text) <= 0)
            {
                MessageBox.Show("Debe ingresar Kilometraje.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtKilometraje.Focus();
                return;
            }
           
            if (txtCantidad.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar cantidad.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCantidad.Focus();
                return;
            }

            if (txtPrecio.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar Precio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Focus();
                return;
            }

            string placa = txtPlaca.Text;
            string codigo = txtCodigo.Text;         
            string fechaDespacho = Convert.ToString(dtpFechaDespacho.Value);
            string chofer = txtConductor.Text;
            string dni = txtDni.Text;
            string producto = cbxProducto.Text;
            string empresa = cbxProveedor.Text;
            int IdEmpresa = Convert.ToInt32(cbxProveedor.SelectedValue);
            decimal kilometraje = Convert.ToDecimal(txtKilometraje.Text);
            decimal cantidad = Convert.ToDecimal(txtCantidad.Text);
            decimal Totalizador = 0;
            int CodigoPreviaje = 0;
            decimal precio = Convert.ToDecimal(txtPrecio.Text);              
            string usuarioCrea = Utilitario.Instancia.SesionUsuario.usuario;

            if (txtCodPreviaje.Text.Length <= 0 && IdEmpresa == 1553)
            {
                MessageBox.Show("Debe ingresar Codigo Previaje.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodPreviaje.Focus();
                return;
            }

            if (txtTotalizador.Text.Length <= 0 && IdEmpresa == 1553)
            {
                MessageBox.Show("Debe ingresar el Totalizador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTotalizador.Focus();
                return;
            }

            if (txtCodPreviaje.Text.Length <= 0) { CodigoPreviaje = 0; }
            else { CodigoPreviaje = Convert.ToInt32(txtCodPreviaje.Text); }

            if (txtTotalizador.Text.Length <= 0) { Totalizador = 0; }
            else { Totalizador = Convert.ToDecimal(txtTotalizador.Text); }

            dtpHora.CustomFormat = "HH:mm:ss";
    
            string Rpta;
            DataTable dtRpta = null;

            if (cantidad <= 0)
            {
                if (MessageBox.Show("¿La cantidad del despacho es 0, desea continuar con el registro...?", "CREAR DESPACHO", MessageBoxButtons.YesNo) == DialogResult.Yes) { }
                else { return; }
            }

            if (_ticket <= 0)
            {
                dtRpta = clsDespachosTercerosBL.Instancia.getDespachosTerceros_Registrar(codigo, placa, dtpFechaDespacho.Text, dtpHora.Text, cantidad, precio, producto,
                                                                                         cbxLugar.Text, IdEmpresa, empresa, dni, chofer, kilometraje,
                                                                                         CodigoPreviaje, usuarioCrea, Totalizador);
            }
            else
            {
                dtRpta = clsDespachosTercerosBL.Instancia.getDespachosTerceros_Actualizar(_ticket, codigo, placa, dtpFechaDespacho.Text, dtpHora.Text, cantidad, precio, producto,
                                                                                         cbxLugar.Text, empresa, dni, chofer, kilometraje,
                                                                                         CodigoPreviaje, usuarioCrea, Totalizador);
            }

            Rpta = Convert.ToString(dtRpta.Rows[0]["exito"]);
            NrRPTA = Rpta.Substring(0, 1);

            if (NrRPTA == "0")
            {
                this.Close();
                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        public bool solonumerosCantidad(int code)
        {
            bool resultado;

            if (code == 46 && (txtCantidad.Text.Contains(".")))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }

        public bool solonumerosOdometro(int code)
        {
            bool resultado;

            if (code == 46 && (txtKilometraje.Text.Contains(".")))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }

        public bool solonumerosPrecio(int code)
        {
            bool resultado;

            if (code == 46 && (txtPrecio.Text.Contains(".")))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }

        public bool solonumeros(int code)
        {
            bool resultado;

            if (code == 46 && (txtPrecio.Text.Contains(".")))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }

        public static void ValidarTextbox(KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)

                e.Handled = false;

            else

                e.Handled = true;
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPlaca.Text.Length > 0)
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {
                    clsVisuales.Instancia.LlenarLw(lstFiltroPlacas, clsDespachosTercerosBL.Instancia.getDocumento_BuscarRelacion(0, txtPlaca.Text), true, false, false);

                    if (lstFiltroPlacas.Items.Count.Equals(0)) { txtPlaca.Focus(); }
                    else
                    {
                        lstFiltroPlacas.Columns[0].Width = 0;
                        lstFiltroPlacas.Columns[1].Width = 60;
                        lstFiltroPlacas.Columns[2].Width = 60;

                        lstFiltroPlacas.Size = new System.Drawing.Size(200, 105);

                        lstFiltroPlacas.BringToFront();
                        lstFiltroPlacas.Visible = true;

                        lstFiltroPlacas.Focus();
                        lstFiltroPlacas.Items[0].Selected = true;
                    }
                }
            }
            else
            {
                lstFiltroPlacas.Visible = false;
                txtPlaca.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstFiltroPlacas.Visible = false;
                txtCodigo.Focus();
            }
        }


        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                txtKilometraje.Focus();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumeros(Convert.ToInt32(e.KeyChar));
            }
        }


        private void dtpFechaDespacho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dtpHora.Focus();
            }
        }


        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    txtProducto.Focus();
            //}

            if (txtConductor.Text.Length > 0)
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {

                    clsVisuales.Instancia.LlenarLw(lstFiltroConductor, clsDespachosTercerosBL.Instancia.getDocumento_BuscarRelacion(1, txtConductor.Text), true, false, false);


                    if (lstFiltroConductor.Items.Count.Equals(0))
                    {
                        txtConductor.Focus();
                    }
                    else
                    {
                        lstFiltroConductor.Columns[0].Width = 0;
                        lstFiltroConductor.Columns[1].Width = 240;
                        lstFiltroConductor.Columns[2].Width = 70;

                        lstFiltroConductor.Size = new System.Drawing.Size(276, 103);

                        lstFiltroConductor.BringToFront();
                        lstFiltroConductor.Visible = true;

                        lstFiltroConductor.Focus();
                        lstFiltroConductor.Items[0].Selected = true;
                    }

                }
            }
            else
            {
                lstFiltroConductor.Visible = false;
                txtConductor.Focus();
            }


            if (e.KeyChar == (char)Keys.Escape)
            {
                lstFiltroConductor.Visible = false;

            }

        }

        private void txtKilometraje_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                txtCantidad.Focus();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumerosOdometro(Convert.ToInt32(e.KeyChar));
            }

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                txtPrecio.Focus();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumerosCantidad(Convert.ToInt32(e.KeyChar));
            }

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                tsBtnGuardar.PerformClick();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumerosPrecio(Convert.ToInt32(e.KeyChar));
            }
        }

        private void txtPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!lstFiltroPlacas.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstFiltroPlacas.SelectedItems[0];

                    DataTable PlacaIns = new DataTable();
                    PlacaIns = clsCombustibleBL.Instancia.ReportesApp_Combustible_InspeccionTanque_BuscarInspeccion(Convert.ToInt32(ItemActual.SubItems[0].Text));

                    if (PlacaIns.Rows.Count > 0)
                    {
                        lblUnidad.Text = PlacaIns.Rows[0]["VEHICULO"].ToString();
                        lblInspeccion.Text = PlacaIns.Rows[0]["TIPO_INSPECCION"].ToString();
                        txtFechaInspeccion.Text = PlacaIns.Rows[0]["FECHA_INSPECCION"].ToString();
                        txtDiasPendientes.Text = PlacaIns.Rows[0]["DIAS_FALTANTES"].ToString();
                        
                        pInsUnidades.Visible = true;
                        pInsUnidades.BringToFront();
                    }
                    else
                    {
                        txtPlaca.Text = ItemActual.SubItems[1].Text;
                        txtPlaca.Tag = ItemActual.SubItems[0].Text;
                    }
                }

                lstFiltroPlacas.Visible = false;
            }
            catch (Exception) { throw; }
        }

        private void lstFiltroPlacas_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (lstFiltroPlacas.SelectedIndices() == true)
                //{
                //    MessageBox.Show("seleccionar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstFiltroPlacas.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstFiltroPlacas.SelectedItems[0];
                    lstFiltroPlacas.Visible = false;

                    DataTable PlacaIns = new DataTable();
                    PlacaIns = clsCombustibleBL.Instancia.ReportesApp_Combustible_InspeccionTanque_BuscarInspeccion(Convert.ToInt32(ItemActual.SubItems[0].Text));

                    if (PlacaIns.Rows.Count > 0)
                    {
                        lblUnidad.Text = PlacaIns.Rows[0]["VEHICULO"].ToString();
                        lblInspeccion.Text = PlacaIns.Rows[0]["TIPO_INSPECCION"].ToString();
                        txtFechaInspeccion.Text = PlacaIns.Rows[0]["FECHA_INSPECCION"].ToString();
                        txtDiasPendientes.Text = PlacaIns.Rows[0]["DIAS_FALTANTES"].ToString();

                        pInsUnidades.Visible = true;
                        pInsUnidades.BringToFront();
                    }
                    else
                    {
                        txtPlaca.Text = ItemActual.SubItems[1].Text;
                        txtPlaca.Tag = ItemActual.SubItems[0].Text;
                        CargarOdometro();
                        txtCodigo.Focus();
                    }
                    
                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lstFiltroPlacas.Visible = false;
                    txtCodigo.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void CargarOdometro()
        {
            DataTable dt = new DataTable();
            dt = clsDespachosTercerosBL.Instancia.ReportesApp_Operaciones_DespachosTerceros_ListarOdometro(txtPlaca.Text, dtpFechaDespacho.Text, dtpHora.Text);

            if (dt != null)
            {
                if (dt.Rows.Count.Equals(0))
                { MessageBox.Show("No se encontró Odometro para esta placa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else
                { txtKilometraje.Text = dt.Rows[0]["ODOMETRO"].ToString(); }
            }
            else
            { MessageBox.Show("No se encontró Odometro para esta placa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void lstFiltroConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (lstFiltroConductor.SelectedIndices() == true)
                //{
                //    MessageBox.Show("seleccionar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstFiltroConductor.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstFiltroConductor.SelectedItems[0];
                    lstFiltroConductor.Visible = false;

                    txtConductor.Text = ItemActual.SubItems[1].Text;
                    txtConductor.Tag = ItemActual.SubItems[0].Text;
                    txtDni.Text = ItemActual.SubItems[2].Text;         

                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lstFiltroConductor.Visible = false;
                    dtpFechaDespacho.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cbxProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cbxProducto.Focus();
            }
        }

        private void cbxProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtConductor.Focus();
            }
        }

        private void lstFiltroPlacas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!lstFiltroPlacas.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstFiltroPlacas.SelectedItems[0];

                    lstFiltroPlacas.Visible = false;
                    txtPlaca.Text = ItemActual.SubItems[1].Text;
                    txtPlaca.Tag = ItemActual.SubItems[0].Text;
                    CargarOdometro();
                }

                lstFiltroPlacas.Visible = false;
                txtCodigo.Focus();

            }
            catch (Exception) { throw; }
        }

        private void lstFiltroConductor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!lstFiltroConductor.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstFiltroConductor.SelectedItems[0];

                    txtConductor.Text = ItemActual.SubItems[1].Text;
                    txtConductor.Tag = ItemActual.SubItems[0].Text;
                    txtDni.Text = ItemActual.SubItems[2].Text;

                }

                lstFiltroConductor.Visible = false;
                dtpFechaDespacho.Focus();

            }
            catch (Exception)
            {

                throw;
            }


        }

        private void dtpHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPlaca.Focus();
            }
        }

        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int IdProveedor = Convert.ToInt32(cbxProveedor.SelectedValue);
                for (int i = 0; i < DtProveedor.Rows.Count; i++)
                {
                    if (IdProveedor == 1553)
                    {
                        //lblPreviaje.Visible = true;
                        //txtCodPreviaje.Visible = true;
                        lblTotalizador.Visible = true;
                        txtTotalizador.Visible = true;
                    }
                    else 
                    {
                        //lblPreviaje.Visible = false;
                        //txtCodPreviaje.Visible = false;
                        lblTotalizador.Visible = false;
                        txtTotalizador.Visible = false;
                    }

                    if (cbxProveedor.Text.ToString() == DtProveedor.Rows[i]["Proveedor"].ToString())
                    {
                        // var_Accesos = Convert.ToInt32(dtTipoProg.Rows[i]["ACCESOS"].ToString());
                        txtPrecio.Text = Convert.ToString(DtProveedor.Rows[i]["Precio"].ToString());
                    }
                }

                DataTable dtLugarXproveedor = new DataTable();
                dtLugarXproveedor = clsDespachosTercerosBL.Instancia.getDocumento_ListarLugarXproveedor(IdProveedor);
                cbxLugar.DataSource = dtLugarXproveedor;
                cbxLugar.DisplayMember = "Lugar";
                // cbxLugar.ValueMember = "idlugar";
                cbxLugar.SelectedIndex = 0;
            }
            catch (Exception)
            {
                
            }
        }

        private void cbxProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cbxProducto.Text == "GASOLINA")
                //{
               
                //    txtKilometraje.Enabled = true;
                //}
                //else
                //{
                  
                //    txtKilometraje.Enabled = false;
                //}
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pInsUnidades.Visible = false;
            pInsUnidades.SendToBack();
            pInsUnidades.Location = new Point(99, 138);

            lblUnidad.Text = "";
            lblInspeccion.Text = "";
            txtFechaInspeccion.Clear();
            txtDiasPendientes.Clear();
        }
    }
}
