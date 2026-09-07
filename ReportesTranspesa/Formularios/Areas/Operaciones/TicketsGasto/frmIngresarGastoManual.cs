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
    public partial class frmIngresarGastoManual : Form
    {
        int _idRuta, _idOperacion;
        string _RutaActiva, _Operacion;
        int adicional, idPeaje = 0;

        public frmIngresarGastoManual()
        {
            InitializeComponent();
            cbxTipoGasto.SelectedIndexChanged -= cbxTipoGasto_SelectedIndexChanged;
            cbxTiempo.SelectedIndexChanged -= cbxTiempo_SelectedIndexChanged;
            cbxPeaje.SelectedIndexChanged -= cbxPeaje_SelectedIndexChanged;
        }

        private void cbxTipoGasto_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboTipo(); }

        private void cbxTiempo_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboTiempo(); }

        private void cbxPeaje_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboPeajes(); }

        private void frmIngresarGastoManual_Shown(object sender, EventArgs e)
        { txtDescripcion.Focus(); }

        private void frmIngresarGastoManual_Load(object sender, EventArgs e)
        {
            CargarComboTipo();
            CargarComboTiempo();
            CargarComboPeajes();
            cbxTipoGasto_DropDownClosed(sender, e);
            lblRuta.Text = _RutaActiva;
            lblProgramacion.Text = _Operacion;
            cbAdicional.Checked = false;
            cbAdicional_CheckedChanged(sender, e);

            ListarGasto();
        }


        private void CargarComboTipo()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarTipoGasto();
            cbxTipoGasto.DataSource = dtTipo;
            cbxTipoGasto.DisplayMember = "Descripcion";
            cbxTipoGasto.ValueMember = "idTipoGasto";
        }

        private void CargarComboTiempo()
        {
            DataTable dtTiempo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarTiempos();
            cbxTiempo.DataSource = dtTiempo;
            cbxTiempo.DisplayMember = "Descripcion";
            cbxTiempo.ValueMember = "idTiempo";
        }

        public void CargarComboPeajes()
        {
            DataTable dtPeaje = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(7, "");
            cbxPeaje.DataSource = dtPeaje;
            cbxPeaje.DisplayMember = "Peaje";
            cbxPeaje.ValueMember = "idPeaje";
        }

        public void EnviarGasto(int idOperacion, string Operacion, int idRuta, string Ruta)
        {
            _idOperacion = idOperacion;
            _idRuta = idRuta;

            _Operacion = Operacion;
            _RutaActiva = Ruta;
        }

        private void ListarGasto()
        {
            DataTable dtListaGasto = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoDetalle(_idRuta, _idOperacion);
            dtgLista.DataSource = dtListaGasto;
            if (dtListaGasto.Rows.Count > 0)
            {
                dgvListaExpressVista.Columns["idGastoxRutaD"].Visible = false;
                dgvListaExpressVista.BestFitColumns();
                /*
                dgvListaExpressVista.Columns["TipoGasto"].Width = 150;
                dgvListaExpressVista.Columns["Descripcion"].Width = 250;
                dgvListaExpressVista.Columns["Efectivo"].Width = 100;
                */

                dgvListaExpressVista.Columns["Efectivo"].Summary.Clear();
                dgvListaExpressVista.Columns["Efectivo"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Efectivo", "{0:N2}");

                if(dgvListaExpressVista.Columns["Efectivo"].SummaryText == "0.00")
                {
                    txtTotal.Clear();
                }
                else
                {
                    txtTotal.Text = dgvListaExpressVista.Columns["Efectivo"].SummaryText;
                }
            }
        }

        private void InsertarGastoDetalle()
        {
            if (txtGasto.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el monto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtGasto.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarGastoDetalle(Convert.ToInt32(cbxTipoGasto.SelectedValue), txtDescripcion.Text, Convert.ToDecimal(txtGasto.Text), Convert.ToInt32(cbxTiempo.SelectedValue), adicional, idPeaje);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    ListarGasto();
                    txtGasto.Clear();
                    txtDescripcion.Clear();
                    txtDescripcion.Focus();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void EliminarGasto()
        {
            int idGastoxRutaD = Convert.ToInt32(dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "idGastoxRutaD"));
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarGastoDetalle(idGastoxRutaD);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                ListarGasto();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 

        private void txMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InsertarGastoDetalle();
                txtGasto.Clear();
                txtDescripcion.Clear();
                txtDescripcion.Focus();
                cbAdicional.Checked = false;
                cbAdicional_CheckedChanged(sender, e);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarGasto();
        }

        private void btnAniadir_Click(object sender, EventArgs e)
        {
            InsertarGastoDetalle();
            cbAdicional.Checked = false;
            cbAdicional_CheckedChanged(sender, e);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cbAdicional_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAdicional.Checked == true) { adicional = 1; }

            if (cbAdicional.Checked == false) { adicional = 0; }
        }

        private void cbxTipoGasto_DropDownClosed(object sender, EventArgs e)
        {
            if(Convert.ToInt32(cbxTipoGasto.SelectedValue) == 1)
            {
                cbxPeaje.Enabled = true;
                cbxPeaje_DropDownClosed(sender, e);
            }
            else
            {
                cbxPeaje.Enabled = false;
                txtGasto.Clear();
                txtDescripcion.Clear();
                idPeaje = 0;
            }
        }

        private void cbxPeaje_DropDownClosed(object sender, EventArgs e)
        {
            DataTable dtListar = new DataTable();
            dtListar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(Convert.ToInt32(cbxPeaje.SelectedValue), 1, "");

            if (dtListar.Rows.Count > 0)
            {
                txtGasto.Text = dtListar.Rows[0]["Monto"].ToString().TrimEnd();
                txtDescripcion.Text = dtListar.Rows[0]["PEAJE"].ToString().TrimEnd();
                idPeaje = Convert.ToInt32(dtListar.Rows[0]["IdPeaje"].ToString());
            }
        }
    }
}
