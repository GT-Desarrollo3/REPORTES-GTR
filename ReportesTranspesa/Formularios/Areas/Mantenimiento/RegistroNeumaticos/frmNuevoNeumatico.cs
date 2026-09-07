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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroNeumaticos
{
    public partial class frmNuevoNeumatico : Form
    {
        public int Opcion;
        public frmListaCodNeumaticos frmListaCodNeumaticos;

        public frmNuevoNeumatico()
        {
            InitializeComponent();
            cbxMarca.SelectedIndexChanged -= cbxMarca_SelectedIndexChanged;
            cbxMedida.SelectedIndexChanged -= cbxMedida_SelectedIndexChanged;
            cbxModelo.SelectedIndexChanged -= cbxModelo_SelectedIndexChanged;
        }

        private void cbxMarca_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMarca(); }

        private void cbxMedida_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMedida(); }

        private void cbxModelo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboModelo(); }

        private void frmNuevoNeumatico_Load(object sender, EventArgs e)
        {
            
        }


        public void CargarComboMarca()
        {
            DataTable dtMarca = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos(2);
            cbxMarca.DataSource = dtMarca;
            cbxMarca.DisplayMember = "Descripcion";
            cbxMarca.ValueMember = "idMarca";
        }

        public void CargarComboMedida()
        {
            DataTable dtMedida = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos(3);
            cbxMedida.DataSource = dtMedida;
            cbxMedida.DisplayMember = "Descripcion";
            cbxMedida.ValueMember = "idMedida";
        }

        public void CargarComboModelo()
        {
            DataTable dtModelo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos(4);
            cbxModelo.DataSource = dtModelo;
            cbxModelo.DisplayMember = "Descripcion";
            cbxModelo.ValueMember = "idModelo";
        }


        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDOT.Focus(); }
        }

        private void txtDOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxMarca.Focus(); }
        }

        private void cbxMarca_DropDownClosed(object sender, EventArgs e) { cbxMedida.Focus(); }

        private void cbxMedida_DropDownClosed(object sender, EventArgs e) { cbxModelo.Focus(); }

        private void cbxModelo_DropDownClosed(object sender, EventArgs e) { dtpFechaInicio.Focus(); }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxTipo.Focus(); }
        }

        public void cbxTipo_DropDownClosed(object sender, EventArgs e) { txtKM.Focus(); }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtNSK.Focus(); }
        }

        private void txtNSK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar.Focus(); }
        }

        private void txtKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtPrecio.Focus(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDOT.Text.Length == 0 || txtPrecio.Text.Length == 0 || txtKM.Text.Length == 0 || txtNSK.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                if (txtCodigo.Text.Length == 0) { txtCodigo.Focus(); }
                else
                {
                    if (txtDOT.Text.Length == 0) { txtDOT.Focus(); }
                    else
                    {
                        if (txtPrecio.Text.Length == 0) { txtPrecio.Focus(); }
                        else
                        {
                            if (txtKM.Text.Length == 0) { txtKM.Focus(); }
                            else { txtNSK.Focus(); }
                        }
                    }
                }

                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                
                if (Opcion == 1)
                {
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_RegistrarEditarNeumaticos(1, txtCodigo.Text, txtDOT.Text, cbxMarca.Text,
                                                               cbxMedida.Text, cbxModelo.Text, cbxTipo.Text, Convert.ToDecimal(txtNSK.Text), Convert.ToDecimal(txtKM.Text),
                                                               Convert.ToDecimal(txtPrecio.Text), dtpFechaInicio.Value, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaCodNeumaticos.ListarRegistroNeumaticos();
                        this.Close();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                
                if (Opcion == 2)
                {
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_RegistrarEditarNeumaticos(2, txtCodigo.Text, txtDOT.Text, cbxMarca.Text,
                                                               cbxMedida.Text, cbxModelo.Text, cbxTipo.Text, Convert.ToDecimal(txtNSK.Text), Convert.ToDecimal(txtKM.Text),
                                                               Convert.ToDecimal(txtPrecio.Text), dtpFechaInicio.Value, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaCodNeumaticos.ListarRegistroNeumaticos();
                        this.Close();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
