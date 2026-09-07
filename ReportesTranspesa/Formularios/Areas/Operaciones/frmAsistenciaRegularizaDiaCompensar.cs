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
    public partial class frmAsistenciaRegularizaDiaCompensar : Form
    {
        public frmAsistenciaRegularizaDiaCompensar()
        {
            InitializeComponent();
        }
        public int IDPersona;
        public string NombrePersona;
        public string Planilla;
        public Boolean Refrescar = false;
        private void frmAsistenciaRegularizaDiaCompensar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasRegularizaFechaParaCompensar("10000000", Convert.ToDateTime(dtpFecha.Text).ToString("MMyyyy"), Planilla, IDPersona, dtpFecha.Text, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Refrescar = true;
                Close();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
