using Comun;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmGestionarSeries : Form
    {

        DataTable dtEmpresasGrupo;
        public frmGestionarSeries()
        {
            InitializeComponent();
        }

        private void frmGestionarSeries_Load(object sender, EventArgs e)
        {
            CargarEmpresa();
            CargarTipoGuia();
            CargarSeries(true);
            
        }

        private void CargarSeries(bool estado)
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarMaestroSeriesGuiasElectronicas(estado);
            if (dt.Rows.Count > 0)
            {

                dgvListaGuiaExpressVista.OptionsBehavior.AutoPopulateColumns = true; //'generar automaticamente las columnas a raíz del dataset
                dgvListaGuiaExpressVista.OptionsView.ColumnAutoWidth = false; //' para mantener el ancho si ajustar al ancho del contenedor
                dtgListaGuias.DataSource = dt; ;//Mi dataSet
                dgvListaGuiaExpressVista.BestFitColumns();

               
           
            }
            else
            {
                dtgListaGuias.DataSource = null;
            }
        }

        private void CargarEmpresa()
        {
            dtEmpresasGrupo = clsOperacionesBL.Instancia.ReportesApp_ListarEmpresasGrupo();
            if (dtEmpresasGrupo.Rows.Count > 0)
            {

                cbxEmpresasGrupo.DataSource = dtEmpresasGrupo;
                cbxEmpresasGrupo.DisplayMember = "RazonSocial";
                cbxEmpresasGrupo.ValueMember = "Compania";
                cbxEmpresasGrupo.SelectedIndex = 0;
            }
        }

        private void txtSeries_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == (char)Keys.Space || e.KeyChar == '-' || e.KeyChar == '´' || e.KeyChar == '+' || e.KeyChar == '?' || e.KeyChar == '¡'
                || e.KeyChar == '¿' || e.KeyChar == '!' || e.KeyChar == ',' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '*'
                || e.KeyChar == '/' || e.KeyChar == '|' || e.KeyChar == '"' || e.KeyChar == '#' || e.KeyChar == '$' || e.KeyChar == '%'
                || e.KeyChar == '&' || e.KeyChar == '=' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == '¨' || e.KeyChar == 'Ñ')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void cbxEmpresasGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbxTipoGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CargarTipoGuia()
        {

            DataTable dtTipoGuias = clsOperacionesBL.Instancia.ListarTipoGuiaElectronica();
            if (dtTipoGuias.Rows.Count > 0)
            {
                cbxTipoGuia.DataSource = dtTipoGuias;
                cbxTipoGuia.DisplayMember = "NombreTipoGuia";
                cbxTipoGuia.ValueMember = "TipoGuia";
                cbxTipoGuia.SelectedIndex = 0;

            }
            else
            {
                MessageBox.Show("Combobox de Tipo Guia no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void cbxTipoGuia_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbxTipoGuia.SelectedValue.ToString() == "T")
            {
                txtSeries.Clear();
                txtSeries.Text = "V";
            }
            if (cbxTipoGuia.SelectedValue.ToString() == "R")
            {
                txtSeries.Clear();
                txtSeries.Text = "T";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (txtDescripcion.Text.Length < 3)
            {
                MessageBox.Show("Tiene que llenar una descripcion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            /*
            if (cbxTipoGuia.SelectedValue.ToString() == "T")
            {
                if (txtSeries.Text.Substring(0, 1) != "V")
                {
                    MessageBox.Show("Para una guia de remision remitente tiene que ir obligatoriamente la letra T al inicio de la serie", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (cbxTipoGuia.SelectedValue.ToString() == "R")
            {
                if (txtSeries.Text.Substring(0, 1) != "T")
                {
                    MessageBox.Show("Para una guia de remision remitente tiene que ir obligatoriamente la letra V al inicio de la serie", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            */ 

            if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarAnularSerieGuiaElectronica(cbxTipoGuia.SelectedValue.ToString(), txtSeries.Text, cbxEmpresasGrupo.SelectedValue.ToString(), txtDescripcion.Text,Utilitario.TipoOperacion.Registrar))
            {
                MessageBox.Show( Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarSeries(true);
            }
            else
            {
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInactivos.Checked)
            {
                CargarSeries(false);
            }
            else
            {
                CargarSeries(true);
            }
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string tipoguia = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                string serie = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                string empresa = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "Empresa").ToString();


            if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarAnularSerieGuiaElectronica(tipoguia, serie, empresa, "", Utilitario.TipoOperacion.Anular))
            {
                CargarSeries(true);
                MessageBox.Show( Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void activarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string tipoguia = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                string serie = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                string empresa = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "Empresa").ToString();


                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarAnularSerieGuiaElectronica(tipoguia, serie, empresa, "", Utilitario.TipoOperacion.Activar))
                {
                    CargarSeries(true);
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {   
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void vincularAnexoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmVincularSeriesPorAnexos vincular = new frmVincularSeriesPorAnexos();
                vincular.tipoguia = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                vincular.serie = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                vincular.empresa = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "Empresa").ToString();

                vincular.ShowDialog();
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void vincularUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmVincularSeriesPorUsuarios vincularUsuario = new FrmVincularSeriesPorUsuarios();
                vincularUsuario.tipoguia = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                vincularUsuario.serie = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                vincularUsuario.empresa = dgvListaGuiaExpressVista.GetRowCellValue(dgvListaGuiaExpressVista.FocusedRowHandle, "Empresa").ToString();
                vincularUsuario.ShowDialog();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
