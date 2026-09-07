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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using Comun;
using Negocio;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;


namespace ReportesTranspesa.Formularios.Areas.Operaciones.LavadoUnidades
{
    public partial class frmListaTicketsLavadero : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaLavados = new DataTable();
        DataTable dtPermisos = new DataTable();
        public int xClick = 0, yClick = 0;
        
        public frmListaTicketsLavadero()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void frmListaTicketsLavadero_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaTicketsLavadero");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { gbFiltros.Enabled = true; }
                else { gbFiltros.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    reimprimirTicketToolStripMenuItem.Enabled = true;
                    actualizarRegistroToolStripMenuItem.Enabled = true;
                }
                else
                {
                    reimprimirTicketToolStripMenuItem.Enabled = false;
                    actualizarRegistroToolStripMenuItem.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarRegistroToolStripMenuItem.Enabled = true; }
                else { eliminarRegistroToolStripMenuItem.Enabled = false; }
            }
            
            dtpFechaProg.Value = DateTime.Now;
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;

            CargarComboOperacion();

            cbxEstado.Text = "TODOS";
            cbxOperacion.Text = "TODO";

            ListarTickets();
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(5,"");
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void ListarTickets()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaLavados = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_Listar(txtNroTicket.Text, txtBuscarPlaca.Text,
                                 dtpFechaIni.Text, dtpFechaFin.Text, cbxEstado.Text, cbxOperacion.Text);
                dtgLavadoUnidades.DataSource = dtListaLavados;
                if (dtListaLavados.Rows.Count > 0)
                {
                    dgvLavadoUnidadesVista.Columns["FECHA_EJECUCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvLavadoUnidadesVista.Columns["FECHA_EJECUCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvLavadoUnidadesVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvLavadoUnidadesVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvLavadoUnidadesVista.Columns["FechaModifica"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvLavadoUnidadesVista.Columns["FechaModifica"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvLavadoUnidadesVista.BestFitColumns();
                }
            }
        }

        public void Imprimir(string NroTicket)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_ListarTicket(NroTicket);

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
                        ticket.AddSubHeaderLine2("TICKET DE LAVADO");
                        ticket.AddSubHeaderLine2("DE UNIDAD");
                        ticket.AddSubHeaderLine2("TL - " + dtListaTicket.Rows[0]["CodLavado"].ToString() + "                         ");
                        ticket.AddSubHeaderLine("Unidad(es): " + dtListaTicket.Rows[0]["PLACA"].ToString());
                        ticket.AddSubHeaderLine("Lavado: " + dtListaTicket.Rows[0]["LAVADO"].ToString());
                        ticket.AddSubHeaderLine("Operación: " + dtListaTicket.Rows[0]["OPERACION"].ToString());
                        ticket.AddSubHeaderLine("F.Programada: " + dtListaTicket.Rows[0]["FECHA"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.PrintTicket(NombreImpresora);
                    }
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_BuscarMaquinas(txtPlaca.Text), true, false, false);
            lstPlaca.Columns[0].Width = 70;
            lstPlaca.Columns[1].Width = 130;
            lstPlaca.Columns[2].Width = 130;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtOperacion.Clear();
                txtTipoUnidad.Clear();
            }
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

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
                txtPlaca.Text = ItemActual.SubItems[0].Text;
                txtTipoUnidad.Text = ItemActual.SubItems[1].Text;
                txtOperacion.Text = ItemActual.SubItems[2].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                dtpFechaProg.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtOperacion.Clear();
                txtTipoUnidad.Clear();
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];
            txtPlaca.Text = ItemActual.SubItems[0].Text;
            txtTipoUnidad.Text = ItemActual.SubItems[1].Text;
            txtOperacion.Text = ItemActual.SubItems[2].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            dtpFechaProg.Focus();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 && txtOperacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor, seleccione una unidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPlaca.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket(txtPlaca.Text.TrimEnd(), dtpFechaProg.Value, txtOperacion.Text, txtTipoUnidad.Text, Usuario);      // GERARDO - 25/04
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    MessageBox.Show("Ticket de Lavado Generado. N° " + Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Imprimir(Respuesta);
                    txtPlaca.Clear();
                    txtTipoUnidad.Clear();
                    txtOperacion.Clear();
                    ListarTickets();
                }
                catch { MessageBox.Show("Error generando el ticket de lavado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtBuscarPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTickets(); }
        }

        private void txtNroTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTickets(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTickets(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTickets(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarTickets(); }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { ListarTickets(); }

        private void dgvLavadoUnidadesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "TERMINADO")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "ANULADO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarTickets(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgLavadoUnidades.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Lavado de Unidades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgLavadoUnidades.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgLavadoUnidades_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string CodLavado = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "CODIGO"));

                if (CodLavado != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        reimprimirTicketToolStripMenuItem.Enabled = true;
                        actualizarRegistroToolStripMenuItem.Enabled = true;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarRegistroToolStripMenuItem.Enabled = true; }
                }
                else
                {
                    reimprimirTicketToolStripMenuItem.Enabled = false;
                    actualizarRegistroToolStripMenuItem.Enabled = false;
                    eliminarRegistroToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                reimprimirTicketToolStripMenuItem.Enabled = false;
                actualizarRegistroToolStripMenuItem.Enabled = false;
                eliminarRegistroToolStripMenuItem.Enabled = false;
            }
        }

        private void reimprimirTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string CodLavado = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "CODIGO"));

                if (MessageBox.Show("¿Desea reimprimir este ticket?", "REIMPRIMIR TICKET", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { Imprimir(CodLavado); }
            }
            catch { MessageBox.Show("Se produjo un error al imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string CodLavado = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "CODIGO"));

                if (MessageBox.Show("¿Desea eliminar este ticket del registro?", "ELIMINAR TICKET", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_EliminarTicket(CodLavado);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarTickets(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void actualizarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblNroTicket.Text = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "CODIGO"));
            lblPlaca.Text = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "PLACA"));
            lblProgramacion.Text = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "OPERACION"));
            dtpNuevaFecha.Value = Convert.ToDateTime(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "FECHA_EJECUCION"));
            cbxNuevoEstado.Text = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "ESTADO"));

            pActualizar.Visible = true;
            pActualizar.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            lblNroTicket.Text = "";
            lblPlaca.Text = "";
            lblProgramacion.Text = "";
            dtpNuevaFecha.Value = DateTime.Now;

            pActualizar.Visible = false;
            pActualizar.SendToBack();
        }

        private void pActualizar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pActualizar.Left = pActualizar.Left + (e.X - xClick);
                pActualizar.Top = pActualizar.Top + (e.Y - yClick);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea actualizar este ticket de lavado?", "ACTUALIZAR TICKET", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtRespuesta = new DataTable();
                string respta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_ActualizarTicket(lblNroTicket.Text, dtpNuevaFecha.Value, cbxNuevoEstado.Text, Utilitario.Instancia.SesionUsuario.usuario);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarTickets();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvLavadoUnidadesVista_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                string CodLavado = Convert.ToString(dgvLavadoUnidadesVista.GetRowCellValue(dgvLavadoUnidadesVista.FocusedRowHandle, "CODIGO"));

                if (e.Column.FieldName == "VALIDACION")
                {
                    if (Convert.ToBoolean(dtListaLavados.Rows[e.RowHandle]["VALIDACION"]) == false) { dtListaLavados.Rows[e.RowHandle]["VALIDACION"] = true; }
                    else { dtListaLavados.Rows[e.RowHandle]["VALIDACION"] = false; }

                    DataTable dtRespuesta = new DataTable();
                    string respta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_MarcarValidacion(CodLavado, Convert.ToByte(dtListaLavados.Rows[e.RowHandle]["VALIDACION"]), Utilitario.Instancia.SesionUsuario.usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                }

                dtListaLavados.AcceptChanges();
                dgvLavadoUnidadesVista.UpdateCurrentRow();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
