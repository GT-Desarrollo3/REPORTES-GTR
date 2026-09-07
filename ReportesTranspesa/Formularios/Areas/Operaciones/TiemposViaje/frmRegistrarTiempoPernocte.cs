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
using ReportesTranspesa.Properties;
using Negocio;
using Comun;


namespace ReportesTranspesa.Formularios.Areas.Operaciones.TiemposViaje
{
    public partial class frmRegistrarTiempoPernocte : Form
    {
        DataTable dtPermisos = new DataTable();
        public frmListaTiemposViaje formulario;
        
        public frmRegistrarTiempoPernocte()
        {
            InitializeComponent();
        }

        private void frmRegistrarTiempoPernocte_Shown(object sender, EventArgs e) { dtpFechaInicio.Focus(); }

        private void frmRegistrarTiempoPernocte_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaTiemposViaje");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsQuitarPernocte.Enabled = true; }
                    else { tsQuitarPernocte.Enabled = false; }
                }
            }
            
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            cbxTipoPernocte.Text = "ENTRADA";
            ListarPernoctes();
        }


        public void ListarPernoctes()
        {
            DataTable dtListaPernoctes = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_FiltrarTiemposPernoctes(Convert.ToInt32(txtPreviaje.Text));
            dtgListaPernoctes.DataSource = dtListaPernoctes;
            if (dtListaPernoctes.Rows.Count > 0)
            {
                dgvListaPernoctesView.Columns["NroTicket"].Visible = false;

                dgvListaPernoctesView.BestFitColumns();
            }
        }


        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaFin.Focus(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtUbicacion.Focus(); }
        }

        private void txtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnIngresarTiempos.Focus(); }
        }

        private void dtgListaPernoctes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idPernocte = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "NRO").ToString();

                if (idPernocte != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsQuitarPernocte.Enabled = true; }
                }
                else { tsQuitarPernocte.Enabled = false; }
            }
            catch { tsQuitarPernocte.Enabled = false; }
        }

        private void btnIngresarTiempos_Click(object sender, EventArgs e)
        {
            if (txtUbicacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la ubicación del pernocte.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUbicacion.Focus();
                return;
            }

            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtRegistrar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRegistrar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte(1, Convert.ToInt32(txtPreviaje.Text), 0, dtpFechaInicio.Value,
                                                                                                                     dtpFechaFin.Value, txtUbicacion.Text, cbxTipoPernocte.Text, Usuario);
                respta = Convert.ToString(dtRegistrar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFechaInicio.Value = DateTime.Now;
                    dtpFechaFin.Value = DateTime.Now;
                    txtUbicacion.Clear();
                    ListarPernoctes();
                    if (formulario != null)
                    {
                        formulario.Filtro = 2;
                        formulario.ListarTiemposViaje();
                    }
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsQuitarPernocte_Click(object sender, EventArgs e)
        {
            try
            {
                int idPernocte = Convert.ToInt32(dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "NRO"));
                int NroTicket = Convert.ToInt32(dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "NroTicket"));

                if (MessageBox.Show("¿Desea eliminar este tiempo de pernocte?", "ELIMINAR PERNOCTE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte(2, NroTicket, idPernocte, DateTime.Now, DateTime.Now, "", "", "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarPernoctes();
                        if (formulario != null)
                        {
                            formulario.Filtro = 2;
                            formulario.ListarTiemposViaje();
                        }
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el pernocte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
