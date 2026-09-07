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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmVincularSeriesPorAnexos : Form
    {
        public string tipoguia;
        public string serie;
        public string empresa;
        public frmVincularSeriesPorAnexos()
        {
            InitializeComponent();
        }

        private void frmVincularSeriesPorAnexos_Load(object sender, EventArgs e)
        {
            try
            {
                txtSerie.Text = serie;
                txtTipo.Text = tipoguia;
                txtEmpresa.Text = empresa;

                DataTable dtAnexos = clsOperacionesBL.Instancia.ReportesApp_Listar_Series_Por_Establecimientos_Anexos();
                DataTable dtVinculados = clsOperacionesBL.Instancia.ReportesApp_Listar_Establecimientos_Vinculados_Series(serie, tipoguia, empresa);

                if (dtAnexos.Rows.Count > 0)
                {
                    for(int i = 0 ; i < dtAnexos.Rows.Count ; i++)
                    {
                        dgvEstablecimientos.Rows.Add(false,
                             dtAnexos.Rows[i]["Codigo"],
                             dtAnexos.Rows[i]["Tipo"],
                             dtAnexos.Rows[i]["Descripcion"],
                             dtAnexos.Rows[i]["Departamento"],
                             dtAnexos.Rows[i]["Provincia"],
                             dtAnexos.Rows[i]["Distrito"],
                             dtAnexos.Rows[i]["Ubigeo"],
                             dtAnexos.Rows[i]["Estado"]
                             );
                    }

                   // dgvEstablecimientos.DataSource = dtAnexos;
                }


                if (dtVinculados.Rows.Count > 0)
                {
                    for (int i = 0; i < dtVinculados.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvEstablecimientos.Rows.Count; j++)
                        {
                            if (dtVinculados.Rows[i]["CodigoEstablecimiento"].ToString() == dgvEstablecimientos.Rows[j].Cells["Codigo"].Value.ToString())
                            {
                                dgvEstablecimientos.Rows[j].Cells["Check"].Value = true;
                            }
                        }
                       
                    }
                }


            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvEstablecimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0;  i < dgvEstablecimientos.Rows.Count  ; i++)
            {
                dgvEstablecimientos.Rows[i].Cells["Check"].Value = false;
            }

            dgvEstablecimientos.CurrentRow.Cells["Check"].Value = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dgvEstablecimientos.CurrentRow.Cells["Check"].Value) == true)
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Actualizar_EstablecimientosAnexados_Serie(serie, tipoguia, empresa, dgvEstablecimientos.CurrentRow.Cells["Codigo"].Value.ToString(), dgvEstablecimientos.CurrentRow.Cells["Descripcion"].Value.ToString()))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }
            catch (Exception ex)
            { 
              MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
