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
    public partial class frmDesbloqueoPlanilla : Form
    {
        public int idConductor, e1 = 0;
        DataTable dtPermisos = new DataTable();

        public frmDesbloqueoPlanilla()
        {
            InitializeComponent();
        }

        private void frmDesbloqueoPlanilla_Load(object sender, EventArgs e)
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
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Desbloquear por Planillas")
                    {
                        btnGuardar.Enabled = true;
                        tsQuitarDesbloqueo.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                        tsQuitarDesbloqueo.Enabled = false;
                    }
                }
            }
            else
            {
                btnGuardar.Enabled = false;
                tsQuitarDesbloqueo.Enabled = false;
            }
            
            dtpFechaCompromiso.Value = DateTime.Now;
            ListarDesbloqueo();
        }


        public void ListarDesbloqueo()
        {
            DataTable dtListaDesbloqueo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConductoresDesbloqueados(txtConductor.Text, dtpFechaCompromiso.Text);
            dtgListaDesbloqueo.DataSource = dtListaDesbloqueo;
            if (dtListaDesbloqueo.Rows.Count > 0)
            {
                dtgvListaDesbloqueo.Columns["idDesbloqueo"].Visible = false;

                dtgvListaDesbloqueo.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaDesbloqueo.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dtgvListaDesbloqueo.BestFitColumns();
            }
        }


        private void txtConductor_Enter(object sender, EventArgs e) { txtConductor.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtConductor_Leave(object sender, EventArgs e) { txtConductor.BackColor = Color.White; }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstConductor, clsConsultaBL.Instancia.GetConductores(txtConductor.Text), true, false, false);
            lstConductor.Columns[0].Width = 0;
            lstConductor.Columns[1].Width = 250;
            lstConductor.Columns[2].Width = 100;
            lstConductor.Columns[3].Width = 0;

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
        {
            if (e.KeyCode == Keys.Down) { lstConductor.Focus(); }
        }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            if (!lstConductor.Items.Count.Equals(0)) { lstConductor.Items[0].Selected = true; }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];
                idConductor = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;

                lstConductor.Visible = false;
                lstConductor.SendToBack();
                ListarDesbloqueo();
                dtpFechaCompromiso.Focus();
            }
        }

        private void lstConductor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstConductor.SelectedItems[0];
            idConductor = Int32.Parse(ItemActual.Text);
            txtConductor.Text = ItemActual.SubItems[1].Text;

            lstConductor.Visible = false;
            lstConductor.SendToBack();
            ListarDesbloqueo();
            dtpFechaCompromiso.Focus();
        }

        private void dtpFechaCompromiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDesbloqueo(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtConductor.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConductor.Focus();
                return;
            }
            else
            {
                DataTable dtDesbloquear = new DataTable();
                string respta;

                dtDesbloquear = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_DesbloqueoConductores(1, 0, idConductor, dtpFechaCompromiso.Value, Utilitario.Instancia.SesionUsuario.usuario);
                respta = Convert.ToString(dtDesbloquear.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idConductor = -1;
                    ListarDesbloqueo();
                    txtConductor.Clear();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgListaDesbloqueo_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Conductor = dtgvListaDesbloqueo.GetRowCellValue(dtgvListaDesbloqueo.FocusedRowHandle, "CONDUCTOR").ToString();
                if (Conductor != "")
                {
                    if (e1 == 1) { tsQuitarDesbloqueo.Enabled = true; }
                }
            }
            catch { tsQuitarDesbloqueo.Enabled = false; }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarDesbloqueo(); }

        private void tsQuitarDesbloqueo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar el desbloqueo de este conductor?", "DESBLOQUEO DE CONDUCTORES", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idDesbloqueo = Convert.ToInt32(dtgvListaDesbloqueo.GetRowCellValue(dtgvListaDesbloqueo.FocusedRowHandle, "idDesbloqueo"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_DesbloqueoConductores(2, idDesbloqueo, 0, DateTime.Now, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarDesbloqueo(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
