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
using System.Globalization;
using System.Diagnostics;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmDesbloquarRutaXConductor : Form
    {
        public DataTable dtConductores;
        public DataTable dtConductoresFiltro;
        public DataTable dtRutas;
        public DataTable dtRutaFiltro;

        public List<string> SelectRutas = new List<string>();
        public List<string> SelectConductores = new List<string>();
        public string nombreConductor = string.Empty;
        public string dniPersona = string.Empty;
        public int idconductor = 0;
        public int permiso;
        public frmDesbloquarRutaXConductor()
        {
            InitializeComponent();
           // txtBuscarConductor.KeyPress -= txtBuscarConductor_KeyPress;
        }

        private void frmDesbloquarRutaXConductor_Load(object sender, EventArgs e)
        {
            try
            {
                dtRutaFiltro = new DataTable();
                dtRutaFiltro.Columns.Add("idRuta", typeof(string));
                dtRutaFiltro.Columns.Add("Ruta", typeof(string));

                dtConductoresFiltro = new DataTable();
                dtConductoresFiltro.Columns.Add("idConductor", typeof(string));
                dtConductoresFiltro.Columns.Add("Nombres", typeof(string));
                dtConductoresFiltro.Columns.Add("DNI", typeof(string));

                DataTable dt = Utilitario.Instancia.ObtenerPermisosPorFormulario("Operaciones_Lista_Conductores");

                int permiso = Convert.ToInt32(dt.Rows[0]["Leer"]);

                if (permiso == 1)
                {
                    btnGuardar.Enabled = false;
                    quitarToolStripMenuItem.Enabled = false;
                }
    
                txtBuscarConductor.Text = nombreConductor;
                ListarConductores();
                ListarRutas();
                ListarRutasxConductor();
              

              /*if (dniPersona.Length > 0)
              {
                  int encontrado = 0;
                  for (int i = 0; i < dgvConductores.Rows.Count; i++)
                  {
                      string x = dgvConductores.Rows[i].Cells["documentoConductor"].Value.ToString().TrimEnd();
                      if (dgvConductores.Rows[i].Cells["documentoConductor"].Value.ToString().TrimEnd() == dniPersona.TrimEnd())
                      {
                          encontrado = i;
                      }
                      else
                      {
                          //dgvConductores.Rows.RemoveAt(0);
                          //dgvConductores.Rows.Remove(dgvConductores.Rows[i]);
                      }
                 
                   
                  }
              }*/

             // txtBuscarConductor.KeyPress += txtBuscarConductor_KeyPress;
              
              
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ListarRutas()
        {

            dtRutas = clsConsultaBL.Instancia.GetRutasActivas(txtBuscarRuta.Text);

            if (dtRutas == null)
            {
                return;
            }

            dgvRutas.Rows.Clear();


            if (dtRutas.Rows.Count > 0)
            {
                for (int i = 0; i < dtRutas.Rows.Count; i++)
                {
                    dgvRutas.Rows.Add(0,dtRutas.Rows[i]["ID"].ToString(), dtRutas.Rows[i]["Descripcion"].ToString());
                    
                }
                   
            }
            else
            {
                    dgvRutas.Rows.Clear();
            }


            if (SelectRutas.Count > 0)
            {
                for (int i = 0; i < dgvRutas.Rows.Count; i++)
                {
                    for (int j = 0; j < SelectRutas.Count; j++)
                    {
                        if (dgvRutas.Rows[i].Cells["Ruta"].Value.ToString() == SelectRutas[j])
                        {
                            dgvRutas.Rows[i].Cells["Check"].Value = true;
                        }
                    }

                }
            }

        }

        private void ListarConductores()
        {
            

            dtConductores = clsConsultaBL.Instancia.GetConductores(txtBuscarConductor.Text);

            if (dtConductores == null)
            {
                return;
            }

            dgvConductores.Rows.Clear();



            if (dtConductores.Rows.Count > 0)
            {
                for (int i = 0; i < dtConductores.Rows.Count; i++)
                {
                    dgvConductores.Rows.Add(0,dtConductores.Rows[i]["ID"].ToString(), dtConductores.Rows[i]["PERSONA"].ToString(), dtConductores.Rows[i]["DOCUMENTO"].ToString());
                }
            }
            else
            {
                dgvConductores.Rows.Clear();
            }

            if (SelectConductores.Count > 0)
            {
                for (int i = 0; i < dgvConductores.Rows.Count; i++)
                {
                    for (int j = 0; j < SelectConductores.Count; j++)
                    {
                        if (dgvConductores.Rows[i].Cells["Nombres"].Value.ToString() == SelectConductores[j])
                        {
                            dgvConductores.Rows[i].Cells["Check"].Value = true;
                        }
                    }

                }
            }

        }

        private void txtBuscarConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void dgvConductores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                if (Convert.ToBoolean(dgvConductores.CurrentRow.Cells["Check"].Value) == false)
                {
                    dgvConductores.CurrentRow.Cells["Check"].Value = true;
                    SelectConductores.Add(dgvConductores.CurrentRow.Cells["Nombres"].Value.ToString());
                    idconductor = Convert.ToInt32(dgvConductores.CurrentRow.Cells["idConductor"].Value);
                   
                }
                else
                {
                    dgvConductores.CurrentRow.Cells["Check"].Value = false;
                    SelectConductores.Remove(dgvConductores.CurrentRow.Cells["Nombres"].Value.ToString());
                 
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRutas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dgvRutas.CurrentRow.Cells["checkRuta"].Value) == false)
                {
                    dgvRutas.CurrentRow.Cells["checkRuta"].Value = true;
                    SelectRutas.Add(dgvRutas.CurrentRow.Cells["Ruta"].ToString());
                }
                else
                {
                    dgvRutas.CurrentRow.Cells["checkRuta"].Value = false;
                    SelectRutas.Remove(dgvRutas.CurrentRow.Cells["Ruta"].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string nombreConductor = string.Empty;

            for(int i = 0 ; i<dgvConductores.Rows.Count ; i++)
            {
                if(Convert.ToBoolean(dgvConductores.Rows[i].Cells["Check"].Value) == true)
                {
                    idconductor = Convert.ToInt32(dgvConductores.Rows[i].Cells["idConductor"].Value);
                    nombreConductor = dgvConductores.Rows[i].Cells["Nombres"].Value.ToString();
                    dtConductoresFiltro.Rows.Add(idconductor, nombreConductor, dgvConductores.Rows[i].Cells["documentoConductor"].Value.ToString());
                }

            }

            if (dtConductoresFiltro.Rows.Count == 0)
            {
                MessageBox.Show("Usted no ha seleccionado un conductor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            for(int i = 0 ; i < dgvRutas.Rows.Count ; i++)
            {
                if(Convert.ToBoolean(dgvRutas.Rows[i].Cells["checkRuta"].Value) == true)
                {
                    dtRutaFiltro.Rows.Add(dgvRutas.Rows[i].Cells["idRuta"].Value.ToString(),dgvRutas.Rows[i].Cells["Ruta"].Value.ToString());
                }
            }

            if(dtRutaFiltro.Rows.Count == 0)
            {
                MessageBox.Show("Usted no ha seleccionado ninguna ruta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string xmlRutas = Utilitario.Instancia.DatatableToXml(dtRutaFiltro);
            string xmlConductores = Utilitario.Instancia.DatatableToXml(dtConductoresFiltro);

            if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_GuardarRutasDesbloqueadas(xmlConductores, xmlRutas))
            {

                MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtRutaFiltro.Rows.Clear();
                dtConductoresFiltro.Rows.Clear();
              
                for (int i = 0; i < dgvRutas.Rows.Count; i++)
                {
                    dgvRutas.Rows[i].Cells["checkRuta"].Value = false;
                }
                for (int i = 0; i < dgvConductores.Rows.Count; i++)
                {
                    dgvConductores.Rows[i].Cells["Check"].Value = false;
                }
                SelectRutas.Clear();
                SelectConductores.Clear();
                ListarRutasxConductor();
                this.Cursor = Cursors.Default;


            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ListarRutasxConductor()
        {
            DataTable dt = clsOperacionesBL.Instancia.Reportesapp_Operaciones_ListarRutasDesbloqueadasXConductor();
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgVistaRutaXConductor.Columns["idRuta"].Visible = false;
                dtgVistaRutaXConductor.Columns["idConductor"].Visible = false;
                //dtgVistaRutaXConductor.BestFitColumns();
            }
            else
            {
                dtgvData.DataSource = null;
            }
        }


        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                Actualizar();
              
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Actualizar()
        {
            for (int i = 0; i < dgvConductores.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvConductores.Rows[i].Cells["Check"].Value) == true)
                {
                    idconductor = Convert.ToInt32(dgvConductores.Rows[i].Cells["idConductor"].Value);
                    break;
                }

            }

            DataTable dt = clsOperacionesBL.Instancia.Reportesapp_Operaciones_ListarRutasDesbloqueadasXConductor();
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgVistaRutaXConductor.Columns["idRuta"].Visible = false;
                dtgVistaRutaXConductor.Columns["idConductor"].Visible = false;
                dtgVistaRutaXConductor.BestFitColumns();
            }
            else
            {
                dtgvData.DataSource = null;
            }
        }

        private void txtBuscarRuta_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtBuscarConductor_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                ListarConductores();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void txtBuscarRuta_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                ListarRutas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_EliminarRutaXConductor(Convert.ToInt32(dtgVistaRutaXConductor.GetFocusedRowCellValue("idConductor")), Convert.ToInt32(dtgVistaRutaXConductor.GetFocusedRowCellValue("idRuta")))) 
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Actualizar();
                    
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
            
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Rutas por conductor" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
