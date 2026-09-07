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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmListaSolicitudDeCambios : Form
    {
        public frmListaSolicitudDeCambios()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSolicitarCambiosGuia frmSolicitar = new frmSolicitarCambiosGuia();
                frmSolicitar.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                if (frmSolicitar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BuscarGuiasElectronicas();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarGuia_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarGuiasElectronicas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarGuiasElectronicas()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_operaciones_ListarSolicitudes_CambioDatosGuia(cbxSerieGuia.Text,txtNumeroGuia.Text);
            if (dt.Rows.Count > 0)
            {
                dtgListaGuiasTransportista.DataSource = dt;
            }
            else
            {
                dtgListaGuiasTransportista.DataSource = null;
            }
        }

        private void frmListaSolicitudDeCambios_Load(object sender, EventArgs e)
        {
            CargarSeries();
            clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(Utilitario.Instancia.SesionUsuario.usuario); //Trae los permisos del usuario
            DataTable dtPermisosEspeciales = null;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("FrmListaGuiasElectronicas");
            
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                }

                if (dtPermisosEspeciales != null)
                {
                    if (dtPermisosEspeciales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                        {
                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Atender Solicitud")
                            {
                                atenderToolStripMenuItem.Visible = true;
                            }

                        }
                    }
                }

        }
        private void CargarSeries()
        {
            DataTable dtSerieGuia = clsOperacionesBL.Instancia.ReportesApp_Listar_SerieGuiasElectronicas("T");


            if (dtSerieGuia.Rows.Count > 0)
            {
                cbxSerieGuia.DataSource = dtSerieGuia;
                cbxSerieGuia.DisplayMember = "SerieGuia";
                cbxSerieGuia.ValueMember = "SerieGuia";
                cbxSerieGuia.SelectedIndex = 0;

            }

        }

        private void cbxSerieGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void atenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSolicitarCambiosGuia frmSolicitar = new frmSolicitarCambiosGuia();
                frmSolicitar.TipoOperacion = Utilitario.TipoOperacion.Editar;
                frmSolicitar.SerieGuia = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("SerieGuia").ToString();
                frmSolicitar.NumeroGuia = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("NumeroGuia").ToString();
                //frmSolicitar.cbxSerieGuia.SelectedValue = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("SerieGuia").ToString();
                frmSolicitar.txtNumeroGuia.Text = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("NumeroGuia").ToString();
                frmSolicitar.txtNroSolicitud.Text = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("NroSolicitud").ToString();
                frmSolicitar.txtNumeroGuia.ReadOnly = true;
                frmSolicitar.cbxSerieGuia.Enabled = false;
                frmSolicitar.cbxCampo.Text = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("CampoSolicitado").ToString();
                frmSolicitar.txtNuevoValor.Text = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("ValorSolicitado").ToString();
                frmSolicitar.txtMotivoSolicitud.Text = dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("MotivoSolicitud").ToString();
                
                if (frmSolicitar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BuscarGuiasElectronicas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}
