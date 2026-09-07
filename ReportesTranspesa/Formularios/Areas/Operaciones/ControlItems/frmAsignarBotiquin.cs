using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using Comun;
using Negocio;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    public partial class frmAsignarBotiquin : Form
    {
        DataTable dtListaItems = new DataTable();
        public frmListaControlItems frmListaControlItems = new frmListaControlItems();
        public int idBotiquinUnidadC = 0;
        public string TipoBotiquin;

        public frmAsignarBotiquin()
        {
            InitializeComponent();
            cbxItem.SelectedIndexChanged -= cbxItem_SelectedIndexChanged;
        }

        private void cbxItem_SelectedIndexChanged(object sender, EventArgs e) { CargarComboItems(); }

        private void frmAsignarBotiquin_Load(object sender, EventArgs e)
        {
            ListarBotiquinPlacas();
        }


        public void CargarComboItems()
        {
            DataTable dtItems = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarItemsAsignados(3, -1);
            cbxItem.DataSource = dtItems;
            cbxItem.DisplayMember = "ITEM";
            cbxItem.ValueMember = "idItemBotiquin";
        }

        public void ListarBotiquinPlacas()
        {
            dtListaItems = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarBotiquines(lblPlaca.Text, lblProgramacion.Text, TipoBotiquin);
            dtgvBotiquinUnidad.DataSource = dtListaItems;
            if (dtListaItems.Rows.Count > 0)
            {
                dtgvBotiquinUnidadView.Columns["idBotiquinUnidadC"].Visible = false;
                dtgvBotiquinUnidadView.Columns["idBotiquinUnidadD"].Visible = false;
                dtgvBotiquinUnidadView.Columns["idItemBotiquin"].Visible = false;
                dtgvBotiquinUnidadView.Columns["UNIDAD"].Visible = false;
                dtgvBotiquinUnidadView.Columns["OPERACION"].Visible = false;
                dtgvBotiquinUnidadView.Columns["UsuarioCrea"].Visible = false;
                dtgvBotiquinUnidadView.Columns["FechaCrea"].Visible = false;
                dtgvBotiquinUnidadView.Columns["UsuarioModifica"].Visible = false;
                dtgvBotiquinUnidadView.Columns["FechaModifica"].Visible = false;

                dtgvBotiquinUnidadView.BestFitColumns();
            }
        }


        private void dtpFVencimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCantidad.Focus(); }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtObservacion.Focus(); }
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la cantidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCantidad.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_CrearModificarBotiquin(idBotiquinUnidadC, Convert.ToInt32(cbxItem.SelectedValue),
                                  Convert.ToInt32(txtCantidad.Text), dtpFVencimiento.Value, txtObservacion.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaControlItems.ListarBotiquinPlacas();
                        ListarBotiquinPlacas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo registrar el ítem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgvBotiquinUnidad_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cbxItem.Text = Convert.ToString(dtgvBotiquinUnidadView.GetRowCellValue(dtgvBotiquinUnidadView.FocusedRowHandle, "ITEM"));
            dtpFVencimiento.Value = Convert.ToDateTime(dtgvBotiquinUnidadView.GetRowCellValue(dtgvBotiquinUnidadView.FocusedRowHandle, "FECHA_VENCIMIENTO"));
            txtCantidad.Text = Convert.ToString(dtgvBotiquinUnidadView.GetRowCellValue(dtgvBotiquinUnidadView.FocusedRowHandle, "CANTIDAD"));
            txtObservacion.Text = Convert.ToString(dtgvBotiquinUnidadView.GetRowCellValue(dtgvBotiquinUnidadView.FocusedRowHandle, "OBSERVACION"));
        }

        private void dtgvBotiquinUnidadView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.Yellow; }

                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "CANTIDAD")
            {
                if (Convert.ToInt32(e.CellValue) == 0)
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
                }
            }
        }

        private void tsQuitarItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idItemBotiquin = Convert.ToInt32(dtgvBotiquinUnidadView.GetRowCellValue(dtgvBotiquinUnidadView.FocusedRowHandle, "idItemBotiquin"));

                if (MessageBox.Show("¿Desea eliminar este ítem del botiquín?", "ELIMINAR ÍTEM", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin(2, idBotiquinUnidadC, idItemBotiquin);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        frmListaControlItems.ListarBotiquinPlacas();
                        ListarBotiquinPlacas();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el ítem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
