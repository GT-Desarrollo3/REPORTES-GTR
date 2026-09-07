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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    public partial class frmNuevoTrabajo : Form
    {
        public frmRegistroOT formulario;

        public frmNuevoTrabajo()
        {
            InitializeComponent();
        }

        private void frmNuevoTrabajo_Load(object sender, EventArgs e) { }


        private void txtNroReq_Enter(object sender, EventArgs e) { txtNroReq.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtNroReq_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstRequerimiento, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos(txtNroReq.Text), true, false, false);
            lstRequerimiento.Columns[0].Width = 120;
            lstRequerimiento.Columns[1].Width = 250;
            lstRequerimiento.Columns[2].Width = 0;
            lstRequerimiento.Columns[3].Width = 100;
            lstRequerimiento.Columns[4].Width = 0;
            lstRequerimiento.Columns[5].Width = 100;
            lstRequerimiento.BringToFront();
            lstRequerimiento.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
            }

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void txtNroReq_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstRequerimiento.Focus(); }
        }

        private void txtNroReq_Leave(object sender, EventArgs e) { txtNroReq.BackColor = Color.White; }

        private void lstRequerimiento_Enter(object sender, EventArgs e)
        {
            if (!lstRequerimiento.Items.Count.Equals(0)) { lstRequerimiento.Items[0].Selected = true; }
        }

        private void lstRequerimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstRequerimiento.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstRequerimiento.SelectedItems[0];
                txtNroReq.Text = ItemActual.SubItems[0].Text;
                txtDescripcion.Text = ItemActual.SubItems[1].Text;
                txtCentroCosto.Text = ItemActual.SubItems[2].Text;
                txtCentroCostoD.Text = ItemActual.SubItems[3].Text;
                txtProyecto.Text = ItemActual.SubItems[4].Text;
                txtProyectoD.Text = ItemActual.SubItems[5].Text;

                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
                dtpFechaProgramada.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
            }
        }

        private void lstRequerimiento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstRequerimiento.SelectedItems[0];
            txtNroReq.Text = ItemActual.SubItems[0].Text;
            txtDescripcion.Text = ItemActual.SubItems[1].Text;
            txtCentroCosto.Text = ItemActual.SubItems[2].Text;
            txtCentroCostoD.Text = ItemActual.SubItems[3].Text;
            txtProyecto.Text = ItemActual.SubItems[4].Text;
            txtProyectoD.Text = ItemActual.SubItems[5].Text;

            lstRequerimiento.Visible = false;
            lstRequerimiento.SendToBack();
            dtpFechaProgramada.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNroReq.Clear();
            txtDescripcion.Clear();
            txtCentroCosto.Clear();
            txtCentroCostoD.Clear();
            txtProyecto.Clear();
            txtProyectoD.Clear();
            txtDescripcion.Clear();
            dtpFechaProgramada.Value = DateTime.Now;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNroReq.Text.Length == 0 || txtDescripcion.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtNroReq.Text.Length == 0) { txtNroReq.Focus(); }
                else { txtDescripcion.Focus(); }
                return;
            }
            else
            {
                DataTable dtRegistroRQ = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRegistroRQ = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_IngresarRequerimientos(txtNroReq.Text, txtCentroCosto.Text, txtProyecto.Text,
                               txtDescripcion.Text, dtpFechaProgramada.Value, Usuario);
                respta = Convert.ToString(dtRegistroRQ.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancelar_Click(sender, e);
                    formulario.ListarOTProgramadas();
                    this.Close();
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
