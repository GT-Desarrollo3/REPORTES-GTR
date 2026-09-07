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
using ReportesTranspesa.ServiceGRT_QA;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors;
using DevExpress.Utils.Win;
using System.IO;
using System.Threading;
using ReportesTranspesa.Properties;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmNuevoPreviajeTolvas : Form
    {
        public DataTable dtDireccionesRuta;
        public DataTable dtDireccionesRutaDestinatario;
        public clsPreviajeTolvas entPreviajeTolvas = new clsPreviajeTolvas();
        int _IdPreviaje;
        int _Anio;
        public string tarjetaCirculacionTracto = string.Empty;
        public string tarjetaCirculacionCarreta = string.Empty;
        public bool registrarGuia = false;
        public string estado = string.Empty;
        public string impresoraSeleccionada;
        public int idRelacion = -1;

        public FrmNuevoPreviajeTolvas()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTipo(); }

        private void FrmNuevoPreviajeTolvas_Load(object sender, EventArgs e)
        {
            lblEstado.Text = estado;

            if (estado == "PENDIENTE" || registrarGuia == false || estado == "PROGRAMADO")
            {
                quitarToolStripMenuItem.Enabled = true;
                asignarViajeToolStripMenuItem.Enabled = false;
            }

            if (entPreviajeTolvas.TipoOperacion == Utilitario.TipoOperacion.Editar)
            {
                dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(entPreviajeTolvas.idRemitente);
                dtDireccionesRutaDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(entPreviajeTolvas.idDestinatario);
            }

            VerificarPermisosFormulario();
        }


        public void CargarComboTipo()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_ListarOperaciones();
            cbxOperacion.DataSource = dtTipo;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "idTipoProceso";
        }

        private void VerificarPermisosFormulario()
        {
            clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(Utilitario.Instancia.SesionUsuario.usuario); //Trae los permisos del usuario
            DataTable dtPermisosEspeciales = null;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("FrmListaGuiasElectronicas");

            asignarViajeToolStripMenuItem.Enabled = false;
            verViajesToolStripMenuItem.Enabled = false;
            quitarToolStripMenuItem.Enabled = false;

            if (dtPermisos.Rows.Count > 0)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                }

                if (dtPermisosEspeciales != null)
                {
                    if (dtPermisosEspeciales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                        {
                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registrar GT")
                            {
                                asignarViajeToolStripMenuItem.Enabled = true;

                            }


                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Ver Viajes Tolvas")
                            {
                                verViajesToolStripMenuItem.Enabled = true;

                            }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Quitar Conductor Tolvas")
                            {
                                quitarToolStripMenuItem.Enabled = true;

                            }

                        }
                    }
                }

            }


        }

        public void EnviarCodigo(int IdPreviaje, int Anio)
        {
            _IdPreviaje = IdPreviaje;
            _Anio = Anio;
        }

        private DataTable cargarDatosPorDefectoSegunCliente()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CompletarDestinatario_Direcciones(Convert.ToInt32(txtCliente.Tag), Convert.ToInt32(txtRuta.Tag));
            return dt;
        }

        private void InsertarTolva()
        {
            for (int i = 0; dgvConductor.Rows.Count < 0; i++)
            {
                if (dgvConductor.Rows[i].Cells["idPlaca"].Value.ToString() == "" || dgvConductor.Rows[i].Cells["idConductor"].Value.ToString() == "" || dgvConductor.Rows[i].Cells["idCarreta"].Value.ToString() == "")
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (Convert.ToInt32(dgvConductor.Rows[i].Cells["idPlaca"].Value) == 0 || Convert.ToInt32(dgvConductor.Rows[i].Cells["idCarreta"].Value) == 0)
                {

                    MessageBox.Show("Placa Tracto o Carreta tiene ID '0', no se encuentra registrado en Spring , regisrado como tercero de guia remitente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (txtOT.Text.Length == 0 || txtRemitente.Text.Length == 0 || txtDireccionPartida.Text.Length == 0 || txtDestinatario.Text.Length == 0 || txtDireccionDestino.Text.Length == 0 || dgvConductor.RowCount == 0 || txtMotonave.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                entPreviajeTolvas.NroTicket = txtNroTicket.Text;
                entPreviajeTolvas.IdOT = Convert.ToInt32(txtOT.Text);
                entPreviajeTolvas.Tarifa = Convert.ToDecimal(txtTarifa.Text);
                entPreviajeTolvas.idClinte = Convert.ToInt32(txtCliente.Tag);
                entPreviajeTolvas.Cliente = txtCliente.Text;
                entPreviajeTolvas.idRemitente = Convert.ToInt32(txtRemitente.Tag);
                entPreviajeTolvas.Remitente = txtRemitente.Text;
                entPreviajeTolvas.idPartida = Convert.ToInt32(txtDireccionPartida.Tag);
                entPreviajeTolvas.DireccionPartida = txtDireccionPartida.Text;
                entPreviajeTolvas.idDestinatario = Convert.ToInt32(txtDestinatario.Tag);
                entPreviajeTolvas.Destinatario = txtDestinatario.Text;
                entPreviajeTolvas.idDestino = Convert.ToInt32(txtDireccionDestino.Tag);
                entPreviajeTolvas.DireccionDestino = txtDireccionDestino.Text;
                entPreviajeTolvas.FechaProgramacion = Convert.ToDateTime(dtpFechaTraslado.Value);
                entPreviajeTolvas.Producto = txtProducto.Text;
                entPreviajeTolvas.idProducto = Convert.ToInt32(txtProducto.Tag);
                entPreviajeTolvas.Ruta = txtRuta.Text;
                entPreviajeTolvas.idRuta = Convert.ToInt32(txtRuta.Tag);
                entPreviajeTolvas.xmlPlacaConductor = Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvConductor));
                entPreviajeTolvas.UMUso = txtUmUso.Text;
                entPreviajeTolvas.Tiempo = txtTiempo.Text;
                entPreviajeTolvas.Distancia = txtDistancia.Text;
                entPreviajeTolvas.TipoProceso = cbxOperacion.Text;
                entPreviajeTolvas.Motonave = txtMotonave.Text;
                entPreviajeTolvas.Tonelaje = Convert.ToDecimal(txtTonelaje.Text);
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                
                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_Insertar(ref entPreviajeTolvas, Usuario))
                {
                    txtNroTicket.Text = entPreviajeTolvas.NroTicket;
                    lblEstado.BackColor = Color.FromArgb(0, 213, 255);
                    lblEstado.Text = entPreviajeTolvas.Estado;
                        
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGuardar.Enabled = false;
                }
                else
                {   
                    lblEstado.Text = "PENDIENTE";
                    lblEstado.BackColor = Color.Beige;
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGuardar.Focus();
                }
            }
        }

        private void ModificarTolva()
        {
            if (dgvConductor.RowCount == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                entPreviajeTolvas.NroTicket = txtNroTicket.Text;
                entPreviajeTolvas.IdOT = Convert.ToInt32(txtOT.Text);
                entPreviajeTolvas.Tarifa = Convert.ToDecimal(txtTarifa.Text);
                entPreviajeTolvas.idClinte = Convert.ToInt32(txtCliente.Tag);
                entPreviajeTolvas.Cliente = txtCliente.Text;
                entPreviajeTolvas.idRemitente = Convert.ToInt32(txtRemitente.Tag);
                entPreviajeTolvas.Remitente = txtRemitente.Text;
                entPreviajeTolvas.idPartida = Convert.ToInt32(txtDireccionPartida.Tag);
                entPreviajeTolvas.DireccionPartida = txtDireccionPartida.Text;
                entPreviajeTolvas.idDestinatario = Convert.ToInt32(txtDestinatario.Tag);
                entPreviajeTolvas.Destinatario = txtDestinatario.Text;
                entPreviajeTolvas.idDestino = Convert.ToInt32(txtDireccionDestino.Tag);
                entPreviajeTolvas.DireccionDestino = txtDireccionDestino.Text;
                entPreviajeTolvas.FechaProgramacion = Convert.ToDateTime(dtpFechaTraslado.Value);
                entPreviajeTolvas.Producto = txtProducto.Text;
                entPreviajeTolvas.idProducto = Convert.ToInt32(txtProducto.Tag);
                entPreviajeTolvas.Ruta = txtRuta.Text;
                entPreviajeTolvas.idRuta = Convert.ToInt32(txtRuta.Tag);
                entPreviajeTolvas.xmlPlacaConductor = Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvConductor));
                entPreviajeTolvas.UMUso = txtUmUso.Text;
                entPreviajeTolvas.Tiempo = txtTiempo.Text;
                entPreviajeTolvas.Distancia = txtDistancia.Text;
                entPreviajeTolvas.Tonelaje = Convert.ToDecimal(txtTonelaje.Text);

                DataTable dtRespuesta = new DataTable();
            

                if(clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_Modificar(_IdPreviaje, _Anio, ref entPreviajeTolvas, Usuario))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnModificar.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnModificar.Focus();
                }
            }
        }


        private void btnOT_Click(object sender, EventArgs e)
        {
            DataTable dtotsdis = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOtsDisponibles();

            if (dtotsdis.Rows.Count > 0)
            {
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

                        label1.Visible = false;
                        dtgvData.Visible = false;
                        
                        txtOT.Text = OTselec;
                       /* txtTarifa.Text = dgvDataView.GetRowCellValue(filas[i], "TARIFA").ToString();
                        //txtRemitente.Text = dgvDataView.GetRowCellValue(filas[i], "CLIENTE").ToString();
                        txtCliente.Text = dgvDataView.GetRowCellValue(filas[i], "CLIENTE").ToString();
                        txtCliente.Tag = dgvDataView.GetRowCellValue(filas[i], "idCliente").ToString();
                        txtProducto.Text = dgvDataView.GetRowCellValue(filas[i], "PRODUCTO").ToString();
                        txtRuta.Text = dgvDataView.GetRowCellValue(filas[i], "RUTA").ToString();
                        txtRuta.Tag = dgvDataView.GetRowCellValue(filas[i], "idRuta").ToString();
                        */
                        DataTable dtDireccionesAutocompletado = cargarDatosPorDefectoSegunCliente();
                        if (dtDireccionesAutocompletado.Rows.Count > 0)
                        {
                            txtRemitente.Text = dtDireccionesAutocompletado.Rows[0]["Remitente"].ToString();
                            txtRemitente.Tag = dtDireccionesAutocompletado.Rows[0]["idRemitente"].ToString();

                            txtDireccionPartida.Text = dtDireccionesAutocompletado.Rows[0]["DireccionPartida"].ToString();
                            txtDireccionPartida.Tag = dtDireccionesAutocompletado.Rows[0]["SecuenciaPartida"].ToString();

                            txtDestinatario.Text = dtDireccionesAutocompletado.Rows[0]["Destinatario"].ToString();
                            txtDestinatario.Tag = dtDireccionesAutocompletado.Rows[0]["idDestinatario"].ToString();

                            txtDireccionDestino.Text = dtDireccionesAutocompletado.Rows[0]["DireccionDestino"].ToString();
                            txtDireccionDestino.Tag = dtDireccionesAutocompletado.Rows[0]["SecuenciaDestino"].ToString();

                            /*txtDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Space)));
                            lstEmpresaDestinatario.Select();
                            lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                            lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));*/
                            
                            dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtCliente.Tag));
                            dtDireccionesRutaDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtDestinatario.Tag));

                        }

                      /*  txtRemitente_KeyPress(this, new KeyPressEventArgs((char)(Keys.Space)));
                        lstEmpresaRemitente.Select();
                        lstEmpresaRemitente_KeyUp(this, new KeyEventArgs(Keys.Down));
                        lstEmpresaRemitente_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));*/

                        txtOT_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                        txtRemitente.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOT_KeyPress(object sender, KeyPressEventArgs e)
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

                        txtCliente.Text = datosOT.Rows[0]["BUSQUEDA"].ToString();
                        txtCliente.Tag = Convert.ToInt32(datosOT.Rows[0]["IdClienteFacturacion"].ToString());
                        txtRuta.Text = datosOT.Rows[0]["DESCRIPCION"].ToString();
                        txtRuta.Tag = Convert.ToInt32(datosOT.Rows[0]["IdRuta"].ToString());
                        txtProducto.Text = datosOT.Rows[0]["Nombre"].ToString();
                        txtProducto.Tag = Convert.ToInt32(datosOT.Rows[0]["Producto"].ToString());
                        txtTarifa.Text = Convert.ToString(datosOT.Rows[0]["TARIFA"].ToString());
                        txtUmUso.Text = datosOT.Rows[0]["UMUso"].ToString();
                        txtTiempo.Text = Convert.ToString(datosOT.Rows[0]["tiempo"].ToString());
                        txtDistancia.Text = Convert.ToString(datosOT.Rows[0]["Distancia"].ToString());
                      
             
                        DataTable dtDireccionesAutocompletado = cargarDatosPorDefectoSegunCliente();
                        if (dtDireccionesAutocompletado.Rows.Count > 0)
                        {
                            txtRemitente.Text = dtDireccionesAutocompletado.Rows[0]["Remitente"].ToString();
                            txtRemitente.Tag = dtDireccionesAutocompletado.Rows[0]["idRemitente"].ToString();

                            txtDireccionPartida.Text = dtDireccionesAutocompletado.Rows[0]["DireccionPartida"].ToString();
                            txtDireccionPartida.Tag = dtDireccionesAutocompletado.Rows[0]["SecuenciaPartida"].ToString();

                            txtDestinatario.Text = dtDireccionesAutocompletado.Rows[0]["Destinatario"].ToString();
                            txtDestinatario.Tag = dtDireccionesAutocompletado.Rows[0]["idDestinatario"].ToString();

                            txtDireccionDestino.Text = dtDireccionesAutocompletado.Rows[0]["DireccionDestino"].ToString();
                            txtDireccionDestino.Tag = dtDireccionesAutocompletado.Rows[0]["SecuenciaDestino"].ToString();

                            /*txtDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Space)));
                            lstEmpresaDestinatario.Select();
                            lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                            lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));*/

                            dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtCliente.Tag));
                            dtDireccionesRutaDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtDestinatario.Tag));

                        }



                    }
                    else
                    {
                        txtCliente.Text = "";
                        txtCliente.Tag = null;
                        txtRuta.Text = "";
                        txtRuta.Tag = null;
                        txtProducto.Text = "";
                        txtProducto.Tag = null;
                        txtTarifa.Text = "";
                        txtUmUso.Text = "";
                        txtTiempo.Text = "";
                        txtDistancia.Text = "";
                      
                    }
                }


            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
             
        private void lstEmpresaRemitente_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRemitente, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtRemitente.Tag));
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaRemitente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem EmpresaRemitente;
            EmpresaRemitente = lstEmpresaRemitente.SelectedItems[0];
            txtRemitente.Text = EmpresaRemitente.SubItems[1].Text;
            txtRemitente.Tag = EmpresaRemitente.SubItems[0].Text;
            lstEmpresaRemitente.Visible = false;
            txtDireccionPartida.Focus();
        }

        private void txtRemitente_Enter(object sender, EventArgs e)
        {
            txtRemitente.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRemitente, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtRemitente.Tag));
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtRemitente.Tag));
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRemitente_Leave(object sender, EventArgs e)
        {
            txtRemitente.BackColor = Color.White;
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

        private void lstDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRuta))
                {
                    g_destinatario.Select();
                    txtDestinatario.Focus();
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

        private void lstDireccionPartida_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem DireccionPartida;
            DireccionPartida = lstDireccionPartida.SelectedItems[0];
            txtDireccionPartida.Text = DireccionPartida.SubItems[1].Text;
            txtDireccionPartida.Tag = DireccionPartida.SubItems[0].Text;
            lstDireccionPartida.Visible = false;
            txtDestinatario.Focus();
        }

        private void txtDireccionPartida_Enter(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRuta))
                {
                    g_destinatario.Select();
                    txtDestinatario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta))
                {
                    g_destinatario.Select();
                    txtDestinatario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionPartida_Leave(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.White;
        }

        private void lstEmpresaDestinatario_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDestinatario, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    dtDireccionesRutaDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtDestinatario.Tag));
              
                    txtDireccionDestino.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaDestinatario_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaDestinatario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem EmpresaDestinatario;
            EmpresaDestinatario = lstEmpresaDestinatario.SelectedItems[0];
            txtDestinatario.Text = EmpresaDestinatario.SubItems[1].Text;
            txtDestinatario.Tag = EmpresaDestinatario.SubItems[0].Text;
            lstEmpresaDestinatario.Visible = false;
            txtDireccionDestino.Focus();
        }

        private void txtDestinatario_Enter(object sender, EventArgs e)
        {
            txtDestinatario.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDestinatario, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    txtDireccionDestino.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtDestinatario_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    txtDireccionDestino.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDestinatario_Leave(object sender, EventArgs e)
        {
            txtDestinatario.BackColor = Color.White;
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
                    g_placa.Select();
                    txtPlaca.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionDestino_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem DireccionDestino;
            DireccionDestino = lstDireccionDestino.SelectedItems[0];
            txtDireccionDestino.Text = DireccionDestino.SubItems[1].Text;
            txtDireccionDestino.Tag = DireccionDestino.SubItems[0].Text;
            lstDireccionDestino.Visible = false;
            txtPlaca.Focus();
        }

        private void txtDireccionDestino_Enter(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRutaDestinatario))
                {
                    g_placa.Select();
                    txtPlaca.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario))
                {
                    g_placa.Select();
                    txtPlaca.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionDestino_Leave(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.White;
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    txtPlaca.Tag = ItemActual.SubItems[0].Text.TrimEnd();
                    tarjetaCirculacionTracto = ItemActual.SubItems[2].Text.TrimEnd();
                    txtCarreta.Select();
                    txtCarreta.Focus();

                    //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                    DataTable ValidaUnidadVencida = new DataTable();
                    ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(Convert.ToInt32(txtPlaca.Tag), "UNIDAD");

                    if (ValidaUnidadVencida.Rows.Count > 0)
                    {
                        idRelacion = Convert.ToInt32(txtPlaca.Tag);

                        grbControlDoc.Visible = true;
                        grbControlDoc.Size = new System.Drawing.Size(420, 150);
                        grbControlDoc.Location = new Point(379, 166);
                        lsvControlDoc.Visible = true;
                        btnCerrarControlDoc.Visible = true;
                        btnGenerarReq.Visible = true;
                        grbControlDoc.BringToFront();

                        clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                        lsvControlDoc.Columns[0].Width = 0;
                        lsvControlDoc.Columns[1].Width = 0;
                        lsvControlDoc.Columns[2].Width = 0;
                        lsvControlDoc.Columns[3].Width = 250;
                        lsvControlDoc.Columns[7].Width = 0;
                        lsvControlDoc.Columns[8].Width = 0;
                        lsvControlDoc.Columns[9].Width = 0;

                        lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                        lsvControlDoc.BringToFront();
                        lsvControlDoc.Visible = true;
                    }
                    else
                    {
                        lsvControlDoc.Clear();
                        grbControlDoc.Visible = false;
                        grbControlDoc.SendToBack();
                    }

                    if (Convert.ToInt32(txtPlaca.Tag) > 0)
                    {
                        DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(Convert.ToInt32(txtPlaca.Tag));
                        string Rpta;

                        if (UnidadBloq.Rows.Count > 0)
                        {
                            Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                            MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            txtPlaca.Clear();
                            txtPlaca.Tag = "";
                            txtPlaca.Focus();
                            return;
                        }

                        // Consulta para verificar si las Inspecciones Técnicas están por vencer
                        DataTable UnidadVencida2 = new DataTable();
                        UnidadVencida2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(Convert.ToInt32(txtPlaca.Tag), "UNIDAD");

                        if (UnidadVencida2.Rows.Count > 0)
                        {
                            MessageBox.Show("No puede seleccionar la unidad " + txtPlaca.Text + " porque su Inspección Técnica Vehicular está por vencer o ya está vencida.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            txtPlaca.Clear();
                            txtPlaca.Tag = "";
                            txtPlaca.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem PlacaActual;
            PlacaActual = lstPlaca.SelectedItems[0];
            txtPlaca.Text = PlacaActual.SubItems[1].Text;
            txtPlaca.Tag = PlacaActual.SubItems[0].Text;
            tarjetaCirculacionTracto = PlacaActual.SubItems[2].Text;
            lstPlaca.Visible = false;
            txtCarreta.Focus();

            DataTable ValidaUnidadVencida = new DataTable();
            ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(Convert.ToInt32(txtPlaca.Tag), "UNIDAD");

            if (ValidaUnidadVencida.Rows.Count > 0)
            {
                idRelacion = Convert.ToInt32(txtPlaca.Tag);

                grbControlDoc.Visible = true;
                grbControlDoc.Size = new System.Drawing.Size(420, 150);
                grbControlDoc.Location = new Point(379, 166);
                lsvControlDoc.Visible = true;
                btnCerrarControlDoc.Visible = true;
                btnGenerarReq.Visible = true;
                grbControlDoc.BringToFront();

                clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                lsvControlDoc.Columns[0].Width = 0;
                lsvControlDoc.Columns[1].Width = 0;
                lsvControlDoc.Columns[2].Width = 0;
                lsvControlDoc.Columns[3].Width = 250;
                lsvControlDoc.Columns[7].Width = 0;
                lsvControlDoc.Columns[8].Width = 0;
                lsvControlDoc.Columns[9].Width = 0;

                lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                lsvControlDoc.BringToFront();
                lsvControlDoc.Visible = true;
            }
            else
            {
                lsvControlDoc.Clear();
                grbControlDoc.Visible = false;
                grbControlDoc.SendToBack();
            }

            if (Convert.ToInt32(txtPlaca.Tag) > 0)
            {
                DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(Convert.ToInt32(txtPlaca.Tag));
                string Rpta;

                if (UnidadBloq.Rows.Count > 0)
                {
                    Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                    MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtPlaca.Clear();
                    txtPlaca.Tag = "";
                    txtPlaca.Focus();
                    return;
                }

                // Consulta para verificar si las Inspecciones Técnicas están por vencer
                DataTable UnidadVencida2 = new DataTable();
                UnidadVencida2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(Convert.ToInt32(txtPlaca.Tag), "UNIDAD");

                if (UnidadVencida2.Rows.Count > 0)
                {
                    MessageBox.Show("No puede seleccionar la unidad " + txtPlaca.Text + " porque su Inspección Técnica Vehicular está por vencer o ya está vencida.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtPlaca.Clear();
                    txtPlaca.Tag = "";
                    txtPlaca.Focus();
                    return;
                }
            }
        }

        private void txtPlaca_Enter(object sender, EventArgs e)
        {
            txtPlaca.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    txtCarreta.Focus();
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
                    txtCarreta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            txtPlaca.BackColor = Color.White;
        }

        private void lstCarreta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstCarreta.SelectedItems[0];
                    txtCarreta.Text = ItemActual.SubItems[1].Text.TrimEnd();
                    txtCarreta.Tag = ItemActual.SubItems[0].Text.TrimEnd();
                    tarjetaCirculacionCarreta = ItemActual.SubItems[2].Text.TrimEnd();
                    txtConductor.Select();
                    txtConductor.Focus();

                    DataTable ValidaUnidadVencida = new DataTable();
                    ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(Convert.ToInt32(txtCarreta.Tag), "UNIDAD");

                    if (ValidaUnidadVencida.Rows.Count > 0)
                    {
                        idRelacion = Convert.ToInt32(txtCarreta.Tag);

                        grbControlDoc.Visible = true;
                        grbControlDoc.Size = new System.Drawing.Size(420, 150);
                        grbControlDoc.Location = new Point(474, 166);
                        lsvControlDoc.Visible = true;
                        btnCerrarControlDoc.Visible = true;
                        btnGenerarReq.Visible = true;
                        grbControlDoc.BringToFront();

                        clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                        lsvControlDoc.Columns[0].Width = 0;
                        lsvControlDoc.Columns[1].Width = 0;
                        lsvControlDoc.Columns[2].Width = 0;
                        lsvControlDoc.Columns[3].Width = 250;
                        lsvControlDoc.Columns[7].Width = 0;
                        lsvControlDoc.Columns[8].Width = 0;
                        lsvControlDoc.Columns[9].Width = 0;

                        lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                        lsvControlDoc.BringToFront();
                        lsvControlDoc.Visible = true;
                    }
                    else
                    {
                        lsvControlDoc.Clear();
                        grbControlDoc.Visible = false;
                        grbControlDoc.SendToBack();
                    }

                    if (Convert.ToInt32(txtCarreta.Tag) > 0)
                    {
                        DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(Convert.ToInt32(txtCarreta.Tag));
                        string Rpta;

                        if (UnidadBloq.Rows.Count > 0)
                        {
                            Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                            MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            txtCarreta.Clear();
                            txtCarreta.Tag = "";
                            txtCarreta.Focus();
                            return;
                        }

                        // Consulta para verificar si las Inspecciones Técnicas están por vencer
                        DataTable UnidadVencida2 = new DataTable();
                        UnidadVencida2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(Convert.ToInt32(txtCarreta.Tag), "UNIDAD");

                        if (UnidadVencida2.Rows.Count > 0)
                        {
                            MessageBox.Show("No puede seleccionar la unidad " + txtCarreta.Text + " porque su Inspección Técnica Vehicular está por vencer o ya está vencida.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            txtCarreta.Clear();
                            txtCarreta.Tag = "";
                            txtCarreta.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
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

        private void lstCarreta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem PlacaActual;
            PlacaActual = lstCarreta.SelectedItems[0];
            txtCarreta.Text = PlacaActual.SubItems[1].Text;
            txtCarreta.Tag = PlacaActual.SubItems[0].Text;
            tarjetaCirculacionCarreta = PlacaActual.SubItems[2].Text.TrimEnd();
            lstCarreta.Visible = false;
            txtConductor.Focus();

            DataTable ValidaUnidadVencida = new DataTable();
            ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(Convert.ToInt32(txtCarreta.Tag), "UNIDAD");

            if (ValidaUnidadVencida.Rows.Count > 0)
            {
                idRelacion = Convert.ToInt32(txtCarreta.Tag);

                grbControlDoc.Visible = true;
                grbControlDoc.Size = new System.Drawing.Size(420, 150);
                grbControlDoc.Location = new Point(474, 166);
                lsvControlDoc.Visible = true;
                btnCerrarControlDoc.Visible = true;
                btnGenerarReq.Visible = true;
                grbControlDoc.BringToFront();

                clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                lsvControlDoc.Columns[0].Width = 0;
                lsvControlDoc.Columns[1].Width = 0;
                lsvControlDoc.Columns[2].Width = 0;
                lsvControlDoc.Columns[3].Width = 250;
                lsvControlDoc.Columns[7].Width = 0;
                lsvControlDoc.Columns[8].Width = 0;
                lsvControlDoc.Columns[9].Width = 0;

                lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                lsvControlDoc.BringToFront();
                lsvControlDoc.Visible = true;
            }
            else
            {
                lsvControlDoc.Clear();
                grbControlDoc.Visible = false;
                grbControlDoc.SendToBack();
            }

            if (Convert.ToInt32(txtCarreta.Tag) > 0)
            {
                DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(Convert.ToInt32(txtCarreta.Tag));
                string Rpta;

                if (UnidadBloq.Rows.Count > 0)
                {
                    Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                    MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtCarreta.Clear();
                    txtCarreta.Tag = "";
                    txtCarreta.Focus();
                    return;
                }

                // Consulta para verificar si las Inspecciones Técnicas están por vencer
                DataTable UnidadVencida2 = new DataTable();
                UnidadVencida2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(Convert.ToInt32(txtCarreta.Tag), "UNIDAD");

                if (UnidadVencida2.Rows.Count > 0)
                {
                    MessageBox.Show("No puede seleccionar la unidad " + txtCarreta.Text + " porque su Inspección Técnica Vehicular está por vencer o ya está vencida.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtCarreta.Clear();
                    txtCarreta.Tag = "";
                    txtCarreta.Focus();
                    return;
                }
            }
        }

        private void txtCarreta_Enter(object sender, EventArgs e)
        {
            txtCarreta.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    txtConductor.Focus();
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
                    txtConductor.Focus();
                }
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

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    if ((e.KeyChar == (char)Keys.Enter) && !lstConductor.Items.Count.Equals(0))
                    {
                        ListViewItem ConductorActual;
                        ConductorActual = lstConductor.SelectedItems[0];
                        txtConductor.Text = ConductorActual.SubItems[1].Text;
                        txtConductor.Tag = ConductorActual.SubItems[0].Text;

                        // Agregar restricción para verificar si el conductor está bloqueado
                        DataTable ConductorBloqueado = new DataTable();
                        ConductorBloqueado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado(Convert.ToInt32(txtConductor.Tag), dtpFechaTraslado.Value);
                        string respta = Convert.ToString(ConductorBloqueado.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            if (txtConductor.Tag != null)
                            { DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor.Tag)); }

                            //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                            DataTable ValidaUnidadVencida = new DataTable();
                            ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(Convert.ToInt32(txtConductor.Tag), "CONDUCTOR");

                            if (ValidaUnidadVencida.Rows.Count > 0)
                            {
                                grbControlDoc.Visible = true;
                                grbControlDoc.Size = new System.Drawing.Size(420, 150);
                                grbControlDoc.Location = new Point(588, 166);
                                lsvControlDoc.Visible = true;
                                btnCerrarControlDoc.Visible = true;
                                btnGenerarReq.Visible = false;
                                grbControlDoc.BringToFront();

                                clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                                lsvControlDoc.Columns[0].Width = 0;
                                lsvControlDoc.Columns[1].Width = 0;
                                lsvControlDoc.Columns[2].Width = 0;
                                lsvControlDoc.Columns[3].Width = 190;
                                lsvControlDoc.Columns[4].Width = 60;
                                lsvControlDoc.Columns[5].Width = 80;
                                lsvControlDoc.Columns[6].Width = 85;
                                lsvControlDoc.Columns[7].Width = 0;
                                lsvControlDoc.Columns[8].Width = 0;
                                lsvControlDoc.Columns[9].Width = 0;

                                lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                                lsvControlDoc.BringToFront();
                                lsvControlDoc.Visible = true;
                                //  lsvControlDoc.Focus();
                            }

                            btnAgregar1.Focus();
                        }
                        else
                        {
                            MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtConductor.Tag = "";
                            txtConductor.Clear();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstConductor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ConductorActual;
            ConductorActual = lstConductor.SelectedItems[0];
            txtConductor.Text = ConductorActual.SubItems[1].Text;
            txtConductor.Tag = ConductorActual.SubItems[0].Text;
            lstConductor.Visible = false;

            // Agregar restricción para verificar si el conductor está bloqueado
            DataTable ConductorBloqueado = new DataTable();
            ConductorBloqueado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado(Convert.ToInt32(txtConductor.Tag), dtpFechaTraslado.Value);
            string respta = Convert.ToString(ConductorBloqueado.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);
            if (NroRspta == "0")
            {
                if (txtConductor.Tag != null)
                { DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor.Tag)); }

                //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(Convert.ToInt32(txtConductor.Tag), "CONDUCTOR");

                if (ValidaUnidadVencida.Rows.Count > 0)
                {
                    grbControlDoc.Visible = true;
                    grbControlDoc.Size = new System.Drawing.Size(420, 150);
                    grbControlDoc.Location = new Point(588, 166);
                    lsvControlDoc.Visible = true;
                    btnCerrarControlDoc.Visible = true;
                    btnGenerarReq.Visible = false;
                    grbControlDoc.BringToFront();

                    clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                    lsvControlDoc.Columns[0].Width = 0;
                    lsvControlDoc.Columns[1].Width = 0;
                    lsvControlDoc.Columns[2].Width = 0;
                    lsvControlDoc.Columns[3].Width = 190;
                    lsvControlDoc.Columns[4].Width = 60;
                    lsvControlDoc.Columns[5].Width = 80;
                    lsvControlDoc.Columns[6].Width = 85;
                    lsvControlDoc.Columns[7].Width = 0;
                    lsvControlDoc.Columns[8].Width = 0;
                    lsvControlDoc.Columns[9].Width = 0;

                    lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                    lsvControlDoc.BringToFront();
                    lsvControlDoc.Visible = true;
                    //  lsvControlDoc.Focus();
                }

                btnAgregar1.Focus();
            }
            else
            {
                MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConductor.Tag = "";
                txtConductor.Clear();
            }
        }

        private void txtConductor_Enter(object sender, EventArgs e)
        {
            txtConductor.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    btnAgregar.Select();
                    btnAgregar.Focus();
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
                    btnAgregar1.Select();
                    btnAgregar1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtConductor_Leave(object sender, EventArgs e)
        {
            txtConductor.BackColor = Color.White;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
     

                if (dgvConductor.CurrentRow.Cells["Guias"].Value.ToString() == "" || dgvConductor.CurrentRow.Cells["Guias"].Value.ToString() == "0")
                {
                    dgvConductor.Rows.RemoveAt(dgvConductor.CurrentRow.Index);
                }
                else
                {
                    MessageBox.Show("Placa y Conductor ya tienen Viaje y Guia Generado, no es posible Eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void asignarViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTolvasOT = new DataTable();
                dtTolvasOT.Columns.Add("Linea", typeof(Int32));
                dtTolvasOT.Columns.Add("OT", typeof(Int32));
                dtTolvasOT.Columns.Add("Viaje", typeof(String));
                dtTolvasOT.Columns.Add("GT", typeof(String));
                dtTolvasOT.Columns.Add("GR", typeof(String));
                dtTolvasOT.Columns.Add("Tarifa", typeof(String));
                dtTolvasOT.Columns.Add("Cantidad", typeof(String));
                dtTolvasOT.Columns.Add("IdProgramacion", typeof(Int32));
                dtTolvasOT.Columns.Add("Anio", typeof(String));
                dtTolvasOT.Columns.Add("NroTicket", typeof(String));
                dtTolvasOT.Columns.Add("IdRuta", typeof(String));
                dtTolvasOT.Columns.Add("Ruta", typeof(String));

                if (lblEstado.Text == "ATENDIDO" && registrarGuia == true)
                {


                    FrmGuiaElectronicaTransportista openGenerarGuia = new FrmGuiaElectronicaTransportista();
                    openGenerarGuia.TipoProgramacion = "TOLVAS";
                    openGenerarGuia.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                    openGenerarGuia.entGuiaTransportista.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                    openGenerarGuia.entGuiaTransportista.idproveedor = Convert.ToInt32("1553"); // GRUPO TRANSPESA
                   
                    openGenerarGuia.txtOT.ReadOnly = true;
                    openGenerarGuia.dtTolvasOT = dtTolvasOT;
                    

                    //Datos del cliente (Sistema)
                    openGenerarGuia.txtCliente.Tag = txtCliente.Tag;
                    openGenerarGuia.txtCliente.Text = txtCliente.Text;
                    openGenerarGuia.entGuiaTransportista.idcliente = Convert.ToInt32(txtCliente.Tag);
                    openGenerarGuia.entGuiaTransportista.Cliente = Convert.ToString(txtCliente.Text);


                    //CODIGO PROGRAMACION TOLVAS
                    openGenerarGuia.entGuiaTransportista.idProgramacion = Convert.ToInt32(entPreviajeTolvas.idPreviajeTolvas);
                    openGenerarGuia.entGuiaTransportista.AnioProgramacion = Convert.ToInt32(entPreviajeTolvas.anio);
                    openGenerarGuia.entGuiaTransportista.CodigoProgramacion = Convert.ToString(entPreviajeTolvas.NroTicket);

                    if (txtOT.Text.Length > 0)
                    {
                        openGenerarGuia.entGuiaTransportista.idOT = Convert.ToInt32(txtOT.Text);
                    }
                    else
                    {
                        MessageBox.Show("Programacion no tiene OT asignada, no es posible generar Guia Electronica", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    openGenerarGuia.entGuiaTransportista.idOT = entPreviajeTolvas.IdOT;
                    openGenerarGuia.entGuiaTransportista.idviaje = 0;
                    openGenerarGuia.entGuiaTransportista.viaje = "";
                    openGenerarGuia.entGuiaTransportista.estadoviaje = "PENDIENTE";
                    openGenerarGuia.entGuiaTransportista.idRuta = Convert.ToInt32(txtRuta.Tag);
                    openGenerarGuia.entGuiaTransportista.ruta = txtRuta.Text;
                    openGenerarGuia.entGuiaTransportista.conductor = dgvConductor.CurrentRow.Cells["Conductor"].Value.ToString(); // nombre y apellidos completos concatenados del conductor
                    openGenerarGuia.entGuiaTransportista.idconductor = Convert.ToInt32(dgvConductor.CurrentRow.Cells["idConductor"].Value.ToString());
                    openGenerarGuia.entGuiaTransportista.tracto = Convert.ToString(dgvConductor.CurrentRow.Cells["Placa"].Value).Replace("-", "").Replace(".", "").TrimEnd(); // placa
                    openGenerarGuia.entGuiaTransportista.idtracto = Convert.ToString(dgvConductor.CurrentRow.Cells["idPlaca"].Value); // id placa
                    openGenerarGuia.entGuiaTransportista.idCarreta = Convert.ToString(dgvConductor.CurrentRow.Cells["idCarreta"].Value);
                    openGenerarGuia.entGuiaTransportista.carreta = Convert.ToString(dgvConductor.CurrentRow.Cells["Carreta"].Value).Replace("-", "").Replace(".", "").TrimEnd();
                    openGenerarGuia.entGuiaTransportista.FechaProgramacion = dtpFechaTraslado.Text;
                    openGenerarGuia.enProductoTolvas.entGTR_ProductoBienes_Descripcion = txtProducto.Text;
                    openGenerarGuia.enProductoTolvas.entGTR_ProductoBienes_Codigo = txtProducto.Tag.ToString();
                    openGenerarGuia.enProductoTolvas.entGTR_ProductoBienes_UnidadMedida = txtUmUso.Text;
                    
      
                    openGenerarGuia.entGuiaTransportista.CodigoProgramacion = entPreviajeTolvas.NroTicket;
                    openGenerarGuia.entGuiaTransportista.idProgramacion = Convert.ToInt32(entPreviajeTolvas.idPreviajeTolvas);
                    openGenerarGuia.entGuiaTransportista.TipoGuia = "T"; //Transportista
                    openGenerarGuia.entGuiaTransportista.idTipoProgramacion = 1; // 1= TOLVAS
                    openGenerarGuia.PesoTotal = "0.00";

                    if (entPreviajeTolvas.Estado == "PROGRAMADO") { openGenerarGuia.entGuiaTransportista.idEstadoProgramacion = 1;}
                    if (entPreviajeTolvas.Estado == "ATENDIDO") { openGenerarGuia.entGuiaTransportista.idEstadoProgramacion = 9; }
                    if (entPreviajeTolvas.Estado == "ANULADO") { openGenerarGuia.entGuiaTransportista.idEstadoProgramacion = 10; }


                    DataTable dtDatos = cargarDatosPorDefectoSegunCliente();
                    if (dtDatos.Rows.Count > 0)
                    {
                        openGenerarGuia.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = Convert.ToString(txtRemitente.Text);
                        openGenerarGuia.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = txtDestinatario.Text;
                        openGenerarGuia.entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia = Convert.ToInt32(dtDatos.Rows[0]["SecuenciaPartida"]);
                        openGenerarGuia.entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia = Convert.ToInt32(dtDatos.Rows[0]["SecuenciaDestino"]);
                        openGenerarGuia.entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = dtDatos.Rows[0]["DireccionPartida"].ToString();
                        openGenerarGuia.entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = dtDatos.Rows[0]["DireccionDestino"].ToString();
                        openGenerarGuia.txtDireccionPartida.Text = dtDatos.Rows[0]["DireccionPartida"].ToString();
                        openGenerarGuia.txtDireccionDestino.Text = dtDatos.Rows[0]["DireccionDestino"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("La ruta de la OT seleccionada no tiene direccion de autocompletado registrado, Favor ingresar a Maestro de Rutas por Clientes y registrar sus direcciones");
                        return;
                    }

                    dtTolvasOT.Rows.Add(1, entPreviajeTolvas.IdOT, "", "", "", entPreviajeTolvas.Tarifa, "0.000000", entPreviajeTolvas.idPreviajeTolvas, entPreviajeTolvas.anio, entPreviajeTolvas.NroTicket, entPreviajeTolvas.idRuta, entPreviajeTolvas.Ruta);
                    
                    openGenerarGuia.txtPlaca.ReadOnly = true;
                    openGenerarGuia.txtCarreta.ReadOnly = true;
                    openGenerarGuia.txtConductor.ReadOnly = true;
                    openGenerarGuia.txtPlaca2.ReadOnly = true;
                    openGenerarGuia.txtCarreta2.ReadOnly = true;
                    openGenerarGuia.txtConductor2.ReadOnly = true;
                    openGenerarGuia.txtTarjetaCirculacion.ReadOnly = true;
                    openGenerarGuia.btnAgregarProgramacion.Enabled = false;
                    openGenerarGuia.txtEmpresaRemitente.ReadOnly = true;
                    openGenerarGuia.txtEmpresaDestinatario.ReadOnly = true;
                    //openGenerarGuia.txtDireccionPartida.ReadOnly = true;
                    //openGenerarGuia.txtDireccionDestino.ReadOnly = true;
                    openGenerarGuia.txtRuta.ReadOnly = true;
                    openGenerarGuia.CargarListaTolvas += new FrmGuiaElectronicaTransportista.CargarListaTolvasEventHandler(CargarRespuesta);
                    openGenerarGuia.Show();
                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRespuesta(bool esRegistroExitoso,FrmGuiaElectronicaTransportista  transportista)
        {
            if(esRegistroExitoso)
            {
                dgvConductor.CurrentRow.Cells["Guias"].Value = Convert.ToString(Convert.ToInt32(dgvConductor.CurrentRow.Cells["Guias"].Value) + 1); 
            }
            
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            InsertarTolva();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarTolva();
        }

        private void dtgvData_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) || e.KeyChar == (char)Keys.Escape)
                {
                    dtgvData.Visible = false;
                    label1.Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void verViajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
          
                    if (Convert.ToInt32(dgvConductor.CurrentRow.Cells["Guias"].Value.ToString()) > 0)
                    {
                        frmListaViajesTolvas frmViajesTolvas = new frmListaViajesTolvas();
                        frmViajesTolvas.txtNroTicket.Text = entPreviajeTolvas.NroTicket;
                        frmViajesTolvas.anio = entPreviajeTolvas.anio;
                        frmViajesTolvas.idPreviajeTolvas = entPreviajeTolvas.idPreviajeTolvas;
                        frmViajesTolvas.txtPlaca.Text = dgvConductor.CurrentRow.Cells["Placa"].Value.ToString();
                        frmViajesTolvas.Show();

                    }
                    else
                    {
                        MessageBox.Show("No existen viajes a consultar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
       
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaca_KeyDown(object sender, KeyEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    txtCarreta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }



        private void btnCerrarControlDoc_Click(object sender, EventArgs e)
        {
            grbControlDoc.Visible = false;
            grbControlDoc.SendToBack(); 
        }

        private void btnAgregar1_Click(object sender, EventArgs e)
        {
            Boolean esigual = false;
            int pos = 0;
            try
            {


                if (txtPlaca.Tag.ToString().Length == 0 || txtCarreta.Tag.ToString().Length == 0 || txtConductor.Tag.ToString().Length == 0 || txtPlaca.Tag.ToString() == "0" || txtCarreta.Tag.ToString() == "0" || txtConductor.Tag.ToString() == "0")
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txtPlaca.Text.Length == 0)
                    {
                        txtPlaca.Focus();
                    }
                    else
                    {
                        if (txtCarreta.Text.Length == 0)
                        {
                            txtCarreta.Focus();
                        }
                        else
                        {
                            txtConductor.Focus();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgvConductor.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvConductor.Rows[i].Cells["idPlaca"].Value) == Convert.ToInt32(txtPlaca.Tag) && Convert.ToInt32(dgvConductor.Rows[i].Cells["idConductor"].Value) == Convert.ToInt32(txtConductor.Tag))
                        {
                            esigual = true;
                            pos = i;
                        }
                    }

                    if (!esigual)
                    {
                        DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor.Tag));
                        dgvConductor.Rows.Add(Convert.ToString(txtPlaca.Tag), txtPlaca.Text, Convert.ToString(txtCarreta.Tag), txtCarreta.Text, Convert.ToString(txtConductor.Tag), txtConductor.Text, tarjetaCirculacionTracto, tarjetaCirculacionCarreta, "0");
                        txtPlaca.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No puede ingresar datos repetidos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    txtPlaca.Tag = "";
                    txtCarreta.Tag = "";
                    txtConductor.Tag = "";
                    txtPlaca.Clear();
                    txtCarreta.Clear();
                    txtConductor.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarReq_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idRelacion, "UNIDAD");

                if (MessageBox.Show("¿Desea generar un requerimiento para estos documentos?", "GENERAR REQUERIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    for (int i = 0; i < ValidaUnidadVencida.Rows.Count; i++)
                    {
                        int idDocumento = Convert.ToInt32(ValidaUnidadVencida.Rows[i]["IDDOCUMENTO"]);
                        int idTipoDocumento = Convert.ToInt32(ValidaUnidadVencida.Rows[i]["ID_TIPO_DOCUMENTO"]);
                        string TipoDocumento = Convert.ToString(ValidaUnidadVencida.Rows[i]["TIPO_DOCUMENTO"]);
                        int idUnidad = idRelacion;
                        string Placa = Convert.ToString(ValidaUnidadVencida.Rows[i]["RELACION_NOMBRE"]);
                        string CentroCosto = Convert.ToString(ValidaUnidadVencida.Rows[i]["CENTRO_COSTO"]);
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        if (idTipoDocumento != 17 && idTipoDocumento != 3)
                        { MessageBox.Show("Solo puede generar requerimientos para inspecciones técnicas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        else
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;

                            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlDocumentos_GenerarRequerimiento(idDocumento, TipoDocumento, idUnidad, Placa, CentroCosto, Usuario);
                            respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRspta = respta.Substring(0, 1);
                            if (NroRspta == "0")
                            {
                                MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    idRelacion = -1;
                    btnCerrarControlDoc_Click(sender, e);
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el requerimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
