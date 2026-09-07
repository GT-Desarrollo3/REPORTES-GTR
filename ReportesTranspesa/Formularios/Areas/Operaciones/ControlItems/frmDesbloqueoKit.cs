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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    public partial class frmDesbloqueoKit : Form
    {
        public int idRuta, idTracto, e1 = 0;
        DataTable dtPermisos = new DataTable();

        public frmDesbloqueoKit()
        {
            InitializeComponent();
        }

        private void frmDesbloqueoKit_Load(object sender, EventArgs e)
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

            dtpFechaCompromiso.Value =  DateTime.Now.AddDays(1);
            ListarDesbloqueo();
            txtTracto.Focus();
        }


        public void ListarDesbloqueo()
        {
            DataTable dtListaDesbloqueo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarUnidadesDesbloqueadas(txtTracto.Text);
            dtgRutaKN.DataSource = dtListaDesbloqueo;
            if (dtListaDesbloqueo.Rows.Count > 0)
            {
                dtgvRutaKN.Columns["idDesbloqueo"].Visible = false;

                dtgvRutaKN.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvRutaKN.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dtgvRutaKN.BestFitColumns();
            }
        }

        private void txtRuta_Enter(object sender, EventArgs e) { txtRuta.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstRuta, clsConsultaBL.Instancia.GetRutasActivas(txtRuta.Text), true, false, false);
            lstRuta.Columns[0].Width = 0;
            lstRuta.Columns[1].Width = 250;

            lstRuta.BringToFront();
            lstRuta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRuta.Visible = false;
                lstRuta.SendToBack();
                idRuta = -1;
            }
        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstRuta.Focus(); }
        }

        private void txtRuta_Leave(object sender, EventArgs e) { txtRuta.BackColor = Color.White; }

        private void lstRuta_Enter(object sender, EventArgs e)
        {
            if (!lstRuta.Items.Count.Equals(0)) { lstRuta.Items[0].Selected = true; }
        }

        private void lstRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstRuta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstRuta.SelectedItems[0];
                idRuta = Int32.Parse(ItemActual.Text);
                txtRuta.Text = ItemActual.SubItems[1].Text;

                lstRuta.Visible = false;
                lstRuta.SendToBack();
                ListarDesbloqueo();
                txtTracto.Focus();
            }
        }

        private void lstRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstRuta.SelectedItems[0];
            idRuta = Int32.Parse(ItemActual.Text);
            txtRuta.Text = ItemActual.SubItems[1].Text;

            lstRuta.Visible = false;
            lstRuta.SendToBack();
            ListarDesbloqueo();
            txtTracto.Focus();
        }

        private void txtTracto_Enter(object sender, EventArgs e) { txtTracto.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtTracto.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 80;
            lstTracto.Columns[2].Width = 0;
            lstTracto.Columns[3].Width = 0;
            lstTracto.Columns[4].Width = 0;
            lstTracto.Columns[5].Width = 0;
            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void txtTracto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        private void txtTracto_Leave(object sender, EventArgs e) { txtTracto.BackColor = Color.White; }

        private void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0)) { lstTracto.Items[0].Selected = true; }
        }

        private void lstTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTracto.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTracto.SelectedItems[0];
                idTracto = Int32.Parse(ItemActual.Text);
                txtTracto.Text = ItemActual.SubItems[1].Text;

                lstTracto.Visible = false;
                lstTracto.SendToBack();
                ListarDesbloqueo();
                dtpFechaCompromiso.Focus();
            }
        }

        private void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];
            idTracto = Int32.Parse(ItemActual.Text);
            txtTracto.Text = ItemActual.SubItems[1].Text;

            lstTracto.Visible = false;
            lstTracto.SendToBack();
            ListarDesbloqueo();
            dtpFechaCompromiso.Focus();
        }

        private void dtpFechaCompromiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDesbloqueo(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtTracto.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese una unidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTracto.Focus();
                return;
            }
            else
            {
                DataTable dtDesbloquear = new DataTable();
                string respta;

                dtDesbloquear = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_DesbloqueoTractoXRuta(1, 0, idTracto, dtpFechaCompromiso.Value, Utilitario.Instancia.SesionUsuario.usuario);
                respta = Convert.ToString(dtDesbloquear.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idRuta = -1; idTracto = -1;
                    ListarDesbloqueo();
                    txtRuta.Clear();
                    txtTracto.Clear();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarDesbloqueo(); }

        private void dtgRutaKN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Ruta = dtgvRutaKN.GetRowCellValue(dtgvRutaKN.FocusedRowHandle, "UNIDAD").ToString();
                if (Ruta != "")
                {
                    if (e1 == 1) { tsQuitarDesbloqueo.Enabled = true; }
                }
            }
            catch { tsQuitarDesbloqueo.Enabled = false; }
        }

        private void tsQuitarDesbloqueo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar el desbloqueo de esta unidad?", "DESBLOQUEO DE UNIDADES", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idDesbloqueo = Convert.ToInt32(dtgvRutaKN.GetRowCellValue(dtgvRutaKN.FocusedRowHandle, "idDesbloqueo"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_DesbloqueoTractoXRuta(2, idDesbloqueo, 0, DateTime.Now, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarDesbloqueo(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
