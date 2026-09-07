using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmProgramacionAlertasStock : Form
    {
        string codigo;
        int xClick, yClick;
        
        public frmProgramacionAlertasStock()
        {
            InitializeComponent();
            cbxAlmacen.SelectedIndexChanged -= cbxAlmacen_SelectedIndexChanged;
        }

        private void cbxAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboAlmacen(); }

        private void frmProgramacionAlertasStock_Load(object sender, EventArgs e)
        {
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmProgramacionAlertasStock");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { EliminarToolStripMenuItem.Enabled = true; }
                else { EliminarToolStripMenuItem.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevo.Enabled = true; }
                else { btnNuevo.Enabled = false; }
            }

            ListarItems();
            ListarAlertas();
            CargarComboAlmacen();
            dgvListaItemsView.OptionsBehavior.Editable = false;
            dgvListaAlertasView.OptionsBehavior.Editable = false;
        }


        private void CargarComboAlmacen()
        {
            DataTable dtAlmacen = clsLogisticaBL.Instancia.ReportesApp_Logistica_AlertaStock_ListarAlmacenes();
            cbxAlmacen.DataSource = dtAlmacen;
            cbxAlmacen.DisplayMember = "DescripcionLocal";
            cbxAlmacen.ValueMember = "AlmacenCodigo";
        }

        private void ListarItems()
        {
            DataTable dtListaItems = clsLogisticaBL.Instancia.ReportesApp_Logistica_ListarItems(txtDescripcion2.Text, Convert.ToString(cbxAlmacen.SelectedValue));
            dtgListaItems.DataSource = dtListaItems;
            dgvListaItemsView.Columns["STOCK_ACTUAL"].DisplayFormat.FormatType = FormatType.Numeric;
            dgvListaItemsView.Columns["STOCK_ACTUAL"].DisplayFormat.FormatString = "n2";
        }

        private void ListarAlertas()
        {
            DataTable dtListaAlertas = clsLogisticaBL.Instancia.ReportesApp_Logistica_ListarAlertasStock(txtBuscarItem.Text);
            dtgListaAlertas.DataSource = dtListaAlertas;
            if (dtListaAlertas.Rows.Count > 0) { dgvListaAlertasView.Columns["IdAlerta"].Visible = false; }
        }

        private void InsertarAlertas()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_InsertarAlertaStock(txtCodigo.Text, txtDescripcion.Text, Convert.ToInt32(txtStockMinimo.Text), Convert.ToInt32(txtTiempo.Text), Convert.ToString(cbxAlmacen.SelectedValue));
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                //MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Clear();
                txtDescripcion.Clear();
                txtStockMinimo.Clear();
                txtTiempo.Clear();
                ListarAlertas();
            }
            else
            { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtStockMinimo.Text.Length == 0 || txtTiempo.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txtStockMinimo.Text.Length == 0) { txtStockMinimo.Focus(); }
                    else
                    {
                        if (txtTiempo.Text.Length == 0) { txtTiempo.Focus(); }
                    }
                    return;
                }
                else { InsertarAlertas(); }
            }
        }

        private void txtTiempo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtStockMinimo.Text.Length == 0 || txtTiempo.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txtTiempo.Text.Length == 0) { txtTiempo.Focus(); }
                    else
                    {
                        if (txtStockMinimo.Text.Length == 0) { txtStockMinimo.Focus(); }
                    }
                    return;
                }
                else { InsertarAlertas(); }
            }
        }

        private void btnBuscarCodigo_Click(object sender, EventArgs e)
        {
            pBuscarItem.Visible = true;
            pBuscarItem.BringToFront();
            btnBuscarCodigo.Enabled = false;
            btnNuevo.Enabled = false;
            txtDescripcion2.Focus();
            ListarItems();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtDescripcion2.Clear();
            ListarItems();
            pBuscarItem.Visible = false;
            pBuscarItem.SendToBack();
            btnBuscarCodigo.Enabled = true;
            btnNuevo.Enabled = true;
            txtStockMinimo.Focus();
        }

        private void txtDescripcion2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarItems(); }
        }

        private void dtgListaItems_DoubleClick(object sender, EventArgs e)
        {
            pBuscarItem.Visible = false;
            pBuscarItem.SendToBack();
            btnBuscarCodigo.Enabled = true;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmProgramacionAlertasStock");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    btnNuevo.Enabled = true;
                }
                else
                {
                    btnNuevo.Enabled = false;
                }
            }
            codigo = Convert.ToString(dgvListaItemsView.GetRowCellValue(dgvListaItemsView.FocusedRowHandle, "CODIGO"));
            txtCodigo.Text = codigo;
            txtDescripcion.Text = Convert.ToString(dgvListaItemsView.GetRowCellValue(dgvListaItemsView.FocusedRowHandle, "ITEM"));
            txtStockMinimo.Focus();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            pBuscarItem.Visible = false;
            pBuscarItem.SendToBack();
            btnBuscarCodigo.Enabled = true;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmProgramacionAlertasStock");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevo.Enabled = true; }
                else { btnNuevo.Enabled = false; }
            }
            codigo = Convert.ToString(dgvListaItemsView.GetRowCellValue(dgvListaItemsView.FocusedRowHandle, "CODIGO"));
            txtCodigo.Text = codigo;
            txtDescripcion.Text = Convert.ToString(dgvListaItemsView.GetRowCellValue(dgvListaItemsView.FocusedRowHandle, "ITEM"));
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtStockMinimo.Text.Length == 0 || txtTiempo.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else { InsertarAlertas(); }
        }

        private void txtBuscarItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAlertas(); }
        }

        private void EliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar la alerta de este ítem?", "ALERTAS DE STOCK DE ÍTEMS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EliminarAlertaStock(Convert.ToInt32(dgvListaAlertasView.GetRowCellValue(dgvListaAlertasView.FocusedRowHandle, "IdAlerta")));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarAlertas(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void pBuscarItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pBuscarItem.Left = pBuscarItem.Left + (e.X - xClick);
                pBuscarItem.Top = pBuscarItem.Top + (e.Y - yClick);
            }
        }
    }
}
