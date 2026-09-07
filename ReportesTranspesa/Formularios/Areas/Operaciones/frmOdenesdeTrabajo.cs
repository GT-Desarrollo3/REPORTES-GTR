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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmOdenesdeTrabajo : Form
    {
        string UsuarioModulo;
        public frmOdenesdeTrabajo()
        {
            InitializeComponent();
        }

        private void frmOdenesdeTrabajo_Load(object sender, EventArgs e)
        {
            gvDATAOTS.OptionsBehavior.Editable = false;
            
            ConsultaUsuarioSpringxWindows();
            if (UsuarioModulo == null)
            {
                UsuarioModulo = Utilitario.Instancia.SesionUsuario.usuario;
            }
            ListarOts();
        }

        private void ListarOts()
        {
            DataTable dtotsdis = new DataTable();
            dtotsdis = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOtsDisponibles();

            if (dtotsdis.Rows.Count > 0)
            {
                dgvListarOts.DataSource = dtotsdis;
                dgvListarOts.Focus();
            }
        }

        string OTselec;
        string fecha;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int[] filas = gvDATAOTS.GetSelectedRows();
                string datoseleccionado = gvDATAOTS.GetFocusedValue().ToString();

                for (int i = 0; i < filas.Length; i++)
                {
                     OTselec = gvDATAOTS.GetRowCellValue(filas[i], "OT").ToString();
                     fecha = gvDATAOTS.GetRowCellValue(filas[i], "FECHAINICIO").ToString();

                    if (Convert.ToInt32(OTselec) > 0)
                    {
                        OTselec = gvDATAOTS.GetRowCellValue(filas[i], "OT").ToString();

                        groupBox1.Visible = true;
                        groupBox1.Text = "Modificar Fecha de OT: " + OTselec;
                        label1.Text = "OT: " + OTselec;
                        label4.Text = fecha;
                        
                    }
                }
            }
            catch (Exception)
            {
               
            }
        }

        void ConsultaUsuarioSpringxWindows()
        {
            DataTable dtUserWind = new DataTable();
            dtUserWind = clsUsuarioBL.Instancia.GetListaUsuariosModulo(Utilitario.Instancia.SesionUsuario.usuario);
            for (int i = 0; i < dtUserWind.Rows.Count; i++)
            {
                UsuarioModulo = dtUserWind.Rows[i]["Usuario"].ToString();
                //  MessageBox.Show("tu Usuario spring es "+UsuarioModulo);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            DataTable dtFechaOts = new DataTable();
            string Respuesta;
            dtFechaOts = clsOperacionesBL.Instancia.GetOperaciones_ActualizarFechaOts(1,OTselec, dateTimePicker1.Text, UsuarioModulo, fecha);
            Respuesta = Convert.ToString(dtFechaOts.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarOts();     
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
              
                int[] filas = gvDATAOTS.GetSelectedRows();
               

                for (int i = 0; i < filas.Length; i++)
                {
                    OTselec = gvDATAOTS.GetRowCellValue(filas[i], "OT").ToString();    

                    if (Convert.ToInt32(OTselec) > 0)
                    {
                        OTselec = gvDATAOTS.GetRowCellValue(filas[i], "OT").ToString();
                        fecha = gvDATAOTS.GetRowCellValue(filas[i], "FECHAINICIO").ToString();

                        if (MessageBox.Show("Desea completar la OT: " + OTselec + "?", "COMPLETAR OT", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DataTable dtFechaOts = new DataTable();
                            string Respuesta;
                            dtFechaOts = clsOperacionesBL.Instancia.GetOperaciones_ActualizarFechaOts(2,OTselec, dateTimePicker1.Text, UsuarioModulo, fecha);
                            Respuesta = Convert.ToString(dtFechaOts.Rows[0]["exito"]);
                            string NroRPTA = Respuesta.Substring(0, 1);
                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ListarOts();
                            }
                            else
                            {
                                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }          
                        }
                        //else                                                
                        //{
                        //   // MessageBox.Show("Completado cancelado...!");
                        //}
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvListarOts.DataSource == null)
                {
                    MessageBox.Show("No hay data para exportar", "AVISO");
                }
                else
                {

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte_OT.xlsx");
                    gvDATAOTS.ExportToXlsx(nombre);
                    System.Diagnostics.Process.Start(nombre);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
