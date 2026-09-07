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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmComponentesDefectuosos : Form
    {
        public string EstadoSolicitud;
        public int idComponente;
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int idSolicitud = 0, idSolicitudDetalle = 0;

        public frmComponentesDefectuosos()
        {
            InitializeComponent();
            cbxComponente.SelectedIndexChanged -= cbxComponente_SelectedIndexChanged;
            cbxDetalle.SelectedIndexChanged -= cbxDetalle_SelectedIndexChanged;
            cbxPosicionLlanta.SelectedIndexChanged -= cbxPosicionLlanta_SelectedIndexChanged;
        }

        private void cbxComponente_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboComponente(); }

        private void cbxDetalle_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboDetalle(); }

        private void cbxPosicionLlanta_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboPosicion(); }

        private void frmComponentesDefectuosos_Load(object sender, EventArgs e)
        {
            idComponente = 1;
            CargarComboComponente();
            CargarComboDetalle();
            CargarComboPosicion();

            fechaInicio.Value = new DateTime(fechaInicio.Value.Year, fechaInicio.Value.Month, 1);
            fechaFin.Value = DateTime.Now;

            rbListaTodas.Checked = true;
            rbListaTodas_Click(sender, e);
        }


        private void CargarComboComponente()
        {
            DataTable dtComponente = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(1, 0);
            cbxComponente.DataSource = dtComponente;
            cbxComponente.DisplayMember = "Descripcion";
            cbxComponente.ValueMember = "idComponente";
        }

        private void CargarComboDetalle()
        {
            DataTable dtDetalle = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(2, idComponente);
            cbxDetalle.DataSource = dtDetalle;
            cbxDetalle.DisplayMember = "Descripcion";
            cbxDetalle.ValueMember = "idComponenteDetalle";
        }

        private void CargarComboPosicion()
        {
            DataTable dtPosicion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(3, 0);
            cbxPosicionLlanta.DataSource = dtPosicion;
            cbxPosicionLlanta.DisplayMember = "Descripcion";
            cbxPosicionLlanta.ValueMember = "idPosicionLlanta";
        }

        private void ListarComponentes()
        {
            DataTable dtListaComponentes = new DataTable();
            dtListaComponentes = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_ListarDefectuosos(txtPlaca.Text, fechaInicio.Text, fechaFin.Text, EstadoSolicitud);
            dtgListaComponentes.DataSource = dtListaComponentes;
            
            if (dtListaComponentes.Rows.Count > 0)
            {
                var result = dtListaComponentes
                .AsEnumerable()
                .Where(myRow => myRow.Field<String>("ESTADO") == "PENDIENTE");

                var resultTerminado = dtListaComponentes
                .AsEnumerable()
                .Where(myRow => myRow.Field<String>("ESTADO") == "TERMINADO");

                lblPendientes.Text = result.Count().ToString();
                lblTerminados.Text = resultTerminado.Count().ToString();

                Double porcentaje = Convert.ToDouble(lblTerminados.Text) / Convert.ToDouble(dtListaComponentes.Rows.Count);
                lblporcentaje.Text = Convert.ToDouble(porcentaje * 100).ToString("F2") + " %";

                dgvListaComponentesView.Columns["idSolicitud"].Visible = false;
                dgvListaComponentesView.Columns["idSolicitudDetalle"].Visible = false;
                dgvListaComponentesView.Columns["ESTADO_SOLICITUD"].Visible = false;

                dgvListaComponentesView.BestFitColumns();
            }
        }


        private void dgvListaComponentesView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (e.CellValue.ToString() == "TERMINADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarComponentes(); }
        }

        private void fechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarComponentes(); }
        }

        private void fechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarComponentes(); }
        }

        private void rbListaTodas_Click(object sender, EventArgs e)
        {
            EstadoSolicitud = " ";
            ListarComponentes();
        }

        private void rbListaPendientes_Click(object sender, EventArgs e)
        {
            EstadoSolicitud = "PENDIENTE";
            ListarComponentes();
        }

        private void rbListaSolucionadas_Click(object sender, EventArgs e)
        {
            EstadoSolicitud = "TERMINADO";
            ListarComponentes();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarComponentes(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaComponentes.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Componentes Defectuosos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaComponentes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgListaComponentes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string estado = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "ESTADO").ToString();

                if (estado == "PENDIENTE")
                {
                    asignarOTToolStripMenuItem.Enabled = true;
                    terminarToolStripMenuItem.Enabled = true;
                    modificarToolStripMenuItem.Enabled = true;
                }
                else
                {
                    asignarOTToolStripMenuItem.Enabled = false;
                    terminarToolStripMenuItem.Enabled = false;
                    modificarToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                asignarOTToolStripMenuItem.Enabled = false;
                terminarToolStripMenuItem.Enabled = false;
                modificarToolStripMenuItem.Enabled = false;
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pModificar.Visible = true;
            pModificar.BringToFront();

            idSolicitud = Convert.ToInt32(dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "idSolicitud"));
            idSolicitudDetalle = Convert.ToInt32(dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "idSolicitudDetalle"));

            txtPlacaUnidad.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "PLACA").ToString();
            txtTipo.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "FALLA").ToString();
            txtSubTipo.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "TIPO").ToString();
            txtOperacion.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "PROGRAMACION").ToString();
            cbxComponente.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "COMPONENTE").ToString();
            cbxComponente_DropDownClosed(sender, e);
            cbxDetalle.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "DETALLE").ToString();
            cbxPosicionLlanta.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "POSICION_LLANTA").ToString();
            txtObservacion.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "OBSERVACION").ToString();
        }
        
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtObservacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor, especifique el tipo de falla", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtObservacion.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Modificar(idSolicitud, idSolicitudDetalle, Convert.ToInt32(cbxComponente.SelectedValue), Convert.ToInt32(cbxDetalle.SelectedValue),
                                                                                                                           Convert.ToInt32(cbxPosicionLlanta.SelectedValue), txtObservacion.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarComponentes();
                    button2_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxComponente.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pModificar.Visible = false;
            pModificar.SendToBack();

            idSolicitud = 0;
            idSolicitudDetalle = 0;
        }

        private void asignarOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string UnidadFalla = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "FALLA").ToString();
            txtPlacaOT.Text = dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "PLACA").ToString();

            if (txtPlacaOT.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la placa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                btnBuscarOT_Click(sender, e);
                pListaOT.Visible = true;
                pListaOT.BringToFront();
            }
        }

        private void terminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idSolicitudDetalle = Convert.ToInt32(dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "idSolicitudDetalle"));
                int IDC = Convert.ToInt32(dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "idSolicitud"));
                string OT = Convert.ToString(dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "OT"));

                if (OT == "-")
                {
                    MessageBox.Show("Este componente no tiene una OT seleccionada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Editar(3, idSolicitudDetalle, IDC, "");
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarComponentes();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El dato seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pListaOT.Visible = false;
            pListaOT.SendToBack();
        }

        private void txtPlacaOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                DataTable dtListaOT = new DataTable();
                dtListaOT = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_FiltrarOT(txtPlacaOT.Text);
                dtgListaOT.DataSource = dtListaOT;
                if (dtListaOT.Rows.Count > 0) { dgvListaOTVista.BestFitColumns(); }
            }
        }

        private void btnBuscarOT_Click(object sender, EventArgs e)
        {
            DataTable dtListaOT = new DataTable();
            dtListaOT = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_FiltrarOT(txtPlacaOT.Text);
            dtgListaOT.DataSource = dtListaOT;
            if (dtListaOT.Rows.Count > 0) { dgvListaOTVista.BestFitColumns(); }
        }

        private void dtgListaOT_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string OT = Convert.ToString(dgvListaOTVista.GetRowCellValue(dgvListaOTVista.FocusedRowHandle, "Nro_OT"));
                int idSolicitudDetalle = Convert.ToInt32(dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "idSolicitudDetalle"));
                int IDC = Convert.ToInt32(dgvListaComponentesView.GetRowCellValue(dgvListaComponentesView.FocusedRowHandle, "idSolicitud"));
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Editar(2, idSolicitudDetalle, IDC, OT);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarComponentes();
                    pListaOT.Visible = false;
                    pListaOT.SendToBack();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("La OT seleccionada no es válida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void pListaOT_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pListaOT.Left = pListaOT.Left + (e.X - xClick);
                pListaOT.Top = pListaOT.Top + (e.Y - yClick);
            }
        }

        private void pModificar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pModificar.Left = pModificar.Left + (e.X - xClick2);
                pModificar.Top = pModificar.Top + (e.Y - yClick2);
            }
        }

        private void cbxComponente_DropDownClosed(object sender, EventArgs e)
        {
            idComponente = Convert.ToInt32(cbxComponente.SelectedValue);
            CargarComboDetalle();
            if (idComponente == 1)
            {
                label8.Visible = true;
                cbxPosicionLlanta.Visible = true;
                cbxPosicionLlanta.SelectedValue = 0;
            }
            else
            {
                label8.Visible = false;
                cbxPosicionLlanta.Visible = false;
                cbxPosicionLlanta.SelectedValue = 0;
            }
        }
    }
}
