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
    public partial class frmCalcularMonto : Form
    {
        int _idFalla;
        string _conductor;

        public frmCalcularMonto()
        {
            InitializeComponent();
            cbxRecibo.SelectedIndexChanged -= cbxRecibo_SelectedIndexChanged;
        }

        private void cbxRecibo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboRecibo();
        }

        private void frmCalcularMonto_Shown(object sender, EventArgs e)
        {
            txtDescripcion.Focus();
        }

        private void frmCalcularMonto_Load(object sender, EventArgs e)
        {
            dtpFechaGasto.Value = DateTime.Now;
            CargarComboRecibo();
            txtComprobante.Enabled = false;
            ListarMonto();
        }


        private void CargarComboRecibo()
        {
            DataTable dtRecibo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarRecibos();
            cbxRecibo.DataSource = dtRecibo;
            cbxRecibo.DisplayMember = "Descripcion";
            cbxRecibo.ValueMember = "idTipoRecibo";
        }

        public void EnviarFalla(int idFalla, string conductor)
        {
            _idFalla = idFalla;
            _conductor = conductor;
        }

        private void ListarMonto()
        {
            if (_conductor == "")
            {
                txtDescripcion.Enabled = true;
                txMonto.Enabled = true;
                cbxRecibo.Enabled = true;
                dtpFechaGasto.Enabled = true;
                btnAgregar.Enabled = true;
                btnAniadir.Enabled = true;
                eliminarToolStripMenuItem.Enabled = true;
            }
            else
            {
                txtDescripcion.Enabled = false;
                txMonto.Enabled = false;
                cbxRecibo.Enabled = false;
                dtpFechaGasto.Enabled = false;
                btnAgregar.Enabled = false;
                btnAniadir.Enabled = false;
                eliminarToolStripMenuItem.Enabled = false;
            }

            DataTable dtListaMonto = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarMontoDetalle(_idFalla);
            dtgLista.DataSource = dtListaMonto;
            if (dtListaMonto.Rows.Count > 0)
            {
                dgvListaExpressVista.Columns["idGastoDetalle"].Visible = false;
                dgvListaExpressVista.Columns["Descripcion"].Width = 150;
                dgvListaExpressVista.Columns["Gasto"].Width = 100;

                dgvListaExpressVista.Columns["Gasto"].Summary.Clear();
                dgvListaExpressVista.Columns["Gasto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Gasto", "{0:N2}");

                txtTotal.Text = dgvListaExpressVista.Columns["Gasto"].SummaryText;
            }
        }

        private void InsertarMontoDetalle()
        {
            if (txtDescripcion.Text.Length == 0 || txMonto.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtDescripcion.Text.Length == 0)
                {
                    txtDescripcion.Focus();
                }
                else
                {
                    txMonto.Focus();
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarMontoDetalle(txtDescripcion.Text, Convert.ToDecimal(txMonto.Text), Convert.ToInt32(cbxRecibo.SelectedValue), txtComprobante.Text, Convert.ToDateTime(dtpFechaGasto.Value));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    ListarMonto();
                }
                else
                {
                    txtDescripcion.Focus();
                }
            }
        }

        private void EliminarMonto()
        {
            int idGastoDetalle = Convert.ToInt32(dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "idGastoDetalle"));
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_EliminarMontoDetalle(idGastoDetalle);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                ListarMonto();
                txtDescripcion.Clear();
                txMonto.Clear();
                txtDescripcion.Focus();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertarMonto(int idFalla)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarMonto(idFalla);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                ListarMonto();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txMonto.Focus();
            }
        }

        private void txMonto_KeyPress(object sender, KeyPressEventArgs e)
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
                InsertarMontoDetalle();
                txtDescripcion.Clear();
                txMonto.Clear();
                txtDescripcion.Focus();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarMonto();
        }

        private void cbxRecibo_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbxRecibo.SelectedValue) == 1)
            {
                txtComprobante.Enabled = false;
                txtComprobante.Clear();
                dtpFechaGasto.Focus();
            }
            else
            {
                if (Convert.ToInt32(cbxRecibo.SelectedValue) == 2 || Convert.ToInt32(cbxRecibo.SelectedValue) == 3 || Convert.ToInt32(cbxRecibo.SelectedValue) == 5)
                {
                    txtComprobante.Enabled = true;
                    txtComprobante.Focus();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            InsertarMonto(_idFalla);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            InsertarMontoDetalle();
            txtDescripcion.Clear();
            txtComprobante.Clear();
            txMonto.Clear();
            dtpFechaGasto.Value = DateTime.Now;
            txtDescripcion.Focus();
        }
    }
}
