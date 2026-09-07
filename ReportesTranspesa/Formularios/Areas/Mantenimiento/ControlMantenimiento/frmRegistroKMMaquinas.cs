using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
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
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmRegistroKMMaquinas : Form
    {
        public frmListaMantenimiento frmListaMantenimiento = new frmListaMantenimiento();
        int xClick = 0, yClick = 0;
        public string MaquinaCodigo;
        DataTable dtListaKM;
        DataTable dtPermisos = new DataTable();

        public frmRegistroKMMaquinas()
        {
            InitializeComponent();
        }

        private void frmRegistroKMMaquinas_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaMantenimiento");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    btnActualizar.Enabled = true;
                    btnGuardar.Enabled = true;
                }
                else
                {
                    btnActualizar.Enabled = false;
                    btnGuardar.Enabled = false;
                }
            }
            
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
            dtpFecha.Value = DateTime.Now;
            ListarKMMaquinas();
        }


        public void ListarKMMaquinas()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtListaKM = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKMMaquina(txtPlaca.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgRegistroKM.DataSource = dtListaKM;
                if (dtListaKM.Rows.Count > 0)
                {
                    dgvRegistroKMVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroKMVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvRegistroKMVista.BestFitColumns();
                }
            }
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKMMaquinas(); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKMMaquinas(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKMMaquinas(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarKMMaquinas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgRegistroKM.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE KM DE MAQUINARIAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgRegistroKM.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            pActualizar.Visible = true;
            pActualizar.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pActualizar.Visible = false;
            pActualizar.SendToBack();
            txtBuscarPlaca.Clear();
            MaquinaCodigo = "";
            dtpFecha.Value = DateTime.Now;
        }

        private void pActualizar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pActualizar.Left = pActualizar.Left + (e.X - xClick);
                pActualizar.Top = pActualizar.Top + (e.Y - yClick);
            }
        }

        private void txtBuscarPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarMaquinas(2, txtBuscarPlaca.Text, "000"), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 86;
            lstPlaca.Columns[2].Width = 0;
            lstPlaca.Columns[3].Width = 0;
            lstPlaca.Columns[4].Width = 80;
            lstPlaca.Columns[5].Width = 80;
            lstPlaca.Columns[6].Width = 0;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                txtBuscarPlaca.Focus();
                lstPlaca.SendToBack();
                MaquinaCodigo = "";
            }
        }

        private void txtBuscarPlaca_KeyUp(object sender, KeyEventArgs e)
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

                txtBuscarPlaca.Text = ItemActual.SubItems[1].Text;
                MaquinaCodigo = ItemActual.SubItems[1].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                dtpFecha.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                txtBuscarPlaca.Focus();
                MaquinaCodigo = "";
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            txtBuscarPlaca.Text = ItemActual.SubItems[1].Text;
            MaquinaCodigo = ItemActual.SubItems[1].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            dtpFecha.Focus();
        }

        private void txtUltKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void dtgRegistroKM_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MaquinaCodigo = dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "PLACA").ToString();
                txtBuscarPlaca.Text = dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "PLACA").ToString();
                dtpFecha.Value = Convert.ToDateTime(dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "FECHA"));
                txtUltKM.Text = dgvRegistroKMVista.GetRowCellValue(dgvRegistroKMVista.FocusedRowHandle, "KM_REAL").ToString();
                pActualizar.Visible = true;
                pActualizar.BringToFront();
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtBuscarPlaca.Text.Length == 0 || txtUltKM.Text.Length == 0)
            {
                if (txtBuscarPlaca.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese la placa de la unidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBuscarPlaca.Focus();
                }
                else
                {
                    MessageBox.Show("El KM de la unidad no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUltKM.Focus();
                }
                return;
            }
            else
            {
                DataTable dtActualizar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtActualizar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMMaquinas(txtBuscarPlaca.Text, dtpFecha.Value, Convert.ToDecimal(txtUltKM.Text));
                respta = Convert.ToString(dtActualizar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pictureBox1_Click(sender, e);
                    ListarKMMaquinas();
                    frmListaMantenimiento.ListarMantenimientoMaquinas();
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
