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
using ReportesTranspesa.Properties;
using Negocio;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmGenerarPlanillaTolvas : Form
    {
        public int idConductor, idTracto, idCarreta, idGastoXRutaC, redondeo;
        public frmListaPlanillasTolvas formulario;
        public string CodGasto;
        DataTable dtRuta = new DataTable();

        public frmGenerarPlanillaTolvas()
        {
            InitializeComponent();
            cbxRuta.SelectedIndexChanged -= cbxRuta_SelectedIndexChanged;
        }

        private void cbxRuta_SelectedIndexChanged(object sender, EventArgs e) { CargarComboRuta(); }

        private void frmGenerarPlanillaTolvas_Load(object sender, EventArgs e)
        {
            dtpFechaViaje.Value = DateTime.Now;
            txtConductor.Focus();
            CargarComboRuta();
            cbxRuta_DropDownClosed(sender, e);
            txtTotalDias.Text = "1";
            cbRedondear.Checked = false;
            cbRedondear_CheckedChanged(sender, e);
        }


        public void CargarComboRuta()
        {
            DataTable dtRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(5, -1, 1, -1);
            cbxRuta.DataSource = dtRutas;
            cbxRuta.DisplayMember = "RUTA";
            cbxRuta.ValueMember = "IdRuta";
        }

        private void BuscarGasto()
        {
            DataTable dtListaGasto = new DataTable();
            dtListaGasto = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGasto(Convert.ToInt32(cbxRuta.SelectedValue), 1);

            if (dtListaGasto.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaGasto.Rows.Count; i++)
                { idGastoXRutaC = Convert.ToInt32(dtListaGasto.Rows[i]["idGastoxRutaC"]); }
            }
        }

        public void ImprimirTicket(string CodGasto)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas2(CodGasto);

                //string NombreImpresora = "EPSON M3170 Series-AB4112";
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
                        ticket.AddSubHeaderLine2("PL - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         ");
                        ticket.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                        ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString());
                        ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                        ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F.Emisión: " + dtListaTicket.Rows[0]["FECHA_EMISION"].ToString());
                        ticket.AddSubHeaderLine("F.Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.AddSubHeaderLine("FIRMA: ");
                        ticket.HeaderImage = Resources.TABLA;
                        ticket.PrintTicket(NombreImpresora);

                        DataTable dtCorrelativo = new DataTable();
                        dtCorrelativo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(6, "");
                        string Correlativo = Convert.ToString(dtCorrelativo.Rows[0]["Codigo"]);

                        Ticket ticket2 = new Ticket();
                        ticket2.AddSubHeaderLine2("TICKET DE DESPACHO");
                        ticket2.AddSubHeaderLine2("DE UNIDAD");
                        ticket2.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                        ticket2.AddSubHeaderLine2("N° " + Correlativo + "                         ");
                        ticket2.AddSubHeaderLine("F. Salida: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                        ticket2.AddSubHeaderLine("Tracto: " + txtTracto.Text + "   Carreta: " + txtCarreta.Text);
                        ticket2.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                        ticket2.AddSubHeaderLine("Destino: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                        ticket2.AddSubHeaderLine("Programación: TOLVAS");
                        ticket2.HeaderImage = Resources.DESPACHO;
                        ticket2.PrintTicket(NombreImpresora);
                    }

                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        private void txtConductor_Enter(object sender, EventArgs e) { txtConductor.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstConductor, clsConsultaBL.Instancia.GetConductores(txtConductor.Text), true, false, false);
            lstConductor.Columns[0].Width = 0;
            lstConductor.Columns[1].Width = 280;
            lstConductor.Columns[2].Width = 100;
            lstConductor.BringToFront();
            lstConductor.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductor = -1;
            }
        }

        private void txtConductor_KeyUp(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Down) { lstConductor.Focus(); } }

        private void txtConductor_Leave(object sender, EventArgs e)
        { txtConductor.BackColor = Color.White; }

        private void lstConductor_Enter(object sender, EventArgs e)
        { if (!lstConductor.Items.Count.Equals(0)) { lstConductor.Items[0].Selected = true; } }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];

                idConductor = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;

                // Agregar restricción para verificar si el conductor está bloqueado
                DataTable ConductorBloqueado = new DataTable();
                ConductorBloqueado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado(idConductor, dtpFechaViaje.Value);
                string respta = Convert.ToString(ConductorBloqueado.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);

                if (NroRspta != "0")
                {
                    MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    idConductor = -1;
                    txtConductor.Clear();
                    lstConductor.Visible = false;
                    lstConductor.SendToBack();
                    txtConductor.Focus();
                    return;
                }

                //Consulta para validar si el conductor contiene un DOCUMETOS VNCIDOS...
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idConductor, "CONDUCTOR");

                if (ValidaUnidadVencida.Rows.Count > 0)
                {
                    MessageBox.Show("No puede seleccionar al conductor " + txtConductor.Text + " porque tiene documentos por vencer y/o vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    idConductor = -1;
                    txtConductor.Clear();
                    lstConductor.Visible = false;
                    lstConductor.SendToBack();
                    txtConductor.Focus();
                    return;
                }

                lstConductor.Visible = false;
                lstConductor.SendToBack();
                txtTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductor = -1;
            }
        }

        private void lstConductor_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstConductor.SelectedItems[0];

            idConductor = Int32.Parse(ItemActual.Text);
            txtConductor.Text = ItemActual.SubItems[1].Text;

            // Agregar restricción para verificar si el conductor está bloqueado
            DataTable ConductorBloqueado = new DataTable();
            ConductorBloqueado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado(idConductor, dtpFechaViaje.Value);
            string respta = Convert.ToString(ConductorBloqueado.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);

            if (NroRspta != "0")
            {
                MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idConductor = -1;
                txtConductor.Clear();
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                txtConductor.Focus();
                return;
            }

            //Consulta para validar si el conductor contiene un DOCUMETOS VNCIDOS...
            DataTable ValidaUnidadVencida = new DataTable();
            ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idConductor, "CONDUCTOR");

            if (ValidaUnidadVencida.Rows.Count > 0)
            {
                MessageBox.Show("No puede seleccionar al conductor " + txtConductor.Text + " porque tiene documentos por vencer y/o vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                idConductor = -1;
                txtConductor.Clear();
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                txtConductor.Focus();
                return;
            }

            lstConductor.Visible = false;
            lstConductor.SendToBack();
            txtTracto.Focus();
        }

        private void txtTracto_Enter(object sender, EventArgs e) { txtTracto.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsConsultaBL.Instancia.GetUnidades(txtTracto.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 70;
            lstPlaca.Columns[2].Width = 60;
            lstPlaca.Columns[3].Width = 0;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                idTracto = -1;
            }
        }

        private void txtTracto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void txtTracto_Leave(object sender, EventArgs e) { txtTracto.BackColor = Color.White; }

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

                idTracto = Int32.Parse(ItemActual.Text);
                txtTracto.Text = ItemActual.SubItems[1].Text;

                DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(idTracto);
                string Rpta;

                if (UnidadBloq.Rows.Count > 0)
                {
                    Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                    MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    idTracto = -1;
                    txtTracto.Clear();
                    lstPlaca.Visible = false;
                    lstPlaca.SendToBack();
                    txtTracto.Focus();
                    return;
                }

                //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idTracto, "UNIDAD");

                if (ValidaUnidadVencida.Rows.Count > 0)
                {
                    MessageBox.Show("No puede seleccionar la unidad " + txtTracto.Text + " porque tiene documentos por vencer y/o vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    idTracto = -1;
                    txtTracto.Clear();
                    lstPlaca.Visible = false;
                    lstPlaca.SendToBack();
                    txtTracto.Focus();
                    return;
                }

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtCarreta.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                idTracto = -1;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idTracto = Int32.Parse(ItemActual.Text);
            txtTracto.Text = ItemActual.SubItems[1].Text;

            DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(idTracto);
            string Rpta;

            if (UnidadBloq.Rows.Count > 0)
            {
                Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                idTracto = -1;
                txtTracto.Clear();
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtTracto.Focus();
                return;
            }

            //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
            DataTable ValidaUnidadVencida = new DataTable();
            ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idTracto, "UNIDAD");

            if (ValidaUnidadVencida.Rows.Count > 0)
            {
                MessageBox.Show("No puede seleccionar la unidad " + txtTracto.Text + " porque tiene documentos por vencer y/o vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                idTracto = -1;
                txtTracto.Clear();
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtTracto.Focus();
                return;
            }

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtCarreta.Focus();
        }

        private void txtCarreta_Enter(object sender, EventArgs e) { txtCarreta.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstCarreta, clsConsultaBL.Instancia.GetUnidades(txtCarreta.Text), true, false, false);
            lstCarreta.Columns[0].Width = 0;
            lstCarreta.Columns[1].Width = 70;
            lstCarreta.Columns[2].Width = 60;
            lstCarreta.Columns[3].Width = 0;
            lstCarreta.BringToFront();
            lstCarreta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                idCarreta = -1;
            }
        }

        private void txtCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstCarreta.Focus(); }
        }

        private void txtCarreta_Leave(object sender, EventArgs e) { txtCarreta.BackColor = Color.White; }

        private void lstCarreta_Enter(object sender, EventArgs e)
        {
            if (!lstCarreta.Items.Count.Equals(0)) { lstCarreta.Items[0].Selected = true; }
        }

        private void lstCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstCarreta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstCarreta.SelectedItems[0];

                idCarreta = Int32.Parse(ItemActual.Text);
                txtCarreta.Text = ItemActual.SubItems[1].Text;

                DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(idCarreta);
                string Rpta;

                if (UnidadBloq.Rows.Count > 0)
                {
                    Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                    MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    idCarreta = -1;
                    txtCarreta.Clear();
                    lstCarreta.Visible = false;
                    lstCarreta.SendToBack();
                    txtCarreta.Focus();
                    return;
                }

                //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idCarreta, "UNIDAD");

                if (ValidaUnidadVencida.Rows.Count > 0)
                {
                    MessageBox.Show("No puede seleccionar la unidad " + txtCarreta.Text + " porque tiene documentos por vencer y/o vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    idCarreta = -1;
                    txtCarreta.Clear();
                    lstCarreta.Visible = false;
                    lstCarreta.SendToBack();
                    txtCarreta.Focus();
                    return;
                }

                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                dtpFechaViaje.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                idCarreta = -1;
            }
        }

        private void lstCarreta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstCarreta.SelectedItems[0];

            idCarreta = Int32.Parse(ItemActual.Text);
            txtCarreta.Text = ItemActual.SubItems[1].Text;

            DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(idCarreta);
            string Rpta;

            if (UnidadBloq.Rows.Count > 0)
            {
                Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                idCarreta = -1;
                txtCarreta.Clear();
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                txtCarreta.Focus();
                return;
            }

            //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
            DataTable ValidaUnidadVencida = new DataTable();
            ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idCarreta, "UNIDAD");

            if (ValidaUnidadVencida.Rows.Count > 0)
            {
                MessageBox.Show("No puede seleccionar la unidad " + txtCarreta.Text + " porque tiene documentos por vencer y/o vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                idCarreta = -1;
                txtCarreta.Clear();
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                txtCarreta.Focus();
                return;
            }

            lstCarreta.Visible = false;
            lstCarreta.SendToBack();
            dtpFechaViaje.Focus();
        }

        private void cbxRuta_DropDownClosed(object sender, EventArgs e)
        {
            dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(cbxRuta.SelectedValue), 1, redondeo);
            if (dtRuta.Rows.Count > 0)
            {
                txtMontoEfectivo.Text = dtRuta.Rows[0]["GastoTotal"].ToString();
                BuscarGasto();
            }
        }

        private void txtTotalDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                decimal Multi;
                if (cbLindley.Checked == true) { Multi = Convert.ToDecimal(txtTotalDias.Text) * Convert.ToDecimal("50.00"); }
                else { Multi = Convert.ToDecimal(txtTotalDias.Text) * Convert.ToDecimal(dtRuta.Rows[0]["GastoTotal"]); }
                txtMontoEfectivo.Text = Convert.ToString(Multi);
            }
        }

        private void txtTotalDias_KeyUp(object sender, KeyEventArgs e)
        {
            decimal Multi;
            if (cbLindley.Checked == true) { Multi = Convert.ToDecimal(txtTotalDias.Text == "" ? 1 : Convert.ToDecimal(txtTotalDias.Text)) * Convert.ToDecimal("50.00"); }
            else { Multi = Convert.ToDecimal(txtTotalDias.Text == "" ? 1 : Convert.ToDecimal(txtTotalDias.Text)) * Convert.ToDecimal(dtRuta.Rows[0]["GastoTotal"]); }
            txtMontoEfectivo.Text = Convert.ToString(Multi);
        }

        private void cbLindley_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLindley.Checked == true)
            {
                decimal Multi;
                Multi = Convert.ToDecimal(txtTotalDias.Text == "" ? 1 : Convert.ToDecimal(txtTotalDias.Text)) * Convert.ToDecimal("50.00");
                txtMontoEfectivo.Text = Convert.ToString(Multi);
            }

            if (cbLindley.Checked == false)
            {
                decimal Multi;
                Multi = Convert.ToDecimal(txtTotalDias.Text == "" ? 1 : Convert.ToDecimal(txtTotalDias.Text)) * Convert.ToDecimal(dtRuta.Rows[0]["GastoTotal"]);
                txtMontoEfectivo.Text = Convert.ToString(Multi);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            idConductor = -1;
            txtConductor.Clear();
            idTracto = -1;
            txtTracto.Clear();
            idCarreta = -1;
            txtCarreta.Clear();
            dtpFechaViaje.Value = DateTime.Now;
            cbRedondear.Checked = false;
            cbRedondear_CheckedChanged(sender, e);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtMontoEfectivo.Text) == 0)
            {
                MessageBox.Show("Por favor, ingrese la cantidad de días del viaje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTotalDias.Focus();
                return;
            }

            if (txtConductor.Text.Length == 0 || txtTracto.Text.Length == 0 || txtCarreta.Text.Length == 0 || txtTotalDias.Text.Length == 0)
            {
                if (txtConductor.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConductor.Focus();
                }
                else
                {
                    if (txtTracto.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese un tracto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTracto.Focus();
                    }
                    else
                    {
                        if (txtCarreta.Text.Length == 0)
                        {
                            MessageBox.Show("Por favor, ingrese una carreta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtTracto.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Por favor, ingrese el total de días.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtTotalDias.Focus();
                        }
                    }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaTolvas(idConductor, idTracto, idCarreta,
                    Convert.ToInt32(cbxRuta.SelectedValue), idGastoXRutaC, Convert.ToDecimal(txtMontoEfectivo.Text), dtpFechaViaje.Value,
                    Convert.ToInt32(txtTotalDias.Text), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodGasto = Respuesta.Substring(30, 8);
                    ImprimirTicket(CodGasto);
                    formulario.ListarPlanillasTolvas();
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void cbRedondear_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRedondear.Checked == true)
            {
                redondeo = 0;
                cbxRuta_DropDownClosed(sender, e);
            }

            if (cbRedondear.Checked == false)
            {
                redondeo = 1;
                cbxRuta_DropDownClosed(sender, e);
            }
        }
    }
}
