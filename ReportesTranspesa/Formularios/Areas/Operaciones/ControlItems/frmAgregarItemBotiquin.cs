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
    public partial class frmAgregarItemBotiquin : Form
    {
        string Codigo, TipoBotiquin;
        DataTable dtListaItems = new DataTable();
        DataTable dtPermisos = new DataTable();

        public frmAgregarItemBotiquin()
        {
            InitializeComponent();
        }

        private void frmAgregarItemBotiquin_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaControlItems");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnGuardar.Enabled = true; }
                else { btnGuardar.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminar.Enabled = true; }
                else { tsEliminar.Enabled = false; }
            }

            rbMTC.Checked = true;
            rbMTC_CheckedChanged(sender, e);

            ListarItems();
        }


        public void ListarItems()
        {
            dtListaItems = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarItemsAsignados(1, -1);
            dtgvListaBotiquin.DataSource = dtListaItems;
            if (dtListaItems.Rows.Count > 0)
            {
                dtgvListaBotiquinView.Columns["idItemBotiquin"].Visible = false;

                dtgvListaBotiquinView.BestFitColumns();
            }
        }


        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCantidad.Focus(); }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDuracion.Focus(); }
        }

        private void rbMTC_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMTC.Checked == true) { TipoBotiquin = "MTC"; }
        }

        private void rbQuemadura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbQuemadura.Checked == true) { TipoBotiquin = "QUEMADURA"; }
        }

        private void txtDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCodigoItem.Focus(); }
        }

        private void txtCodigoItem_Enter(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.SeaShell; }

        private void txtCodigoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstItems, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems(txtCodigoItem.Text), true, false, false);
            lstItems.Columns[0].Width = 100;
            lstItems.Columns[1].Width = 323;
            lstItems.Columns[2].Width = 0;
            lstItems.Columns[3].Width = 0;
            lstItems.Columns[4].Width = 0;
            lstItems.BringToFront();
            lstItems.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Codigo = "";
                txtCodigo.Clear();
                lstItems.Visible = false;
                lstItems.SendToBack();
            }
        }

        private void txtCodigoItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstItems.Focus(); }
        }

        private void txtCodigoItem_Leave(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.White; }

        private void lstItems_Enter(object sender, EventArgs e)
        {
            if (!lstItems.Items.Count.Equals(0)) { lstItems.Items[0].Selected = true; }
        }

        private void lstItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstItems.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstItems.SelectedItems[0];

                Codigo = ItemActual.SubItems[0].Text;
                txtCodigo.Text = Codigo;
                txtCodigoItem.Text = ItemActual.SubItems[1].Text;

                lstItems.Visible = false;
                lstItems.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Codigo = "";
                txtCodigo.Clear();
                lstItems.Visible = false;
                lstItems.SendToBack();
            }
        }

        private void lstItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItems.SelectedItems[0];

            Codigo = ItemActual.SubItems[0].Text;
            txtCodigo.Text = Codigo;
            txtCodigoItem.Text = ItemActual.SubItems[1].Text;

            lstItems.Visible = false;
            lstItems.SendToBack();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtDuracion.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese correctamente el ítem.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtDescripcion.Text.Length == 0) { txtDescripcion.Focus(); }
                else
                {
                    if (txtCodigo.Text.Length == 0) { txtCodigoItem.Focus(); }
                    else
                    {
                        if (txtCantidad.Text.Length == 0) { txtCantidad.Focus(); }
                        else { txtDuracion.Focus(); }
                    }
                }
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarItemBotiquin(txtDescripcion.Text, Codigo.TrimEnd(),
                                  Convert.ToInt32(txtCantidad.Text), Convert.ToInt32(txtDuracion.Text), TipoBotiquin, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = Respuesta.Substring(0, 1);
                    
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDescripcion.Clear();
                        txtCodigo.Clear();
                        txtCodigoItem.Clear();
                        txtCantidad.Clear();
                        txtDuracion.Clear();
                        Codigo = "";

                        ListarItems();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo registrar el ítem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idItemBotiquin = Convert.ToInt32(dtgvListaBotiquinView.GetRowCellValue(dtgvListaBotiquinView.FocusedRowHandle, "idItemBotiquin"));

                if (MessageBox.Show("¿Desea eliminar este ítem del maestro?", "ELIMINAR ITEM", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin(1, -1, idItemBotiquin);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarItems(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el ítem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
