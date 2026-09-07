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
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmListaFaltantesMercaderia : Form
    {
        public int valorEstado;
        public string esVALE;
        public string Moneda;
        public int xClick = 0, yClick = 0;

        public frmListaFaltantesMercaderia()
        {
            InitializeComponent();
            cbxAsume.SelectedIndexChanged -= cbxAsume_SelectedIndexChanged;
            cbxEstado.SelectedIndexChanged -= cbxEstado_SelectedIndexChanged;
            cbxMotivo.SelectedIndexChanged -= cbxMotivo_SelectedIndexChanged;
        }

        private void cbxAsume_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboAsume();
        }

        private void cbxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboEstado();
        }

        private void cbxMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboMotivo();
        }

        private void frmRegistroFaltantesMercaderia_Shown(object sender, EventArgs e)
        {
            txtConductor.Focus();
        }

        private void frmListaFaltantesMercaderia_Load(object sender, EventArgs e)
        {
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaFaltantesMercaderia");
            try
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        btnAgregar.Enabled = true;
                        btnNuevoFaltante.Enabled = true;
                    }
                    else { btnAgregar.Enabled = false; btnNuevoFaltante.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        EditarFaltanteToolStripMenuItem.Enabled = true;
                        ModificarFaltanteToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        EditarFaltanteToolStripMenuItem.Enabled = false;
                        ModificarFaltanteToolStripMenuItem.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                    {
                        cerrarFaltanteToolStripMenuItem.Enabled = true;
                    }
                    else { cerrarFaltanteToolStripMenuItem.Enabled = false; }
                }
            }
            catch
            {
                btnAgregar.Enabled = false;
                btnNuevoFaltante.Enabled = false;
                EditarFaltanteToolStripMenuItem.Enabled = false;
                cerrarFaltanteToolStripMenuItem.Enabled = false;
                ModificarFaltanteToolStripMenuItem.Enabled = false;
            }

            dtpFechaIncidente.Value = DateTime.Now;
            fechaInicio.Value = DateTime.Now;
            fechaFin.Value = DateTime.Now.AddDays(1);

            CargarComboAsume();
            CargarComboEstado();
            CargarComboMotivo();
        }


        private void CargarComboAsume()
        {
            DataTable dtAsume = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarAsume();
            cbxAsume.DataSource = dtAsume;
            cbxAsume.DisplayMember = "Descripcion";
            cbxAsume.ValueMember = "idAsume";
        }

        private void CargarComboEstado()
        {
            DataTable dtEstado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarEstado();
            cbxEstado.DataSource = dtEstado;
            cbxEstado.DisplayMember = "Descripcion";
            cbxEstado.ValueMember = "idEstado";
        }

        private void CargarComboMotivo()
        {
            DataTable dtMotivo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarMotivo();
            cbxMotivo.DataSource = dtMotivo;
            cbxMotivo.DisplayMember = "Descripcion";
            cbxMotivo.ValueMember = "idMotivo";
        }

        private void ListarFaltantes()
        {
            DataTable dtListaFaltantes = new DataTable();

            if(valorEstado == 0)
            {
                dtListaFaltantes = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarTodos(txtConductor.Text, fechaInicio.Text, fechaFin.Text);
            }
            else
            {
                dtListaFaltantes = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Listar(txtConductor.Text, fechaInicio.Text, fechaFin.Text, valorEstado);
            }
            
            dtgListaFaltantes.DataSource = dtListaFaltantes;

            if (dtListaFaltantes.Rows.Count > 0)
            {
                dgvListaFaltantesView.Columns["FECHA_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaFaltantesView.Columns["FECHA_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaFaltantesView.Columns["FECHA_DESCARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaFaltantesView.Columns["FECHA_DESCARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaFaltantesView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaFaltantesView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaFaltantesView.Columns["UltimaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaFaltantesView.Columns["UltimaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvListaFaltantesView.Columns["idMotivo"].Visible = false;
                dgvListaFaltantesView.Columns["idAsume"].Visible = false;
                dgvListaFaltantesView.Columns["idEstado"].Visible = false;

                dgvListaFaltantesView.BestFitColumns();
                dgvListaFaltantesView.ExpandAllGroups();
            }
        }

        private void ListarPreviaje()
        {
            DataTable dtLista = new DataTable();
            dtLista = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarPreviaje(Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN")));

            if (dtLista.Rows.Count > 0)
            {
                for (int i = 0; i < dtLista.Rows.Count; i++)
                {
                    txtFactura.Text = dtLista.Rows[i]["FACTURA"].ToString();
                    txtMonto.Text = dtLista.Rows[i]["MONTO"].ToString();
                    txtDescripcion.Text = dtLista.Rows[i]["DESCRIPCION"].ToString();
                    txtComentarios.Text = dtLista.Rows[i]["COMENTARIOS"].ToString();
                    cbxAsume.SelectedValue = Convert.ToInt32(dtLista.Rows[i]["idAsume"].ToString());
                    cbxEstado.SelectedValue = Convert.ToInt32(dtLista.Rows[i]["idEstado"].ToString());
                    if (dtLista.Rows[i]["MONEDA"].ToString() == "SOLES")
                    {
                        rbSoles.Checked = true;
                    }
                    else
                    {
                        rbDolares.Checked = true;
                    }
                }
            }

            lblNroTicket.Text = Convert.ToString(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN"));
        }

        private void MostrarFaltanteSinViaje()
        {
            DataTable dtLista = new DataTable();
            dtLista = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_BuscarFaltanteSinViaje(Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN")));
            if (dtLista.Rows.Count > 0)
            {
                for (int i = 0; i < dtLista.Rows.Count; i++)
                {
                    dtpFechaIncidente.Value = Convert.ToDateTime(dtLista.Rows[i]["FECHA_INCIDENTE"]);
                    txtCliente.Text = dtLista.Rows[i]["CLIENTE"].ToString();
                    cbxMotivo.SelectedValue = Convert.ToInt32(dtLista.Rows[i]["idMotivo"].ToString());
                }
            }
        }

        private void ModificarFSV()
        {
            if (txtCliente.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCliente.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                int NroTicket = Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ModificarSinViaje(NroTicket, Convert.ToDateTime(dtpFechaIncidente.Value), txtCliente.Text, Convert.ToInt32(cbxMotivo.SelectedValue), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pFaltanteSinViaje.Visible = false;
                    ListarFaltantes();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMonto.Focus();
                }
            }
        }

        private void ModificarRegistroFaltante()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            int NroTicket = Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Modificar(NroTicket, txtDescripcion.Text, txtFactura.Text, Convert.ToInt32(cbxAsume.SelectedValue), Convert.ToDecimal(txtMonto.Text), Moneda, Convert.ToInt32(cbxEstado.SelectedValue), txtComentarios.Text, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pFacturarFaltante.Visible = false;
                ListarFaltantes();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonto.Focus();
            }
        }

        private void CerrarRegistroFaltante()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            int NroTicket = Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Cerrar(NroTicket, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarFaltantes();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonto.Focus();
            }
        }

        private void ModificarEnter()
        {
            if (txtDescripcion.Text.Length == 0 || txtMonto.Text.Length == 0 || txtComentarios.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtDescripcion.Text.Length == 0)
                {
                    txtDescripcion.Focus();
                }
                else
                {
                    if (txtFactura.Text.Length == 0)
                    {
                        txtFactura.Focus();
                    }
                    else
                    {
                        if (txtMonto.Text.Length == 0)
                        {
                            txtMonto.Focus();
                        }
                        else
                        {
                            txtComentarios.Focus();
                        }
                    }
                }
                return;
            }
            else
            {
                ModificarRegistroFaltante();
            }
        }

        private void IngresarFaltanteSinViaje()
        {
            if (txtCliente.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCliente.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_InsertarFaltanteSinViaje(Convert.ToDateTime(dtpFechaIncidente.Value), txtCliente.Text, Convert.ToInt32(cbxMotivo.SelectedValue), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pFaltanteSinViaje.Visible = false;
                    ListarFaltantes();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpFechaIncidente.Focus();
                }
            }
        }

        private void VerFaltante()
        {
            DataTable dtListaFaltantes = new DataTable();
            dtListaFaltantes = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarPreviaje(Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN")));
            if (dtListaFaltantes.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaFaltantes.Rows.Count; i++)
                {
                    if (dtListaFaltantes.Rows[i]["ESTADO"].ToString() == "CERRADO")
                    {
                        pFacturarFaltante.Visible = true;
                        txtDescripcion.Text = dtListaFaltantes.Rows[i]["DESCRIPCION"].ToString();
                        txtFactura.Text = dtListaFaltantes.Rows[i]["FACTURA"].ToString();
                        txtMonto.Text = dtListaFaltantes.Rows[i]["MONTO"].ToString();
                        cbxAsume.SelectedValue = Convert.ToInt32(dtListaFaltantes.Rows[i]["idAsume"]).ToString();
                        cbxEstado.SelectedValue = Convert.ToInt32(dtListaFaltantes.Rows[i]["idEstado"]).ToString();
                        txtComentarios.Text = dtListaFaltantes.Rows[i]["COMENTARIOS"].ToString();
                        if(dtListaFaltantes.Rows[i]["MONEDA"].ToString() == "SOLES")
                        {
                            rbSoles.Checked = true;
                        }
                        else
                        {
                            rbDolares.Checked = true;
                        }
                    }
                }
            }

            lblNroTicket.Text = Convert.ToString(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN"));
        }


        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarFaltantes();
            }
        }

        private void ModificarFaltanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnModificar.Visible = true;
            pFaltanteSinViaje.Visible = true;
            MostrarFaltanteSinViaje();
        }

        private void EditarFaltanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbSoles.Checked = true;
            rbSoles_Click(sender, e);
            btnCancelar.Enabled = true;
            btnAgregar.Enabled = true;
            groupBox2.Enabled = true;
            txtMonto.Text = Convert.ToString(0.00M);
            int NroProgramacion = Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            pFacturarFaltante.Visible = true;
            ListarPreviaje();
        }

        private void cerrarFaltanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarRegistroFaltante();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pFacturarFaltante.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pFaltanteSinViaje.Visible = false;
        }

        private void rbSoles_Click(object sender, EventArgs e)
        {
            if (rbSoles.Checked == true)
            {
                Moneda = "SOLES";
            }
        }

        private void rbDolares_Click(object sender, EventArgs e)
        {
            if (rbDolares.Checked == true)
            {
                Moneda = "DOLARES";
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ModificarEnter();
        }

        private void btnAgregar2_Click(object sender, EventArgs e)
        {
            IngresarFaltanteSinViaje();
            dtpFechaIncidente.Value = DateTime.Now;
            txtCliente.Clear();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarFSV();
            dtpFechaIncidente.Value = DateTime.Now;
            txtCliente.Clear();
            cbxMotivo.SelectedValue = 1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtFactura.Clear();
            txtMonto.Clear();
            txtDescripcion.Clear();
            txtComentarios.Clear();
        }

        private void btnCancelar2_Click(object sender, EventArgs e)
        {
            dtpFechaIncidente.Value = DateTime.Now;
            txtCliente.Clear();
        }

        private void rbListaTodas_Click(object sender, EventArgs e)
        {
            valorEstado = 0;
        }

        private void rbListaPendientes_Click(object sender, EventArgs e)
        {
            valorEstado = 1;
        }

        private void rbListaSolucionadas_Click(object sender, EventArgs e)
        {
            valorEstado = 2;
        }

        private void btnNuevoFaltante_Click(object sender, EventArgs e)
        {
            dtpFechaIncidente.Value = DateTime.Now;
            txtCliente.Clear();
            cbxMotivo.SelectedValue = 1;
            pFaltanteSinViaje.Visible = true;
            btnModificar.Visible = false;
            dtpFechaIncidente.Focus();
        }

        private void fechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarFaltantes();
            }
        }

        private void fechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarFaltantes();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarFaltantes();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaFaltantes.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Faltante de Mercaderia" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaFaltantes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgListaFaltantes_DoubleClick(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnAgregar.Enabled = false;
            groupBox2.Enabled = false;
            VerFaltante();
        }

        private void dtgListaFaltantes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string estado = dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "ESTADO").ToString();
                int idTicket = Convert.ToInt32(dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "PROGRAMACIÓN"));
                string cliente = dgvListaFaltantesView.GetRowCellValue(dgvListaFaltantesView.FocusedRowHandle, "CLIENTE").ToString();

                if (estado == "CERRADO" || idTicket == 0)
                {
                    EditarFaltanteToolStripMenuItem.Enabled = false;
                    cerrarFaltanteToolStripMenuItem.Enabled = false;
                    ModificarFaltanteToolStripMenuItem.Enabled = false;
                }
                else
                {
                    if(cliente != "")
                    {
                        ModificarFaltanteToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        ModificarFaltanteToolStripMenuItem.Enabled = false;
                    }
                    EditarFaltanteToolStripMenuItem.Enabled = true;
                    cerrarFaltanteToolStripMenuItem.Enabled = true;
                }
            }
            catch
            {
                EditarFaltanteToolStripMenuItem.Enabled = false;
                cerrarFaltanteToolStripMenuItem.Enabled = false;
                ModificarFaltanteToolStripMenuItem.Enabled = false;
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
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
                txtComentarios.Focus();
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtFactura.Focus();
            }
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtMonto.Focus();
            }
        }

        private void txtComentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ModificarEnter();
            }
        }

        private void lstClientes_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCliente, ref lstClientes, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCliente, ref lstClientes, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarClientes))
                {
                    if (esVALE == "SI")
                    {
                        cbxMotivo.Focus();
                    }
                    else
                    {
                        txtCliente.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstClientes_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCliente, ref lstClientes, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstClientes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ClienteActual;
            ClienteActual = lstClientes.SelectedItems[0];
            txtCliente.Text = ClienteActual.SubItems[1].Text;
            lstClientes.Visible = false;
            cbxMotivo.Focus();
        }

        private void txtCliente_Enter(object sender, EventArgs e)
        {
            txtCliente.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtCliente, ref  lstClientes, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarClientes))
                {
                    if (esVALE == "SI")
                    {
                        cbxMotivo.Focus();
                    }
                    else
                    {
                        txtCliente.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbxMotivo.Focus();
            }
        }

        private void txtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtCliente, ref lstClientes, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            txtCliente.BackColor = Color.White;
        }

        private void pFacturarFaltante_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pFacturarFaltante.Left = pFacturarFaltante.Left + (e.X - xClick);
                pFacturarFaltante.Top = pFacturarFaltante.Top + (e.Y - yClick);
            }
        }

        private void pFaltanteSinViaje_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pFaltanteSinViaje.Left = pFaltanteSinViaje.Left + (e.X - xClick);
                pFaltanteSinViaje.Top = pFaltanteSinViaje.Top + (e.Y - yClick);
            }
        }
    }
}
