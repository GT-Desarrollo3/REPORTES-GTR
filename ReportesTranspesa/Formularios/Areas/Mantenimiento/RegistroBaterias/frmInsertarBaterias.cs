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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroBaterias
{
    public partial class frmInsertarBaterias : Form
    {
        public int idVehiculo = 0, opcion, _idBateria = 0;
        public frmListaBaterias formulario;

        public frmInsertarBaterias()
        {
            InitializeComponent();
        }

        private void frmInsertarBaterias_Load(object sender, EventArgs e) { }


        public void RecibirDatos(int idBateria)
        {
            _idBateria = idBateria;

            DataTable dtBaterias = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_FiltrarBaterias(idBateria);
            if (dtBaterias.Rows.Count > 0)
            {
                txtPlaca.Text = dtBaterias.Rows[0]["VEHICULO"].ToString();
                txtTipo.Text = dtBaterias.Rows[0]["TIPO_VEHICULO"].ToString();
                txtCodigo.Text = dtBaterias.Rows[0]["CODIGO"].ToString();
                txtMarca.Text = dtBaterias.Rows[0]["MARCA"].ToString();
                txtModelo.Text = dtBaterias.Rows[0]["MODELO / PLACA"].ToString();
                FechaIni.Value = Convert.ToDateTime(dtBaterias.Rows[0]["FECHA_INICIO"]);
                txtDuracion.Text = dtBaterias.Rows[0]["DIAS_DURACION"].ToString();
                txtIntervalo.Text = dtBaterias.Rows[0]["DURACION"].ToString();
            }
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos(txtPlaca.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 100;
            lstPlaca.Columns[2].Width = 100;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtTipo.Clear();
                idVehiculo = 0;
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

                idVehiculo = Int32.Parse(ItemActual.Text);
                txtPlaca.Text = ItemActual.SubItems[1].Text;
                txtTipo.Text = ItemActual.SubItems[2].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtCodigo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                txtPlaca.Focus();
                txtTipo.Clear();
                idVehiculo = 0;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idVehiculo = Int32.Parse(ItemActual.Text);
            txtPlaca.Text = ItemActual.SubItems[1].Text;
            txtTipo.Text = ItemActual.SubItems[2].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtCodigo.Focus();
        }

        private void txtIntervalo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (opcion == 1)
            {
                txtPlaca.Clear();
                txtTipo.Clear();
                idVehiculo = 0;
            }
            txtCodigo.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            FechaIni.Value = DateTime.Now;
            txtIntervalo.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtMarca.Text.Length == 0 || txtModelo.Text.Length == 0 || txtIntervalo.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtCodigo.Text.Length == 0) { txtCodigo.Focus(); }
                else
                {
                    if (txtMarca.Text.Length == 0) { txtMarca.Focus(); }
                    else
                    {
                        if (txtIntervalo.Text.Length == 0) { txtIntervalo.Focus(); }
                        else { txtModelo.Focus(); }
                    }
                }
                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (idVehiculo == 0 && opcion == 1) { opcion = 3; }

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_RegistrarModificarBaterias(opcion, _idBateria, txtCodigo.Text, txtMarca.Text, txtModelo.Text, idVehiculo,
                            FechaIni.Value, Convert.ToInt32(txtIntervalo.Text), Usuario);
                    
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarBaterias();
                    txtCodigo.Clear();
                    txtMarca.Clear();
                    txtModelo.Clear();
                    txtIntervalo.Clear();
                    FechaIni.Value = DateTime.Now;
                    if (opcion == 2) { this.Close(); }
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
