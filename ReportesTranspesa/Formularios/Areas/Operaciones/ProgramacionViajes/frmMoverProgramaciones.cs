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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmMoverProgramaciones : Form
    {
        int _idProgramacion;       
        int _tipoProgramacion, tipoPrograMover;
        string _Programacion;
        DataTable dtTipoProg = new DataTable();
        public frmMoverProgramaciones()
        {
            InitializeComponent();
        }

        public void enviarDatos(int var_codProgramacion, int tipoprogramacion, string Programacion)
        {
             _idProgramacion = var_codProgramacion;              
             _tipoProgramacion = tipoprogramacion;
             _Programacion = Programacion;
        }

        private void frmMoverProgramaciones_Load(object sender, EventArgs e)
        {
            dtTipoProg = clsOperacionesBL.Instancia.GetOperacionesMover();           

            cboProgramaciones.DisplayMember = "Descripcion";
            cboProgramaciones.ValueMember = "IdOperacion";
            cboProgramaciones.DataSource = dtTipoProg;
            lblCodigo.Text= "Código: " + _idProgramacion;
            lblProgramacion.Text = "Programación Actual: " + _Programacion;            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // tipoPrograMover = cboProgramaciones.SelectedIndex + 1;
            tipoPrograMover = Convert.ToInt32(cboProgramaciones.SelectedValue);
            if (_tipoProgramacion == tipoPrograMover)
            {
                MessageBox.Show("La programación seleccionada es la misma", "Alerta");
                return;
            }

            DataTable dtMoverProgramacion = new DataTable();
            dtMoverProgramacion = clsOperacionesBL.Instancia.GetMoverProgramaciones(_idProgramacion, _tipoProgramacion, tipoPrograMover, Utilitario.Instancia.SesionUsuario.usuario);

            string Rpta = Convert.ToString(dtMoverProgramacion.Rows[0]["exito"]);
            string NrRPTA = Rpta.Substring(0, 1);
            if (NrRPTA == "0")
            {
                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /*frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                f1.Val_Respuesta = "1";*/
                this.Close();
            }
            else
            {
                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}
