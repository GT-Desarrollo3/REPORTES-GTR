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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmAsistenciaModificar : Form
    {
        public frmAsistenciaModificar()
        {
            InitializeComponent();
        }
        public int IDPersona;
        public string NombrePersona;
        public string Planilla;
        public int Dia;
        public string Periodo;
        public string XmlAsistencias;
        public Boolean Refrescar = false;
        private void frmAsistenciaModificar_Load(object sender, EventArgs e)
        {
            lblnombre.Text = NombrePersona;
            cargaTipoAsis();
        }
        private void cargaTipoAsis()
        {
            DataTable dt;

            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasTipoCargar();

            if (dt.Rows.Count > 0)
            {
                dgvTipoAsis.DataSource = dt;
                dgvTipoAsis.Columns["IdTipoAsist"].Visible = false;
                dgvTipoAsis.Columns["ConceptoAcceso"].Visible = false;
            }
            else
            {
                MessageBox.Show("No hay Datos", "Aviso");
            }
        }

        private void dgvTipoAsis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvTipoAsis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasRegistrar("10000000", Periodo, Planilla, XmlAsistencias, Convert.ToInt32(txtCodigo.Tag), Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Refrescar = true;
                Close();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvTipoAsis_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTipoAsis.Columns["IdTipoAsist"] != null)
            {
                if (e.RowIndex != -1)
                {
                    string IdTipoAsist = dgvTipoAsis.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string Descripcion = dgvTipoAsis.Rows[e.RowIndex].Cells[1].Value.ToString();

                    txtCodigo.Tag = IdTipoAsist;
                    txtCodigo.Text = Descripcion;

                }
            }
        }
    }
}
