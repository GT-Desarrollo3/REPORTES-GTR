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
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using System.IO;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmListarPlanillas : MetroFramework.Forms.MetroForm
    {
        public int xClick = 0, yClick = 0, xClick2 = 0, yClick2 = 0, xClick3 = 0, yClick3 = 0, OpcionViaticos, idConductor = 0, Detalle;
        DataTable dtViatico = new DataTable();
        DataTable dtHistorialRutas = new DataTable();
        public int idListaViatico, idConductorViatico;
        public string PlanillaViatico, Sucursal;

        public frmListarPlanillas()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxOperacionH.SelectedIndexChanged -= cbxOperacionH_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void cbxOperacionH_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion2(); }

        private void frmListarPlanillas_Load(object sender, EventArgs e)
        {
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarPlanillas");
            DataTable dtEspeciales = new DataTable();

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Pagar Planillas")
                    {
                        agregarReintegroToolStripMenuItem.Enabled = true;
                        i = 999;
                    }
                    else { agregarReintegroToolStripMenuItem.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Liquidar")
                    {
                        liquidarToolStripMenuItem.Enabled = true;
                        i = 999;
                    }
                    else { liquidarToolStripMenuItem.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Autorizar Reintegros")
                    {
                        autorizarReintegroToolStripMenuItem.Enabled = true;
                        i = 999;
                    }
                    else { autorizarReintegroToolStripMenuItem.Enabled = false; }
                }
            }
            else
            {
                agregarReintegroToolStripMenuItem.Enabled = false;
                liquidarToolStripMenuItem.Enabled = false;
                autorizarReintegroToolStripMenuItem.Enabled = false;
            }

            CargarComboOperacion();
            cbxOperaciones.SelectedValue = 5;
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            dtpViaticoInicio.Value = new DateTime(dtpViaticoInicio.Value.Year, dtpViaticoInicio.Value.Month, 1);
            dtpViaticoFin.Value = DateTime.Now;
            dtpViaticoAdicional.Value = DateTime.Now;
            rbLiquidadas.Checked = true;
            rbLiquidadas_Click(sender, e); 
            cbxOperaciones_DropDownClosed(sender, e);
        }


        private void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        private void CargarComboOperacion2()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperacionH.DataSource = dtOperacion;
            cbxOperacionH.DisplayMember = "Descripcion";
            cbxOperacionH.ValueMember = "IdOperacion";
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
                Ticket ticket = new Ticket();
                ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                ticket.AddSubHeaderLine2("ENTREGA DE VUELTO");
                ticket.AddSubHeaderLine2("DEL CONDUCTOR");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("Fecha: " + DateTime.Now.ToShortDateString());
                ticket.AddSubHeaderLine("Chofer: " + dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "CONDUCTOR").ToString());
                ticket.AddSubHeaderLine("Placa: " + dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "TRACTO").ToString());
                ticket.AddSubHeaderLine("Planilla: PL-" + dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "PLANILLA").ToString());
                ticket.AddSubHeaderLine("Vuelto: S/. " + dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "VUELTO").ToString());
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("                         ");
                ticket.AddSubHeaderLine("_____________    _____________");
                ticket.AddSubHeaderLine("CONDUCTOR         RECIBIDO POR");
                ticket.PrintTicket(NombreImpresora);

                MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ListarHistorialRutas()
        {
            if (dtpHFechaIni.Value > dtpHFechaFin.Value)
            {
                MessageBox.Show("La fecha de inicio debe ser menor o igual que la fecha fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpHFechaIni.Focus();
                return;
            }
            else
            {
                dtHistorialRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarHistorialRutas(txtRuta2.Text, dtpHFechaIni.Text, dtpHFechaFin.Text,
                                                              Convert.ToInt32(cbxOperacionH.SelectedValue));
                dtgHistorialRuta.DataSource = dtHistorialRutas;
                if (dtHistorialRutas.Rows.Count > 0)
                {
                    dgvHistorialRutaVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialRutaVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvHistorialRutaVista.BestFitColumns();
                }
            }
        }


        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { btnBuscar_Click(sender, e); }

        private void rbPendientes_Click(object sender, EventArgs e)
        {
            if (rbPendientes.Checked == true)
            {
                label1.Text = "PLANILLAS PENDIENTES";
                rbLiquidadas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbPendientes.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                rbEliminadas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                btnBuscar_Click(sender, e);
            }
        }

        private void rbLiquidadas_Click(object sender, EventArgs e)
        {
            if (rbLiquidadas.Checked == true)
            {
                label1.Text = "PLANILLAS LIQUIDADAS";
                rbLiquidadas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                rbPendientes.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbEliminadas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                btnBuscar_Click(sender, e);
            }
        }
        
        private void rbEliminadas_Click(object sender, EventArgs e)
        {
            if (rbEliminadas.Checked == true)
            {
                label1.Text = "PLANILLAS ELIMINADAS";
                rbLiquidadas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbPendientes.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbEliminadas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                btnBuscar_Click(sender, e);
            }
        }

        public void btnBuscar_Click(object sender, EventArgs e)
        {
            int filtro = 0;
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }

            dtgPlanillasLiquidadas.DataSource = null;
            dgvPlanillasLiquidadasVista.Columns.Clear();

            dtgPlanillasPendientes.DataSource = null;
            dgvPlanillasPendientesVista.Columns.Clear();

            dtgPlanillasEliminadas.DataSource = null;
            dgvPlanillasEliminadasVista.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();

            if (rbLiquidadas.Checked == true)
            {
                filtro = 1;
                dtgPlanillasLiquidadas.Visible = true;
                dtgPlanillasPendientes.Visible = false;
                dtgPlanillasEliminadas.Visible = false;

                dtgPlanillasLiquidadas.BringToFront();
                dtgPlanillasPendientes.SendToBack();
                dtgPlanillasEliminadas.SendToBack();
            }
            else
            {
                
                if (rbPendientes.Checked == true)
                {
                    filtro = 0;
                    dtgPlanillasLiquidadas.Visible = false;
                    dtgPlanillasPendientes.Visible = true;
                    dtgPlanillasEliminadas.Visible = false;

                    dtgPlanillasLiquidadas.SendToBack();
                    dtgPlanillasPendientes.BringToFront();
                    dtgPlanillasEliminadas.SendToBack();
                }
                else
                {
                    filtro = 2;
                    dtgPlanillasLiquidadas.Visible = false;
                    dtgPlanillasPendientes.Visible = false;
                    dtgPlanillasEliminadas.Visible = true;

                    dtgPlanillasLiquidadas.SendToBack();
                    dtgPlanillasPendientes.SendToBack();
                    dtgPlanillasEliminadas.BringToFront();
                }
            }

            dt.Clear();
            dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasLiquidadas(Convert.ToInt32(cbxOperaciones.SelectedValue), dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString(),
                                            txtConductor.Text, txtPlanilla.Text, txtRuta.Text, filtro);
            if (dt.Rows.Count > 0)
            {
                if (filtro == 1)
                {
                    dtgPlanillasLiquidadas.DataSource = dt;
                    dgvPlanillasLiquidadasVista.Columns["idTicketGasto"].Visible = false;
                    dgvPlanillasLiquidadasVista.Columns["IdConductor"].Visible = false;
                    dgvPlanillasLiquidadasVista.Columns["IdRuta"].Visible = false;
                    dgvPlanillasLiquidadasVista.Columns["TipoProgramacion"].Visible = false;

                    dgvPlanillasLiquidadasVista.Columns["IMPORTE_TOTAL"].Summary.Clear();
                    dgvPlanillasLiquidadasVista.Columns["IMPORTE_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "IMPORTE_TOTAL", "{0:N2}");
                    dgvPlanillasLiquidadasVista.Columns["TOTAL_GASTO"].Summary.Clear();
                    dgvPlanillasLiquidadasVista.Columns["TOTAL_GASTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL_GASTO", "{0:N2}");
                    dgvPlanillasLiquidadasVista.Columns["VUELTO"].Summary.Clear();
                    dgvPlanillasLiquidadasVista.Columns["VUELTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "VUELTO", "{0:N2}");
                    dgvPlanillasLiquidadasVista.Columns["DESCUENTO"].Summary.Clear();
                    dgvPlanillasLiquidadasVista.Columns["DESCUENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DESCUENTO", "{0:N2}");
                    dgvPlanillasLiquidadasVista.Columns["REINTEGRO"].Summary.Clear();
                    dgvPlanillasLiquidadasVista.Columns["REINTEGRO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "REINTEGRO", "{0:N2}");
                    dgvPlanillasLiquidadasVista.Columns["SUCURSAL"].Summary.Clear();
                    dgvPlanillasLiquidadasVista.Columns["SUCURSAL"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "SUCURSAL", "Total: {0}");

                    dgvPlanillasLiquidadasVista.BestFitColumns();
                }
                else
                {
                    if (filtro == 0)
                    {
                        dtgPlanillasPendientes.DataSource = dt;
                        dgvPlanillasPendientesVista.Columns["idTicketGasto"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["idGastoXRutaC"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["IdConductor"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["ViConductor"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["Permiso"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["IdRuta"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["TipoProgramacion"].Visible = false;

                        dgvPlanillasPendientesVista.Columns["TOTAL_GASTO"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["VUELTO"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["DESCUENTO"].Visible = false;
                        dgvPlanillasPendientesVista.Columns["REINTEGRO"].Visible = false;

                        dgvPlanillasPendientesVista.BestFitColumns();
                    }
                    else
                    {
                        dtgPlanillasEliminadas.DataSource = dt;

                        dgvPlanillasEliminadasVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvPlanillasEliminadasVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                        dgvPlanillasEliminadasVista.Columns["FECHA_ANULA"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvPlanillasEliminadasVista.Columns["FECHA_ANULA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                        dgvPlanillasEliminadasVista.Columns["IMPORTE_TOTAL"].Summary.Clear();
                        dgvPlanillasEliminadasVista.Columns["IMPORTE_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "IMPORTE_TOTAL", "{0:N2}");
                        dgvPlanillasEliminadasVista.Columns["ADICIONAL"].Summary.Clear();
                        dgvPlanillasEliminadasVista.Columns["ADICIONAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ADICIONAL", "{0:N2}");

                        dgvPlanillasEliminadasVista.BestFitColumns();
                    }
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (rbLiquidadas.Checked == true)
            {
                if (dtgPlanillasLiquidadas.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Planillas Liquidadas el dia " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                    dtgPlanillasLiquidadas.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            else
            {
                if (rbPendientes.Checked == true)
                {
                    if (dtgPlanillasPendientes.DataSource == null)
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
                        string nombre = System.IO.Path.Combine(desktop, "Planillas por liquidar - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                        dtgPlanillasPendientes.ExportToXlsx(nombre);
                        Process.Start(nombre);
                    }
                }
                else
                {
                    if (dtgPlanillasEliminadas.DataSource == null)
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
                        string nombre = System.IO.Path.Combine(desktop, "Planillas eliminadas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                        dtgPlanillasEliminadas.ExportToXlsx(nombre);
                        Process.Start(nombre);
                    }
                }
            }
        }

        private void btnFiltroPlanillas_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel4.BringToFront();
            cbxSucursal.Text = "TODAS";
            cbxSucursal_DropDownClosed(sender, e);
            cbDetalle.Checked = false;
            cbDetalle_CheckedChanged(sender, e);

            dtpReporteInicio.Value = new DateTime(dtpReporteInicio.Value.Year, dtpReporteInicio.Value.Month, 1);
            dtpReporteFin.Value = DateTime.Now;
        }

        private void btnConsolidadoPendientes_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt4 = new System.Data.DataTable();
            dt4 = clsOperacionesBL.Instancia.ReportesApp_Liquidacion_PlanillasPendientesConsolidado(dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString(), Utilitario.Instancia.SesionUsuario.usuario);

            if (dt4.Rows.Count > 0)
            {
                dtgConsolidado.DataSource = null;
                dtgvConsolidadoVista.Columns.Clear();
                dtgConsolidado.DataSource = dt4;
                dtgvConsolidadoVista.BestFitColumns();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "ReportePlanillaXLiquidarConsolidado - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgConsolidado.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
        }

        private void btnListaAdicionales_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt5 = new System.Data.DataTable();
            dt5 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarAdelantosAdicionales(Convert.ToInt32(cbxOperaciones.SelectedValue),
                                             dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString());

            if (dt5.Rows.Count > 0)
            {
                dtgListarAdicionales.DataSource = null;
                dgvListarAdicionalesVista.Columns.Clear();
                dtgListarAdicionales.DataSource = dt5;
                dgvListarAdicionalesVista.BestFitColumns();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Gastos Adicionales Registrados - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgListarAdicionales.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
        }

        private void btnListaViaticos_Click(object sender, EventArgs e)
        {
            OpcionViaticos = 2;
            metroLabel19.Text = "REGISTRO DE VIÁTICOS DE CONDUCTORES";
            dtpViaticoInicio.Value = new DateTime(dtpViaticoInicio.Value.Year, dtpViaticoInicio.Value.Month, 1);
            dtpViaticoFin.Value = DateTime.Now;
            dtgvListaViaticos.DataSource = null;
            panel3.Visible = true;
            btnBuscarViatico_Click(sender, e);
            panel3.BringToFront();
        }

        private void tsHistorialCambiosRuta_Click(object sender, EventArgs e)
        {
            txtRuta2.Clear();
            dtpHFechaIni.Value = DateTime.Now;
            dtpHFechaFin.Value = DateTime.Now;
            CargarComboOperacion2();
            cbxOperacionH.SelectedValue = 5;

            dtgHistorialRuta.DataSource = null;
            dgvHistorialRutaVista.Columns.Clear();
            ListarHistorialRutas();

            pHistorialRutas.Location = new System.Drawing.Point(573, 319);
            pHistorialRutas.Visible = true;
            pHistorialRutas.BringToFront();
        }

        private void pHistorialRutas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pHistorialRutas.Left = pHistorialRutas.Left + (e.X - xClick2);
                pHistorialRutas.Top = pHistorialRutas.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrarHist_Click(object sender, EventArgs e)
        {
            txtRuta2.Clear();
            dtpHFechaIni.Value = DateTime.Now;
            dtpHFechaFin.Value = DateTime.Now;
            
            pHistorialRutas.Visible = false;
            pHistorialRutas.SendToBack();
        }

        private void txtRuta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialRutas(); }
        }

        private void dtpHFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialRutas(); }
        }

        private void dtpHFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialRutas(); }
        }

        private void cbxOperacionH_DropDownClosed(object sender, EventArgs e) { ListarHistorialRutas(); }

        private void dgvHistorialRutaVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "PAGADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }
            }
        }

        private void btnBuscarH_Click(object sender, EventArgs e) { ListarHistorialRutas(); }

        private void btnExcelH_Click(object sender, EventArgs e)
        {
            if (dtgHistorialRuta.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE CAMBIOS DE RUTA - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgHistorialRuta.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            OpcionViaticos = 1;
            metroLabel19.Text = "VIÁTICOS ADICIONALES DE CONDUCTORES";
            dtpViaticoInicio.Value = new DateTime(dtpViaticoInicio.Value.Year, dtpViaticoInicio.Value.Month, 1);
            dtpViaticoFin.Value = DateTime.Now;
            dtgvListaViaticos.DataSource = null;
            panel3.Visible = true;
            btnBuscarViatico_Click(sender, e);
            panel3.BringToFront();
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaFin.Focus(); }
        }

        private void txtPlanilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void dgvPlanillasPendientesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            Bitmap gastosi = Properties.Resources.gastosentregados;
            Bitmap gastono = Properties.Resources.gastospendientes;

            //Image Pagado = Image.FromFile("R:\\imagen\\gastosentregados.jpg");
            //Image Pendiente = Image.FromFile("R:\\imagen\\gastospendientes.jpg");

            if (e.Column.FieldName == "CAJA")
            {
                if (e.CellValue.ToString() == "PAGADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "GENERADO") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }

                if (e.CellValue.ToString() == " ") { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
            }

            if (e.Column.FieldName == "CAJA_ADICIONAL")
            {
                if (e.CellValue.ToString() == "PAGADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "GENERADO") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }

                if (e.CellValue.ToString() == " ") { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
            }

            if (e.Column.FieldName == "AUTORIZACION")
            {
                if (e.CellValue.ToString() == "OK") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }

            if (e.Column.FieldName == "ALERTA_RUTA")
            {
                if (e.CellValue.ToString() == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void dgvPlanillasLiquidadasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO_V")
            {
                if (e.CellValue.ToString() == "PEN") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (e.CellValue.ToString().Contains("2") || e.CellValue.ToString().Contains("3")) { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
            }

            if (e.Column.FieldName == "ALERTA_RUTA")
            {
                if (e.CellValue.ToString() == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void dtgPlanillasLiquidadas_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlanillasLiquidadasVista.FocusedColumn.FieldName == "RUTA" || dgvPlanillasLiquidadasVista.FocusedColumn.FieldName == "ALERTA_RUTA")
                {
                    DataTable dtCambioRutas = new DataTable();
                    string Planilla = dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "PLANILLA").ToString();

                    dtCambioRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarCambioRutas(Planilla);

                    if (dtCambioRutas.Rows.Count > 0)
                    {
                        txtPlanillaCR.Clear();
                        txtRutaOriginalCR.Clear();
                        txtRutaActualCR.Clear();
                        txtUsuarioCR.Clear();
                        dtpFechaCR.Value = DateTime.Now;

                        txtPlanillaCR.Text = Planilla;
                        txtRutaOriginalCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["RUTA_ORIGINAL"]);
                        txtRutaActualCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["RUTA_NUEVA"]);
                        txtUsuarioCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["USUARIO"]);
                        dtpFechaCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["FECHA"]);

                        pCambioRuta.Visible = true;
                        pCambioRuta.BringToFront();
                    }
                }
                else
                {
                    dtgAdelantos.DataSource = null;
                    dtgvAdelantosView.Columns.Clear();

                    dtgvListaComprobantes.DataSource = null;
                    dtgvListaComprobantesView.Columns.Clear();

                    txtPlanillaGA.Clear();
                    txtGastoGA.Clear();
                    txtRGRA.Clear();

                    string Planilla = dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "PLANILLA").ToString();
                    string GastoTotal = dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "TOTAL_GASTO").ToString();
                    decimal ImporteTotal = Convert.ToDecimal(dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "IMPORTE_TOTAL"));

                    txtPlanillaGA.Text = Planilla;
                    txtGastoGA.Text = "S/. " + GastoTotal;
                    txtRGRA.Text = dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "RG").ToString();

                    DataTable dtRecibos = new DataTable();
                    DataTable dtRecibos2 = new DataTable();
                    DataTable dtRecibos3 = new DataTable();

                    dtRecibos = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRecibosLiquidados(Planilla);
                    dtRecibos2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ActualizarRecibosLiquidados(Planilla, ImporteTotal);

                    if (dtRecibos.Rows.Count > 0)
                    {
                        dtgvListaComprobantes.DataSource = dtRecibos;

                        dtgvListaComprobantesView.Columns["PLANILLA"].Visible = false;
                        dtgvListaComprobantesView.Columns["CONCEPTO_GASTO"].Visible = false;

                        dtgvListaComprobantesView.Columns["MONTO_AFECTO"].DisplayFormat.FormatType = FormatType.Numeric;
                        dtgvListaComprobantesView.Columns["MONTO_AFECTO"].DisplayFormat.FormatString = "n2";
                        dtgvListaComprobantesView.Columns["MONTO_IMPUESTO"].DisplayFormat.FormatType = FormatType.Numeric;
                        dtgvListaComprobantesView.Columns["MONTO_IMPUESTO"].DisplayFormat.FormatString = "n2";
                        dtgvListaComprobantesView.Columns["MONTO_TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                        dtgvListaComprobantesView.Columns["MONTO_TOTAL"].DisplayFormat.FormatString = "n2";
                        dtgvListaComprobantesView.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvListaComprobantesView.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy";

                        dtgvListaComprobantesView.Columns["MONTO_AFECTO"].Summary.Clear();
                        dtgvListaComprobantesView.Columns["MONTO_AFECTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_AFECTO", "{0:N2}");
                        dtgvListaComprobantesView.Columns["MONTO_IMPUESTO"].Summary.Clear();
                        dtgvListaComprobantesView.Columns["MONTO_IMPUESTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_IMPUESTO", "{0:N2}");
                        dtgvListaComprobantesView.Columns["MONTO_TOTAL"].Summary.Clear();
                        dtgvListaComprobantesView.Columns["MONTO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_TOTAL", "{0:N2}");

                        dtgvListaComprobantesView.BestFitColumns();
                    }

                    dtRecibos3 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarAdelantosPlanilla(Planilla);
                    if (dtRecibos3.Rows.Count > 0)
                    {
                        dtgAdelantos.DataSource = dtRecibos3;

                        dtgvAdelantosView.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvAdelantosView.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                        dtgvAdelantosView.Columns["UltimaFechaModif"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvAdelantosView.Columns["UltimaFechaModif"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                        dtgvAdelantosView.Columns["MONTO_TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                        dtgvAdelantosView.Columns["MONTO_TOTAL"].DisplayFormat.FormatString = "n2";

                        dtgvAdelantosView.Columns["MONTO_TOTAL"].Summary.Clear();
                        dtgvAdelantosView.Columns["MONTO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_TOTAL", "{0:N2}");

                        dtgvAdelantosView.BestFitColumns();
                    }

                    panel1.Visible = true;
                    panel1.BringToFront();
                    tabReporteGA.SelectedTab = tabGastos;
                }
            }
            catch { MessageBox.Show("La planilla seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgPlanillasPendientes_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlanillasPendientesVista.FocusedColumn.FieldName == "RUTA" || dgvPlanillasPendientesVista.FocusedColumn.FieldName == "ALERTA_RUTA")
                {
                    DataTable dtCambioRutas = new DataTable();
                    string Planilla = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();

                    dtCambioRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarCambioRutas(Planilla);

                    if (dtCambioRutas.Rows.Count > 0)
                    {
                        txtPlanillaCR.Clear();
                        txtRutaOriginalCR.Clear();
                        txtRutaActualCR.Clear();
                        txtUsuarioCR.Clear();
                        dtpFechaCR.Value = DateTime.Now;

                        txtPlanillaCR.Text = Planilla;
                        txtRutaOriginalCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["RUTA_ORIGINAL"]);
                        txtRutaActualCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["RUTA_NUEVA"]);
                        txtUsuarioCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["USUARIO"]);
                        dtpFechaCR.Text = Convert.ToString(dtCambioRutas.Rows[0]["FECHA"]);

                        pCambioRuta.Visible = true;
                        pCambioRuta.BringToFront();
                    }
                }
                else
                {
                    dtgAdelantos.DataSource = null;
                    dtgvAdelantosView.Columns.Clear();

                    dtgvListaComprobantes.DataSource = null;
                    dtgvListaComprobantesView.Columns.Clear();

                    txtPlanillaGA.Clear();
                    txtGastoGA.Clear();
                    txtRGRA.Clear();

                    string Planilla = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
                    decimal ImporteTotal = Convert.ToDecimal(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IMPORTE_TOTAL"));
                    decimal Adicional = Convert.ToDecimal(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "ADICIONAL"));
                    decimal GastoTotal = ImporteTotal + Adicional;

                    txtPlanillaGA.Text = Planilla;
                    txtGastoGA.Text = "S/. " + Convert.ToString(GastoTotal);
                    txtRGRA.Text = " ";

                    DataTable dtRecibos = new DataTable();
                    dtRecibos = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarAdelantosPlanilla(Planilla);

                    if (dtRecibos.Rows.Count > 0)
                    {
                        dtgAdelantos.DataSource = dtRecibos;

                        dtgvAdelantosView.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvAdelantosView.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                        dtgvAdelantosView.Columns["UltimaFechaModif"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvAdelantosView.Columns["UltimaFechaModif"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                        dtgvAdelantosView.Columns["MONTO_TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                        dtgvAdelantosView.Columns["MONTO_TOTAL"].DisplayFormat.FormatString = "n2";

                        dtgvAdelantosView.Columns["MONTO_TOTAL"].Summary.Clear();
                        dtgvAdelantosView.Columns["MONTO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_TOTAL", "{0:N2}");

                        dtgvAdelantosView.BestFitColumns();
                    }

                    panel1.Visible = true;
                    panel1.BringToFront();
                    tabReporteGA.SelectedTab = tabAdelantos;
                }
            }
            catch { MessageBox.Show("La planilla seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrarCR_Click(object sender, EventArgs e)
        {
            txtPlanillaCR.Clear();
            txtRutaOriginalCR.Clear();
            txtRutaActualCR.Clear();
            txtUsuarioCR.Clear();
            dtpFechaCR.Value = DateTime.Now;

            pCambioRuta.Visible = false;
            pCambioRuta.SendToBack();
            pCambioRuta.Location = new System.Drawing.Point(680, 339);
        }

        private void pCambioRuta_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick3 = e.X; yClick3 = e.Y; }
            else
            {
                pCambioRuta.Left = pCambioRuta.Left + (e.X - xClick3);
                pCambioRuta.Top = pCambioRuta.Top + (e.Y - yClick3);
            }
        }

        private void cambioDeRutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Quiere añadir un reintegro a esta planilla?", "AÑADIR REINTEGRO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    int NroTicket = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE"));
                    int idGastoxRutaC = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "idGastoXRutaC"));
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_AgregarDiferencialRuta(NroTicket, idGastoxRutaC, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBuscar_Click(sender, e);
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("La planilla seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gastoDeViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            int idOperacion = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion"));

            if (idOperacion == 1)
            {
                int idConductor = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "ViConductor"));
                decimal monto = Convert.ToDecimal(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IMPORTE_TOTAL"));
                int Planilla = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_PagarSinViaje(idConductor, Planilla, monto, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable dtAsistencia = new DataTable();
                    dtAsistencia = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas(Planilla.ToString());
                    string RAsistencia = Convert.ToString(dtAsistencia.Rows[0]["exito"]);
                    string NroRAsistencia = RAsistencia.Substring(0, 1);

                    if (NroRAsistencia != "0") { MessageBox.Show(RAsistencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    btnBuscar_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                int NroTicket = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE"));
                int idGastoxRutaC = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "idGastoXRutaC"));
                string Planilla2 = Convert.ToString(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ImprimirPago(NroTicket, idGastoxRutaC, Planilla2, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable dtAsistencia = new DataTable();
                    dtAsistencia = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas(Planilla2.ToString());
                    string RAsistencia = Convert.ToString(dtAsistencia.Rows[0]["exito"]);
                    string NroRAsistencia = RAsistencia.Substring(0, 1);

                    if (NroRAsistencia != "0") { MessageBox.Show(RAsistencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    btnBuscar_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void viaticoSinViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            int idConductor = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "ViConductor"));
            decimal monto = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IMPORTE_TOTAL"));
            int Planilla = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_PagarSinViaje(idConductor, Planilla, monto, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBuscar_Click(sender, e);
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void viaticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                int idOperacion = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion"));

                if (MessageBox.Show("¿Quiere añadir este viático a la planilla?", "AÑADIR VIÁTICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (idOperacion == 1)
                    {
                        int idConductor = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "ViConductor"));
                        int Planilla = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA"));
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_PagarViaticosTolvas(idConductor, Planilla, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DataTable dtAsistencia = new DataTable();
                            dtAsistencia = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas(Planilla.ToString());
                            string RAsistencia = Convert.ToString(dtAsistencia.Rows[0]["exito"]);
                            string NroRAsistencia = RAsistencia.Substring(0, 1);

                            if (NroRAsistencia != "0") { MessageBox.Show(RAsistencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                            btnBuscar_Click(sender, e);
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        int NroTicket = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE"));
                        int Planilla2 = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA")); 
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_PagarViaticos(NroTicket, Usuario, Planilla2.ToString());
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DataTable dtAsistencia = new DataTable();
                            dtAsistencia = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas(Planilla2.ToString());
                            string RAsistencia = Convert.ToString(dtAsistencia.Rows[0]["exito"]);
                            string NroRAsistencia = RAsistencia.Substring(0, 1);

                            if (NroRAsistencia != "0") { MessageBox.Show(RAsistencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                            btnBuscar_Click(sender, e);
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            catch { MessageBox.Show("La planilla seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void reimprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idOperacion = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion"));

                if (idOperacion == 1)
                {
                    string CodGasto = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();

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
                            ticket.AddSubHeaderLine("ESTADO CAJA: " + dtListaTicket.Rows[0]["CAJA"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("F.Emisión: " + dtListaTicket.Rows[0]["FECHA_EMISION"].ToString());
                            ticket.AddSubHeaderLine("F.Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.AddSubHeaderLine("FIRMA: ");
                            ticket.HeaderImage = Resources.TABLA;
                            ticket.PrintTicket(NombreImpresora);

                            MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    int NroTicket = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE"));
                    string Planilla3 = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ImprimirDiferencial(NroTicket, Planilla3);
                    string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    btnBuscar_Click(sender, e);

                    string PlanillaEvento = Convert.ToString(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "OBSERVACION"));

                    if (PlanillaEvento == "PLANILLA EVENTO")
                    {
                        frmGenerarTicketGasto frmGenerarTicketGasto = new frmGenerarTicketGasto();
                        frmGenerarTicketGasto.Imprimir(Planilla3);
                    }
                    else
                    {
                        DataTable dtConsultarImpresora = new DataTable();
                        dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                        DataTable dtListaTicket = new DataTable();
                        dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(Planilla3);

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

                                if (dtListaTicket.Rows[0]["OBSERVACION"].ToString() == "PLANILLA EVENTO")
                                { ticket.AddSubHeaderLine2("PLE - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }
                                
                                if (dtListaTicket.Rows[0]["OBSERVACION"].ToString() == "PLANILLA RECONOCIMIENTO")
                                {
                                    if (idOperacion == 2)
                                    { ticket.AddSubHeaderLine2("PLR - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }

                                    if (idOperacion == 3 || idOperacion == 10)
                                    { ticket.AddSubHeaderLine2("PLA - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }
                                }
                                else
                                { ticket.AddSubHeaderLine2("PL - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }

                                ticket.AddSubHeaderLine("Chofer: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                                ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                                ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                                ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                                ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                                ticket.AddSubHeaderLine("                              ");
                                ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                                ticket.AddSubHeaderLine("ESTADO CAJA: " + dtListaTicket.Rows[0]["CAJA"].ToString());
                                ticket.AddSubHeaderLine("                              ");
                                ticket.AddSubHeaderLine("F.Emisión: " + dtListaTicket.Rows[0]["FECHA_EMISION"].ToString());
                                ticket.AddSubHeaderLine("F.Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                ticket.AddSubHeaderLine("FIRMA: ");
                                ticket.HeaderImage = Resources.TABLA;
                                ticket.PrintTicket(NombreImpresora);
                            }

                            MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void liquidarPlanillaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string Caja = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CAJA").ToString();
                string Adicional = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "ADICIONAL").ToString();
                string CambioRuta = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CAMBIO_RUTA").ToString();

                if (Caja == "PAGADO" && Adicional == "0.00" && CambioRuta == "0.00")
                {
                    int NroTicket, idOperacion, idConductor, Permiso, idRuta, idGastoXRutaC;
                    string Planilla, Conductor, Ruta, Placa, GastoTotal;

                    idOperacion = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion").ToString() == "" ? 0 : Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion"));
                    idConductor = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdConductor"));
                    Conductor = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CONDUCTOR").ToString();
                    Ruta = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "RUTA").ToString();
                    Placa = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TRACTO").ToString();
                    Permiso = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "Permiso"));
                    idRuta = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdRuta").ToString() == "" ? 0 : Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdRuta"));
                    Planilla = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
                    idGastoXRutaC = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "idGastoXRutaC"));

                    if (idOperacion == 1)
                    { NroTicket = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString()) * -1; }
                    else
                    { NroTicket = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE")); }

                    decimal Gasto = Convert.ToDecimal(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IMPORTE_TOTAL")) +
                                        Convert.ToDecimal(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "ADICIONAL")) +
                                        Convert.ToDecimal(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CAMBIO_RUTA"));

                    GastoTotal = Gasto.ToString();

                    frmLiquidarGastosViaje frmLiquidarGastosViaje = new frmLiquidarGastosViaje();
                    frmLiquidarGastosViaje.EnviarPlanilla(NroTicket, idConductor, Planilla, Conductor, Ruta, Placa, GastoTotal, Permiso, idRuta, idOperacion, idGastoXRutaC);
                    frmLiquidarGastosViaje.Fecha = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "FECHA").ToString();
                    frmLiquidarGastosViaje.frmListarPlanillas(this);
                    frmLiquidarGastosViaje.Show();
                }
                else
                {
                    if (Caja != "PAGADO")
                    { MessageBox.Show("El adelanto de esta planilla aún no ha sido pagado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else
                    {
                        if (Adicional != "0.00" && CambioRuta != "0.00")
                        { MessageBox.Show("Esta planilla tiene gastos adicionales por pagar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            catch { MessageBox.Show("La planilla seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void buscarReporteGastoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           try
           {
               string Planilla = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
               string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

               DataTable dtRespuesta = new DataTable();
               dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarReporteGasto(Planilla, Usuario);
               string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
               string NroRPTA = Respuesta.Substring(0, 1);
               if (NroRPTA == "0")
               {
                   MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   btnBuscar_Click(sender, e);
               }
               else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
           }
           catch { MessageBox.Show("La planilla seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void verAlimentacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Planilla = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
                int idConductor = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdConductor"));
                int NroTicket = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE"));
                int idOperacion = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion"));
                int IdRuta = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdRuta"));

                frmGastosAdicionalesTolvas frmGastosAdicionalesTolvas = new TicketsGasto.frmGastosAdicionalesTolvas();
                frmGastosAdicionalesTolvas.idConductor = idConductor;
                frmGastosAdicionalesTolvas.lblConductor.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CONDUCTOR").ToString();
                frmGastosAdicionalesTolvas.txtPlanilla2.Text = Planilla;
                frmGastosAdicionalesTolvas.NroProgramacion = NroTicket;
                frmGastosAdicionalesTolvas.idOperacion = idOperacion;
                frmGastosAdicionalesTolvas.idRuta = IdRuta;
                frmGastosAdicionalesTolvas.frmListarPlanillas = this;
                frmGastosAdicionalesTolvas.frmLPT = 2;
                frmGastosAdicionalesTolvas.ShowDialog(this);
            }
            catch { MessageBox.Show("La planilla seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void agregarCambioDeRutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Planilla = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
            int IdRuta = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdRuta"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            int idOperacion = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion"));

            DataTable dtGastoRuta = new DataTable();

            dtGastoRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(6, -1, Convert.ToInt32(Planilla), -1);

            if (dtGastoRuta.Rows.Count > 0)
            {
                if (idOperacion == 1)
                {
                    TicketsGasto.frmViaticoCambioRuta frmViaticoCambioRuta = new TicketsGasto.frmViaticoCambioRuta();
                    frmViaticoCambioRuta.frmLPT = 2;
                    frmViaticoCambioRuta.frmListarPlanillas = this;
                    frmViaticoCambioRuta.lblPlanilla.Text = Planilla;
                    frmViaticoCambioRuta.txtRutaOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["RUTA"]);
                    frmViaticoCambioRuta.txtGastoOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["GASTO"]);
                    frmViaticoCambioRuta.CargarComboRuta();
                    frmViaticoCambioRuta.idRutaNueva = IdRuta;
                    frmViaticoCambioRuta.cbxRuta.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "RUTA").ToString();
                    frmViaticoCambioRuta.idProg = idOperacion;
                    frmViaticoCambioRuta.cbxRuta.Visible = true;
                    frmViaticoCambioRuta.cbxRuta.BringToFront();
                    frmViaticoCambioRuta.ShowDialog(this);
                }
                else
                {
                    TicketsGasto.frmViaticoCambioRuta frmViaticoCambioRuta = new TicketsGasto.frmViaticoCambioRuta();
                    frmViaticoCambioRuta.frmLPT = 2;
                    frmViaticoCambioRuta.frmListarPlanillas = this;
                    frmViaticoCambioRuta.lblPlanilla.Text = Planilla;
                    frmViaticoCambioRuta.txtRutaOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["RUTA"]);
                    frmViaticoCambioRuta.txtGastoOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["GASTO"]);
                    frmViaticoCambioRuta.idRutaNueva = IdRuta;
                    frmViaticoCambioRuta.txtNuevaRuta.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "RUTA").ToString();
                    frmViaticoCambioRuta.idProg = idOperacion;
                    frmViaticoCambioRuta.cbxRuta.Visible = false;
                    frmViaticoCambioRuta.cbxRuta.SendToBack();
                    frmViaticoCambioRuta.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("La ruta asignada no tiene un gasto definido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void autorizarReintegroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Planilla = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_AutorizarReintegro(Planilla, Usuario);
                string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    //MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("La planilla seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscarViatico_Click(object sender, EventArgs e)
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpViaticoInicio.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpViaticoFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpViaticoInicio.Value > dtpViaticoFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }

            dtgvListaViaticos.DataSource = null;
            dtgvListaViaticosView.Columns.Clear();
            
            System.Data.DataTable dt2 = new System.Data.DataTable();

            dt2.Clear();
            dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistroViaticos(OpcionViaticos, dtpViaticoInicio.Value.ToShortDateString(), dtpViaticoFin.Value.ToShortDateString(), txtNombreConductor.Text);
            if (dt2.Rows.Count > 0)
            {
                dtgvListaViaticos.DataSource = dt2;
                if (OpcionViaticos == 1) { dtgvListaViaticosView.Columns["idViatico"].Visible = false; }
                else
                {
                    dtgvListaViaticosView.Columns["idListaViatico"].Visible = false;
                    dtgvListaViaticosView.Columns["IdConductor"].Visible = false;
                }
                
                dtgvListaViaticosView.Columns["IMPORTE"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvListaViaticosView.Columns["IMPORTE"].DisplayFormat.FormatString = "n2";
                dtgvListaViaticosView.Columns["FECHA_VIATICO"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaViaticosView.Columns["FECHA_VIATICO"].DisplayFormat.FormatString = "dd/MM/yyyy";

                dtgvListaViaticosView.BestFitColumns();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dtgvListaViaticos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Viaticos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx"); 
                dtgvListaViaticos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void txtNombreConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarViatico_Click(sender, e); }
        }

        private void dtpViaticoInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarViatico_Click(sender, e); }
        }

        private void dtpViaticoFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarViatico_Click(sender, e); }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel4.SendToBack();

            cbxSucursal.Text = "TODAS";
            cbxSucursal_DropDownClosed(sender, e);
            cbDetalle.Checked = false;
            cbDetalle_CheckedChanged(sender, e);
            dtpReporteInicio.Value = new DateTime(dtpReporteInicio.Value.Year, dtpReporteInicio.Value.Month, 1);
            dtpReporteFin.Value = DateTime.Now;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel1.SendToBack();
            dtgvListaComprobantes.DataSource = null;
            //btnBuscar_Click(sender, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel3.SendToBack();
            txtNombreConductor.Clear();
            dtgvListaViaticos.DataSource = null;
            dtpViaticoInicio.Value = new DateTime(dtpViaticoInicio.Value.Year, dtpViaticoInicio.Value.Month, 1);
            dtpViaticoFin.Value = DateTime.Now;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                panel1.Left = panel1.Left + (e.X - xClick);
                panel1.Top = panel1.Top + (e.Y - yClick);
            }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                panel3.Left = panel3.Left + (e.X - xClick);
                panel3.Top = panel3.Top + (e.Y - yClick);
            }
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                panel4.Left = panel4.Left + (e.X - xClick);
                panel4.Top = panel4.Top + (e.Y - yClick);
            }
        }

        private void dtgPlanillasPendientes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string caja = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CAJA").ToString();
                if (caja == " ")
                {
                    gastoDeViajeToolStripMenuItem.Enabled = true;
                    //viaticoSinViajeToolStripMenuItem.Enabled = true;
                    viaticoToolStripMenuItem.Enabled = false;
                    cambioDeRutaToolStripMenuItem.Enabled = false;
                }
                else
                {
                    gastoDeViajeToolStripMenuItem.Enabled = false;
                    viaticoSinViajeToolStripMenuItem.Enabled = false;
                    viaticoToolStripMenuItem.Enabled = true;
                    cambioDeRutaToolStripMenuItem.Enabled = true;
                }

                string Operacion = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "OPERACION").ToString();
                
                if (Operacion == "LINDLEY" || Operacion == "LIMAGAS" || Operacion == "VOLCAN")
                {
                    tsReconocimiento.Enabled = true;

                    if (Operacion == "LINDLEY") { tsReconocimiento.Text = "Generar Reconocimiento"; }
                    else { tsReconocimiento.Text = "Generar Apoyo"; }
                }
                else
                {
                    tsReconocimiento.Text = "Generar Apoyo";
                    tsReconocimiento.Enabled = false;
                }
            }
            catch
            {
                gastoDeViajeToolStripMenuItem.Enabled = false;
                viaticoSinViajeToolStripMenuItem.Enabled = false;
                viaticoToolStripMenuItem.Enabled = false;
                cambioDeRutaToolStripMenuItem.Enabled = false;
                tsReconocimiento.Enabled = false;
            }
        }

        private void dtgPlanillasLiquidadas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vuelto = dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "VUELTO").ToString();
                string EstadoV = dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "ESTADO_V").ToString();

                if (Vuelto == "") { tsRegistrarVuelto.Enabled = false; }
                else
                {
                    if (Vuelto != "0.00" && EstadoV == "PEN") { tsRegistrarVuelto.Enabled = true; }
                    else { tsRegistrarVuelto.Enabled = false; }
                }
            }
            catch { tsRegistrarVuelto.Enabled = false; }
        }

        private void dtgvListaViaticos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (OpcionViaticos == 2)
                {
                    idListaViatico = Convert.ToInt32(dtgvListaViaticosView.GetRowCellValue(dtgvListaViaticosView.FocusedRowHandle, "idListaViatico"));
                    idConductorViatico = Convert.ToInt32(dtgvListaViaticosView.GetRowCellValue(dtgvListaViaticosView.FocusedRowHandle, "IdConductor"));

                    if (idListaViatico != 0)
                    {
                        PlanillaViatico = Convert.ToString(dtgvListaViaticosView.GetRowCellValue(dtgvListaViaticosView.FocusedRowHandle, "PLANILLA"));
                        dtpViaticoAdicional.Value = Convert.ToDateTime(dtgvListaViaticosView.GetRowCellValue(dtgvListaViaticosView.FocusedRowHandle, "FECHA_VIATICO"));
                        txtConductorV.Text = Convert.ToString(dtgvListaViaticosView.GetRowCellValue(dtgvListaViaticosView.FocusedRowHandle, "CONDUCTOR"));
                        txtComprobante.Text = Convert.ToString(dtgvListaViaticosView.GetRowCellValue(dtgvListaViaticosView.FocusedRowHandle, "COMPROBANTE"));

                        pModificarViatico.Visible = true;
                        pModificarViatico.BringToFront();
                    }
                    else { MessageBox.Show("El viático seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }

            }
            catch { MessageBox.Show("No se ha asignado ningún presupuesto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pModificarViatico.Visible = false;
            pModificarViatico.SendToBack();
        }

        private void pModificarViatico_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pModificarViatico.Left = pModificarViatico.Left + (e.X - xClick);
                pModificarViatico.Top = pModificarViatico.Top + (e.Y - yClick);
            }
        }

        private void btnGuardarAdicional_Click(object sender, EventArgs e)
        {
            if (txtComprobante.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el comprobante.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpViaticoAdicional.Focus();
                return;
            }
            else
            {
                DataTable dtFechaViatico = new DataTable();
                string respta;

                dtFechaViatico = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ModificarFechaViatico(idListaViatico, idConductorViatico, PlanillaViatico, txtComprobante.Text, dtpViaticoAdicional.Value);
                respta = Convert.ToString(dtFechaViatico.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pictureBox4_Click(sender, e);
                    btnBuscarViatico_Click(sender, e);
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void cbxSucursal_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxSucursal.Text == "TODAS") { Sucursal = "B"; }
            if (cbxSucursal.Text == "TRUJILLO") { Sucursal = "BTRU"; }
            if (cbxSucursal.Text == "LIMA") { Sucursal = "BLIM"; }
            if (cbxSucursal.Text == "PIURA") { Sucursal = "PAIT"; }
            if (cbxSucursal.Text == "AREQUIPA") { Sucursal = "BARE"; }
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt5 = new System.Data.DataTable();
            dt5 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarReportePlanillas(Sucursal, dtpReporteInicio.Value.ToShortDateString(), dtpReporteFin.Value.ToShortDateString(), Detalle);

            if (dt5.Rows.Count > 0)
            {
                if (Detalle == 0)
                {
                    dtgReporte.DataSource = null;
                    dtgvReporteVista.Columns.Clear();
                    dtgReporte.DataSource = dt5;
                    dtgvReporteVista.BestFitColumns();

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Listado de Reporte de Gastos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgReporte.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                
                if (Detalle == 1)
                {
                    dtgReporte2.DataSource = null;
                    dtgvReporte2Vista.Columns.Clear();
                    dtgReporte2.DataSource = dt5;
                    dtgvReporte2Vista.BestFitColumns();

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Listado de Reporte de Gastos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgReporte2.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }

            pictureBox1_Click(sender, e);
        }

        private void btnReporteReintegros_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt6 = new System.Data.DataTable();
            dt6 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarReporteReintegros(Sucursal, dtpReporteInicio.Value.ToShortDateString(), dtpReporteFin.Value.ToShortDateString());

            dtgReintegros.DataSource = null;
            dtgvReintegrosVista.Columns.Clear();
            dtgReintegros.DataSource = dt6;
            dtgvReintegrosVista.BestFitColumns();

            System.Data.DataTable dt7 = new System.Data.DataTable();
            dt7 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoXConcepto(Sucursal, dtpReporteInicio.Value.ToShortDateString(), dtpReporteFin.Value.ToShortDateString());
            
            dtgReintegros2.DataSource = null;
            dtgvReintegros2Vista.Columns.Clear();
            dtgReintegros2.DataSource = dt7;
            dtgvReintegros2Vista.BestFitColumns();

            dtgReintegros.ForceInitialize();
            dtgReintegros2.ForceInitialize();
            compositeLink1.CreatePageForEachLink();

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            dtfi.TimeSeparator = ".";
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
            options.ExportMode = XlsxExportMode.SingleFilePageByPage;
            string nombre = System.IO.Path.Combine(desktop, "Listado de Reporte de Reintegros - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
            compositeLink1.ExportToXlsx(nombre, options);
            Process.Start(nombre);
        }

        private void cbDetalle_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDetalle.Checked == true) { Detalle = 1; }

            if (cbDetalle.Checked == false) { Detalle = 0; }
        }

        private void tsRegistrarVuelto_Click(object sender, EventArgs e)
        {
            try
            {
                string Planilla = dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "PLANILLA").ToString();
                int RG = Convert.ToInt32(dgvPlanillasLiquidadasVista.GetRowCellValue(dgvPlanillasLiquidadasVista.FocusedRowHandle, "RG"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (MessageBox.Show("¿Desea registrar el vuelto de esta planilla?", "REGISTRAR VUELTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarVuelto(Planilla, RG, Usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);

                    if (NroRspta == "0")
                    {
                        ImprimirRecibo();
                        ImprimirRecibo();
                        MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBuscar_Click(sender, e);
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al registrar el vuelto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsReconocimiento_Click(object sender, EventArgs e)
        {
            int idProg2 = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "TipoProgramacion"));

            if (idProg2 == 3 || idProg2 == 10)
            {
                frmGenerarPlanillaRE frmGenerarPlanillaRE = new TicketsGasto.frmGenerarPlanillaRE();
                frmGenerarPlanillaRE.idOperacion = idProg2;
                frmGenerarPlanillaRE.lblTitulo.Text = "GENERAR PLANILLA DE APOYO";
                frmGenerarPlanillaRE.txtPreviaje.Text = Convert.ToString(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE"));
                frmGenerarPlanillaRE.txtPlanilla.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
                frmGenerarPlanillaRE.idConductorP = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdConductor"));
                frmGenerarPlanillaRE.txtConductorP.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CONDUCTOR").ToString();
                frmGenerarPlanillaRE.idRuta = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdRuta"));
                frmGenerarPlanillaRE.txtRuta.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "RUTA").ToString();
                frmGenerarPlanillaRE.cbRedondear.Checked = false;
                frmGenerarPlanillaRE.cbRedondear_CheckedChanged(sender, e);
                frmGenerarPlanillaRE.label5.Text = "Gasto Ruta Asignado: ";
                frmGenerarPlanillaRE.ShowDialog(this);
            }
            else
            {
                frmGenerarPlanillaRE frmGenerarPlanillaRE = new TicketsGasto.frmGenerarPlanillaRE();
                frmGenerarPlanillaRE.idOperacion = idProg2;
                frmGenerarPlanillaRE.lblTitulo.Text = "GENERAR PLANILLA DE RECONOCIMIENTO";
                frmGenerarPlanillaRE.txtPreviaje.Text = Convert.ToString(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PREVIAJE"));
                frmGenerarPlanillaRE.txtPlanilla.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "PLANILLA").ToString();
                frmGenerarPlanillaRE.idConductorP = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdConductor"));
                frmGenerarPlanillaRE.txtConductorP.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "CONDUCTOR").ToString();
                frmGenerarPlanillaRE.idRuta = Convert.ToInt32(dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "IdRuta"));
                frmGenerarPlanillaRE.txtRuta.Text = dgvPlanillasPendientesVista.GetRowCellValue(dgvPlanillasPendientesVista.FocusedRowHandle, "RUTA").ToString();
                frmGenerarPlanillaRE.cbRedondear.Enabled = false;
                frmGenerarPlanillaRE.label5.Text = "Gasto Reconocimiento: ";
                frmGenerarPlanillaRE.frmListarPlanillas = this;
                frmGenerarPlanillaRE.ShowDialog(this);
            }
        }

        private void btnExcelAd_Click(object sender, EventArgs e)
        {
            if (dtgvListaComprobantes.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Gastos Liquidados de Planilla " + txtPlanillaGA.Text + " - " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgvListaComprobantes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
