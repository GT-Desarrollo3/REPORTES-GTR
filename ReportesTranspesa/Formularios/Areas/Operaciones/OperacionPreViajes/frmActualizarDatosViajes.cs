using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Entidades;
using Negocio;
using System.Drawing.Printing;
using ReportesTranspesa.Properties;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmActualizarDatosViajes : Form
    {
        public int Opcion;

        public frmActualizarDatosViajes()
        {
            InitializeComponent();
        }

        private void frmActualizarDatosViajes_Load(object sender, EventArgs e)
        {
            cbxOperacion.Text = "ELIMINAR GUÍA";
            cbxOperacion_DropDownClosed(sender, e);
        }


        public void MostrarGuias()
        {
            DataTable dtlistarGuia = new DataTable();
            dtlistarGuia = clsOperacionesBL.Instancia.ReportesApp_Operaciones_DatosOT_ListarGuiasViaje(1, txtCodigo.Text);
            
            if (dtlistarGuia.Rows.Count > 0)
            {
                txtOT.Text = dtlistarGuia.Rows[0]["OT"].ToString();
                txtGuiaT.Text = dtlistarGuia.Rows[0]["GUIAT"].ToString();
                txtGuiaR.Text = dtlistarGuia.Rows[0]["GUIAR"].ToString();

                ListarViajeGuia();
            }
        }

        public void ListarViajeGuia()
        {
            DataTable dtViajeGuias = new DataTable();
            dtViajeGuias = clsOperacionesBL.Instancia.ReportesApp_Operaciones_DatosOT_ListarGuiasViaje(2, txtCodigo.Text);
            dtgViajeGuias.DataSource = dtViajeGuias;

            if (dtViajeGuias.Rows.Count > 0)
            {
                dgvViajeGuiasView.Columns["idGuia"].Visible = false;

                dgvViajeGuiasView.BestFitColumns();
            }
        }


        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { MostrarGuias(); }

            if (e.KeyChar == (char)Keys.Back)
            {
                txtOT.Clear();
                txtGuiaT.Clear();
                txtGuiaR.Clear();

                dtgViajeGuias.DataSource = null;
                dgvViajeGuiasView.Columns.Clear();
            }
        }

        private void dtgViajeGuias_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idGuia = dgvViajeGuiasView.GetRowCellValue(dgvViajeGuiasView.FocusedRowHandle, "idGuia").ToString();

                if (idGuia != "")
                {
                    txtOT.Text = dgvViajeGuiasView.GetRowCellValue(dgvViajeGuiasView.FocusedRowHandle, "OT").ToString();
                    txtGuiaT.Text = dgvViajeGuiasView.GetRowCellValue(dgvViajeGuiasView.FocusedRowHandle, "TRANSPORTISTA").ToString();
                    txtGuiaR.Text = dgvViajeGuiasView.GetRowCellValue(dgvViajeGuiasView.FocusedRowHandle, "REMITENTE").ToString();
                }
                else { }
            }
            catch { }
        }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxOperacion.Text == "ELIMINAR GUÍA") { Opcion = 1; }

            //if (cbxOperacion.Text == "ACTUALIZAR GUÍA") { Opcion = 2; }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el código del viaje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Focus();
                return;
            }

            if (txtOT.Text.Length == 0 || txtGuiaT.Text.Length == 0 || txtGuiaR.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_DatosOT_EditarGuiasViaje(Opcion, txtCodigo.Text, Convert.ToInt32(txtOT.Text), txtGuiaT.Text, txtGuiaR.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarGuias();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
