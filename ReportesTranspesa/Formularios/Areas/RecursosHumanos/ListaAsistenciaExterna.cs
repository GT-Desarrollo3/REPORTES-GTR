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
    public partial class ListaAsistenciaExterna : Form
    {
       

        public ListaAsistenciaExterna()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                cargarLista();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarLista()
        {
         DataTable dtLista =   clsRecursosHumanosBL.Instancia.ReportesApp_ListarAsistenciaExterna(dtpFechaInicio.Text, dtpFechaFin.Text);
         dtgLista.DataSource = dtLista;
         if (dtLista.Rows.Count > 0)
         {
             dgvListaVista.Columns["Id"].Visible = false;
             dgvListaVista.Columns["idPersona"].Visible = false;
             dgvListaVista.Columns["Fecha"].Visible = false;
             dgvListaVista.BestFitColumns();
        

         }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                AsistenciaExterna asistenciaExterna = new AsistenciaExterna();
                if (asistenciaExterna.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    cargarLista();
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);;
            }
        }

   
    }
}
