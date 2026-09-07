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

using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Formularios.Areas.Operaciones.ControlDocumentos;

using System.Xml;
using System.IO;


namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlDocumentos
{
    public partial class frmHistorialDocumentos : Form
    {
        DataTable dtHistorial = new DataTable();
        DataTable dtHVehiculos = new DataTable();
        string TipoRelacion;

        public frmHistorialDocumentos()
        {
            InitializeComponent();
        }

        private void frmHistorialDocumentos_Load(object sender, EventArgs e)
        {
            FechaModIni.Value = new DateTime(FechaModIni.Value.Year, FechaModIni.Value.Month, 1);
            FechaModFin.Value = DateTime.Now;
            rbConductores.Checked = true;
            rbConductores_Click(sender, e);
            ListarHistorial();
        }


        private void ListarHistorial()
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = FechaModIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = FechaModFin.Value.ToShortDateString() + " 23:59:59";

            if (FechaModIni.Value > FechaModFin.Value)
            {
                MessageBox.Show("La Fecha de Inicio debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FechaModIni.Focus();
                return;
            }

            dtHistorial = clsControlDocumentosBL.Instancia.ReportesApp_Operaciones_ControlDocumentos_ListarHistorial(1, txtConductor.Text, fechin, fechfin);
            dgvHistorialDocumentos.DataSource = dtHistorial;
            if (dtHistorial.Rows.Count > 0)
            {
                dgvHistorialDocumentosView.Columns["IdDocHistorial"].Visible = false;
                dgvHistorialDocumentosView.Columns["IdDocumento"].Visible = false;

                dgvHistorialDocumentosView.Columns["FECHA_EMISION_ANT"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialDocumentosView.Columns["FECHA_EMISION_ANT"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialDocumentosView.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialDocumentosView.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialDocumentosView.Columns["FECHA_INICIO_VALIDEZ_ANT"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialDocumentosView.Columns["FECHA_INICIO_VALIDEZ_ANT"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialDocumentosView.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialDocumentosView.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialDocumentosView.Columns["FECHA_FIN_VALIDEZ_ANT"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialDocumentosView.Columns["FECHA_FIN_VALIDEZ_ANT"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialDocumentosView.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialDocumentosView.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";

                dgvHistorialDocumentosView.Columns["ULTIMA_MODIFICACION"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialDocumentosView.Columns["ULTIMA_MODIFICACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvHistorialDocumentosView.BestFitColumns();
                dgvHistorialDocumentosView.ExpandAllGroups();
            }
        }

        private void ListarHVehiculos()
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = FechaModIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = FechaModFin.Value.ToShortDateString() + " 23:59:59";

            if (FechaModIni.Value > FechaModFin.Value)
            {
                MessageBox.Show("La Fecha de Inicio debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FechaModIni.Focus();
                return;
            }

            dtHVehiculos = clsControlDocumentosBL.Instancia.ReportesApp_Operaciones_ControlDocumentos_ListarHistorial(2, txtVehiculo.Text, fechin, fechfin);
            dgvHistorialVehiculos.DataSource = dtHVehiculos;
            if (dtHVehiculos.Rows.Count > 0)
            {
                dgvHistorialVehiculosView.Columns["IdDocHistorial"].Visible = false;
                dgvHistorialVehiculosView.Columns["IdDocumento"].Visible = false;

                dgvHistorialVehiculosView.Columns["FECHA_EMISION_ANT"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVehiculosView.Columns["FECHA_EMISION_ANT"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialVehiculosView.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVehiculosView.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialVehiculosView.Columns["FECHA_INICIO_VALIDEZ_ANT"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVehiculosView.Columns["FECHA_INICIO_VALIDEZ_ANT"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialVehiculosView.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVehiculosView.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialVehiculosView.Columns["FECHA_FIN_VALIDEZ_ANT"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVehiculosView.Columns["FECHA_FIN_VALIDEZ_ANT"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialVehiculosView.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVehiculosView.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";

                dgvHistorialVehiculosView.Columns["ULTIMA_MODIFICACION"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVehiculosView.Columns["ULTIMA_MODIFICACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvHistorialVehiculosView.BestFitColumns();
                dgvHistorialVehiculosView.ExpandAllGroups();
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (TipoRelacion == "C")
            {
                ListarHistorial();
            }

            if (TipoRelacion == "V")
            {
                ListarHVehiculos();
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarHistorial();
            }
        }

        private void txtVehiculo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarHVehiculos();
            }
        }

        private void rbConductores_Click(object sender, EventArgs e)
        {
            if (rbConductores.Checked == true)
            {
                TipoRelacion = "C";
                panel2.Visible = false;
                dgvHistorialVehiculos.Visible = false;
            }
        }

        private void rbVehiculos_Click(object sender, EventArgs e)
        {
            if (rbVehiculos.Checked == true)
            {
                TipoRelacion = "V";
                panel2.Visible = true;
                ListarHVehiculos();
                dgvHistorialVehiculos.Visible = true;
            }
        }
    }
}
