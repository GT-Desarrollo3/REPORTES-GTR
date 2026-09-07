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
using Negocio;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class AsistenciaExterna : Form
    {
        DataTable dtFecha;
        public AsistenciaExterna()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            
        }

        private void AsistenciaExterna_Load(object sender, EventArgs e)
        {

            ListarCompania();
            timer1.Enabled = true;
            txtUsuario.Text = Utilitario.Instancia.SesionUsuario.usuario;
            txtPersona.Text = Utilitario.Instancia.SesionUsuario.nombres + " " + Utilitario.Instancia.SesionUsuario.apellidos;
        }

        private void ListarCompania()
        {
          DataTable dtCompania =   clsConsultaBL.Instancia.GetCompañias();
          cbxCompania.DataSource = dtCompania;
          cbxCompania.DisplayMember = "COMPDESC";
          cbxCompania.ValueMember = "IDCOMP";
          cbxCompania.SelectedIndex = 4;

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string TipoRegistro = "";
                if (rbtIngreso.Checked)
                {
                    TipoRegistro = "INGRESO";
                }
                if (rbtSalida.Checked) 
                {
                    TipoRegistro = "SALIDA";
                }
                if (rbtIngresoDescanso.Checked)
                {
                    TipoRegistro = "DESCANSO";
                }
                if (rbtTerminoDescanso.Checked)
                {
                    TipoRegistro = "FIN DESCANSO";
                }
                dtFecha =  clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ObtenerFechaServidor();
                if (clsRecursosHumanosBL.Instancia.ReporteApp_RRHH_RegistrarAsistenciaExterna(TipoRegistro, Convert.ToDateTime(lblFecha.Text).ToString("dd/MM/yyyy HH:mm:ss"), Convert.ToDateTime(dtFecha.Rows[0]["Fecha"]).ToString("dd/MM/yyyy HH:mm:ss"), txtUsuario.Text, cbxCompania.SelectedValue.ToString()))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }else
                {
                     MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
