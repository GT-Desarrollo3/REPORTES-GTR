using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Negocio;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Sistema;

using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmRptListaHistorialDesbloqueos : Form
    {
        DataTable dtDatos = new DataTable();
        string fechin = "01/01/1980";
        string fechfin = "31/12/2030";
        int ValFecha, ValMotivo;

        public FrmRptListaHistorialDesbloqueos()
        {
            InitializeComponent();
            cbxMotivoBloqueo.SelectedIndexChanged -= cbxMotivoBloqueo_SelectedIndexChanged;
        }

        private void cbxMotivoBloqueo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void FrmConductoresBloqueados_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            CargarCombo();
            chkFecha_Click(sender, e);
            chkMotivoBloqueo_Click(sender, e);

            /*
            dtDatos = clsOperacionesBL.Instancia.Obtener_Lista_HistoricoDesbloqueos(fechin, fechfin, txtConductor.Text, 0, 0);
            if (dtDatos != null && dtDatos.Rows.Count > 0)
            {
                dtgvListaDesbloqueos.DataSource = dtDatos;
                dtgvListaDesbloqueosView.BestFitColumns();
                dtgvListaDesbloqueosView.Columns["ID_MOTIVO"].Visible = false;
                dtgvListaDesbloqueosView.Columns["ID_DESBLOQUEO"].Visible = false;
            }
            */
        }


        private void CargarCombo()
        {
            DataTable dtMotivoBloqueo = clsOperacionesBL.Instancia.ReportesApp_ListarMotivoBloqueo();
            cbxMotivoBloqueo.DataSource = dtMotivoBloqueo;
            cbxMotivoBloqueo.DisplayMember = "Descripcion";
            cbxMotivoBloqueo.ValueMember = "IdMotivo";
        }

        private void ListaConductoresBloqueados()
        {
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }

            DataTable dtDatos = new DataTable();
            dtDatos = clsOperacionesBL.Instancia.Obtener_Lista_HistoricoDesbloqueos(fechin, fechfin, txtConductor.Text, ValFecha, ValMotivo, Convert.ToInt32(cbxMotivoBloqueo.SelectedValue), 0);
            if (dtDatos != null && dtDatos.Rows.Count > 0)
            {
                dtgvListaDesbloqueos.DataSource = dtDatos;
                dtgvListaDesbloqueosView.Columns["ID_MOTIVO"].Visible = false;
                dtgvListaDesbloqueosView.Columns["ID_DESBLOQUEO"].Visible = false;
                dtgvListaDesbloqueosView.BestFitColumns();
            }
            else
            {
                dtgvListaDesbloqueos.DataSource = null;
                MessageBox.Show("No se encontraron registros.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListaConductoresBloqueados();
            
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvListaDesbloqueos.DataSource == null)
            {

                MessageBox.Show("No hay data para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Historico Desbloqueados " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvListaDesbloqueos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void lvConductor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvConductor.SelectedItems[0];

                //cliente = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;
                lvConductor.Visible = false;
                txtConductor.Focus();
                ListaConductoresBloqueados();
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                clsVisuales.Instancia.LlenarLw(lvConductor, clsConsultaBL.Instancia.GetConductores(txtConductor.Text), true, false, false);

                lvConductor.Columns[0].Width = 0;
                lvConductor.Columns[1].Width = -1;
                lvConductor.Columns[2].Width = -1;

                lvConductor.BringToFront();
                lvConductor.Visible = true;
                lvConductor.Focus();

                //ListaConductoresBloqueados();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvConductor.Visible = false;
                txtConductor.Focus();
            }
        }

        private void lvConductor_Enter(object sender, EventArgs e)
        {
            if (!lvConductor.Items.Count.Equals(0))
            {
                lvConductor.Items[0].Selected = true;
            }
        }

        private void lvConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvConductor.SelectedItems[0];

                //cliente = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;
                lvConductor.Visible = false;
                txtConductor.Focus();
                ListaConductoresBloqueados();

            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvConductor.Visible = false;
                txtConductor.Focus();
            }
        }

        private void cbxMotivoBloqueo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void chkFecha_Click(object sender, EventArgs e)
        {
            if (chkFecha.Checked == true)
            {
                ValFecha = 1;
                groupBox2.Enabled = true;
            }
            else
            {
                ValFecha = 0;
                groupBox2.Enabled = false;
            }
        }

        private void chkMotivoBloqueo_Click(object sender, EventArgs e)
        {
            if (chkMotivoBloqueo.Checked == true)
            {
                ValMotivo = 1;
                groupBox3.Enabled = true;
            }
            else
            {
                ValMotivo = 0;
                groupBox3.Enabled = false;
            }
        }
    }
}
