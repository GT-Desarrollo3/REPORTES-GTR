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
using Negocio;
using Comun;
using ReportesTranspesa.Sistema;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmNuevoEPPS : Form
    {
        public frmNuevoEPPS()
        {
            InitializeComponent();
            cbxTipoEPPS.SelectedIndexChanged -= cbxTipoEPPS_SelectedIndexChanged;
        }

        private void cbxTipoEPPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void frmNuevoEPPS_Shown(object sender, EventArgs e)
        {
            txtPersonal.Focus();
        }

        private void frmNuevoEPPS_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombo();
                FechaInicio.Value = new DateTime(FechaInicio.Value.Year, FechaInicio.Value.Month, 1);
                FechaFin.Value = DateTime.Now;
                cbxTipoEPPS.Text = "";
                cbxTipoEPPS.SelectedValue = 0;
                ListarEPPS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarCombo()
        {
            DataTable dtTipoEPPS = clsSeguridadBL.Instancia.ReportesApp_ListarComboTiposEPPS();
            cbxTipoEPPS.DataSource = dtTipoEPPS;
            cbxTipoEPPS.DisplayMember = "Nombre";
            cbxTipoEPPS.ValueMember = "TipoEPPS";
        }

        private void ListarEPPS()
        {
            DataTable dtEPPS = clsSeguridadBL.Instancia.ReportesApp_ListarEPPS(txtPersonal.Text, FechaInicio.Text, FechaFin.Text, Convert.ToInt32(cbxTipoEPPS.SelectedValue));
            dtgData.DataSource = dtEPPS;
            if (dtEPPS.Rows.Count > 0)
            {
                dgvExpressVista.Columns["idTipoEPPS"].Visible = false;

                dgvExpressVista.Columns["FECHA CREACIÓN"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvExpressVista.Columns["FECHA CREACIÓN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvExpressVista.Columns["ÚLTIMA MODIFICACIÓN"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvExpressVista.Columns["ÚLTIMA MODIFICACIÓN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvExpressVista.BestFitColumns();
                dgvExpressVista.ExpandAllGroups();
            }
        }


        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarEPPS();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarEPPS();
        }

        private void btnExcelS_Click(object sender, EventArgs e)
        {
            if (dtgData.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Historial de EPPS Devueltos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
