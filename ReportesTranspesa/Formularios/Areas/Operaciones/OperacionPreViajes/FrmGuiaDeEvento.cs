using Comun;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using ReportesTranspesa.Properties;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmGuiaDeEvento : Form
    {
        public int tipoDocFiscal = 0;
        public DataTable MaestroGR = new DataTable();
        public int NroPreviaje;
        public int idTipoProgramacion;
        public int Redondeo, GenerarPE;
        public int idConductorNuevo;
        public clsGRT entGuiaTransportista = new clsGRT();
        public DataTable dtDireccionesRuta;
        public DataTable dtDireccionesRutaDestinatario;
        public string DireccionUbigeoOrigen;
        public string DireccionUbigeoFin;
        public DataTable dtConductores;
        public DataTable dtUnidades;
        public bool esValido = true;
        public entConductor entNuevoConductor = new entConductor();
        string TipoVehiculoCarreta = "";
        public string tipoEvento = "";


        public FrmGuiaDeEvento()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRuta_Enter(object sender, EventArgs e)
        {
            txtRuta.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRuta_Leave(object sender, EventArgs e)
        {
            txtRuta.BackColor = Color.White;
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRuta, ref  lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {

                    groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {
                    groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmGuiaDeEvento_Load(object sender, EventArgs e)
        {
            try
            {
                CargarTipoDocumento();
                cbxTipoEvento.SelectedIndex = 0;

                DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica(entGuiaTransportista.ruta);
                if (dtRuta.Rows.Count > 0)
                {
                    //la ruta la cargado desde el formulario Listar Guias
                    entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = dtRuta.Rows[0]["UbigeoOrigen"].ToString();// ubigeo partida
                    DireccionUbigeoOrigen = dtRuta.Rows[0]["DescripcionOrigen"].ToString();
                    entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = dtRuta.Rows[0]["UbigeoFin"].ToString(); // ubigeo destino
                    DireccionUbigeoFin = dtRuta.Rows[0]["DescripcionFin"].ToString();

                    txtDireccionPartida.Enabled = true;
                    txtDireccionDestino.Enabled = true;
                }

                llenarConductores();

                for (int i = 0; i < cbxTipoEvento.Items.Count; i++)
                {
                    if (cbxTipoEvento.Text == tipoEvento) { cbxTipoEvento.SelectedIndex = i; }
                }

                txtOT_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                cbRedondeo.Checked = false;
                cbRedondeo_CheckedChanged(sender, e);

                DataTable TablaViatico = new DataTable();
                TablaViatico = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarViaticosTicket(1, NroPreviaje, -1);

                if (TablaViatico.Rows.Count > 0)
                {
                    txtGastoOrigen.Text = Convert.ToString(TablaViatico.Rows[0]["Viatico"]);
                    txtPlanilla.Text = Convert.ToString(TablaViatico.Rows[0]["Planilla"]);
                }
                else { txtGastoViaje.Text = "0.00"; }

                cbPlanillaEvento.Checked = false;
                cbPlanillaEvento_CheckedChanged(sender, e);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }



        private void llenarConductores()
        {
            DataTable dtTarjetaCarreta = clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idCarreta));
            if (dtTarjetaCarreta.Rows.Count > 0)
            {
                TipoVehiculoCarreta = dtTarjetaCarreta.Rows[0]["TIPO"].ToString();
            }
            
            dtConductores = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaTransportista.xml_entGTR_Conductor_M);
            dgvConductor.Rows.Add(dtConductores.Rows[0]["idConductor"].ToString(),
                dtConductores.Rows[0]["Nombres_Conductor"].ToString() + " " + dtConductores.Rows[0]["Apellidos_Conductor"].ToString(),
                dtConductores.Rows[0]["Licencia_Conductor"].ToString(),
                dtConductores.Rows[0]["NumeroDocIdentidad_Conductor"].ToString(),
                dtConductores.Rows[0]["TipoDocIdentidad_Conductor"].ToString(),
                dtConductores.Rows[0]["Nombres_Conductor"].ToString(),
                dtConductores.Rows[0]["Apellidos_Conductor"].ToString(),
                dtConductores.Rows[0]["idPlaca"].ToString(),
                dtConductores.Rows[0]["Tracto"].ToString(),
                dtConductores.Rows[0]["idCarreta"].ToString(),
                dtConductores.Rows[0]["Carreta"].ToString(),
                dtConductores.Rows[0]["PlacaTarjetaCircula"].ToString(),
                dtConductores.Rows[0]["CarretaTarjetaCircula"].ToString());
        }

        private void CargarTipoDocumento()
        {
            DataTable dtTipoGuias = clsOperacionesBL.Instancia.ListarTipoGuiaElectronica();
            if (dtTipoGuias.Rows.Count > 0)
            {
                cbxTipoDocumentoFiscal.DataSource = dtTipoGuias;
                cbxTipoDocumentoFiscal.DisplayMember = "NombreTipoGuia";
                cbxTipoDocumentoFiscal.ValueMember = "TipoGuia";
                cbxTipoDocumentoFiscal.SelectedIndex = 0;

            }
            else
            {
                MessageBox.Show("Combobox de Tipo Guia no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void cbxTipoDocumentoFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbxTipoEvento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           DataTable dtotsdis = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOtsDisponibles();

            if (dtotsdis.Rows.Count > 0)
            {
       
                //label21.Visible = false;
                dtgvData.Visible = true;
                dtgvData.Size = new System.Drawing.Size(828, 300);
                dtgvData.Location = new Point(107, 178);
                label1.Size = new System.Drawing.Size(828, 35);
                label1.Visible = true;
                dtgvData.Visible = true;
                dtgvData.DataSource = dtotsdis;
                dtgvData.Focus();
            }
        }

        private void dtgvData_Click(object sender, EventArgs e)
        {

        }

        private void dtgvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int[] filas = dgvDataView.GetSelectedRows();
                string datoseleccionado = dgvDataView.GetFocusedValue().ToString();

                for (int i = 0; i < filas.Length; i++)
                {
                    string OTselec = dgvDataView.GetRowCellValue(filas[i], "OT").ToString();

                    if (Convert.ToInt32(OTselec) > 0)
                    {
                        OTselec = dgvDataView.GetRowCellValue(filas[i], "OT").ToString();
                        txtOT.Text = OTselec;
                        label1.Visible = false;
                        dtgvData.Visible = false;
                        txtOT.Focus();
                        entGuiaTransportista.idOT = Convert.ToInt32(txtOT.Text);
                        txtOT_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    }

                }

                txtGuiaEventoTransportista.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgvData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {             
                dtgvData.Visible = false;
                dtgvData.Size = new System.Drawing.Size(793, 10);
                dtgvData.Location = new Point(107, 178);
                label1.Size = new System.Drawing.Size(793, 10);
                label1.Visible = false;
                dtgvData.Visible = false;
              

            }
        }

        public void txtOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtOT.Text.Equals(""))
                {
                    return;
                }
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {
                    int otbuscar;
                    otbuscar = int.Parse(txtOT.Text);

                    DataTable datosOT = new DataTable();

                    datosOT = clsOperacionesBL.Instancia.GetOperaciones_ListarDatosOts(otbuscar);

                    if (datosOT.Rows.Count > 0)
                    {
                        for (int i = 0; i < datosOT.Rows.Count; i++)
                        {

                            txtRuta.Text = datosOT.Rows[i]["DESCRIPCION"].ToString();
                            txtRuta.Tag = Convert.ToInt32(datosOT.Rows[i]["IdRuta"].ToString());

                            MaestroGR = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(txtRuta.Tag), idTipoProgramacion, Redondeo);
                            if (MaestroGR.Rows.Count > 0)
                            {
                                for (int j = 0; j < MaestroGR.Rows.Count; j++)
                                {
                                    decimal viaticosSuma = Convert.ToDecimal(MaestroGR.Rows[0]["GastoTotal"]);
                                    txtGastoViaje.Text = viaticosSuma.ToString();
                                }
                            }
                            else { txtGastoViaje.Text = "0.00"; }
                           
                            dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtRemitente.Tag));
                            dtDireccionesRutaDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtDestinatario.Tag));

                            DataTable dtDireccionesAutocompletado = cargarDatosPorDefectoSegunCliente();
                            if (dtDireccionesAutocompletado.Rows.Count > 0)
                            {
                                txtDireccionPartida.Text = dtDireccionesAutocompletado.Rows[0]["DireccionPartida"].ToString();
                                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = dtDireccionesAutocompletado.Rows[0]["DireccionPartida"].ToString();
                                txtDireccionPartida.Tag = dtDireccionesAutocompletado.Rows[0]["SecuenciaPartida"].ToString();
                                entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia = Convert.ToInt32(dtDireccionesAutocompletado.Rows[0]["SecuenciaPartida"]);

                                
                                txtDireccionDestino.Text = dtDireccionesAutocompletado.Rows[0]["DireccionDestino"].ToString();
                                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = dtDireccionesAutocompletado.Rows[0]["DireccionDestino"].ToString();
                                txtDireccionDestino.Tag = dtDireccionesAutocompletado.Rows[0]["SecuenciaDestino"].ToString();
                                entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia = Convert.ToInt32(dtDireccionesAutocompletado.Rows[0]["SecuenciaDestino"]);
                            }

                            txtGuiaEventoTransportista.Focus();
                            //txtProducto.Text = datosOT.Rows[i]["Nombre"].ToString();
                            //idProducto = Convert.ToInt32(datosOT.Rows[i]["Producto"].ToString());
                            //lblUniMedida.Text = datosOT.Rows[i]["UMUso"].ToString();
                            //tiempo = Convert.ToDecimal(datosOT.Rows[i]["tiempo"].ToString());
                            //txtZona.Text = datosOT.Rows[i]["ZONA"].ToString();
                            //txtDireccionDestino.Text = datosOT.Rows[i]["DESTINO"].ToString();
                            //lblDias.Text = datosOT.Rows[i]["DIA"].ToString();
                            /* if (datosOT.Rows[i]["BUSQUEDA"].ToString().Equals("AC LOGISTICA DEL PERU S.A.C") || datosOT.Rows[i]["UMUso"].ToString().Equals("VI"))
                             {
                                 txtPesoAlmacen.Text = "1";
                             }

                             decimal price = Math.Ceiling(tiempo);
                             DateTime fecha = Convert.ToDateTime(dtFechaProgramada.Text);
                             DateTime fecha2 = fecha.AddHours(Convert.ToDouble(price));

                             string vfehapro = Convert.ToString(fecha2);
                             dtFechaFin.Text = vfehapro;
                             dtFechaFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                             dtLlegada.Text = dtFechaFin.Text;
                             dtLlegada.CustomFormat = "HH:mm:ss";*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private DataTable cargarDatosPorDefectoSegunCliente()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CompletarDestinatario_Direcciones(Convert.ToInt32(txtRemitente.Tag), Convert.ToInt32(txtRuta.Tag));

            return dt;
        }

        private void lstRuta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRuta, ref  lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {
                    entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = txtRuta.Tag.ToString(); // ubigeo partida
                    DireccionUbigeoOrigen  = lstRuta.SelectedItems[0].SubItems[3].Text;

                    entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = lstRuta.SelectedItems[0].SubItems[2].Text; // ubigeo destino
                    DireccionUbigeoFin = lstRuta.SelectedItems[0].SubItems[4].Text;

                    if (txtRuta.Tag != null)
                    {
                        txtDireccionDestino.Enabled = true;
                        txtDireccionPartida.Enabled = true;
                    }
                    else
                    {
                        MaestroGR = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(txtRuta.Tag), idTipoProgramacion, Redondeo);
                        if (MaestroGR.Rows.Count > 0)
                        {
                            for (int j = 0; j < MaestroGR.Rows.Count; j++)
                            {
                                decimal viaticosSuma = Convert.ToDecimal(MaestroGR.Rows[0]["GastoTotal"]);
                                txtGastoViaje.Text = viaticosSuma.ToString();
                            }
                        }
                        else { txtGastoViaje.Text = "0.00"; }

                        txtDireccionDestino.Enabled = false;
                        txtDireccionPartida.Enabled = false;
                    }
                    groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar == (char)Keys.Escape)
            {
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;

            }

            if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRuta))
            {
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
                groupDireccionDestino.Select();
                txtDireccionDestino.Focus();
                lstDireccionPartida.Visible = false;
            }*/
        }

        private void txtDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == (char)Keys.Escape)
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;

                }
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta))
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                    lstDireccionPartida.Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionPartida_Enter(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionPartida_Leave(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.White;
        }

        private void lstDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRuta))
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;

                    ListViewItem ItemActual;
                    ItemActual = lstDireccionPartida.SelectedItems[0];
                    txtDireccionPartida.Tag = Convert.ToInt32(ItemActual.SubItems[2].Text);
                    entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia = Convert.ToInt32(ItemActual.SubItems[2].Text);





                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                entGuiaTransportista.idRuta = Convert.ToInt32(txtRuta.Tag);
                entGuiaTransportista.ruta = txtRuta.Text;
                entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia = Convert.ToInt32(txtDireccionPartida.Tag);
                entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia = Convert.ToInt32(txtDireccionDestino.Tag);
                entGuiaTransportista.xml_entGTR_Conductor_M =Utilitario.Instancia.QuitarTildes(Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvConductor)));
                entGuiaTransportista.TipoEvento = cbxTipoEvento.Text.ToString();
                entGuiaTransportista.TipoGuia = cbxTipoDocumentoFiscal.SelectedValue.ToString();
                entGuiaTransportista.entGRT_Generales_Serie_M = txtSerie.Text;
                entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(txtNumero.Text);
                entGuiaTransportista.idOT = Convert.ToInt32(txtOT.Text);
                entGuiaTransportista.GuiaEventoTransportista = txtGuiaEventoTransportista.Text;
                entGuiaTransportista.GuiaEventoRemitente = txtGuiaEventoRemitente.Text;
                esValido = true;
                
                DataTable dt = clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica(txtDestinatario.Text);
                if (dt.Rows.Count <= 0) { MessageBox.Show("No se obtuvo pk del destinatario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                {
                    entGuiaTransportista.idDestinatario = Convert.ToInt32(txtDestinatario.Tag);
                    entGuiaTransportista.entGRT_Remitente_RazonSocial_M = txtDestinatario.Text;
                }

                if (txtGuiaEventoTransportista.Text.Length == 0)
                {
                    esValido = false;
                    MessageBox.Show("Usted no a ingresado el Numero de la guia de evento de transportista", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtGuiaEventoRemitente.Text.Length == 0 && tipoEvento != "TRASBORDO NO PORGRAMADO")
                {
                    esValido = false;
                    MessageBox.Show("Usted no a ingresado el Numero de la guia de evento de Remitente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (txtOT.Text.Length == 0)
                {
                    esValido = false;
                    MessageBox.Show("Usted no a seleccionado la nueva OT", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtDireccionPartida.Text.Length == 0)
                {
                    esValido = false;
                    MessageBox.Show("Usted no a seleccionado una direccion de partida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia == 0)
                {
                    esValido = false;
                    MessageBox.Show("La Secuencia de Direccion de Partida no puede ser Null", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtDireccionDestino.Text.Length == 0)
                {
                    esValido = false;
                    MessageBox.Show("Usted no a seleccionado una direccion de destino", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia == 0)
                {
                    esValido = false;
                    MessageBox.Show("La Secuencia de Direccion de Destino no puede ser Null", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (cbxTipoEvento.Text.Length == 0)
                {
                    esValido = false;
                    MessageBox.Show("El tipo de evento no puede estar vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (entGuiaTransportista.ruta.Length == 0)
                {
                    esValido = false;
                    MessageBox.Show("La ruta no puede estar vacia", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (entGuiaTransportista.idRuta == 0)
                {
                    esValido = false;
                    MessageBox.Show("codigo interno de ruta no puede estar vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtSerie.Text.Length ==0)
                {
                    esValido = false;
                    MessageBox.Show("La serie de la guia de referencia no puede estar vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtNumero.Text.Length == 0)
                {
                    esValido = false;
                    MessageBox.Show("El numero de la guia de referencia no puede estar vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (entGuiaTransportista.idcliente == 0 )
                {
                    esValido = false;
                    MessageBox.Show("el codigo interno del cliente no puede estar vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (txtDestinatario.Tag == null)
                {
                    esValido = false;
                    MessageBox.Show("el codigo interno del Destinatario no puede estar vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if(dgvConductor.Rows.Count == 0)
                {
                    esValido = false;
                    MessageBox.Show("La cantidad de conductores no puede ser 0", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (entGuiaTransportista.idCarreta == "")
                {
                    esValido = false;
                    MessageBox.Show("No a seleccionado una carreta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (entGuiaTransportista.idtracto == "")
                {
                    esValido = false;
                    MessageBox.Show("No a seleccionado un tracto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (esValido == true)
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarGuiaDeEvento_Electronica(entGuiaTransportista))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (GenerarPE == 1)
                        {
                            if (txtPlanilla.Text.Length == 0)
                            { MessageBox.Show("No se pudo generar la planilla de evento porque no se encontró la planilla anterior.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            else
                            {
                                DataTable dtRespuesta = new DataTable();
                                string Respuesta;
                                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento(txtPlanilla.Text, NroPreviaje, idTipoProgramacion,
                                                                         entGuiaTransportista.idRuta, idConductorNuevo, Convert.ToDecimal(txtGastoViaje.Text), Usuario);
                                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                string PlanillaEvento = Respuesta.Substring(41, 8);

                                string NroRPTA = Respuesta.Substring(0, 1);
                                if (NroRPTA == "0")
                                {
                                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    try
                                    {
                                        DataTable dtConsultarImpresora = new DataTable();
                                        dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                                        DataTable dtListaTicket = new DataTable();
                                        dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(PlanillaEvento);

                                        string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                                        if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                                        {
                                            MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        else
                                        {
                                            if (dtListaTicket.Rows.Count > 0)
                                            {
                                                Ticket ticket = new Ticket();

                                                ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                                                ticket.AddSubHeaderLine2("PLE - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         ");
                                                ticket.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                                                ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                                                ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                                                ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                                                ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                                                ticket.AddSubHeaderLine("                              ");
                                                ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                                                ticket.AddSubHeaderLine("                              ");
                                                ticket.AddSubHeaderLine("F.Emisión: " + dtListaTicket.Rows[0]["FECHA_EMISION"].ToString());
                                                ticket.AddSubHeaderLine("F.Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                                ticket.AddSubHeaderLine("FIRMA: ");
                                                ticket.HeaderImage = Resources.TABLA;

                                                ticket.PrintTicket(NombreImpresora);

                                                string Usuario2 = Utilitario.Instancia.SesionUsuario.usuario;

                                                if (Usuario2 != "EGENNELL" && Usuario != "LQUEZADA" && Usuario != "RCCAMA" && Usuario != "MADELEINEC" && Usuario != "JALBAN")
                                                {
                                                    DataTable dtCorrelativo = new DataTable();
                                                    dtCorrelativo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(6, "");
                                                    string Correlativo = Convert.ToString(dtCorrelativo.Rows[0]["Codigo"]);

                                                    Ticket ticket2 = new Ticket();
                                                    ticket2.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                                                    ticket2.AddSubHeaderLine2("TICKET DE DESPACHO");
                                                    ticket2.AddSubHeaderLine2("DE UNIDAD");
                                                    ticket2.AddSubHeaderLine2("N° " + Correlativo + "                         ");
                                                    ticket2.AddSubHeaderLine("F. Salida: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                                                    ticket2.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   Carreta: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                                                    ticket2.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                                                    ticket2.AddSubHeaderLine("Destino: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                                                    ticket2.AddSubHeaderLine("Programación: " + dtListaTicket.Rows[0]["PROGRAMACION"].ToString());
                                                    ticket2.HeaderImage = Resources.DESPACHO;
                                                    ticket2.PrintTicket(NombreImpresora);
                                                }
                                            }
                                        }
                                    }
                                    catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }
                                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                        }

                        LimpiarFormulario();
                    }
                    else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }


        
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtOT.Clear();
            txtOT.Tag = null;
            txtRuta.Clear();
            txtRuta.Tag = null;
            txtDireccionPartida.Clear();
            txtDireccionPartida.Tag = null;
            txtDireccionDestino.Clear();
            txtDireccionDestino.Tag = null ;


        }

        private void lstDireccionPartida_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtDireccionDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar == (char)Keys.Escape)
            {
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;
            }



            if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRutaDestinatario))
            {
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;

            }*/
           
        }

        private void txtDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == (char)Keys.Escape)
                {
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;
                }
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario))
                {
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionDestino_Enter(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionDestino_Leave(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.White;
        }

        private void lstDireccionDestino_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstDireccionDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRutaDestinatario))
                {
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text + ", " + DireccionUbigeoFin;

                    ListViewItem ItemActual;
                    ItemActual = lstDireccionDestino.SelectedItems[0];
                    entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia = Convert.ToInt32(ItemActual.SubItems[2].Text);
                    txtDireccionDestino.Tag = Convert.ToInt32(ItemActual.SubItems[2].Text);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAgregarConductor_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPlaca.Tag  != null && txtConductor.Tag != null & txtCarreta.Tag != null)
                {
                    DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor.Tag));
                    dgvConductor.Rows.Add(Convert.ToString(txtConductor.Tag), txtConductor.Text, txtLicencia.Text, txtDocIdentidad.Text, Convert.ToString(txtDocIdentidad.Tag), dt.Rows[0]["Nombres"].ToString(), dt.Rows[0]["Apellidos"].ToString(), Convert.ToString(txtPlaca.Tag), txtPlaca.Text, Convert.ToString(txtCarreta.Tag), txtCarreta.Text, txtTarjetaCirculacion.Text, txtTarjetaCirculacionCarreta.Text);

                    idConductorNuevo = Convert.ToInt32(txtConductor.Tag);

                    txtPlaca.Clear();
                    txtCarreta.Clear();
                    txtConductor.Clear();
                    txtTarjetaCirculacion.Clear();
                    txtTarjetaCirculacionCarreta.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvConductor.CurrentRow.Index == 0)
                { MessageBox.Show("El conductor de la guia principal no se puede borrar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else { dgvConductor.Rows.RemoveAt(dgvConductor.CurrentRow.Index); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstPlaca.SelectedItems[0];
                    txtPlaca.Text = ItemActual.SubItems[1].Text.TrimEnd();
                    entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M = txtPlaca.Text.TrimEnd();
                    entGuiaTransportista.idtracto = txtPlaca.Tag.ToString();
                    entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion = ItemActual.SubItems[2].Text;
                    txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length == 9 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.ToString() : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.ToString();
                    txtConductor.Select();
                    txtConductor.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ; }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    gConductor.Select();
                    txtConductor.Focus();


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    gConductor.Select();
                    txtConductor.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtPlaca_Enter(object sender, EventArgs e) { txtPlaca.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca_Leave(object sender, EventArgs e) { txtConductor.BackColor = Color.White; }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstConductor.SelectedItems[0];
                    txtConductor.Text = ItemActual.SubItems[1].Text;

                    if (txtConductor.Tag != null)
                    {
                        DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor.Tag));
                        txtLicencia.Text = dt.Rows[0]["Brevete"].ToString();
                        txtDocIdentidad.Text = dt.Rows[0]["Documento"].ToString();
                        txtTipoDocumentoIdentidad.Text = dt.Rows[0]["TipoDocumento"].ToString();
                        txtTipoDocumentoIdentidad.Tag = dt.Rows[0]["Codigo"].ToString();

                        entNuevoConductor.entGRT_Conductor_Nombres_M = dt.Rows[0]["Nombres"].ToString();
                        entNuevoConductor.entGRT_Conductor_Apellidos_M = dt.Rows[0]["Apellidos"].ToString();
                        entNuevoConductor.entGRT_Conductor_Licencia_M = dt.Rows[0]["Brevete"].ToString();
                        entNuevoConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M = dt.Rows[0]["Documento"].ToString();
                        entNuevoConductor.entGRT_Conductor_TipoDocumentoIdentidad_M = dt.Rows[0]["Codigo"].ToString();
                        entGuiaTransportista.entConductor = entNuevoConductor;
                    }

                    txtCarreta.Select();
                    txtCarreta.Focus();
                }
            }
            catch (Exception ex) {  MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtConductor_Enter(object sender, EventArgs e) { txtConductor.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtConductor_Leave(object sender, EventArgs e) { txtConductor.BackColor = Color.White; }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    txtCarreta.Select();
                    txtCarreta.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    txtCarreta.Select();
                    txtCarreta.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void listView3_Enter(object sender, EventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstCarreta.SelectedItems[0];
                    txtCarreta.Text = ItemActual.SubItems[1].Text;
                    entGuiaTransportista.carreta = txtCarreta.Text.Replace("-", "").Replace(".", "");
                    entGuiaTransportista.idCarreta = txtCarreta.Tag.ToString();
                    TipoVehiculoCarreta = ItemActual.SubItems[4].Text;
                    
                    if (TipoVehiculoCarreta != "CISTERNA")
                    {
                        entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = ItemActual.SubItems[2].Text;
                        txtTarjetaCirculacionCarreta.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.Length == 9 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.ToString() : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.ToString();

                        if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta != "")
                        {
                            entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta;
                            txtTarjetaCirculacionCarreta.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCarreta_Leave(object sender, EventArgs e)
        {
            txtCarreta.BackColor = Color.White;
        }

        private void txtCarreta_Enter(object sender, EventArgs e)
        {
            txtCarreta.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
          /*  try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    txtTarjetaCirculacion.Select();
                    txtTarjetaCirculacion.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    txtTarjetaCirculacion.Select();
                    txtTarjetaCirculacion.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgvData_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtGuiaEventoTransportista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtGuiaEventoRemitente.Focus();
            }
        }

        private void cbPlanillaEvento_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPlanillaEvento.Checked == true)
            {
                GenerarPE = 1;
                groupBox16.Enabled = true;
            }

            if (cbPlanillaEvento.Checked == false)
            {
                GenerarPE = 0;
                groupBox16.Enabled = false;
            }
        }

        private void cbRedondeo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRedondeo.Checked == true) { Redondeo = 0; }

            if (cbRedondeo.Checked == false) { Redondeo = 1; }

            DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(txtRuta.Tag), idTipoProgramacion, Redondeo);
            if (dtRuta.Rows.Count > 0)
            { txtGastoViaje.Text = dtRuta.Rows[0]["GastoTotal"].ToString(); }
        }
    }
}
