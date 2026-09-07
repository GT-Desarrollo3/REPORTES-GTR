using Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;


namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmVincularSeriesPorUsuarios : Form
    {
        public string tipoguia;
        public string serie;
        public string empresa;
        public FrmVincularSeriesPorUsuarios()
        {
            InitializeComponent();
        }



        private void lstPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPersonal, ref lstPersonal, clsConsultaBL.Instancia.GetPersona);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstPersonal_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPersonal, ref lstPersonal, clsConsultaBL.Instancia.GetPersona);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPersonal, ref  lstPersonal, clsConsultaBL.Instancia.GetPersona))
                {

                    if (txtPersonal.Tag == null)
                    {
                        MessageBox.Show("No a seleccionado correctamente el personal", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmVincularSeriesPorUsuarios_Load(object sender, EventArgs e)
        {
            txtSerie.Text = serie;
            txtEmpresa.Text = empresa;
            txtTipo.Text = tipoguia;

            ListarUsuariosVinculadosPorSerie();
          
            

        }

        private void ListarUsuariosVinculadosPorSerie()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarUsuariosVinculadosxSeries(serie, empresa, tipoguia);
            if (dt.Rows.Count > 0)
            {
                dgvUsuarios.DataSource = dt;
            }
            else
            {
                dgvUsuarios.DataSource = null;
            }
        }

        private void txtPersonal_Enter(object sender, EventArgs e)
        {
            txtPersonal.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPersonal_Leave(object sender, EventArgs e)
        {
            txtPersonal.BackColor = Color.White;
        }

        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPersonal, ref  lstPersonal, clsConsultaBL.Instancia.GetPersona))
                {


                    if (txtPersonal.Tag == null)
                    {
                        txtPersonal.Clear();

                    }


                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPersonal, ref lstPersonal, clsConsultaBL.Instancia.GetPersona))
                {
                    if (txtPersonal.Tag == null)
                    {
                        txtPersonal.Clear();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPersonal.Tag != null)
                {

                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_VincularSeriesPorUsuario(empresa, tipoguia, serie, Convert.ToInt32(txtPersonal.Tag), txtPersonal.Text, Utilitario.TipoOperacion.Registrar))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarUsuariosVinculadosPorSerie();
                        txtPersonal.Tag = null;
                        txtPersonal.Clear();
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void desvincularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["idUsuario"].Value);

                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_VincularSeriesPorUsuario(empresa, tipoguia, serie, idUsuario, "", Utilitario.TipoOperacion.Anular))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarUsuariosVinculadosPorSerie();
                        txtPersonal.Tag = null;
                        txtPersonal.Clear();
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
