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
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportesTranspesa.Sistema;
using System.Globalization;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.MaestroUnidadConductor
{
    public partial class frmNuevaUnidadConductor : Form

    {
        int IdConductor, _Accion;
        int idtracto, idCarreta, _mochila, _tomafuerza, _urea, _manguera, _Senaletica, _LlaveOriginal, _LlaveDuplicada, _Camaras;
        public string TipoUnidad = "";
        int _ver1Crear2 =0;
        int _nro;
        int _idunidad=0, _idOperacion=0;
        string _placa;
        string  _Operacion;
        string _Observacion;
        string _Transmision;
        string _Bitacora, _Bocamaza;
        decimal _Peso, _Galones; 

        public frmNuevaUnidadConductor()
        {
            InitializeComponent();
        }

        public void DatosEDITAR(int creaModifica,int Accion, int nro, int IdOperacion, string Operacion, int IdUnidad, string Unidad, string Observacion, int mochila,
                                int tomafuerza, int urea, int manguera, int Senaletica, int LlaveOriginal, int LlaveDuplicada, int Camaras, string Transmision, decimal Peso,
                                decimal Galones, string Bitacora, string Bocamaza)
        {
            _ver1Crear2 = creaModifica;
            _Accion = Accion;
            _nro = nro;
            idtracto = IdUnidad;
            _placa = Unidad;          
            _Operacion = Operacion;
            _Observacion = Observacion;
            _idOperacion = IdOperacion;
            _mochila = mochila;
            _tomafuerza = tomafuerza;
            _urea = urea;
            _manguera = manguera;
            _Senaletica = Senaletica;
            _LlaveOriginal = LlaveOriginal;
            _LlaveDuplicada = LlaveDuplicada;
            _Camaras = Camaras;
            _Transmision = Transmision;
            _Peso = Peso;
            _Galones = Galones;
            _Bitacora = Bitacora;
            _Bocamaza = Bocamaza;

            DataTable dtDatosUnidad = new DataTable();
            dtDatosUnidad = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_FiltrarUnidad(idtracto);
            if (dtDatosUnidad.Rows.Count > 0)
            {
                label9.Text = dtDatosUnidad.Rows[0]["TIPO_UNIDAD"].ToString();
                txtOrigen.Text = dtDatosUnidad.Rows[0]["ORIGEN"].ToString();
                txtTipo.Text = dtDatosUnidad.Rows[0]["TIPO_UNIDAD"].ToString();
                txtSubTipo.Text = dtDatosUnidad.Rows[0]["SUB_TIPO_UNIDAD"].ToString();
                txtCompania.Text = dtDatosUnidad.Rows[0]["COMPANIA"].ToString();
                txtSucursal.Text = dtDatosUnidad.Rows[0]["SUCURSAL"].ToString();
                txtDescripcion.Text = dtDatosUnidad.Rows[0]["Descripcion"].ToString();
                txtCentroCostos.Text = dtDatosUnidad.Rows[0]["CENTRO_COSTO"].ToString();
                txtProyecto.Text = dtDatosUnidad.Rows[0]["Proyecto"].ToString();
                txtMarca.Text = dtDatosUnidad.Rows[0]["MARCA"].ToString();
                txtModelo.Text = dtDatosUnidad.Rows[0]["MODELO"].ToString();
                txtTarjeta.Text = dtDatosUnidad.Rows[0]["TarjetaPropiedad"].ToString();
                txtSerieMotor.Text = dtDatosUnidad.Rows[0]["MOTOR"].ToString();
                txtConfig.Text = dtDatosUnidad.Rows[0]["CodigoVehicular"].ToString();
                txtChasis.Text = dtDatosUnidad.Rows[0]["CHASIS"].ToString();
                txtKilometraje.Text = dtDatosUnidad.Rows[0]["KILOMETRAJE"].ToString();
                txtTipoComb.Text = dtDatosUnidad.Rows[0]["COMBUSTIBLE"].ToString();
                txtCapComb.Text = dtDatosUnidad.Rows[0]["CAP_COMB"].ToString();
                txtCapacidad.Text = dtDatosUnidad.Rows[0]["CAPACIDAD"].ToString();
                txtTaraReal.Text = dtDatosUnidad.Rows[0]["TARA_REAL"].ToString();
                txtTara.Text = dtDatosUnidad.Rows[0]["TARA"].ToString();
                txtPesoSeco.Text = dtDatosUnidad.Rows[0]["PESO_SECO"].ToString();
                txtPesoBruto.Text = dtDatosUnidad.Rows[0]["PESO_BRUTO"].ToString();
                txtTraccion.Text = dtDatosUnidad.Rows[0]["TIPO_TRACCION"].ToString();
                txtNroTraccion.Text = dtDatosUnidad.Rows[0]["Traccion"].ToString();
                txtDireccional.Text = dtDatosUnidad.Rows[0]["TIPO_DIRECCIONAL"].ToString();
                txtNroDireccional.Text = dtDatosUnidad.Rows[0]["Direccional"].ToString();
                txtEjes.Text = dtDatosUnidad.Rows[0]["EJES"].ToString();
                txtNormal.Text = dtDatosUnidad.Rows[0]["NORMAL"].ToString();
                txtRepuesto.Text = dtDatosUnidad.Rows[0]["REPUESTO"].ToString();
            }
        }

        private void frmNuevaUnidadConductor_Load(object sender, EventArgs e)
        {
            cbxTransmision.SelectedIndex = 0;

            DataTable dtOperaciones = new DataTable();
            dtOperaciones = clsOperacionesBL.Instancia.GetOperaciones_ListarTipoProgramaciones();

            if (dtOperaciones.Rows.Count > 0)
            {
                cboProgramaciones.DisplayMember = "DESCRIPCION";
                cboProgramaciones.ValueMember = "ID";
                cboProgramaciones.DataSource = dtOperaciones;
            }

            if (_ver1Crear2 == 1)
            {
                btnGuardar.Enabled = false;
                cboProgramaciones.Enabled = false;
                cbBocamaza.Enabled = false;
                txtUnidad.ReadOnly = true;
                txtObservacion.ReadOnly = true;
                txtPeso.ReadOnly = true;
                txtGalones.ReadOnly = true;

                txtNroRegistro.Text = _nro.ToString();
                txtUnidad.Text = _placa;
                txtObservacion.Text = _Observacion;
                txtPeso.Text = Convert.ToString(_Peso);
                txtGalones.Text = Convert.ToString(_Galones);
                txtBitacora.Text = _Bitacora;
                cbBocamaza.Text = _Bocamaza;
                cboProgramaciones.Text = _Operacion;
                

                if (_Transmision == "MECANICO" || _Transmision == "") { cbxTransmision.SelectedIndex = 0; }
                else { cbxTransmision.SelectedIndex = 1; }
            }

            if (_ver1Crear2 == 2)
            {
                btnGuardar.Enabled = true;
                cboProgramaciones.Enabled = true;
                cbBocamaza.Enabled = true;
                txtUnidad.ReadOnly = false;
                txtObservacion.ReadOnly = false;
                txtPeso.ReadOnly = false;
                txtGalones.ReadOnly = false;

                txtNroRegistro.Text = _nro.ToString();
                txtUnidad.Text = _placa;
                txtObservacion.Text = _Observacion;
                txtPeso.Text = Convert.ToString(_Peso);
                txtGalones.Text = Convert.ToString(_Galones);
                txtBitacora.Text = _Bitacora;
                cboProgramaciones.Text = _Operacion;
                cbBocamaza.Text = _Bocamaza;

                if (_Transmision == "MECANICO" || _Transmision == "") { cbxTransmision.SelectedIndex = 0; }
                else { cbxTransmision.SelectedIndex = 1; }

               // cboProgramaciones.SelectedItem = _idOperacion - 1;

                if (_mochila == 1) { chbMochila.Checked = true; }
                if (_tomafuerza == 1) { chbTomafuerza.Checked = true; }
                if (_urea == 1) { chbUrea.Checked = true; }
                if (_manguera == 1) { chbManguera.Checked = true; }
                if (_Senaletica == 1) { chbSenaletica.Checked = true; }
                if (_LlaveOriginal == 1) { chbLlaveOriginal.Checked = true; }
                if (_LlaveDuplicada == 1) { chbLlaveDuplicada.Checked = true; }
                if (_Camaras == 1) { chbCamaras.Checked = true; }
            }

            txtNroRegistro.Enabled = true;

            txtUsuarioModifica.Text = Utilitario.Instancia.SesionUsuario.usuario.ToString();
            txtFechaModifica.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }

        private void button2_Click(object sender, EventArgs e) { this.Close(); }

        private void txtUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                clsVisuales.Instancia.LlenarLw(lstTracto, clsConsultaBL.Instancia.GetUnidades(txtUnidad.Text), true, false, false);

                lstTracto.Columns[0].Width = 0;
                lstTracto.Columns[1].Width = 80;
                lstTracto.Columns[2].Width = 50;
                lstTracto.Columns[3].Width = 120;

                lstTracto.Size = new System.Drawing.Size(260, 103);

                lstTracto.BringToFront();
                lstTracto.Visible = true;
                lstTracto.Focus();
                TipoUnidad = "TRACTO";

                if (TipoUnidad == "TRACTO" || TipoUnidad == "CAMION")
                {
                    groupBox6.Enabled = false;
                    cbxTipoCortina.Text = "";
                    groupBox7.Enabled = false;
                    cbxModeloChasis.Text = "";
                    groupBox8.Enabled = false;
                    cbxNivel.Text = "";
                    cbxNivel_DropDownClosed(sender, e);
                    groupBox9.Enabled = false;
                    groupBox12.Enabled = false;
                    cbxSuspension.Text = "";
                    groupBox10.Enabled = false;
                    groupBox11.Enabled = false;
                    txtPlanos.Text = "";
                    cbxPiso1.Text = "";
                    cbxPiso_DropDownClosed(sender, e);
                    cbxPiso2.Text = "";
                    cbxPiso2_DropDownClosed(sender, e);
                }
                else
                {
                    groupBox6.Enabled = true;
                    cbxTipoCortina.Text = "";
                    groupBox7.Enabled = true;
                    cbxModeloChasis.Text = "";
                    groupBox8.Enabled = true;
                    cbxNivel.Text = "";
                    cbxNivel_DropDownClosed(sender, e);
                    groupBox9.Enabled = true;
                    groupBox12.Enabled = true;
                    cbxSuspension.Text = "";
                    groupBox10.Enabled = true;
                    groupBox11.Enabled = true;
                    txtPlanos.Text = "";
                    cbxPiso1.Text = "";
                    cbxPiso_DropDownClosed(sender, e);
                    cbxPiso2.Text = "";
                    cbxPiso2_DropDownClosed(sender, e);
                }
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                idtracto = -1;
                txtUnidad.Focus();
            }
        }

        private void lstTracto_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstTracto.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    // int idConductor;
                    ItemActual = lstTracto.SelectedItems[0];

                    idtracto = Convert.ToInt32(ItemActual.Text);
                    // IdConductor.Text = Convert.ToString(idConductor);
                    txtUnidad.Text = ItemActual.SubItems[1].Text;
                    TipoUnidad = ItemActual.SubItems[3].Text;

                    if (TipoUnidad != "TRACTO")
                    {
                        cbxTransmision.Enabled = false;
                        groupBox1.Enabled = false;
                    }

                    if (TipoUnidad == "TRACTO" || TipoUnidad == "CAMION")
                    {
                        groupBox6.Enabled = false;
                        cbxTipoCortina.Text = "";
                        groupBox7.Enabled = false;
                        cbxModeloChasis.Text = "";
                        groupBox8.Enabled = false;
                        cbxNivel.Text = "";
                        cbxNivel_DropDownClosed(sender, e);
                        groupBox9.Enabled = false;
                        groupBox12.Enabled = false;
                        cbxSuspension.Text = "";
                        groupBox10.Enabled = false;
                        groupBox11.Enabled = false;
                        txtPlanos.Text = "";
                        cbxPiso1.Text = "";
                        cbxPiso_DropDownClosed(sender, e);
                        cbxPiso2.Text = "";
                        cbxPiso2_DropDownClosed(sender, e);
                    }
                    else
                    {
                        groupBox6.Enabled = true;
                        cbxTipoCortina.Text = "";
                        groupBox7.Enabled = true;
                        cbxModeloChasis.Text = "";
                        groupBox8.Enabled = true;
                        cbxNivel.Text = "";
                        cbxNivel_DropDownClosed(sender, e);
                        groupBox9.Enabled = true;
                        groupBox12.Enabled = true;
                        cbxSuspension.Text = "";
                        groupBox10.Enabled = true;
                        cbxPiso1.Text = "";
                        cbxPiso_DropDownClosed(sender, e);
                        cbxPiso2.Text = "";
                        cbxPiso2_DropDownClosed(sender, e);

                        if (TipoUnidad == "CORTINERA") { groupBox11.Enabled = true; }
                        else { groupBox11.Enabled = false; }
                        txtPlanos.Text = "";
                    }

                   // txtConductorInicio.Text = ItemActual.SubItems[1].Text;
                    lstTracto.Visible = false;
                    cbxTransmision.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstTracto.Visible = false;
                    idtracto = -1;
                    txtUnidad.Focus();
                }
            }
            catch (Exception) { throw; }
        }

        private void lstTracto_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ListViewItem ItemActual;
            // int idConductor;
            ItemActual = lstTracto.SelectedItems[0];

            idtracto = Convert.ToInt32(ItemActual.Text);
            // IdConductor.Text = Convert.ToString(idConductor);
            txtUnidad.Text = ItemActual.SubItems[1].Text;
            TipoUnidad = ItemActual.SubItems[3].Text;

            if (TipoUnidad != "TRACTO")
            {
                cbxTransmision.Enabled = false;
                groupBox1.Enabled = false;
            }

            if (TipoUnidad == "TRACTO" || TipoUnidad == "CAMION")
            {
                groupBox6.Enabled = false;
                cbxTipoCortina.Text = "";
                groupBox7.Enabled = false;
                cbxModeloChasis.Text = "";
                groupBox8.Enabled = false;
                cbxNivel.Text = "";
                cbxNivel_DropDownClosed(sender, e);
                groupBox9.Enabled = false;
                groupBox12.Enabled = false;
                cbxSuspension.Text = "";
                groupBox10.Enabled = false;
                groupBox11.Enabled = false;
                txtPlanos.Text = "";
                cbxPiso1.Text = "";
                cbxPiso_DropDownClosed(sender, e);
                cbxPiso2.Text = "";
                cbxPiso2_DropDownClosed(sender, e);
            }
            else
            {
                groupBox6.Enabled = true;
                cbxTipoCortina.Text = "";
                groupBox7.Enabled = true;
                cbxModeloChasis.Text = "";
                groupBox8.Enabled = true;
                cbxNivel.Text = "";
                cbxNivel_DropDownClosed(sender, e);
                groupBox9.Enabled = true;
                groupBox12.Enabled = true;
                cbxSuspension.Text = "";
                groupBox10.Enabled = true;
                cbxPiso1.Text = "";
                cbxPiso_DropDownClosed(sender, e);
                cbxPiso2.Text = "";
                cbxPiso2_DropDownClosed(sender, e);

                if (TipoUnidad == "CORTINERA") { groupBox11.Enabled = true; }
                else { groupBox11.Enabled = false; }
                txtPlanos.Text = "";
            }

            // txtConductorInicio.Text = ItemActual.SubItems[1].Text;
            lstTracto.Visible = false;
            cbxTransmision.Focus();
        }

        private void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0)) { lstTracto.Items[0].Selected = true; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPeso.Text.Length == 0 || txtGalones.Text.Length == 0 || txtNroLlantas.Text.Length == 0)
            {
                if (txtPeso.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese el peso de la unidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPeso.Focus();
                }
                else
                {
                    if (txtGalones.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese el galón de la mochila.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtGalones.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese el número de llantas.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNroLlantas.Focus();
                    }
                }
                return;
            }
            else
            {
                if (chbMochila.Checked == true) { _mochila = 1; }
                else { _mochila = 0; }

                if (chbTomafuerza.Checked == true) { _tomafuerza = 1; }
                else { _tomafuerza = 0; }

                if (chbUrea.Checked == true) { _urea = 1; }
                else { _urea = 0; }

                if (chbManguera.Checked == true) { _manguera = 1; }
                else { _manguera = 0; }

                if (chbSenaletica.Checked == true) { _Senaletica = 1; }
                else { _Senaletica = 0; }

                if (chbLlaveOriginal.Checked == true) { _LlaveOriginal = 1; }
                else { _LlaveOriginal = 0; }

                if (chbLlaveDuplicada.Checked == true) { _LlaveDuplicada = 1; }
                else { _LlaveDuplicada = 0; }

                if (chbCamaras.Checked == true) { _Camaras = 1; }
                else { _Camaras = 0; }

                if (cboProgramaciones.Text.Equals("LOCAL")) { _idOperacion = 9; }
                else { _idOperacion = cboProgramaciones.SelectedIndex + 1; }

                DataTable dtGuardarDatos = new DataTable();
                dtGuardarDatos = clsOperacionesBL.Instancia.GetOperaciones_Operaciones_UnidadesConductor_CreaModifica(_Accion, _nro, idtracto, Convert.ToInt32(cboProgramaciones.SelectedValue), txtObservacion.Text, Utilitario.Instancia.SesionUsuario.usuario, _mochila,
                                                            _tomafuerza, _urea, _manguera, _Senaletica, _LlaveOriginal, _LlaveDuplicada, _Camaras, cbxTransmision.Text, Convert.ToDecimal(txtPeso.Text), Convert.ToDecimal(txtGalones.Text), cbxTipoCortina.Text,
                                                            cbxModeloChasis.Text, cbxNivel.Text, cbxTipoNivel.Text, cbxSuspension.Text, cbxPiso1.Text, cbxPisoMaterial1.Text, cbxPiso2.Text, cbxPisoMaterial2.Text, Convert.ToInt32(txtNroLlantas.Text), txtPlanos.Text,
                                                            txtBitacora.Text, cbBocamaza.Text);
                string Rpta = Convert.ToString(dtGuardarDatos.Rows[0]["Exito"]);
                string valor = Rpta.Substring(0, 1);

                if (valor == "0")
                {
                    MessageBox.Show(Rpta, "OPERACION EXITOSA");
                    this.Close();
                }
                else { MessageBox.Show(Rpta, "ALERTA"); }
            }
        }

        private void txtPeso_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void txtGalones_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void txtNroLlantas_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void cbxTipoCortina_DropDownClosed(object sender, EventArgs e) { cbxModeloChasis.Focus(); }

        private void cbxModeloChasis_DropDownClosed(object sender, EventArgs e) { cbxNivel.Focus(); }

        public void cbxNivel_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxNivel.Text == "")
            {
                cbxTipoNivel.Text = " ";
                cbxTipoNivel.Enabled = false;

                cbxPiso1.Text = "";
                cbxPiso1.Enabled = false;
                cbxPisoMaterial1.Text = "";
                cbxPisoMaterial1.Enabled = false;
                cbxPiso2.Text = "";
                cbxPiso2.Enabled = false;
                cbxPisoMaterial2.Text = "";
                cbxPisoMaterial2.Enabled = false;
            }
            
            if (cbxNivel.Text == "01")
            {
                cbxTipoNivel.Text = " ";
                cbxTipoNivel.Enabled = false;

                cbxPiso1.Text = "";
                cbxPiso1.Enabled = true;
                cbxPisoMaterial1.Text = "";
                cbxPisoMaterial1.Enabled = false;
                cbxPiso2.Text = "";
                cbxPiso2.Enabled = false;
                cbxPisoMaterial2.Text = "";
                cbxPisoMaterial2.Enabled = false;
            }

            if (cbxNivel.Text == "02")
            {
                cbxTipoNivel.Text = "FIJAS";
                cbxTipoNivel.Enabled = true;

                cbxPiso1.Text = "";
                cbxPiso1.Enabled = true;
                cbxPisoMaterial1.Text = "";
                cbxPisoMaterial1.Enabled = false;
                cbxPiso2.Text = "";
                cbxPiso2.Enabled = true;
                cbxPisoMaterial2.Text = "";
                cbxPisoMaterial2.Enabled = false;
            }
        }

        private void cbxTipoNivel_DropDownClosed(object sender, EventArgs e) { cbxSuspension.Focus(); }

        private void cbxSuspension_DropDownClosed(object sender, EventArgs e) { cbxPiso1.Focus(); }

        public void cbxPiso_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxPiso1.Text == "")
            {
                cbxPisoMaterial1.Text = " ";
                cbxPisoMaterial1.Enabled = false;
            }
            else
            {
                cbxPisoMaterial1.Text = "METAL";
                cbxPisoMaterial1.Enabled = true;
            }
        }

        public void cbxPiso2_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxPiso2.Text == "")
            {
                cbxPisoMaterial2.Text = " ";
                cbxPisoMaterial2.Enabled = false;
            }
            else
            {
                cbxPisoMaterial2.Text = "METAL";
                cbxPisoMaterial2.Enabled = true;
            }
        }

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
                        txtPlanos.Clear();
                        txtPlanos.Text = "file:///" + nuevoEnlace;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrar_Click(object sender, EventArgs e) { txtPlanos.Clear(); }

        private void txtPlanos_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(e.LinkText); }
            catch { MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlaceVitacora;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlaceVitacora = op.FileName.Replace(" ", "%20");
                        txtBitacora.Clear();
                        txtBitacora.Text = "file:///" + nuevoEnlaceVitacora;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void txtBitacora_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {

            try { System.Diagnostics.Process.Start(e.LinkText); }
            catch { MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
