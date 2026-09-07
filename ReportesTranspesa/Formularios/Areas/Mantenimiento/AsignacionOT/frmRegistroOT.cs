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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    public partial class frmRegistroOT : Form
    {
        public int Persona, Opcion;
        public string Nombre;
        public DataTable dtListaMecanicos = new DataTable();
        public DataTable dtListaAuxilios = new DataTable();
        public DataTable dtPermisos = new DataTable();
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int xClick3 = 0, yClick3 = 0;
        public int xClick4 = 0, yClick4 = 0;

        public frmRegistroOT()
        {
            InitializeComponent();
            cbxCompania.SelectedIndexChanged -= cbxCompania_SelectedIndexChanged;
        }

        private void cbxCompania_SelectedIndexChanged(object sender, EventArgs e) { CargarComboCompania(); }

        private void frmRegistroOT_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroOT");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        groupBox18.Enabled = true;
                        btnNuevaTarea.Enabled = true;
                        btnHistorial.Enabled = true;
                    }
                    else
                    {
                        groupBox18.Enabled = false;
                        btnNuevaTarea.Enabled = false;
                        btnHistorial.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { cambiarTurnoToolStripMenuItem.Enabled = true; }
                    else { cambiarTurnoToolStripMenuItem.Enabled = false; }
                    
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarMecanicoToolStripMenuItem.Enabled = true; }
                    else { eliminarMecanicoToolStripMenuItem.Enabled = false; }
                }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            CargarComboCompania();
            ListarOTProgramadas();
            ListarMecanicos();
        }


        public void CargarComboCompania()
        {
            DataTable dtCompania = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(4, "");
            cbxCompania.DataSource = dtCompania;
            cbxCompania.DisplayMember = "DescripcionCorta";
            cbxCompania.ValueMember = "CompaniaCodigo";
        }

        public void ListarOTProgramadas()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtgListaOT.DataSource = null;
                dgvListaOTVista.Columns.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarOTProgramadas(txtPlaca.Text, txtDescripcion.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                if (dt.Rows.Count > 0)
                {
                    dtgListaOT.DataSource = dt;
                    dgvListaOTVista.BestFitColumns();
                }
            }
        }

        public void ListarMecanicos()
        {
            dtListaMecanicos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(1, "");
            dtgListaMecanicos.DataSource = dtListaMecanicos;
            if (dtListaMecanicos.Rows.Count > 0)
            {
                dgvListaMecanicosVista.Columns["Persona"].Visible = false;

                dgvListaMecanicosVista.BestFitColumns();
            }
        }

        public void ListarAuxilios()
        {
            dtgAuxilios.DataSource = null;
            dgvAuxiliosVista.Columns.Clear();
            dtListaAuxilios = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarAuxilios(txtPlacaA.Text, txtDescripcionA.Text);
            dtgAuxilios.DataSource = dtListaAuxilios;
            
            if (dtListaAuxilios.Rows.Count > 0)
            {
                dgvAuxiliosVista.Columns["NRO"].Visible = false;

                dgvAuxiliosVista.BestFitColumns();
            }
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarOTProgramadas(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarOTProgramadas(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarOTProgramadas(); }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarOTProgramadas(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarOTProgramadas(); }

        private void dtgListaOT_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmMecanicosAsignados formulario = new frmMecanicosAsignados();

                formulario.lblDescripcion.Text = Convert.ToString(dgvListaOTVista.GetRowCellValue(dgvListaOTVista.FocusedRowHandle, "DESCRIPCION"));
                formulario.lblPlaca.Text = Convert.ToString(dgvListaOTVista.GetRowCellValue(dgvListaOTVista.FocusedRowHandle, "PLACA"));
                formulario.lblCodigoOT.Text = Convert.ToString(dgvListaOTVista.GetRowCellValue(dgvListaOTVista.FocusedRowHandle, "OT"));
                formulario.ListarAsignaciones(Convert.ToString(dgvListaOTVista.GetRowCellValue(dgvListaOTVista.FocusedRowHandle, "OT")));

                formulario.ShowDialog();
            }
            catch
            { MessageBox.Show("La OT seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnListaMecanicos_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            cbxTurno.Text = "MAÑANA";
            pListaMecanicos.Visible = true;
            pListaMecanicos.BringToFront();
            btnListaMecanicos.Enabled = false;
            ListarMecanicos();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            frmHistorialOT formulario = new frmHistorialOT();
            formulario.ShowDialog();
        }

        private void btnNuevaTarea_Click(object sender, EventArgs e)
        {
            frmNuevoTrabajo frmNuevoTrabajo = new frmNuevoTrabajo();
            frmNuevoTrabajo.formulario = this;
            frmNuevoTrabajo.dtpFechaProgramada.Value = DateTime.Now;
            frmNuevoTrabajo.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pListaMecanicos.Visible = false;
            pListaMecanicos.SendToBack();
            Persona = -1;
            Nombre = "";
            txtNuevoCodigo.Clear();
            txtNombre.Clear();
            btnListaMecanicos.Enabled = true;
        }

        private void pListaMecanicos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pListaMecanicos.Left = pListaMecanicos.Left + (e.X - xClick);
                pListaMecanicos.Top = pListaMecanicos.Top + (e.Y - yClick);
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e) { txtNombre.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(1, txtNombre.Text), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 500;
            lstPersonal.Columns[2].Width = 0;
            lstPersonal.Columns[3].Width = 0;
            lstPersonal.Columns[4].Width = 0;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                Nombre = "";
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void txtNombre_Leave(object sender, EventArgs e) { txtNombre.BackColor = Color.White; }

        private void lstPersonal_Enter(object sender, EventArgs e)
        {
            if (!lstPersonal.Items.Count.Equals(0)) { lstPersonal.Items[0].Selected = true; }
        }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal.SelectedItems[0];

                Persona = Int32.Parse(ItemActual.Text);
                txtNombre.Text = ItemActual.SubItems[1].Text;
                cbxCompania.Text = ItemActual.SubItems[4].Text;

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                btnRegistrar.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                Nombre = "";
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void lstPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal.SelectedItems[0];

            Persona = Int32.Parse(ItemActual.Text);
            txtNombre.Text = ItemActual.SubItems[1].Text;
            cbxCompania.Text = ItemActual.SubItems[4].Text;

            lstPersonal.Visible = false;
            lstPersonal.SendToBack();
            btnRegistrar.Focus();
        }

        private void txtNuevoCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtNombre.Focus(); }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNuevoCodigo.Text.Length == 0 || txtNombre.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtNuevoCodigo.Text.Length == 0) { txtNuevoCodigo.Focus(); }
                else { txtNombre.Focus(); }
                return;
            }
            else
            {
                DataTable dtRegistroOT = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRegistroOT = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_InsertarMecanico(Opcion, Persona, txtNuevoCodigo.Text, txtNombre.Text, cbxTurno.Text, cbxCompania.Text);
                respta = Convert.ToString(dtRegistroOT.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Persona = -1;
                    Nombre = "";
                    txtNuevoCodigo.Clear();
                    txtNombre.Clear();
                    ListarMecanicos();
                    Opcion = 1;
                    cbxTurno.Text = "MAÑANA";
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void eliminarMecanicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este mecánico?", "ELIMINAR MECÁNICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Persona = Convert.ToInt32(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "Persona"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_InsertarMecanico(2, Persona, "", "","","");
                    ListarMecanicos();
                }
            }
            catch { MessageBox.Show("El mecánico seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvListaMecanicosVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "DISPONIBLE") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "OCUPADO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgListaMecanicos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idPersona = Convert.ToString(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "Persona"));
                string Estado = Convert.ToString(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "ESTADO"));

                if (idPersona != "")
                {
                    if (Estado == "OCUPADO") { liberarMecanicoToolStripMenuItem.Enabled = true; }
                    else { liberarMecanicoToolStripMenuItem.Enabled = false; }

                    tsAsignarAuxilio.Enabled = true;
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarMecanicoToolStripMenuItem.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { cambiarTurnoToolStripMenuItem.Enabled = true; }
                }
                else
                {
                    liberarMecanicoToolStripMenuItem.Enabled = false;
                    eliminarMecanicoToolStripMenuItem.Enabled = false;
                    cambiarTurnoToolStripMenuItem.Enabled = false;
                    tsAsignarAuxilio.Enabled = false;
                }
            }
            catch
            {
                liberarMecanicoToolStripMenuItem.Enabled = false;
                eliminarMecanicoToolStripMenuItem.Enabled = false;
                cambiarTurnoToolStripMenuItem.Enabled = false;
                tsAsignarAuxilio.Enabled = false;
            }
        }

        private void liberarMecanicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea dar por finalizado el trabajo del mecánico?", "TERMINAR OT", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Persona = Convert.ToInt32(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "Persona"));
                    string NumeroOrden = Convert.ToString(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "OT"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico(2, Persona, NumeroOrden);
                    ListarMecanicos();
                }
            }
            catch { MessageBox.Show("El mecánico seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cambiarTurnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Opcion = 3;
            Persona = Convert.ToInt32(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "Persona"));
            txtNuevoCodigo.Text = Convert.ToString(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "CODIGO"));
            txtNombre.Text = Convert.ToString(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "NOMBRE"));
        }

        private void tsAsignarAuxilio_Click(object sender, EventArgs e)
        {
            Persona = Convert.ToInt32(dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "Persona"));
            Nombre = dgvListaMecanicosVista.GetRowCellValue(dgvListaMecanicosVista.FocusedRowHandle, "NOMBRE").ToString();

            pListaAuxilios.Location = new System.Drawing.Point(159, 231);
            pListaAuxilios.Visible = true;
            pListaAuxilios.BringToFront();

            ListarAuxilios();
        }

        private void pListaAuxilios_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick4 = e.X; yClick4 = e.Y; }
            else
            {
                pListaAuxilios.Left = pListaAuxilios.Left + (e.X - xClick4);
                pListaAuxilios.Top = pListaAuxilios.Top + (e.Y - yClick4);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pListaAuxilios.Visible = false;
            pListaAuxilios.SendToBack();

            dtgAuxilios.DataSource = null;
            dgvAuxiliosVista.Columns.Clear();
            txtPlacaA.Clear();
            txtDescripcionA.Clear();
        }

        private void txtPlacaA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAuxilios(); }
        }

        private void txtDescripcionA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAuxilios(); }
        }

        private void dtgAuxilios_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea asignar este auxilio al mecánico?", "ASIGNAR AUXILIO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtAsignacion = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int idFalla = Convert.ToInt32(dgvAuxiliosVista.GetRowCellValue(dgvAuxiliosVista.FocusedRowHandle, "NRO"));
                string Placa = dgvAuxiliosVista.GetRowCellValue(dgvAuxiliosVista.FocusedRowHandle, "TRACTO").ToString() + " | " +
                               dgvAuxiliosVista.GetRowCellValue(dgvAuxiliosVista.FocusedRowHandle, "CARRETA").ToString();
                string Lugar = dgvAuxiliosVista.GetRowCellValue(dgvAuxiliosVista.FocusedRowHandle, "UBICACIÓN").ToString();

                dtAsignacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarAuxilio(1, Persona, idFalla, Usuario);
                respta = Convert.ToString(dtAsignacion.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        DataTable dtConsultarImpresora = new DataTable();
                        dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
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
                            ticket.AddSubHeaderLine2("ASIGNACIÓN DE");
                            ticket.AddSubHeaderLine2("AUXILIOS MECÁNICOS");
                            ticket.AddSubHeaderLine2("                         ");
                            ticket.AddSubHeaderLine("Placa: " + Placa);
                            ticket.AddSubHeaderLine("Lugar: " + Lugar);
                            ticket.AddSubHeaderLine("Técnico: " + Nombre);
                            ticket.AddSubHeaderLine("                         ");
                            ticket.AddSubHeaderLine("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                            ticket.PrintTicket(NombreImpresora);
                        }
                    }
                    catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    btnCerrar2_Click(sender, e);
                    ListarMecanicos();
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea desvincular este auxilio al mecánico?", "DESVINCULAR AUXILIO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtAsignacion = new DataTable();
                string respta;
                dtAsignacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarAuxilio(2, Persona, 0, "");
                respta = Convert.ToString(dtAsignacion.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnCerrar2_Click(sender, e);
                    ListarMecanicos();
                }
            }
        }
    }
}
