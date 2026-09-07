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
    public partial class frmAgregarDescarga : Form
    {
        int _codProgramacion;
        int _Accion;
        string _anio;
        public bool esFechaTermino = false;
        public frmAgregarDescarga()
        {
            InitializeComponent();
        }

        public void envioviaje(int var_codProgramacion, int Accion, string anio)
        {

            _codProgramacion = var_codProgramacion;
            _Accion = Accion;

            _anio = anio;

        }

        private void frmAgregarDescarga_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            if (esFechaTermino)
            {
                label1.Text = "FECHA DE TERMINO DE VIAJE";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (esFechaTermino == false)
            {
                string rspta;
                DataTable dtPeso = new DataTable();
                dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(12, _codProgramacion, 0, dateTimePicker1.Text, _anio);
                rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                string NroRPTA = rspta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                    f1.Val_Respuesta = "1";
                    this.Close();
                }
                else
                {
                    MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarFechaTermino(_codProgramacion, _anio, dateTimePicker1.Text))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                    f1.Val_Respuesta = "1";
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }
    }
}
