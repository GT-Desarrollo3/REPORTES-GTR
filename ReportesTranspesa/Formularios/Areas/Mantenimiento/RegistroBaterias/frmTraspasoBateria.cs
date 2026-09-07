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
    public partial class frmTraspasoBateria : Form
    {
        public int _idBateria, idVehiculoACT = 0, idVehiculoANT;
        public frmListaBaterias formulario;

        public frmTraspasoBateria()
        {
            InitializeComponent();
        }

        private void frmTraspasoBateria_Load(object sender, EventArgs e)
        { }


        public void RecibirDatos (int idBateria)
        {
            _idBateria = idBateria;

            DataTable dtBaterias = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_FiltrarBaterias(idBateria);
            if (dtBaterias.Rows.Count > 0)
            {
                txtCodigo.Text = dtBaterias.Rows[0]["CODIGO"].ToString();
                txtMarca.Text = dtBaterias.Rows[0]["MARCA"].ToString();
                txtModelo.Text = dtBaterias.Rows[0]["MODELO / PLACA"].ToString();
                FechaIni.Enabled = false;
                FechaIni.Value = Convert.ToDateTime(dtBaterias.Rows[0]["FECHA_INICIO"]);
                txtDuracion.Text = dtBaterias.Rows[0]["DIAS_DURACION"].ToString();
                txtIntervalo.Text = dtBaterias.Rows[0]["DURACION"].ToString();
                dtpFechaCambio.Value = Convert.ToDateTime(dtBaterias.Rows[0]["FECHA_CAMBIO"]);
                dtpFechaInspeccion.Value = Convert.ToDateTime(dtBaterias.Rows[0]["FECHA_INSPECCION"]);
                txtNivelCarga.Text = dtBaterias.Rows[0]["NIVEL_CARGA"].ToString();
                txtEstadoB.Text = dtBaterias.Rows[0]["ESTADO_BATERIA"].ToString();
            }
        }


        private void txtPlacaAct_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos(txtPlacaAct.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 100;
            lstPlaca.Columns[2].Width = 100;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                txtPlacaAct.Focus();
                txtTipoAct.Clear();
                idVehiculoACT = 0;
            }
        }

        private void txtPlacaAct_KeyUp(object sender, KeyEventArgs e)
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

                idVehiculoACT = Int32.Parse(ItemActual.Text);
                txtPlacaAct.Text = ItemActual.SubItems[1].Text;
                txtTipoAct.Text = ItemActual.SubItems[2].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtMotivo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                txtPlacaAct.Focus();
                txtTipoAct.Clear();
                idVehiculoACT = 0;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idVehiculoACT = Int32.Parse(ItemActual.Text);
            txtPlacaAct.Text = ItemActual.SubItems[1].Text;
            txtTipoAct.Text = ItemActual.SubItems[2].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtMotivo.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtPlacaAct.Text.Length == 0 || txtTipoAct.Text.Length == 0 || txtMotivo.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPlacaAct.Text.Length == 0) { txtPlacaAct.Focus(); }
                else
                {
                    if (txtTipoAct.Text.Length == 0) { txtTipoAct.Focus(); }
                    else { txtMotivo.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_RegistrarTraspaso(_idBateria, idVehiculoANT, idVehiculoACT, txtMotivo.Text, Usuario);

                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarBaterias();
                    this.Close();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
