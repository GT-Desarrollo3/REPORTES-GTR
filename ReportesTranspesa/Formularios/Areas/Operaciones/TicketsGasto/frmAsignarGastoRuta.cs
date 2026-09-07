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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmAsignarGastoRuta : Form
    {
        public string esVALE;
        public int idRuta, idOperacion, Detalle = 0;
        public frmListarGastosRuta formulario;      // GERARDO - 27/11

        public frmAsignarGastoRuta()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboOperacion();
        }

        private void frmAsignarGastoRuta_Shown(object sender, EventArgs e)
        {
            cbxOperaciones.Focus();
        }

        private void frmAsignarGastoRuta_Load(object sender, EventArgs e)
        { }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        private void AsignarGasto()
        {
            if (txtRutasActivas.Text.Length == 0 || txtGasto.Text.Length == 0 || txtDias.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtRutasActivas.Text.Length == 0) { txtRutasActivas.Focus(); }
                else
                {
                    if (txtDias.Text.Length == 0) { txtDias.Focus(); }
                    else { txtGasto.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarGasto(idRuta, Convert.ToInt32(cbxOperaciones.SelectedValue), Convert.ToInt32(txtDias.Text), Convert.ToDecimal(txtGasto.Text), Detalle);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRutasActivas.Clear();
                    idRuta = 0;
                    txtGasto.Clear();
                    txtDias.Clear();
                    txtRutasActivas.Focus();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }


        private void lvRuta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRutasActivas, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRutasActivas, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas))
                {
                    if (esVALE == "SI")
                    {
                        txtDias.Focus();
                    }
                    else
                    {
                        txtRutasActivas.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if ((e.KeyChar == (char)Keys.Enter) && !lvRuta.Items.Count.Equals(0))
            {
                ListViewItem RutaActual;
                RutaActual = lvRuta.SelectedItems[0];
                idRuta = Convert.ToInt32(RutaActual.SubItems[0].Text);
                txtRutasActivas.Text = RutaActual.SubItems[1].Text;
                lvRuta.Visible = false;
                txtDias.Focus();
            }
        }

        private void lvRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRutasActivas, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem RutaActual;
            RutaActual = lvRuta.SelectedItems[0];
            idRuta = Convert.ToInt32(RutaActual.SubItems[0].Text);
            txtRutasActivas.Text = RutaActual.SubItems[1].Text;
            lvRuta.Visible = false;
            txtDias.Focus();
        }

        private void txtRutasActivas_Enter(object sender, EventArgs e)
        {
            txtRutasActivas.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRutasActivas_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRutasActivas, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas))
                {
                    if (esVALE == "SI")
                    {
                        txtDias.Focus();
                    }
                    else
                    {
                        ListViewItem RutaActual;
                        RutaActual = lvRuta.SelectedItems[0];
                        idRuta = Convert.ToInt32(RutaActual.SubItems[0].Text);
                        txtRutasActivas.Text = RutaActual.SubItems[1].Text;
                        lvRuta.Visible = false;
                        txtDias.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            { txtDias.Focus(); }

            if (e.KeyChar == (char)Keys.Back)
            { idRuta = 0; }
        }

        private void txtRutasActivas_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRutasActivas, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (e.KeyCode == Keys.Down) { lvRuta.Focus(); }
        }

        private void txtRutasActivas_Leave(object sender, EventArgs e)
        {
            txtRutasActivas.BackColor = Color.White;
        }

        private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtGasto.Focus(); }
        }

        private void txtGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                AsignarGasto();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AsignarGasto();
            formulario.ListarGatoRuta();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cbxOperaciones.SelectedValue = 2;
            txtRutasActivas.Clear();
            idRuta = 0;
            txtGasto.Clear();
            txtDias.Clear();
            txtRutasActivas.Focus();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (txtRutasActivas.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese una ruta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRutasActivas.Focus();
                return;
            }
            else
            {
                frmIngresarGastoManual frmIngresarGastoManual = new frmIngresarGastoManual();
                frmIngresarGastoManual.EnviarGasto(Convert.ToInt32(cbxOperaciones.SelectedValue), cbxOperaciones.Text, idRuta, txtRutasActivas.Text);
                if (frmIngresarGastoManual.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtGasto.Text = frmIngresarGastoManual.txtTotal.Text;
                    Detalle = 1;
                }
            }
        }
    }
}
