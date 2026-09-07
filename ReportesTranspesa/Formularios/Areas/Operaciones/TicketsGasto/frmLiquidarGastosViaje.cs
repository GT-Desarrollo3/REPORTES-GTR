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
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmLiquidarGastosViaje : Form
    {
        public string esVALE;
        char TipoImpuesto;
        DataTable dtRecibo;
        public int _NroTicket, _idConductor, _Permiso, _idRuta, _idOperacion, _idGastoXRutaC;
        public string _Planilla, _Conductor, _Ruta, _Placa, _GastoTotal;
        public string Fecha;
        string Correlativo;
        frmListarPlanillas _frmListarPlanillas;

        public void frmListarPlanillas(frmListarPlanillas frmListarPlanillas)
        { _frmListarPlanillas = frmListarPlanillas; }

        public frmLiquidarGastosViaje()
        {
            InitializeComponent();
            cbxConcepto.SelectedIndexChanged -= cbxConcepto_SelectedIndexChanged;
            cbxRecibo.SelectedIndexChanged -= cbxRecibo_SelectedIndexChanged;
            cbxPeaje.SelectedIndexChanged -= cbxPeaje_SelectedIndexChanged;
        }

        private void cbxConcepto_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboConcepto(); }

        private void cbxRecibo_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboRecibo(); }

        private void cbxPeaje_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboPeajes(); }

        private void frmLiquidarGastosViaje_Shown(object sender, EventArgs e)
        { cbxConcepto.Focus(); }

        private void frmLiquidarGastosViaje_Load(object sender, EventArgs e)
        {
            dtgvListaComprobantesView.OptionsBehavior.Editable = true;
            dtgvListaComprobantesView.OptionsNavigation.EnterMoveNextColumn = true;

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in dtgvListaComprobantesView.Columns)
                col.OptionsColumn.AllowEdit = false;

            lblConductor.Text = _Conductor;

            if (_NroTicket < 0) { lblProgramacion.Text = "TOLVAS"; }
            else { lblProgramacion.Text = Convert.ToString(_NroTicket); }
            lblRuta.Text = _Ruta;
            lblPlanilla.Text = _Planilla;

            CargarComboConcepto();
            CargarComboPeajes();
            dtpFecha.Value = DateTime.Now;
            dtpFechaLiquidacion.Value = DateTime.Now;
            dtpFechaViatico.Value = DateTime.Now;
            txtGasto.Text = _GastoTotal;
            ListarReintegro();
            ListarLiquidaciones();
            rbNinguno.Checked = true;
            rbNinguno_Click(sender, e);
            txtDescripcionRG.Text = "REND. GV - " + _Placa + " DEL " + Fecha.Substring(0, 10) + " - " + _Ruta;
            Limpiar(sender, e);

            dtgvListaComprobantesView.Columns["MONTO_AFECTO"].OptionsColumn.AllowEdit = true;
            dtgvListaComprobantesView.Columns["MONTO_NO_AFECTO"].OptionsColumn.AllowEdit = true;
            dtgvListaComprobantesView.Columns["MONTO_IMPUESTOS"].OptionsColumn.AllowEdit = true;
        }


        private void CargarComboConcepto()
        {
            DataTable dtConcepto = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(1,"");
            cbxConcepto.DataSource = dtConcepto;
            cbxConcepto.DisplayMember = "DescripcionLocal";
            cbxConcepto.ValueMember = "ConceptoGasto";
        }

        private void CargarComboRecibo()
        {
            DataTable dtRecibo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(2,"");
            cbxRecibo.DataSource = dtRecibo;
            cbxRecibo.DisplayMember = "Descripcion";
            cbxRecibo.ValueMember = "CodigoDocumento";
        }

        public void CargarComboPeajes()
        {
            DataTable dtPeaje = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_FiltrarPeaje(Convert.ToInt32(_Planilla), _idRuta);
            cbxPeaje.DataSource = dtPeaje;
            cbxPeaje.DisplayMember = "PEAJE";
            cbxPeaje.ValueMember = "idPeaje";
        }

        public void EnviarPlanilla(int NroTicket, int idConductor, string Planilla, string Conductor, string Ruta, string Placa, string GastoTotal, int Permiso,
                                   int idRuta, int idOperacion, int idGastoXRutaC)
        {
            _NroTicket = NroTicket;
            _idConductor = idConductor;
            _Planilla = Planilla;
            _Conductor = Conductor;
            _Ruta = Ruta;
            _Placa = Placa;
            _GastoTotal = GastoTotal;
            _Permiso = Permiso;
            _idRuta = idRuta;
            _idOperacion = idOperacion;
            _idGastoXRutaC = idGastoXRutaC;
        }

        private void ListarReintegro()
        {
            DataTable dtReintegro = new DataTable();
            dtReintegro = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(3, _idRuta, _idOperacion, -1);
            if (dtReintegro.Rows.Count > 0)
            { txtRAutorizado.Text = Convert.ToString(dtReintegro.Rows[0]["REINTEGRO"]); }
            else
            { txtRAutorizado.Text = "0.00"; }
        }

        private void InsertarRecibos(object sender, EventArgs e)
        {
            if (txtMontoAfecto.Text == "0.00" && txtMontoNoAfecto.Text == "0.00" && txtMontoImpuestos.Text == "0.00")
            {
                MessageBox.Show("No puede ingresar un gasto sin monto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMontoAfecto.Focus();
                return;
            }

            if (txtDescripcion.Text.Length == 0 || txtMontoAfecto.Text.Length == 0 || txtMontoNoAfecto.Text.Length == 0 || txtMontoImpuestos.Text.Length == 0 || txtMontoPagado.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtDescripcion.Text.Length == 0) { txtDescripcion.Focus(); }
                else
                {
                    if (txtMontoAfecto.Text.Length == 0) { txtMontoAfecto.Focus(); }
                    else { txtMontoNoAfecto.Focus(); }
                }
                return;
            }
            else
            {
                if (Convert.ToString(cbxConcepto.SelectedValue) == "0002" || (Convert.ToString(cbxConcepto.SelectedValue) == "0107") || (Convert.ToString(cbxConcepto.SelectedValue) == "0077"))
                {
                    DataTable dtRespuesta2 = new DataTable();
                    string Respuesta2;
                    dtRespuesta2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarViaticos(_idConductor, Convert.ToDateTime(dtpFechaViatico.Value), txtNroRecibo.Text, Convert.ToDecimal(txtMontoPagado.Text), txtRUC.Text, _Planilla);
                    Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                    string NroRPTA2 = Respuesta2.Substring(0, 1);
                    if (NroRPTA2 == "0") { }
                    else
                    {
                        MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cbxConcepto.Focus();
                        return;
                    }
                }
                
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarLiquidaciones(_Planilla, Convert.ToDateTime(dtpFecha.Value), TipoImpuesto, Convert.ToString(cbxConcepto.SelectedValue), txtDescripcion.Text, txtRUC.Text, txtNombre.Text,
                                                                                                                   Convert.ToString(cbxRecibo.SelectedValue), txtNroRecibo.Text, Convert.ToDecimal(txtMontoAfecto.Text), Convert.ToDecimal(txtMontoNoAfecto.Text),
                                                                                                                   Convert.ToDecimal(txtMontoImpuestos.Text), Convert.ToDecimal(txtMontoPagado.Text), txtMotivoGasto.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    ListarLiquidaciones();
                    Limpiar(sender, e);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxConcepto.Focus();
                }
            }
        }

        private void ListarLiquidaciones()
        {
            int focusedRowHandle = dtgvListaComprobantesView.FocusedRowHandle;
            
            dtRecibo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(3, _Planilla);
            dtgvListaComprobantes.DataSource = dtRecibo;
            if (dtRecibo.Rows.Count > 0)
            {
                dtgvListaComprobantesView.Columns["PLANILLA"].Visible = false;
                dtgvListaComprobantesView.Columns["TipoImpuesto"].Visible = false;
                dtgvListaComprobantesView.Columns["ConceptoGasto"].Visible = false;
                dtgvListaComprobantesView.Columns["CodigoDocumento"].Visible = false;
                dtgvListaComprobantesView.Columns["Contador"].Visible = false;

                dtgvListaComprobantesView.Columns["MONTO_AFECTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvListaComprobantesView.Columns["MONTO_AFECTO"].DisplayFormat.FormatString = "n2";
                dtgvListaComprobantesView.Columns["MONTO_NO_AFECTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvListaComprobantesView.Columns["MONTO_NO_AFECTO"].DisplayFormat.FormatString = "n2";
                dtgvListaComprobantesView.Columns["MONTO_IMPUESTOS"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvListaComprobantesView.Columns["MONTO_IMPUESTOS"].DisplayFormat.FormatString = "n2";
                dtgvListaComprobantesView.Columns["MONTO_PAGADO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvListaComprobantesView.Columns["MONTO_PAGADO"].DisplayFormat.FormatString = "n2";
                dtgvListaComprobantesView.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaComprobantesView.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dtgvListaComprobantesView.Columns["MONTO_PAGADO"].Summary.Clear();
                dtgvListaComprobantesView.Columns["MONTO_PAGADO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_PAGADO", "{0:N2}");

                txtTotal.Text = Convert.ToString(dtgvListaComprobantesView.Columns["MONTO_PAGADO"].SummaryText);
                txtReintegro.Text = Convert.ToString(-(Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtGasto.Text)));

                dtgvListaComprobantesView.BestFitColumns();
            }
            else
            {
                txtTotal.Text = Convert.ToString(0.00M);
                txtReintegro.Text = Convert.ToString(-(Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtGasto.Text)));
            }

            if((Convert.ToDecimal(txtReintegro.Text) >= 0))
            {
                txtReintegro.BackColor = Color.Lime;
                txtReintegro.ForeColor = Color.Black;
                btnImprimir.Enabled = false;
            }
            else
            {
                txtReintegro.BackColor = Color.Red;
                txtReintegro.ForeColor = Color.White;
                btnImprimir.Enabled = true;
            }
        }

        private void EliminarRecibo()
        {
            int idNroLiquidacion = Convert.ToInt32(dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "Nro"));
            string CodGasto = Convert.ToString(dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "PLANILLA"));
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarLiquidaciones(idNroLiquidacion, CodGasto);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                ListarLiquidaciones();
                cbxConcepto.Focus();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Limpiar(object sender, EventArgs e)
        {
            cbxConcepto.SelectedValue = "0078";
            dtpFecha.Value = DateTime.Now;
            txtDescripcion.Clear();
            cbxConcepto_DropDownClosed(sender, e);
            txtRUC.Clear();
            txtNombre.Clear();
            cbxRecibo.SelectedValue = "          ";
            txtNroRecibo.Enabled = false;
            txtNroRecibo.Clear();
            rbNinguno.Checked = true;
            rbNinguno_Click(sender, e);
            //txtMontoAfecto.ReadOnly = true;
            //txtMontoNoAfecto.ReadOnly = false;
            txtMontoAfecto.Text = Convert.ToString(0.00M);
            txtMontoNoAfecto.Text = Convert.ToString(0.00M);
            txtMontoImpuestos.Text = Convert.ToString(0.00M);
            txtMontoPagado.Clear();
            txtMotivoGasto.Clear();
            label21.Visible = false;
            dtpFechaViatico.Visible = false;
            dtpFechaViatico.Value = DateTime.Now;
            cbxPeaje.Enabled = false;
        }

        private void LiquidarRecibos()
        {
            dtRecibo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(3, _Planilla);
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            if (dtRecibo.Rows.Count == 0 || txtDescripcionRG.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescripcionRG.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_LiquidarTicketGasto(_idConductor,_NroTicket,_Placa,
                                                        Convert.ToDateTime(dtpFechaLiquidacion.Value), Convert.ToInt32(_Planilla), Convert.ToDecimal(txtGasto.Text),
                                                        Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtReintegro.Text), txtDescripcionRG.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescripcionRG.Clear();
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void ImprimirRecibo()
        {
            DataTable dtConsultarImpresora = new DataTable();
            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

            //string NombreImpresora = "EPSONA3F19D (WF-C5710 Series)";
            string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

            if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
            {
                MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString().TrimEnd() == "0078")
                {
                    Ticket ticket = new Ticket();
                    ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                    ticket.AddSubHeaderLine2("001-" + dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "Contador").ToString());
                    ticket.AddSubHeaderLine("ENTREGA DE EFECTIVO DEL");
                    ticket.AddSubHeaderLine("CONDUCTOR");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("Fecha: " + DateTime.Now.ToShortDateString());
                    ticket.AddSubHeaderLine("Chofer: " + _Conductor);
                    ticket.AddSubHeaderLine("Placa: " + _Placa);
                    ticket.AddSubHeaderLine("Planilla: PL-" + _Planilla);
                    ticket.AddSubHeaderLine("Vuelto: S/. " + dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "MONTO_PAGADO").ToString());
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("_____________    _____________");
                    ticket.AddSubHeaderLine("CONDUCTOR         RECIBIDO POR" );
                    ticket.PrintTicket(NombreImpresora);

                    MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Ticket ticket = new Ticket();
                    ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                    if (dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString().TrimEnd() == "0062" ||
                    dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString().TrimEnd() == "0107")
                    {
                        ticket.AddSubHeaderLine2("RECIBO DE CAJA");
                        ticket.AddSubHeaderLine2(dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "COMPROBANTE").ToString());
                        ticket.AddSubHeaderLine("Planilla: PL-" + _Planilla);
                        ticket.AddSubHeaderLine("                         ");
                        ticket.AddSubHeaderLine("He recibido de GRUPO TRANSPESA S.A.C. la cantidad de S/. " +
                                            dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "MONTO_PAGADO").ToString()
                                            + " por concepto de " + dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "DESCRIPCION").ToString()
                                            + " el día: " + Convert.ToDateTime(dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "FECHA_EMISION").ToString()).ToShortDateString());
                    }
                    else
                    {
                        ticket.AddSubHeaderLine2("GASTO DE VIAJE");
                        ticket.AddSubHeaderLine2("SIN SUSTENTO");
                        ticket.AddSubHeaderLine("N° Planilla: PL-" + _Planilla);
                        ticket.AddSubHeaderLine("                         ");
                        ticket.AddSubHeaderLine("He recibido de GRUPO TRANSPESA S.A.C. la cantidad de S/. " +
                                            dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "MONTO_PAGADO").ToString()
                                            + " por concepto de: " + dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "DESCRIPCION").ToString()
                                            + " el día: ");
                    }

                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("                         ");
                    ticket.AddSubHeaderLine("_________________________");
                    ticket.AddSubHeaderLine("Recibí conforme");
                    ticket.AddSubHeaderLine("Nombre: " + _Conductor);

                    ticket.PrintTicket(NombreImpresora);

                    MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ImprimirReintegro()
        {
            decimal Reintegro = Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtGasto.Text);

            DataTable dtConsultarImpresora = new DataTable();
            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

            //string NombreImpresora = "EPSONA3F19D (WF-C5710 Series)";
            string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

            if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
            {
                MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Ticket ticket = new Ticket();
                ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                ticket.AddSubHeaderLine2("001-" + Correlativo);
                ticket.AddSubHeaderLine("ENTREGA DE EFECTIVO DEL");
                ticket.AddSubHeaderLine("CONDUCTOR");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("Fecha: " + DateTime.Now.ToShortDateString());
                ticket.AddSubHeaderLine("Chofer: " + _Conductor);
                ticket.AddSubHeaderLine("Placa: " + _Placa);
                ticket.AddSubHeaderLine("Planilla: PL-" + _Planilla);
                ticket.AddSubHeaderLine("Reintegro: S/. " + Reintegro.ToString());
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("_____________    _____________");
                ticket.AddSubHeaderLine("  CONDUCTOR      ENTREGADO POR");
                ticket.PrintTicket(NombreImpresora);

                MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnAniadir_Click(object sender, EventArgs e) { InsertarRecibos(sender, e); }

        private void btnCancelar_Click(object sender, EventArgs e) { Limpiar(sender, e); }

        private void btnAgregarFalla_Click(object sender, EventArgs e)
        {
            decimal absoluto;
            if (_Permiso == 0)
            {
                if (Convert.ToDecimal(txtReintegro.Text) < 0) { absoluto = Convert.ToDecimal(txtReintegro.Text) * -1; }
                else { absoluto = Convert.ToDecimal(txtReintegro.Text); }

                if ((Convert.ToDecimal(txtRAutorizado.Text)) - absoluto >= 0) { _Permiso = 1; }
                else { _Permiso = 0; }
            }

            if (_Permiso == 0 && Convert.ToDouble(txtReintegro.Text) < 0.00)
            {
                MessageBox.Show("Necesita autorización del área de Finanzas para generar un reintegro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea liquidar esta planilla?", "LIQUIDAR PLANILLA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LiquidarRecibos();
                    _frmListarPlanillas.btnBuscar_Click(sender, e);
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Permiso == 0)
                {
                    decimal absoluto;
                    if (Convert.ToDecimal(txtReintegro.Text) < 0) { absoluto = Convert.ToDecimal(txtReintegro.Text) * -1; }
                    else { absoluto = Convert.ToDecimal(txtReintegro.Text); }

                    if ((Convert.ToDecimal(txtRAutorizado.Text)) - absoluto >= 0) { _Permiso = 1; }
                    else { _Permiso = 0; }
                }

                if (_Permiso == 0)
                {
                    MessageBox.Show("Necesita autorización del área de Finanzas para generar un reintegro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable dtCorrelativo = new DataTable();
                    dtCorrelativo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(5, "");
                    Correlativo = Convert.ToString(dtCorrelativo.Rows[0]["Codigo"]);

                    dtRecibo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(3, _Planilla);
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    if (dtRecibo.Rows.Count == 0 || txtDescripcionRG.Text.Length == 0)
                    {
                        MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDescripcionRG.Focus();
                        return;
                    }
                    else
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarReintegro(_idConductor, _NroTicket, _Placa,
                                                               Convert.ToDateTime(dtpFechaLiquidacion.Value), Convert.ToInt32(_Planilla), Convert.ToDecimal(txtGasto.Text),
                                                               Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtReintegro.Text), txtDescripcionRG.Text, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (MessageBox.Show("¿Desea imprimir el voucher de reintegro?", "IMPRIMIR REINTEGRO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ImprimirReintegro();
                                ImprimirReintegro();
                            }
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                
            }
            catch { MessageBox.Show("El comprobante seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { EliminarRecibo(); }
            catch { MessageBox.Show("El comprobante seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (Usuario != "EGENNELL" && Usuario != "LQUEZADA" && Usuario != "MADELEINEC" && Usuario != "JALBAN")
                {
                    ImprimirRecibo();
                    if (dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString() == "0078 " ||
                        dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString() == "0107 ")
                    { ImprimirRecibo(); }
                }
                else
                {
                    ImprimirRecibo();
                    if (dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString() == "0078 ")
                    { ImprimirRecibo(); }
                }

            }
            catch { MessageBox.Show("El comprobante seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void comprobanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idNroLiquidacion = Convert.ToInt32(dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "Nro"));

            if (txtDescripcion.Text.Length == 0 || txtMontoAfecto.Text.Length == 0 || txtMontoNoAfecto.Text.Length == 0 || txtMontoImpuestos.Text.Length == 0 || txtMontoPagado.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtDescripcion.Text.Length == 0) { txtDescripcion.Focus(); }
                else
                {
                    if (txtMontoAfecto.Text.Length == 0) { txtMontoAfecto.Focus(); }
                    else { txtMontoNoAfecto.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ComprobanteLiquidaciones(idNroLiquidacion, _Planilla, Convert.ToDateTime(dtpFecha.Value), TipoImpuesto, txtRUC.Text, txtNombre.Text,
                                                Convert.ToString(cbxRecibo.SelectedValue), txtNroRecibo.Text, Convert.ToDecimal(txtMontoAfecto.Text), Convert.ToDecimal(txtMontoNoAfecto.Text),
                                                Convert.ToDecimal(txtMontoImpuestos.Text), Convert.ToDecimal(txtMontoPagado.Text));

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarLiquidaciones();
                    Limpiar(sender, e);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxConcepto.Focus();
                }
            }
        }

        private void cbxConcepto_DropDownClosed(object sender, EventArgs e)
        {
            txtDescripcion.Focus();

            if (Convert.ToString(cbxConcepto.SelectedValue) == "0078")
            {
                txtDescripcion.Text = "INGRESO A CAJA";
                txtMontoAfecto.ReadOnly = true;
                txtMontoImpuestos.ReadOnly = true;
                txtMontoNoAfecto.ReadOnly = false;
                rbNinguno.Checked = true;
                rbNinguno_Click(sender, e);
            }
            else
            {
                rbNinguno.Checked = true;
                rbNinguno_Click(sender, e);
                cbxPeaje.Enabled = false;
                txtDescripcion.Clear();
                label21.Visible = false;
                dtpFechaViatico.Visible = false;
                txtRUC.Clear();
                txtNombre.Clear();
                txtMontoAfecto.ReadOnly = false;
                txtMontoImpuestos.ReadOnly = false;
                txtMontoNoAfecto.ReadOnly = false;

                //txtDescripcion.Clear();
                //txtMontoAfecto.ReadOnly = false;
                //txtMontoImpuestos.ReadOnly = false;
                //rbNinguno.Checked = true;
                //rbNinguno_Click(sender, e);
            }

            if (Convert.ToString(cbxConcepto.SelectedValue) == "0002")
            {
                txtMontoAfecto.ReadOnly = false;
                txtMontoNoAfecto.ReadOnly = false;
                txtMontoImpuestos.ReadOnly = false;
                txtMontoImpuestos.Text = Convert.ToString(0.00M);
                label21.Visible = true;
                dtpFechaViatico.Visible = true;
                txtDescripcion.Text = "ALIMENTOS GRAVADOS";
                rbRegistro.Checked = true;
                rbRegistro_Click(sender, e);
            }

            if (Convert.ToString(cbxConcepto.SelectedValue) == "0077")
            {
                label21.Visible = true;
                dtpFechaViatico.Visible = true;
                txtDescripcion.Text = "ALIMENTACION PARCIAL";
            }

            if (Convert.ToString(cbxConcepto.SelectedValue) == "0107")
            {
                txtMontoAfecto.ReadOnly = false;
                txtMontoNoAfecto.ReadOnly = false;
                txtMontoImpuestos.ReadOnly = false;
                txtMontoImpuestos.Text = Convert.ToString(0.00M);
                label21.Visible = true;
                dtpFechaViatico.Visible = true;
                txtDescripcion.Clear();
                rbNinguno.Checked = true;
                rbNinguno_Click(sender, e);
                txtNroRecibo.Text = "ALIM " + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            }
            /*else
            {
                //txtMontoImpuestos.ReadOnly = true;
                txtMontoImpuestos.Text = Convert.ToString(0.00M);
                label21.Visible = false;
                dtpFechaViatico.Visible = false;
            }*/

            if (Convert.ToString(cbxRecibo.SelectedValue) == "OT        " && Convert.ToString(cbxConcepto.SelectedValue) == "0062")
            { txtNroRecibo.Text = Convert.ToString("RC001-" + _Planilla); }
            //else { txtNroRecibo.Clear(); }

            if (Convert.ToString(cbxConcepto.SelectedValue) == "0001")
            {
                txtMontoAfecto.ReadOnly = false;
                txtMontoNoAfecto.ReadOnly = true;
                txtMontoImpuestos.ReadOnly = false;
                rbRegistro.Checked = true;
                rbRegistro_Click(sender, e);
                cbxPeaje.Enabled = true;
                cbxPeaje_DropDownClosed(sender, e);
            }
            /*else
            {
                txtMontoNoAfecto.ReadOnly = false;
                rbNinguno.Checked = true;
                rbNinguno_Click(sender, e);
                txtDescripcion.Clear();
                cbxPeaje.Enabled = false;
            }
             
            if (Convert.ToString(cbxConcepto.SelectedValue) == "101 ") { txtMontoNoAfecto.Text = "-"; }
            else { txtMontoNoAfecto.Clear(); }
            */
        }

        private void cbxPeaje_DropDownClosed(object sender, EventArgs e)
        {
            DataTable dtListar = new DataTable();
            dtListar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(Convert.ToInt32(cbxPeaje.SelectedValue), 1, "");

            if (dtListar.Rows.Count > 0)
            {
                if (Convert.ToInt32(cbxPeaje.SelectedValue) == 28 || Convert.ToInt32(cbxPeaje.SelectedValue) == 58 || Convert.ToInt32(cbxPeaje.SelectedValue) == 57
                    || Convert.ToInt32(cbxPeaje.SelectedValue) == 56)
                {
                    txtDescripcion.Text = dtListar.Rows[0]["PEAJE"].ToString().TrimEnd();
                    txtRUC.Text = dtListar.Rows[0]["NroRUC"].ToString().TrimEnd();
                    txtNombre.Text = dtListar.Rows[0]["NombreCompleto"].ToString().TrimEnd();
                    rbRegistro.Checked = true;
                    rbRegistro_Click(sender, e);
                    cbxRecibo.SelectedValue = "TI";
                    cbxRecibo_DropDownClosed(sender, e);
                    txtMontoPagado.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                    txtMontoAfecto.Text = Convert.ToString(0.00M);
                    txtMontoNoAfecto.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                    txtMontoImpuestos.Text = Convert.ToString(0.00M);
                }
                else
                {
                    txtDescripcion.Text = dtListar.Rows[0]["PEAJE"].ToString().TrimEnd();
                    txtRUC.Text = dtListar.Rows[0]["NroRUC"].ToString().TrimEnd();
                    txtNombre.Text = dtListar.Rows[0]["NombreCompleto"].ToString().TrimEnd();
                    rbRegistro.Checked = true;
                    rbRegistro_Click(sender, e);
                    txtMontoPagado.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                    txtMontoAfecto.Text = dtListar.Rows[0]["MontoAfecto"].ToString().TrimEnd();
                    txtMontoImpuestos.Text = dtListar.Rows[0]["MontoImpuesto"].ToString().TrimEnd();
                }
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtRUC.Focus(); }
        }

        private void txtRUC_Enter(object sender, EventArgs e)
        { txtRUC.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRUC, ref lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC))
                {
                    //if (lstPlaca.Columns.Count >= 6) { lstPlaca.Columns[5].Width = -1; lstPlaca.Columns[2].Width = -1; }
                    lvRUC.Columns[1].Width = -1;
                    lvRUC.Columns[2].Width = 300;
                    lvRUC.Size = new System.Drawing.Size(400, 200);

                    if (esVALE == "SI") { cbxRecibo.Focus(); }
                    else
                    {
                        ListViewItem RUCActual;
                        RUCActual = lvRUC.SelectedItems[0];
                        txtRUC.Text = Convert.ToString(RUCActual.SubItems[1].Text);
                        txtNombre.Text = Convert.ToString(RUCActual.SubItems[2].Text);
                        lvRUC.Visible = false;
                        cbxRecibo.Focus();
                    }
                }
            }
            catch (Exception ex) { }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxRecibo.Focus(); }

            if (e.KeyChar == Convert.ToChar(Keys.Back)) { txtNombre.Clear(); }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtNombre, ref lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC))
                {
                    //if (lstPlaca.Columns.Count >= 6) { lstPlaca.Columns[5].Width = -1; lstPlaca.Columns[2].Width = -1; }
                    lvRUC.Columns[1].Width = -1;
                    lvRUC.Columns[2].Width = 300;
                    lvRUC.Size = new System.Drawing.Size(400, 200);

                    if (esVALE == "SI") { cbxRecibo.Focus(); }
                    else
                    {
                        ListViewItem RUCActual;
                        RUCActual = lvRUC.SelectedItems[0];
                        txtRUC.Text = Convert.ToString(RUCActual.SubItems[1].Text);
                        txtNombre.Text = Convert.ToString(RUCActual.SubItems[2].Text);
                        lvRUC.Visible = false;
                        cbxRecibo.Focus();
                    }
                }
            }
            catch (Exception ex) { }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxRecibo.Focus(); }

            if (e.KeyChar == Convert.ToChar(Keys.Back)) { txtRUC.Clear(); }
        }

        private void txtRUC_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRUC, ref lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC);
                //if (lstPlaca.Columns.Count >= 6) { lstPlaca.Columns[5].Width = -1; lstPlaca.Columns[2].Width = -1; }
                lvRUC.Columns[1].Width = -1;
                lvRUC.Columns[2].Width = 300;
                lvRUC.Size = new System.Drawing.Size(400, 200);
            }
            catch (Exception ex) { }
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtNombre, ref lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC);
                //if (lstPlaca.Columns.Count >= 6) { lstPlaca.Columns[5].Width = -1; lstPlaca.Columns[2].Width = -1; }
                lvRUC.Columns[1].Width = -1;
                lvRUC.Columns[2].Width = 300;
                lvRUC.Size = new System.Drawing.Size(400, 200);
            }
            catch (Exception ex) { }
        }

        private void txtRUC_Leave(object sender, EventArgs e) { txtRUC.BackColor = Color.White; }

        private void lvRUC_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRUC, ref lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lvRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRUC, ref lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC))
                {
                    if (esVALE == "SI") { cbxRecibo.Focus(); }
                    else
                    {
                        ListViewItem RUCActual;
                        RUCActual = lvRUC.SelectedItems[0];
                        txtRUC.Text = Convert.ToString(RUCActual.SubItems[1].Text);
                        txtNombre.Text = Convert.ToString(RUCActual.SubItems[2].Text);
                        lvRUC.Visible = false;
                        cbxRecibo.Focus();

                        if (Convert.ToString(cbxConcepto.SelectedValue) == "0001")
                        {
                            DataTable dtListar = new DataTable();
                            dtListar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(Convert.ToInt32(cbxPeaje.SelectedValue), 2, txtRUC.Text);

                            if (dtListar.Rows.Count > 0)
                            {
                                txtDescripcion.Text = "PEAJE";
                                rbRegistro.Checked = true;
                                rbRegistro_Click(sender, e);
                                txtMontoPagado.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                                txtMontoAfecto.Text = dtListar.Rows[0]["MontoAfecto"].ToString().TrimEnd();    
                                txtMontoImpuestos.Text = dtListar.Rows[0]["MontoImpuesto"].ToString().TrimEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lvRUC_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRUC, ref lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lvRUC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem RUCActual;
            RUCActual = lvRUC.SelectedItems[0];
            txtRUC.Text = Convert.ToString(RUCActual.SubItems[1].Text);
            txtNombre.Text = Convert.ToString(RUCActual.SubItems[2].Text);
            lvRUC.Visible = false;
            cbxRecibo.Focus();

            if (Convert.ToString(cbxConcepto.SelectedValue) == "0001")
            {
                DataTable dtListar = new DataTable();
                dtListar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(Convert.ToInt32(cbxPeaje.SelectedValue), 2, txtRUC.Text);

                if (dtListar.Rows.Count > 0)
                {
                    txtDescripcion.Text = "PEAJE";
                    rbRegistro.Checked = true;
                    rbRegistro_Click(sender, e);
                    txtMontoPagado.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                    txtMontoAfecto.Text = dtListar.Rows[0]["MontoAfecto"].ToString().TrimEnd();      
                    txtMontoImpuestos.Text = dtListar.Rows[0]["MontoImpuesto"].ToString().TrimEnd();
                }
            }

            /*
                DataTable dtRUC = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC(txtRUC.Text);
                if (dtRUC.Rows.Count > 0)
                {
                    for (int i = 0; i < dtRUC.Rows.Count; i++)
                    {
                        txtNombre.Clear();
                        txtNombre.Text = dtRUC.Rows[i]["NombreCompleto"].ToString();
                    }
                }
                else { txtNombre.Clear(); }
                cbxRecibo.Focus();
             */
        }

        private void cbxRecibo_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToString(cbxRecibo.SelectedValue) == "OT        " && Convert.ToString(cbxConcepto.SelectedValue) == "0062")
            { txtNroRecibo.Text = Convert.ToString("RC001-" + _Planilla); }
            else
            {
                if (Convert.ToString(cbxConcepto.SelectedValue) == "0107")
                { txtNroRecibo.Text = "ALIM " + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString(); }
                else { txtNroRecibo.Clear(); }
            }
            
            if (Convert.ToString(cbxRecibo.SelectedValue) == "          ")
            {
                txtNroRecibo.Enabled = false;
                /*
                txtNroRecibo.Clear();
                if (rbNinguno.Checked == true)
                    txtMontoNoAfecto.Focus();
                else
                {
                    if (rbRegistro.Checked == true)
                        txtMontoAfecto.Focus();
                }
                */
            }
            else
            {
                txtNroRecibo.Enabled = true;
                txtNroRecibo.Focus();
            }

            if (Convert.ToString(cbxRecibo.SelectedValue).TrimEnd() == "TI" || Convert.ToString(cbxRecibo.SelectedValue).TrimEnd() == "BV")    
            {
                txtMontoAfecto.ReadOnly = false;
                txtMontoNoAfecto.ReadOnly = false;
                txtMontoImpuestos.ReadOnly = false;

                txtMontoAfecto.Text = Convert.ToString(0.00M);
                txtMontoNoAfecto.Text = Convert.ToString(0.00M);
                txtMontoImpuestos.Text = Convert.ToString(0.00M);
                txtMontoPagado.Text = Convert.ToString(0.00M);

                if (Convert.ToString(cbxConcepto.SelectedValue) == "0001")
                {
                    DataTable dtListar = new DataTable();
                    dtListar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(Convert.ToInt32(cbxPeaje.SelectedValue), 1, "");

                    if (dtListar.Rows.Count > 0)
                    {
                        txtMontoPagado.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                        txtMontoNoAfecto.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                    }
                }
            }
            else
            {
                txtMontoAfecto.ReadOnly = false;
                txtMontoNoAfecto.ReadOnly = false;
                txtMontoImpuestos.ReadOnly = false;

                txtMontoAfecto.Text = Convert.ToString(0.00M);
                txtMontoNoAfecto.Text = Convert.ToString(0.00M);
                txtMontoImpuestos.Text = Convert.ToString(0.00M);
                txtMontoPagado.Text = Convert.ToString(0.00M);

                if (Convert.ToString(cbxConcepto.SelectedValue) == "0001")
                {
                    DataTable dtListar = new DataTable();
                    dtListar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(Convert.ToInt32(cbxPeaje.SelectedValue), 1, "");

                    if (dtListar.Rows.Count > 0)
                    {
                        txtMontoPagado.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                        txtMontoAfecto.Text = dtListar.Rows[0]["MontoAfecto"].ToString().TrimEnd();
                        txtMontoImpuestos.Text = dtListar.Rows[0]["MontoImpuesto"].ToString().TrimEnd();
                    }
                }
            }
        }

        private void txtNroRecibo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (rbNinguno.Checked == true)
                    txtMontoNoAfecto.Focus();
                else
                {
                    if (rbRegistro.Checked == true)
                        txtMontoAfecto.Focus();
                }
            }
        }

        private void txtMontoAfecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try
                {
                    txtMontoPagado.Text = Math.Round(Convert.ToDecimal(txtMontoAfecto.Text) * 1.18m, 2, MidpointRounding.AwayFromZero).ToString();
                    txtMontoImpuestos.Text = Math.Round(Convert.ToDecimal(txtMontoAfecto.Text) * 0.18m, 2, MidpointRounding.AwayFromZero).ToString();

                    /*
                    txtMontoPagado.Text = Math.Round((Convert.ToDecimal(txtMontoAfecto.Text) * Convert.ToDecimal(1.18)), 2).ToString();
                    txtMontoImpuestos.Text = Math.Round((Convert.ToDecimal(txtMontoAfecto.Text) * Convert.ToDecimal(0.18)), 2).ToString();
                    */
                }
                catch
                {
                    txtMontoPagado.Clear();
                    txtMontoImpuestos.Text = Convert.ToString(0.00M);
                }
            }

            if (e.KeyChar == Convert.ToChar(Keys.Back)) { txtMontoPagado.Clear(); }
        }

        private void txtMontoNoAfecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtMontoPagado.Text = txtMontoNoAfecto.Text; }

            if (e.KeyChar == Convert.ToChar(Keys.Back)) { txtMontoPagado.Clear(); }
        }

        private void txtMontoImpuestos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try { txtMontoPagado.Text = Math.Round((Convert.ToDecimal(txtMontoAfecto.Text) + Convert.ToDecimal(txtMontoImpuestos.Text)), 2, MidpointRounding.AwayFromZero).ToString(); }
                catch { txtMontoImpuestos.Text = Convert.ToString(0.00M); }
            }
        }

        private void txtMontoPagado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }
        }

        /*
        public void dgvPlanillas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            panel1.Visible = true;
            Planilla = Convert.ToInt32(dgvPlanillas.SelectedRows[0].Cells[0].Value);
            txtGasto.Text = Convert.ToString(dgvPlanillas.SelectedRows[0].Cells[2].Value);
            ListarLiquidaciones();
            rbNinguno.Checked = true;
            rbNinguno_Click(sender, e);
            Limpiar();

            //txtNroRecibo.Enabled = false;
        }
        */

        private void dtgvListaComprobantes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string ConceptoGasto = dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString();
                if (ConceptoGasto == "0002 ")
                {
                    rbRegistro.Checked = true;
                    rbRegistro_Click(sender, e);

                    cbxConcepto.Text = dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "CONCEPTO_GASTO").ToString();
                    txtDescripcion.Text = dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "DESCRIPCION").ToString();
                    txtMontoPagado.Text = dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "MONTO_PAGADO").ToString();
                    txtRUC.Focus();
                }
            }
            catch { MessageBox.Show("El gasto seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgvListaComprobantesView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            string campo = view.FocusedColumn.FieldName;

            if (campo != "MONTO_AFECTO" && campo != "MONTO_NO_AFECTO" && campo != "MONTO_IMPUESTOS") { e.Cancel = true; }
        }

        private void dtgvListaComprobantesView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName != "MONTO_AFECTO" && e.Column.FieldName != "MONTO_NO_AFECTO" && e.Column.FieldName != "MONTO_IMPUESTOS")
                    return;

                GridView view = sender as GridView;

                string planilla = Convert.ToString(view.GetRowCellValue(e.RowHandle, "PLANILLA"));
                string ruc = Convert.ToString(view.GetRowCellValue(e.RowHandle, "RUC"));
                string comprobante = Convert.ToString(view.GetRowCellValue(e.RowHandle, "COMPROBANTE"));
                decimal montoAfecto = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "MONTO_AFECTO") ?? 0);
                decimal montoNoAfecto = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "MONTO_NO_AFECTO") ?? 0);
                decimal montoImpuestos = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "MONTO_IMPUESTOS") ?? 0);

                DataTable dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ActualizarLiquidaciones(planilla, ruc, comprobante, montoAfecto, montoNoAfecto, montoImpuestos);

                string respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string nroRpta = respuesta.Substring(0, 1);

                if (nroRpta != "0") { MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                ListarLiquidaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("El monto no se pudo actualizar.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ListarLiquidaciones();
            }
        }

        private void dtgvListaComprobantes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view == null) return;

                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    view.PostEditor();
                    view.UpdateCurrentRow();
                }

                if (e.KeyCode == Keys.Escape)
                {
                    e.Handled = true;
                    view.HideEditor();
                    view.CancelUpdateCurrentRow();
                    ListarLiquidaciones();
                }
            }
            catch (Exception ex)
            { MessageBox.Show("El monto no se pudo actualizar.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgvListaComprobantes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string TipoImpuesto = dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "TipoImpuesto").ToString();
                string ConceptoGasto = dtgvListaComprobantesView.GetRowCellValue(dtgvListaComprobantesView.FocusedRowHandle, "ConceptoGasto").ToString();

                if (TipoImpuesto == "N    " /*|| ConceptoGasto == "0107 "*/)
                {
                    eliminarToolStripMenuItem.Enabled = true;
                    imprimirToolStripMenuItem.Enabled = true;
                }
                else
                {
                    eliminarToolStripMenuItem.Enabled = true;
                    imprimirToolStripMenuItem.Enabled = false;
                }

                if (ConceptoGasto == "0002 ") { comprobanteToolStripMenuItem.Enabled = true; }
                else { comprobanteToolStripMenuItem.Enabled = false; }
            }
            catch
            {
                eliminarToolStripMenuItem.Enabled = false;
                imprimirToolStripMenuItem.Enabled = false;
                comprobanteToolStripMenuItem.Enabled = false;
            }
        }

        private void rbNinguno_Click(object sender, EventArgs e)
        {
            if (rbNinguno.Checked == true)
            {
                CargarComboRecibo();
                //txtMontoNoAfecto.ReadOnly = false;
                txtMontoNoAfecto.Clear();
                txtMontoNoAfecto.Text = Convert.ToString(0.00M);
                txtMontoPagado.Clear();
                txtMontoAfecto.Text = Convert.ToString(0.00M);
                //txtMontoAfecto.ReadOnly = true;
                txtMontoImpuestos.Text = Convert.ToString(0.00M);
                cbxRecibo.SelectedValue = "          ";
                cbxRecibo_DropDownClosed(sender, e);
                TipoImpuesto = Convert.ToChar("N");
            }
        }

        private void rbRegistro_Click(object sender, EventArgs e)
        {
            if (rbRegistro.Checked == true)
            {
                CargarComboRecibo();
                //txtMontoAfecto.ReadOnly = false;
                txtMontoAfecto.Clear();
                txtMontoAfecto.Text = Convert.ToString(0.00M);
                txtMontoPagado.Clear();
                txtMontoNoAfecto.Text = Convert.ToString(0.00M);
                /*
                if (Convert.ToString(cbxConcepto.SelectedValue) == "0002") { txtMontoNoAfecto.ReadOnly = false; }
                else { txtMontoNoAfecto.ReadOnly = true; }
                */
                txtMontoImpuestos.Text = Convert.ToString(0.00M);
                cbxRecibo.SelectedValue = "FL";
                cbxRecibo_DropDownClosed(sender, e);
                TipoImpuesto = Convert.ToChar("I");
            }
        }
    }
}
