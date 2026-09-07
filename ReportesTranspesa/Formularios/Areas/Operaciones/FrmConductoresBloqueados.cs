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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmConductoresBloqueados : Form
    {
        public FrmConductoresBloqueados()
        {
            InitializeComponent();
        }

        private void FrmConductoresBloqueados_Load(object sender, EventArgs e)
        {
            ListaConductoresBloqueados();
        }

        private void ListaConductoresBloqueados()
        {
            DataTable dtDatos = new DataTable();

            dtDatos = clsOperacionesBL.Instancia.Obtener_Lista_ConductoresBloqueados();

            if (dtDatos.Rows.Count > 0)
            {
                dgvListaConductores.DataSource = dtDatos;

                lblTotalCBloqueados.Text = "TOTAL: " + dtDatos.Rows.Count;
            
                if (dgvListaConductores.Rows.Count  > 0)
                {
                    dgvListaConductores.Columns["IdPersona"].Visible = true;
                    dgvListaConductores.Columns["UsuarioBloquea"].Visible = true;
                    dgvListaConductores.Columns["Conductor"].Width = 400;
                    dgvListaConductores.Columns["MotivoBloqueo"].Width = 350;

                    dgvListaConductores.Columns["MotivoBloqueo"].HeaderText = "Motivo Bloqueo";
                    dgvListaConductores.Columns["UsuarioBloquea"].HeaderText = "Usuario Bloquea";
                    dgvListaConductores.Columns["FHBloquea"].HeaderText = "Fecha y Hora Bloquea";                   

                }
            }

        }





    }
}
