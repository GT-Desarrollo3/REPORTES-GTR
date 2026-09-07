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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmViaticosAdicionales : Form
    {
        public string Planilla;
        public int idConductor, NroTicket = 0, form = 0, Opcion;
        public int frmLP = 0;
        public string Usuario, Descripcion;
        public int idOperacion, idRuta, idGastoXRutaC;
        public frmListarPlanillas frmListarPlanillas;
        public frmOperacion_Previajes frmOperacion_Previajes;
        DataTable dtViatico = new DataTable();

        public frmViaticosAdicionales()
        {
            InitializeComponent();
            cbxTipoViatico.SelectedIndexChanged -= cbxTipoViatico_SelectedIndexChanged;
            cbxTipoGasto.SelectedIndexChanged -= cbxTipoGasto_SelectedIndexChanged;
            cbxRuta.SelectedIndexChanged -= cbxRuta_SelectedIndexChanged;
        }

        private void cbxTipoViatico_SelectedIndexChanged(object sender, EventArgs e) { CargarComboViatico(); }

        private void cbxTipoGasto_SelectedIndexChanged(object sender, EventArgs e) { CargarComboGasto(); }

        private void cbxRuta_SelectedIndexChanged(object sender, EventArgs e) { CargarComboRuta(); }

        private void frmViaticosAdicionales_Load(object sender, EventArgs e)
        {
            dtpFechaViatico.Value = DateTime.Now;
            CargarComboRuta();
            cbRuta.Checked = false;
            cbRedondeo.Checked = false;
            cbRuta_CheckedChanged(sender, e);
            cbxRuta.SelectedValue = idRuta;
        }


        public void CargarComboViatico()
        {
            DataTable dtViatico = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(4, "");
            cbxTipoViatico.DataSource = dtViatico;
            cbxTipoViatico.DisplayMember = "Descripcion";
            cbxTipoViatico.ValueMember = "idTipoViatico";
        }

        public void CargarComboGasto()
        {
            DataTable dtTipoGasto = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(1, idRuta, idOperacion, -1);
            cbxTipoGasto.DataSource = dtTipoGasto;
            cbxTipoGasto.DisplayMember = "Descripcion";
            cbxTipoGasto.ValueMember = "idGastoxRutaD";
        }

        public void CargarComboRuta()
        {
            DataTable dtRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(5, -1, idOperacion, -1);
            cbxRuta.DataSource = dtRutas;
            cbxRuta.DisplayMember = "RUTA";
            cbxRuta.ValueMember = "IdRuta";
        }

        public void ListarViatico(int Opcion)
        {
            dtViatico = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarViaticos(Opcion, idConductor);
            dtgvViaticos.DataSource = dtViatico;

            if (dtViatico.Rows.Count > 0)
            {
                dtgvViaticosView.Columns["idViatico"].Visible = false;
                dtgvViaticosView.Columns["idConductor"].Visible = false;
                dtgvViaticosView.Columns["EstadoLiquidacion"].Visible = false;
                //dtgvViaticosView.Columns["NroTicket"].Visible = false;

                dtgvViaticosView.Columns["IMPORTE"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViaticosView.Columns["IMPORTE"].DisplayFormat.FormatString = "n2";
                dtgvViaticosView.Columns["FECHA_VIATICO"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvViaticosView.Columns["FECHA_VIATICO"].DisplayFormat.FormatString = "dd/MM/yyyy";

                dtgvViaticosView.BestFitColumns();
            }
        }


        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            //if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }  
        }

        private void adjuntarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Planilla2 = dtgvViaticosView.GetRowCellValue(dtgvViaticosView.FocusedRowHandle, "PLANILLA").ToString();

            DataTable dtListaRegistros = new DataTable();
            dtListaRegistros = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro(NroTicket);

            if (dtListaRegistros.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaRegistros.Rows.Count; i++)
                {
                    idRuta = Convert.ToInt32(dtListaRegistros.Rows[i]["IdRuta"]);
                    idOperacion = Convert.ToInt32(dtListaRegistros.Rows[i]["IdOperacion"]);
                }
            }

            DataTable dtListaGasto = new DataTable();
            dtListaGasto = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGasto(idRuta, idOperacion);

            if (dtListaGasto.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaGasto.Rows.Count; i++)
                { idGastoXRutaC = Convert.ToInt32(dtListaGasto.Rows[i]["idGastoxRutaC"]); }
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_AdjuntarPlanilla(Planilla2, idConductor, NroTicket, idOperacion, idGastoXRutaC, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmGenerarTicketGasto frmGenerarTicketGasto = new frmGenerarTicketGasto();
                frmGenerarTicketGasto.Imprimir(Planilla2);
                this.Close();
                frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                f1.CargarDatos();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este viático?", "ELIMINAR VIÁTICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idViatico = Convert.ToInt32(dtgvViaticosView.GetRowCellValue(dtgvViaticosView.FocusedRowHandle, "idViatico"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarViaticos(NroTicket, idViatico, idConductor, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarViatico(Opcion);
                        dtpFechaViatico.Focus();

                        if (form == 1) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                        else { frmOperacion_Previajes.CargarDatos(); }
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch
            { MessageBox.Show("El viático seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtViaticoConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstConductores, clsConsultaBL.Instancia.GetConductores(txtViaticoConductor.Text), true, false, false);
            lstConductores.Columns[0].Width = 0;
            lstConductores.Columns[1].Width = 300;
            lstConductores.Columns[2].Width = 0;
            lstConductores.BringToFront();
            lstConductores.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductores.Visible = false;
                lstConductores.SendToBack();
                txtViaticoConductor.Focus();
                idConductor = 0;
            }
        }

        private void txtViaticoConductor_KeyUp(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Down) { lstConductores.Focus(); } }

        private void lstConductores_Enter(object sender, EventArgs e)
        { if (!lstConductores.Items.Count.Equals(0)) { lstConductores.Items[0].Selected = true; } }

        private void lstConductores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstConductores.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductores.SelectedItems[0];

                idConductor = Int32.Parse(ItemActual.Text);
                txtViaticoConductor.Text = ItemActual.SubItems[1].Text;
                ListarViatico(1);

                lstConductores.Visible = false;
                lstConductores.SendToBack();
                dtpFechaViatico.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductores.Visible = false;
                lstConductores.SendToBack();
                txtViaticoConductor.Focus();
                idConductor = 0;
            }
        }

        private void lstConductores_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstConductores.SelectedItems[0];

            idConductor = Int32.Parse(ItemActual.Text);
            txtViaticoConductor.Text = ItemActual.SubItems[1].Text;
            ListarViatico(1);

            lstConductores.Visible = false;
            lstConductores.SendToBack();
            dtpFechaViatico.Focus();
        }

        public void cbxTipoViatico_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbxTipoViatico.SelectedValue) == 1) { txtMonto.Text = "40.00"; }
            if (Convert.ToInt32(cbxTipoViatico.SelectedValue) == 2) { txtMonto.Text = "12.00"; }
            if (Convert.ToInt32(cbxTipoViatico.SelectedValue) == 3 || Convert.ToInt32(cbxTipoViatico.SelectedValue) == 4) { txtMonto.Text = "14.00"; }
            if (Convert.ToInt32(cbxTipoViatico.SelectedValue) == 6) { txtMonto.Text = "28.00"; }
            if (Convert.ToInt32(cbxTipoViatico.SelectedValue) == 5 || Convert.ToInt32(cbxTipoViatico.SelectedValue) == 7) { txtMonto.Text = "26.00"; }
            txtMonto.Focus();
            txtMotivo.Text = cbxTipoViatico.Text;
        }

        public void cbxTipoGasto_DropDownClosed(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(2, -1, -1, Convert.ToInt32(cbxTipoGasto.SelectedValue));
            if (dtRespuesta.Rows.Count > 0) { txtMonto2.Text = dtRespuesta.Rows[0]["Gasto"].ToString(); }
            txtMotivo.Text = cbxTipoGasto.Text;
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            if (txtViaticoConductor.Text.Length == 0 || txtMonto.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtViaticoConductor.Text.Length == 0) { txtViaticoConductor.Focus(); }
                else { txtMonto.Focus(); }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ViaticoSinProg(idConductor, Convert.ToDateTime(dtpFechaViatico.Value), Convert.ToInt32(cbxTipoViatico.SelectedValue),
                                                                                                Convert.ToDecimal(txtMonto.Text), Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarViatico(1);
                    //btnImprimir2_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtViaticoConductor.Focus();
                }
            }
        }

        private void btnImprimir2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarViaticoProg(idConductor);

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
                        ticket.AddSubHeaderLine2("PL - " + dtListaTicket.Rows[0]["PLANILLA"].ToString() + "                         ");
                        ticket.AddSubHeaderLine("Chofer: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                        ticket.AddSubHeaderLine("Viático: " + dtListaTicket.Rows[0]["TIPO_VIATICO"].ToString());
                        ticket.AddSubHeaderLine("F.Viático: " + dtListaTicket.Rows[0]["FECHA_VIATICO"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("He recibido un viático adicional de S/. " + dtListaTicket.Rows[0]["IMPORTE"].ToString() + ".");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.AddSubHeaderLine("FIRMA: ");
                        ticket.HeaderImage = Resources.TABLA;
                        ticket.PrintTicket(NombreImpresora);
                    }
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            string Respuesta;
            string Descripcion;

            if (frmLP == 0)
            {
                if (txtMonto2.Text.Length == 0 || txtMotivo.Text.Length == 0 || txtMotivo.Text == " ")
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    if (txtMonto2.Text.Length == 0) { txtMonto2.Focus(); }
                    else { txtMotivo.Focus(); }
                    
                    return;
                }
                else
                {
                    if (cbRuta.Checked == true) { Descripcion = cbxRuta.Text; }
                    else { Descripcion = cbxTipoGasto.Text; }

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarViaticos(NroTicket, Planilla, idConductor, Convert.ToDateTime(dtpFechaViatico.Value),
                                                             Convert.ToInt32(cbxTipoGasto.SelectedValue), Convert.ToDecimal(txtMonto2.Text), Descripcion, txtMotivo.Text.ToUpper(), Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarViatico(2);
                        dtpFechaViatico.Value = DateTime.Now;
                        txtMotivo.Clear();

                        if (form == 1) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                        else { frmOperacion_Previajes.CargarDatos(); }
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dtpFechaViatico.Focus();
                    }
                }
            }
            else
            {
                if (txtMonto.Text.Length == 0 || txtMotivo.Text.Length == 0 || txtMotivo.Text == " ")
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (txtMonto.Text.Length == 0) { txtMonto.Focus(); }
                    else { txtMotivo.Focus(); }

                    return;
                }
                else
                {
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarViaticos(NroTicket, Planilla, idConductor, Convert.ToDateTime(dtpFechaViatico.Value),
                                                             Convert.ToInt32(cbxTipoViatico.SelectedValue), Convert.ToDecimal(txtMonto.Text), cbxTipoViatico.Text, txtMotivo.Text.ToUpper(), Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarViatico(1);
                        dtpFechaViatico.Value = DateTime.Now;
                        txtMotivo.Clear();

                        if (form == 1) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                        else { frmOperacion_Previajes.CargarDatos(); }
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dtpFechaViatico.Focus();
                    }
                }
            }
        }

        private void btnImprimirViatico_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ImprimirDiferencial(NroTicket, Planilla);
                string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (form == 1) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                else { frmOperacion_Previajes.CargarDatos(); }

                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(Planilla);

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
                        ticket.AddSubHeaderLine("Chofer: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                        ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                        ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                        ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                        ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("Al conductor se le añadió un gasto adicional de S/. " + dtListaTicket.Rows[0]["VIATICOS"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.AddSubHeaderLine("FIRMA: ");
                        ticket.HeaderImage = Resources.TABLA;
                        ticket.PrintTicket(NombreImpresora);
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cbRuta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRuta.Checked == true)
            {
                cbxRuta.Enabled = true;
                cbRedondeo.Enabled = true;
                cbxRuta_DropDownClosed(sender, e);
                cbxTipoGasto.Enabled = false;
                txtMotivo.Text = "CAMBIO DE RUTA - " + cbxRuta.Text;
            }

            if (cbRuta.Checked == false)
            {
               if (frmLP == 1)
               {
                   cbxRuta.Enabled = false;
                   cbRedondeo.Enabled = false;
                   cbxTipoViatico_DropDownClosed(sender, e);
                   cbxTipoViatico.Enabled = true;
               }
               else
               {
                   cbxRuta.Enabled = false;
                   cbRedondeo.Checked = false;
                   cbRedondeo.Enabled = false;
                   cbxTipoGasto_DropDownClosed(sender, e);
                   cbxTipoGasto.Enabled = true;
               }
            }
        }

        private void cbxRuta_DropDownClosed(object sender, EventArgs e)
        {
            if (cbRedondeo.Checked == false)
            {
                DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(4, Convert.ToInt32(cbxRuta.SelectedValue), idOperacion, -1);
                if (dtRuta.Rows.Count > 0)
                {
                    txtMonto2.Text = dtRuta.Rows[0]["GASTO"].ToString();
                    txtMotivo.Text = cbxTipoGasto.Text;
                }
            }
            
            if (cbRedondeo.Checked == true)
            {
                DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(cbxRuta.SelectedValue), idOperacion, 2);
                if (dtRuta.Rows.Count > 0)
                {
                    txtMonto2.Text = dtRuta.Rows[0]["GastoTotal"].ToString();
                    txtMotivo.Text = "CAMBIO DE RUTA - " + cbxRuta.Text;
                }
            }
        }

        private void cbRedondeo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRedondeo.Checked == false)
            {
                DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(4, Convert.ToInt32(cbxRuta.SelectedValue), idOperacion, -1);
                if (dtRuta.Rows.Count > 0) { txtMonto2.Text = dtRuta.Rows[0]["GASTO"].ToString(); }
            }

            if (cbRedondeo.Checked == true)
            {
                DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(cbxRuta.SelectedValue), idOperacion, 2);
                if (dtRuta.Rows.Count > 0) { txtMonto2.Text = dtRuta.Rows[0]["GastoTotal"].ToString(); }
            }
        }
    }
}
