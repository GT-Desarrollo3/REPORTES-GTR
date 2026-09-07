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
    public partial class frmRegistroDocumentos : Form
    {
        public DataTable DtCompanias;
        public DataTable DtTipoDocumentos;
        public DataTable DtSucursal;
        public DataTable DtTipoRelacion;
        public DataTable DtTipoMoneda;
        public DataTable DtCategoria;

        public int _IdDocumento;
        public string _compania;
        public string _sucursal;
        public string _tipoRelacion;
        public int _idRelacion;
        public string _RelacionNombre;
        public int _idTipoDocumento;
        public string _codigo;
        public string _centroCosto;
        public string _centroCosto_descripcion;
        public string _situacionRenovacion;
        public string _fechaEmision;
        public string _fechaInicioValidez;
        public string _fechaFinValidez;
        public string _moneda;
        public decimal _montoTotal;
        public int _estado;
        public string _usuarioCrea;
        public bool _esAfectoValidacion;
        public bool _esVencimiento;
        public string _marca;
        public string _tipoVehiculo;
        public string _observacion;
        public string _RutaLocal;
        public string _RutaNube;
        public int _idCategoria;
        public int _tipo;
        public string _Operacion;

        public string NrRPTA;
        bool permitir = true;

        public frmRegistroDocumentos()
        {
            InitializeComponent();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegistroDocumentos_Load(object sender, EventArgs e)
        {
            CargarControles();
            txtRelacion.Text = "";
            txtObservacion.Text = "";
            txtRelacion.Tag = 0;
            txtMonto.Text = "0.00";
            chkEsAfecto.Checked = true;
            chkEsVencimiento.Checked = true;

            if (_IdDocumento > 0)
            {
                cargarDatosDocumento();
            }
        }


        public void cargarDatosDocumento()
        {
            cbxCompania.SelectedValue = _compania;
            cbxSucursal.SelectedValue = _sucursal;
            cbxTipoRelacion.SelectedValue = _tipoRelacion;
            txtRelacion.Tag = _idRelacion;
            txtRelacion.Text = _RelacionNombre;
            cbxTipoDocumento.SelectedValue = _idTipoDocumento;
            txtCodigo.Text = _codigo;
            txtCentroCosto.Text = _centroCosto.ToString();
            txtDescripcionCC.Text = _centroCosto_descripcion;
            dtpFechaEmision.Text = _fechaEmision;
            dtpFechaInicia.Text = _fechaInicioValidez;
            dtpFechaVencimiento.Text = _fechaFinValidez;
            cbxTipoMoneda.SelectedValue = _moneda.ToString();
            txtMonto.Text = _montoTotal.ToString();
            chkEsAfecto.Checked = _esAfectoValidacion;
            chkEsVencimiento.Checked = _esVencimiento;
            txtMarca.Text = _marca;
            txtTipoVehiculo.Text = _tipoVehiculo;
            txtObservacion.Text = _observacion;
            txtRutaLocal.Text = _RutaLocal;
            txtRutaNube.Text = _RutaNube;
            cbxCategoria.SelectedValue = _idCategoria;
            txtOperacion.Text = _Operacion;

            if (cbxTipoRelacion.SelectedIndex == 0)
            {
                txtMarca.Visible = true;
                txtTipoVehiculo.Visible = true;
                lblMarca.Visible = true;
                lblTipoVehiculo.Visible = true;
                btnBuscar.Enabled = true;
                btnCerrarLocal.Enabled = true;
                txtRutaNube.Enabled = true;
            }
            else
            {
                txtMarca.Visible = false;
                txtTipoVehiculo.Visible = false;
                lblMarca.Visible = false;
                lblTipoVehiculo.Visible = false;
                txtRutaNube.ReadOnly = true;
                btnBuscar.Enabled = false;
                btnCerrarLocal.Enabled = false;
            }

            if (_tipo == 2)
            {
                dtpFechaEmision.Enabled = true;
                dtpFechaInicia.Enabled = true;
                dtpFechaVencimiento.Enabled = true;
                chkEsAfecto.Enabled = false;
                chkEsVencimiento.Enabled = false;
                txtRutaNube.Enabled = true;
                btnBuscar.Enabled = true;
                btnCerrarLocal.Enabled = true;
            }
            else if (_tipo == 3)
            {
                dtpFechaEmision.Enabled = true;
                dtpFechaInicia.Enabled = true;
                dtpFechaVencimiento.Enabled = true;
                chkEsAfecto.Enabled = true;
                chkEsVencimiento.Enabled = true;
                txtRutaNube.Enabled = true;
                btnBuscar.Enabled = true;
                btnCerrarLocal.Enabled = true;
            }
            else if (_tipo == 4)
            {              
                cbxCompania.Enabled = false;
                cbxSucursal.Enabled = false;
                cbxTipoRelacion.Enabled = false;
                txtRelacion.Enabled = false;           
                txtCentroCosto.Enabled = false;
                txtDescripcionCC.Enabled = false;
                cbxTipoDocumento.Enabled = false;
                txtCodigo.Enabled = false;
                cbxTipoMoneda.Enabled = false;
                txtMonto.Enabled = false;
                chkEsAfecto.Enabled = false;
                chkEsVencimiento.Enabled = false;
                cbxCategoria.Enabled = false;
                dtpFechaEmision.Enabled = false;
                dtpFechaInicia.Enabled = false;
                dtpFechaVencimiento.Enabled = false;
                txtObservacion.Enabled = false;
                txtRutaNube.ReadOnly = true;
                btnBuscar.Enabled = false;
                btnCerrarLocal.Enabled = false;
            }
        }

        public void CargarControles()
        {
            //Llenado de Empresas
            cbxCompania.DataSource = DtCompanias;
            cbxCompania.DisplayMember = "Compania";
            cbxCompania.ValueMember = "Codigo";
            cbxCompania.SelectedIndex = 0;

            //Llenado de Sucursal 
            cbxSucursal.DataSource = DtSucursal;
            cbxSucursal.DisplayMember = "Descripcion";
            cbxSucursal.ValueMember = "Codigo";
            cbxSucursal.SelectedIndex = 0;

            //Llenado de Tipo de Relacion
            cbxTipoRelacion.DataSource = DtTipoRelacion;
            cbxTipoRelacion.DisplayMember = "Descripcion";
            cbxTipoRelacion.ValueMember = "Codigo";
            cbxTipoRelacion.SelectedIndex = 0;

            //Llenado de Tipo Moneda 
            cbxTipoMoneda.DataSource = DtTipoMoneda;
            cbxTipoMoneda.DisplayMember = "Descripcion";
            cbxTipoMoneda.ValueMember = "Codigo";
            cbxTipoMoneda.SelectedIndex = 0;

            //Llenado de Categoria 
            cbxCategoria.DataSource = DtCategoria;
            cbxCategoria.DisplayMember = "Descripcion";
            cbxCategoria.ValueMember = "IdCategoria";
            cbxCategoria.SelectedIndex = 0;
        }


        private void tsBtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRelacion.Text.Length <= 0)
            {
                MessageBox.Show("Debe ingresar descripción de Relación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRelacion.Focus();
                return;
            }

            if(txtCodigo.Text.Length == 0){
                MessageBox.Show("Debe ingresar codigo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Focus();
                return;
            }
            
            if (txtMonto.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar monto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonto.Focus();
                return;
            }

            if (txtCentroCosto.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar un Centro de Costo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCentroCosto.Focus();
                return;
            }

            string compania = cbxCompania.SelectedValue.ToString();
            string sucursal = cbxSucursal.SelectedValue.ToString();
            string tipoRelacion = cbxTipoRelacion.SelectedValue.ToString();
            int idRelacion = Convert.ToInt32(txtRelacion.Tag);
            int idTipoDocumento = Convert.ToInt32(cbxTipoDocumento.SelectedValue.ToString());
            string codigo = txtCodigo.Text;
            string centroCosto = txtCentroCosto.Text;
            string situacionRenovacion = "VI";
            string fechaEmision = dtpFechaEmision.Text;
            string fechaInicioValidez = dtpFechaInicia.Text;
            string fechaFinValidez = dtpFechaVencimiento.Text;
            string moneda = cbxTipoMoneda.SelectedValue.ToString();
            decimal montoTotal = Convert.ToDecimal(txtMonto.Text);
            string usuarioCrea = Utilitario.Instancia.SesionUsuario.usuario;
            string observacion = txtObservacion.Text;
            string RutaLocal = txtRutaLocal.Text;
            string RutaNube = txtRutaNube.Text;
            string categoria = "0";

            if (cbxTipoRelacion.SelectedIndex == 0)
            {
                categoria = "0";
            }
            else
            {
                if (cbxTipoDocumento.SelectedValue.ToString() == "22")
                {
                    categoria = cbxCategoria.SelectedValue.ToString();
                }
                else
                {
                    categoria = "0";
                }
            }

            int esAfectoValidacion = 1;
            if (chkEsAfecto.Checked == true)
            {
                esAfectoValidacion = 1;
            }
            else
            {
                esAfectoValidacion = 0;
            }

            int esVencimiento = 1;
            if (chkEsVencimiento.Checked == true)
            {
                esVencimiento = 1;
            }
            else
            {
                esVencimiento = 0;
            }
            
            string Rpta;
            DataTable dtRpta = null;

            if (_IdDocumento <= 0)
            {
                dtRpta = clsControlDocumentosBL.Instancia.getDocumento_RegistrarDocumento(compania, sucursal, tipoRelacion, idRelacion, idTipoDocumento, codigo, centroCosto,
                                                                                          situacionRenovacion, fechaEmision, fechaInicioValidez, fechaFinValidez, esVencimiento,
                                                                                          moneda, montoTotal, esAfectoValidacion, usuarioCrea, observacion, categoria, RutaLocal, RutaNube);
            }
            else
            {
                if (_tipo == 2)
                {
                    dtRpta = clsControlDocumentosBL.Instancia.getDocumento_ActualizarDocumento(_IdDocumento, compania, sucursal, tipoRelacion, idRelacion, idTipoDocumento, codigo, centroCosto,
                                                                                          situacionRenovacion, fechaEmision, fechaInicioValidez, fechaFinValidez, esVencimiento,
                                                                                          moneda, montoTotal, esAfectoValidacion, usuarioCrea, observacion, categoria, RutaLocal, RutaNube);
                }
                else if (_tipo == 3)
                {
                    dtRpta = clsControlDocumentosBL.Instancia.getDocumento_ActualizarDocumento(_IdDocumento, compania, sucursal, tipoRelacion, idRelacion, idTipoDocumento, codigo, centroCosto,
                                                                                          situacionRenovacion, fechaEmision, fechaInicioValidez, fechaFinValidez, esVencimiento,
                                                                                          moneda, montoTotal, esAfectoValidacion, usuarioCrea, observacion, categoria, RutaLocal, RutaNube);
                }
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

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                chkEsAfecto.Focus();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumeros(Convert.ToInt32(e.KeyChar)); 
            }
        }

        public bool solonumeros(int code)
        {
            bool resultado;

            if (code == 46 && txtMonto.Text.Contains(".") && txtMonto.Text.Contains(".") )//se evalua si es punto y si es punto se revisa si ya existe en el textbox
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

        private void txtRelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtRelacion.Text.Length > 0)
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {
                    clsVisuales.Instancia.LlenarLw(lstFiltroRelacion, clsControlDocumentosBL.Instancia.getDocumento_BuscarRelacion(cbxTipoRelacion.SelectedIndex, txtRelacion.Text), true, false, false);

                    lstFiltroRelacion.Columns[0].Width = 0;
                    lstFiltroRelacion.Columns[1].Width = 240;
                    lstFiltroRelacion.Columns[2].Width = 70;

                    lstFiltroRelacion.Size = new System.Drawing.Size(339, 103);

                    lstFiltroRelacion.BringToFront();
                    lstFiltroRelacion.Visible = true;

                    if (lstFiltroRelacion.Items.Count.Equals(0))
                    {
                        txtRelacion.Focus();
                    }
                    else
                    {
                        lstFiltroRelacion.Focus();
                        lstFiltroRelacion.Items[0].Selected = true;
                    }
                }
            }
            else
            {
                lstFiltroRelacion.Visible = false;
                txtRelacion.Focus();
            }
            
            if (e.KeyChar == (char)Keys.Escape)
            {
                lstFiltroRelacion.Visible = false;
                cbxTipoDocumento.Focus();

            }
        }

        private void lstFiltroRelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (lstFiltroRelacion.SelectedIndices() == true)
                //{
                //    MessageBox.Show("seleccionar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstFiltroRelacion.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstFiltroRelacion.SelectedItems[0];

                    txtRelacion.Text = ItemActual.SubItems[1].Text;
                    txtRelacion.Tag = ItemActual.SubItems[0].Text;

                    int tipoRelacion = Convert.ToInt32(cbxTipoRelacion.SelectedIndex.ToString());

                    if (tipoRelacion == 0)
                    {
                        txtMarca.Text =  ItemActual.SubItems[3].Text;
                        txtCentroCosto.Text = ItemActual.SubItems[4].Text;
                        txtDescripcionCC.Text = ItemActual.SubItems[5].Text;
                        txtTipoVehiculo.Text = ItemActual.SubItems[6].Text;
                    }

                    if (tipoRelacion == 1)
                    {
                        txtCentroCosto.Text = ItemActual.SubItems[3].Text;
                        txtDescripcionCC.Text = ItemActual.SubItems[4].Text;
                        txtOperacion.Text = ItemActual.SubItems[5].Text;
                    }

                    lstFiltroRelacion.Visible = false;
                    cbxTipoDocumento.Focus();
                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lstFiltroRelacion.Visible = false;
                    cbxTipoDocumento.Focus();
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.ToString());
            }
        }

        public void cbxTipoRelacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRelacion.Text = "";
            txtRelacion.Focus();

            txtMarca.Text = "";
            txtTipoVehiculo.Text = "";
            txtCentroCosto.Text = "";
            txtDescripcionCC.Text = "";

            if (cbxTipoRelacion.Text != "CONDUCTOR")
            {
                CargarTipoDocumento("VH");
                lblRelacion.Text = "Descripción Relación UNIDAD:";

                txtMarca.Visible = true;
                txtTipoVehiculo.Visible = true;
                lblMarca.Visible = true;
                lblTipoVehiculo.Visible = true;
                cbxSucursal.Visible = true;
                label10.Visible = true;
            }
            else
            {
                CargarTipoDocumento("CD");
                lblRelacion.Text = "Descripción Relación CONDUCTOR:";
                txtMarca.Visible = false;
                txtTipoVehiculo.Visible = false;
                lblMarca.Visible = false;
                lblTipoVehiculo.Visible = false;
                cbxSucursal.Visible = false;
                label10.Visible = false;
            }
        }

        public void CargarTipoDocumento(string TipoRelacion)
        {
            DataRow[] resultRow = DtTipoDocumentos.Select("TipoRelacion ='" + TipoRelacion + "'");
            DataTable DtTipoDocumento = resultRow.CopyToDataTable();

            //Llenado de TiposDocumentos 
            cbxTipoDocumento.DataSource = DtTipoDocumento;
            cbxTipoDocumento.DisplayMember = "Descripcion";
            cbxTipoDocumento.ValueMember = "IdTipoDocumento";
            cbxTipoDocumento.SelectedIndex = 0;
        }

        private void txtCentroCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstCentroCosto, clsControlDocumentosBL.Instancia.getDocumento_BuscarCentroCosto(txtCentroCosto.Text), true, false, false);

                lstCentroCosto.Columns[0].Width = 80;
                lstCentroCosto.Columns[1].Width = 240;
                lstCentroCosto.Size = new System.Drawing.Size(339, 103);
                lstCentroCosto.BringToFront();
                lstCentroCosto.Visible = true;
                lstCentroCosto.Focus();

                if (lstCentroCosto.Items.Count.Equals(0))
                {
                    txtCentroCosto.Focus();
                }
                else
                {
                    lstCentroCosto.Focus();
                    lstCentroCosto.Items[0].Selected = true;
                }
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstCentroCosto.Visible = false;
                txtCentroCosto.Focus();
            }
        }

        private void lstCentroCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstCentroCosto.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstCentroCosto.SelectedItems[0];

                    txtCentroCosto.Text = ItemActual.SubItems[0].Text;
                    txtDescripcionCC.Text = ItemActual.SubItems[1].Text;

                    lstCentroCosto.Visible = false;
                    cbxTipoMoneda.Focus();
                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lstCentroCosto.Visible = false;
                    txtCentroCosto.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void lstCentroCosto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!lstCentroCosto.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstCentroCosto.SelectedItems[0];

                    txtCentroCosto.Text = ItemActual.SubItems[0].Text;
                    txtDescripcionCC.Text = ItemActual.SubItems[1].Text;

                    lstCentroCosto.Visible = false;
                    cbxTipoMoneda.Focus();
                }
                else
                {
                    lstCentroCosto.Visible = false;
                    txtCentroCosto.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cbxCompania_KeyPress(object sender, KeyPressEventArgs e)
        {         
            if (e.KeyChar == (char)Keys.Enter)
            {
               cbxSucursal.Focus();
            }
        }

        private void cbxSucursal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cbxTipoRelacion.Focus();
            } 
        }

        private void cbxTipoRelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtRelacion.Focus();
            } 
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cbxTipoMoneda.Focus();
            } 
        }

        private void dtpFechaEmision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dtpFechaInicia.Focus();
            } 
        }

        private void dtpFechaInicia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dtpFechaVencimiento.Focus();
            } 
        }

        private void dtpFechaVencimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                chkEsVencimiento.Focus();
            } 
        }

        private void chkEsVencimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtObservacion.Focus();
            } 
        }

        private void chkEsAfecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dtpFechaEmision.Focus();
            } 
        }

        private void txtRelacion_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            try
            {
                if (!lstFiltroRelacion.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstFiltroRelacion.SelectedItems[0];

                    txtRelacion.Text = ItemActual.SubItems[1].Text;
                    txtRelacion.Tag = ItemActual.SubItems[0].Text;

                    int tipoRelacion = Convert.ToInt32(cbxTipoRelacion.SelectedIndex.ToString());

                    if (tipoRelacion == 0)
                    {
                        txtMarca.Text = ItemActual.SubItems[3].Text;
                        txtCentroCosto.Text = ItemActual.SubItems[4].Text;
                        txtDescripcionCC.Text = ItemActual.SubItems[5].Text;
                        txtTipoVehiculo.Text = ItemActual.SubItems[6].Text;
                    }

                    if (tipoRelacion == 1)
                    {
                        txtCentroCosto.Text = ItemActual.SubItems[3].Text;
                        txtDescripcionCC.Text = ItemActual.SubItems[4].Text;
                    }

                    lstFiltroRelacion.Visible = false;
                    cbxTipoDocumento.Focus();

                }
                else
                {
                    lstFiltroRelacion.Visible = false;
                    cbxTipoDocumento.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                tsBtnGuardar.PerformClick();
            } 
        }

        private void lstFiltroRelacion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!lstCentroCosto.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;

                    ItemActual = lstFiltroRelacion.SelectedItems[0];

                    txtRelacion.Text = ItemActual.SubItems[1].Text;
                    txtRelacion.Tag = ItemActual.SubItems[0].Text;

                    int tipoRelacion = Convert.ToInt32(cbxTipoRelacion.SelectedIndex.ToString());

                    if (tipoRelacion == 0)
                    {
                        txtMarca.Text = ItemActual.SubItems[3].Text;
                        txtCentroCosto.Text = ItemActual.SubItems[4].Text;
                        txtDescripcionCC.Text = ItemActual.SubItems[5].Text;
                        txtTipoVehiculo.Text = ItemActual.SubItems[6].Text;
                    }

                    if (tipoRelacion == 1)
                    {
                        txtCentroCosto.Text = ItemActual.SubItems[3].Text;
                        txtDescripcionCC.Text = ItemActual.SubItems[4].Text;
                    }

                    lstFiltroRelacion.Visible = false;
                    cbxTipoDocumento.Focus();
                }
                else
                {
                    lstFiltroRelacion.Visible = false;
                    txtRelacion.Focus();
                }         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void cbxTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTipoDocumento.SelectedValue.ToString() == "22")
            {
                lblCategoria.Visible = true;
                cbxCategoria.Visible = true;
            }
            else
            {
                lblCategoria.Visible = false;
                cbxCategoria.Visible = false;
            }
        }

        //GERARDO - 01/09
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtRutaLocal.Clear();
                        txtRutaLocal.Text = "file:///" + nuevoEnlace;
                        txtObservacion.Focus();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            txtRutaLocal.Clear();
        }

        private void txtRutaLocal_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
            catch
            {
                MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRutaNube_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
            catch
            {
                MessageBox.Show("No se puede abrir la página.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRutaNube_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(txtRutaNube.Text);
            }
            catch
            {
                MessageBox.Show("No se puede abrir la página.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRutaNube_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {  
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Cortar");
                menuItem.Click += new EventHandler(Cortar);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copiar");
                menuItem.Click += new EventHandler(Copiar);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Pegar");
                menuItem.Click += new EventHandler(Pegar);
                contextMenu.MenuItems.Add(menuItem);

                txtRutaNube.ContextMenu = contextMenu;
            }
        }

        private void txtRutaNube_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                txtObservacion.Focus();
            }
        } 

        void Cortar(object sender, EventArgs e)
        {
            txtRutaNube.Cut();
        }

        void Copiar(object sender, EventArgs e)
        {
            Clipboard.SetText(txtRutaNube.SelectedText);
        }

        void Pegar(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                txtRutaNube.Clear();
                txtRutaNube.Text += Clipboard.GetText(TextDataFormat.Text).ToString();
            }
        }
        //GERARDO - 01/09
    }
}
