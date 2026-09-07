using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using Comun;
using FMBUtilitario;
using Word = Microsoft.Office.Interop.Word;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.ConstanciaNoDeudo
{
    public partial class frmConstanciaNoDeudo : Form
    {
        private Word.Application objWord;
        private Object oMissing = System.Reflection.Missing.Value;
        string permisoMemos = "";
        string Cuerpo1 = "";
        string Cuerpo2 = "";
        public frmConstanciaNoDeudo()
        {
            InitializeComponent();
        }
        private frmConstanciaImprimir frmConstanciaImprimir;
        private void txtEmpleado_Click(object sender, EventArgs e)
        {

        }
       

        private void buscar(int op)
        {
            DataTable dt = new DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetDataRRHH_ConstanciaNoDeudo(op);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                button1.Enabled = false;
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                dtgvData.DataSource = null;
                button1.Enabled = true;
            }
        }

        private void frmConstanciaNoDeudo_Load(object sender, EventArgs e)
        {

        }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvEmpleado, clsConsultaBL.Instancia.GetPersona(txtEmpleado.Text), true, false, false);

                lvEmpleado.Columns[0].Width = 0;
                lvEmpleado.Columns[1].Width = 206;
                lvEmpleado.Columns[2].Width = 110;

                lvEmpleado.BringToFront();
                lvEmpleado.Visible = true;
                lvEmpleado.Focus();
                txtIdEmpleado.Text = "";
                button2.Enabled = false;
                button3.Enabled = false;
                //splitContainer1.SplitterDistance = lvEmpleado.Top + lvEmpleado.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                txtIdEmpleado.Text = "";
                button2.Enabled = false;
                button3.Enabled = false;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                //splitContainer1.SplitterDistance = 77;
                buscar(0);
            }
        }

        private void lvEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvEmpleado.SelectedItems[0];
                txtOperacion.Text = "";
                DataTable dt  =  clsRecursosHumanosBL.Instancia.ReportesApp_Operacion_OperacionxConductor(Convert.ToInt32(ItemActual.Text));
                if(dt.Rows.Count > 0){

                    txtOperacion.Text = dt.Rows[0]["Descripcion"].ToString();
                }
                
                txtIdEmpleado.Text = ItemActual.Text;
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                txtNumero.Text = txtIdEmpleado.Text;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                //splitContainer1.SplitterDistance = 77;
                if (txtIdEmpleado.Text.Length > 0)
                {
                    if (txtIdEmpleado.Text.Length > 0)
                    {
                        button2.Enabled = true;
                        button3.Enabled = true;
                    }
                    else
                    {
                        button2.Enabled = false;
                        button3.Enabled = false;
                    }

                    buscar(Convert.ToInt32(txtIdEmpleado.Text));
          
                }
                else
                {
                    MessageBox.Show("Usted no ha seleccionado correctamente al empleado", "Mensaje");
                    return;
                }
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                txtIdEmpleado.Text = "";
                button2.Enabled = false;
                button3.Enabled = false;
                //splitContainer1.SplitterDistance = 77;
            }
        }

        private void lvEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lvEmpleado.Items.Count.Equals(0))
            {
                lvEmpleado.Items[0].Selected = true;
            }
        }

        private void txtEmpleado_TextChanged(object sender, EventArgs e)
        {
            int length = txtEmpleado.Text.Length;
            if (length == 0)
            {
                buscar(0);
                txtIdEmpleado.Text = "";
                txtNumero.Text = "";
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbCese.Checked == false && rbVacaciones.Checked == false)
            {
                MessageBox.Show("Debe seleccionar un Motivo por Cese o Vacaciones", "Mensaje");
                return;
            }

            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetDataListarUsuariocrearMemos(Utilitario.Instancia.SesionUsuario.usuario);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    permisoMemos = Convert.ToString(dt.Rows[i]["carpeta"].ToString());
                    Cuerpo1 = Convert.ToString(dt.Rows[i]["CuerpoConstanciaNODEUDO1"].ToString());
                    Cuerpo2 = Convert.ToString(dt.Rows[i]["CuerpoConstanciaNODEUDO2"].ToString());
                }
            }

            if (permisoMemos.Equals(""))
            {
                MessageBox.Show("No tiene ruta definida para guardar documentos, Comunicarte con Sistemas", "Aviso");
                return;
            }
            else
            {
                AgregarDocumento();
            }

            AgregarDocumento();
        }
        public void AgregarDocumento()
        {
            
                if (frmConstanciaImprimir == null || frmConstanciaImprimir.IsDisposed)
                {
                    frmConstanciaImprimir = new frmConstanciaImprimir();
                    frmConstanciaImprimir.CargarDoc += new frmConstanciaImprimir.CargarDocEventHandler(PrepararDoc);

                    if (rbCese.Checked == true)
                    {
                        frmConstanciaImprimir.Asuntodoc = "Constancia de No Adeudo por Cese";
                    }
                    if (rbVacaciones.Checked == true)
                    {
                        frmConstanciaImprimir.Asuntodoc = "Constancia de No Adeudo por Vacaciones";
                    }

                    frmConstanciaImprimir.Cuerpodoc = Cuerpo1 + txtEmpleado.Text+ Cuerpo2;

                    //frmMemos_Compromisos.MdiParent = this.ParentForm;
                    //frmMemos_Compromisos.Tipodoc = Tipodoc;
                    //frmMemos_Compromisos.Asuntodoc = Asuntodoc;
                    //frmMemos_Compromisos.Fechadoc = Fechadoc;
                    //frmMemos_Compromisos.Cuerpodoc = Cuerpodoc;
                    frmConstanciaImprimir.Show();
                }
                else
                {
                    frmConstanciaImprimir.Activate();
                }
            
        }
        public void PrepararDoc(Boolean EsCorrecto)
        {
            if (EsCorrecto)
            {
                 string filename = "";
                int correlativo;
                //int contador;
                string empleado = "";
                string saludo = "";
                bool resultado;
               
                correlativo = clsRecursosHumanosBL.Instancia.GetCorrelativoNoDeudo("C") + 1;


                filename = "T:\\MEMORAMDUM\\" + permisoMemos + "\\Constancia No Adeudo ";
              
                   
                    empleado = "Sr(a). " + txtEmpleado.Text.Replace(",","").ToString();
                    saludo = "Estimado(a) " + empleado;

                    generaPDFDocumento(
                        filename + (correlativo).ToString() + " " + txtEmpleado.Text.Replace(",", "").ToString() + ".pdf",
                        (correlativo).ToString(),
                        frmConstanciaImprimir.Tipodoc,
                        empleado,
                        "",//Cargo
                        frmConstanciaImprimir.Asuntodoc,
                        frmConstanciaImprimir.Fechadoc,
                        saludo,
                        frmConstanciaImprimir.Cuerpodoc,
                        frmConstanciaImprimir.Firma);

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ConstanciaNoDeudo_Registrar(correlativo,
                    Convert.ToInt32(txtIdEmpleado.Text),
                    frmConstanciaImprimir.Tipodoc, frmConstanciaImprimir.Asuntodoc,
                    frmConstanciaImprimir.Fechadoc, frmConstanciaImprimir.Cuerpodoc, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //DialogResult dialogResult = MessageBox.Show("Se generaron 1 documentos satisfactoriamente. ¿Desea imprimirlos?", "Confirmación", MessageBoxButtons.YesNo);
                        //if (dialogResult == DialogResult.Yes)
                        //{

                        //    ImprimirPDF(filename + (correlativo).ToString() + " " + txtEmpleado.Text.Replace(",", "").ToString() + ".pdf");
                            
                        //}
                        
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
                
                
            }
            else { dtgvDataView.ClearSelection(); }
        }

        private string generaPDFDocumento(string filename, string correlativo, string tipodoc, string empleado, string cargo,
            string asunto, string fecha, string saludo, string cuerpo, bool firma)
        {
            try
            {
                foreach (Process proceso in Process.GetProcesses())
                    if (proceso.ProcessName.ToLower().CompareTo("winword") == 0)
                        proceso.Kill();

                string plantilla;
                string Reporte = "C:\\Transpesa\\Plantillas\\reporte.docx";

             

                    if (firma == true)
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Constancia_nodeudo.docx";
                    }
                    else
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Constancia_nodeudo.docx";
                    }
                
               
                ///////////////////////////////////////////

                System.IO.File.Copy(plantilla, Reporte, true);

                objWord = new Word.Application();
                objWord.Documents.Open(Reporte, oMissing, oMissing);

                #region Markers

                object Correlativo = "Correlativo";
                object Empleado = "Empleado";
                object Cargo = "Cargo";
                object Asunto = "Asunto";
                object Fecha = "Fecha";
                object Saludo = "Saludo";
                object Cuerpo = "Cuerpo";

                if (objWord.ActiveDocument.Bookmarks.Count > 0)
                {
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Correlativo).Range.Text = correlativo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Empleado).Range.Text = empleado;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Cargo).Range.Text = cargo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Asunto).Range.Text = asunto;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Fecha).Range.Text = fecha;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Saludo).Range.Text = saludo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Cuerpo).Range.Text = cuerpo;
                }

                #endregion

                objWord.ActiveDocument.Save();

                Export.ToPDF(objWord, filename, false, true);

                Process.Start(filename);

                return filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }

          
        }
        private void ImprimirPDF(string ruta)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = ruta;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string Motivo="";
            if (rbCese.Checked == false && rbVacaciones.Checked == false)
            {
                MessageBox.Show("Debe seleccionar un Motivo por Cese o Vacaciones", "Mensaje");
                return;
            }

            if (txtIdEmpleado.Text=="")
            {
                MessageBox.Show("Debe seleccionar un empleado", "Mensaje");
                return;
            }


            if (rbCese.Checked == true)
            {
                Motivo = "Cese";
            }

            if (rbVacaciones.Checked == true)
            {
                Motivo = "Vacaciones";
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ConstanciaNoDeudo_Alertar_Pendientes(1, Convert.ToInt32(txtIdEmpleado.Text), Motivo, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Motivo = "";
            if (rbCese.Checked == false && rbVacaciones.Checked == false)
            {
                MessageBox.Show("Debe seleccionar un Motivo por Cese o Vacaciones", "Mensaje");
                return;
            }

            if (txtIdEmpleado.Text == "")
            {
                MessageBox.Show("Debe seleccionar un empleado", "Mensaje");
                return;
            }


            if (rbCese.Checked == true)
            {
                Motivo = "Cese";
            }

            if (rbVacaciones.Checked == true)
            {
                Motivo = "Vacaciones";
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ConstanciaNoDeudo_Alertar_Pendientes(2, Convert.ToInt32(txtIdEmpleado.Text), Motivo, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
