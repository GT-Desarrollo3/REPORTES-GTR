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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmGenerarOrdenTrabajo : Form
    {
        public int idSolicitud, Persona = -1;
        public string CodTarea;
        public DataTable dtDetalle;
        public frmSolicitudMantenimiento formulario;

        public frmGenerarOrdenTrabajo()
        {
            InitializeComponent();
            cbxTipoMtto.SelectedIndexChanged -= cbxTipoMtto_SelectedIndexChanged;
            cbxClasificacion.SelectedIndexChanged -= cbxClasificacion_SelectedIndexChanged;
        }

        private void cbxTipoMtto_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTipo(); }

        private void cbxClasificacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboClasificacion(); }

        private void frmGenerarOrdenTrabajo_Load(object sender, EventArgs e)
        {
            CargarComboTipo();
            CargarComboClasificacion();
            ListarDetalle();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            cbxUbicacion.Text = "TRUJILLO";
        }


        private void CargarComboTipo()
        {
            DataTable dtTipo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarTiposMtto(1);
            cbxTipoMtto.DataSource = dtTipo;
            cbxTipoMtto.DisplayMember = "DescripcionLocal";
            cbxTipoMtto.ValueMember = "TipoMantenimiento";
        }

        private void CargarComboClasificacion()
        {
            DataTable dtClasificacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarTiposMtto(2);
            cbxClasificacion.DataSource = dtClasificacion;
            cbxClasificacion.DisplayMember = "DescripcionLocal";
            cbxClasificacion.ValueMember = "TipoMantenimientoGrupo";
        }

        private void ListarDetalle()
        {
            dtgvListaSolicitudes.DataSource = null;
            dtDetalle = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Listar(3, idSolicitud, 0);
            dtgvListaSolicitudes.DataSource = dtDetalle;
            if (dtDetalle.Rows.Count > 0)
            {
                dtgvListaSolicitudesView.Columns["idSolicitudDetalle"].Visible = false;
                dtgvListaSolicitudesView.Columns["idSolicitud"].Visible = false;

                dtgvListaSolicitudesView.Columns["INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaSolicitudesView.Columns["INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dtgvListaSolicitudesView.Columns["FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaSolicitudesView.Columns["FIN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dtgvListaSolicitudesView.BestFitColumns();
            }
        }


        private void txtEmpleado_Enter(object sender, EventArgs e) { txtEmpleado.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarEmpleados(txtEmpleado.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 500;
            lstEmpleado.Columns[2].Width = 110;
            lstEmpleado.BringToFront();
            lstEmpleado.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                Persona = -1;
            }
        }

        private void txtEmpleado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleado.Focus(); }
        }

        private void txtEmpleado_Leave(object sender, EventArgs e) { txtEmpleado.BackColor = Color.White; }

        private void lstEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lstEmpleado.Items.Count.Equals(0)) { lstEmpleado.Items[0].Selected = true; }
        }

        private void lstEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleado.SelectedItems[0];
                Persona = Int32.Parse(ItemActual.Text);
                txtEmpleado.Text = ItemActual.SubItems[1].Text;

                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtTarea.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtEmpleado.Focus();
                Persona = -1;
            }
        }

        private void lstEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstEmpleado.SelectedItems[0];
            Persona = Int32.Parse(ItemActual.Text);
            txtEmpleado.Text = ItemActual.SubItems[1].Text;

            lstEmpleado.Visible = false;
            lstEmpleado.SendToBack();
            txtTarea.Focus();
        }

        private void txtTarea_Enter(object sender, EventArgs e) { txtTarea.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTarea_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTarea, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_ListarActividades(lblPlaca.Text, txtTarea.Text), true, false, false);
            lstTarea.Columns[0].Width = 120;
            lstTarea.Columns[1].Width = 350;
            lstTarea.BringToFront();
            lstTarea.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                CodTarea = "";
                lstTarea.Visible = false;
                lstTarea.SendToBack();
            }
        }

        private void txtTarea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTarea.Focus(); }
        }

        private void txtTarea_Leave(object sender, EventArgs e) { txtTarea.BackColor = Color.White; }

        private void lstTarea_Enter(object sender, EventArgs e)
        {
            if (!lstTarea.Items.Count.Equals(0)) { lstTarea.Items[0].Selected = true; }
        }

        private void lstTarea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTarea.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTarea.SelectedItems[0];
                CodTarea = ItemActual.SubItems[0].Text;
                txtTarea.Text = ItemActual.SubItems[1].Text;

                lstTarea.Visible = false;
                lstTarea.SendToBack();
                dtpFechaInicio.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTarea.Visible = false;
                lstTarea.SendToBack();
            }
        }

        private void lstTarea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTarea.SelectedItems[0];
            CodTarea = ItemActual.SubItems[0].Text;
            txtTarea.Text = ItemActual.SubItems[1].Text;

            lstTarea.Visible = false;
            lstTarea.SendToBack();
            dtpFechaInicio.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dtpFechaInicio.Value >= dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicio debe ser menor que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            
            if (txtTarea.Text.Length == 0 || dtgvListaSolicitudesView.RowCount == 0)
            {
                if (dtgvListaSolicitudesView.RowCount == 0)
                { MessageBox.Show("La tabla no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else
                {
                    if (txtTarea.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, seleccione una tarea para la orden.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTarea.Focus();
                    }
                }

                return;
            }
            else
            {
                int idSolicitud = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitud"));
                int idSolicitudD = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitudDetalle"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarTarea(1, idSolicitudD, idSolicitud, CodTarea, dtpFechaInicio.Value, dtpFechaFin.Value);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodTarea = "";
                    txtTarea.Clear();
                    ListarDetalle();
                }
                else
                { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgvListaSolicitudes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "COD_TAREA").ToString();
                if (Codigo != "") { tsEliminar.Enabled = true; }
                else { tsEliminar.Enabled = false; }
            }
            catch { tsEliminar.Enabled = false; }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar la tarea de esta unidad?", "ELIMINAR TAREA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idSolicitud = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitud"));
                int idSolicitudD = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitudDetalle"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarTarea(2, idSolicitudD, idSolicitud, "", dtpFechaInicio.Value, dtpFechaFin.Value);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarDetalle(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtEmpleado.Text.Length == 0 || dtgvListaSolicitudesView.RowCount == 0)
            {
                if (dtgvListaSolicitudesView.RowCount == 0)
                { MessageBox.Show("La tabla no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else
                {
                    if (txtDescripcion.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese una descripción para la OT.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, asigne un empleado para la OT.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDescripcion.Focus();
                    }
                }

                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea generar una Orden de Trabajo?", "GENERAR OT", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idSolicitud = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitud"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_RegistrarOrdenTrabajo(idSolicitud, lblPlaca.Text, Convert.ToString(cbxTipoMtto.SelectedValue),
                                  Convert.ToString(cbxClasificacion.SelectedValue), cbxUbicacion.Text, txtDescripcion.Text, dtpFechaInicio.Value, dtpFechaFin.Value, Persona, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formulario.btnCerrar2_Click(sender, e);
                        formulario.ListarSolicitud(idSolicitud);
                        this.Close();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
