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
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmAsistenciaCompensarNoche : Form
    {
        public frmAsistenciaCompensarNoche()
        {
            InitializeComponent();
        }
        public int IDPersona;
        public string NombrePersona;
        public string Planilla;
        public int Dia;
        public string Periodo;
        public string FechaCompensa;
        public string XmlAsistencias;
        public Boolean Refrescar = false;
        private void frmAsistenciaCompensarNoche_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.AddMonths(-2);

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpDesde.Value = oPrimerDiaDelMes;


            lblnombre.Text = NombrePersona;
            txtFechaACompensar.Text = FechaCompensa;
            cargaFechasDisponibles(IDPersona, dtpDesde.Text, dtpHasta.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargaFechasDisponibles(IDPersona, dtpDesde.Text, dtpHasta.Text);
        }
        private void cargaFechasDisponibles(int IDPersona, string FechaIni, string FechaFin)
        {
            DataTable dt;

            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasBuscarXCompensarNoche("10000000", "CD", IDPersona, FechaIni, FechaFin);

            if (dt.Rows.Count > 0)
            {
                dgvItems.DataSource = dt;
                dgvItemsView.Columns["IDPersona"].Visible = false;
            }
            else
            {
                MessageBox.Show("No hay Datos", "Aviso");
            }
        }

        private void dgvItems_Click(object sender, EventArgs e)
        {

            Decimal Suma=0;

            int[] filas = dgvItemsView.GetSelectedRows();
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    Suma = Suma + Convert.ToDecimal(dgvItemsView.GetRowCellValue(filas[i], "CantidadNoche").ToString());
                    txtCantidadNoches.Text = Suma.ToString();
                  

                    //DataTable dtRespuesta = new DataTable();
                    //string Respuesta;
                    //dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(Convert.ToInt32(CodHta), Convert.ToInt32(Persona), Utilitario.Instancia.SesionUsuario.usuario, 1);
                    //Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    //string NroRPTA = Respuesta.Substring(0, 1);
                    //if (NroRPTA == "0")
                    //{
                    //    txtNombreHerramienta.Text = Respuesta;
                    //}
                    //else
                    //{
                    //    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    break;
                    //}
                }
                //cargarHerramientas();
                //buscar(Convert.ToInt32(txtNumero.Text));
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCantidadNoches.Text = "0.00";
                return;
            }
        }
        string xmlNoches;
        private void button1_Click(object sender, EventArgs e)
        {

            string Planilla;
            Planilla = "CD";

            if(txtCantidadNoches.Text!="1.00")
            {
                MessageBox.Show("Debe completar 1.00 Noche para poder compensar. Revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<r>");

            int[] filas = dgvItemsView.GetSelectedRows();
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    string dia;
                    dia = Convert.ToDateTime(dgvItemsView.GetRowCellValue(filas[i], "Fecha").ToString()).ToString("dd/MM/yyyy");//dtpPeriodo.Text.Substring(dtpPeriodo.Text.Length - 2, 2) + "/" + dtpPeriodo.Text.Substring(0, 4);
                    sb.Append("<d ");
                    sb.Append("IDPersona=\"");
                    sb.Append(IDPersona); //sb.Append(dgvAsistenciaView.SelectedCells[i].RowIndex.ToString());
                    sb.Append("\"");
                    sb.Append(" Fecha=\"");
                    sb.Append(dia);
                    sb.Append("\"");
                    sb.Append(" />");
                }
                sb.Append("</r>");
                //sb.Append("Total: " + selectedCellCount.ToString());
                //MessageBox.Show(sb.ToString(), "Selected Cells");
                xmlNoches = sb.ToString();
                //MessageBox.Show(xmlNoches);

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistencias_Noches_Compensar("10000000", Periodo, Planilla, IDPersona,xmlNoches, txtFechaACompensar.Text, Utilitario.Instancia.SesionUsuario.usuario);
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
            else
            {
                MessageBox.Show("No ha seleccionado ningún registro. No puede Compensar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Planilla;
            Planilla = "CD";
            
            DialogResult result = MessageBox.Show("La Fecha que se liberará es :"+txtFechaACompensar.Text + ". Seguro de continuar?", "Atención", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistencias_Noches_Liberar("10000000", Planilla, IDPersona, txtFechaACompensar.Text, Utilitario.Instancia.SesionUsuario.usuario);
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

        private void button5_Click(object sender, EventArgs e)
        {

            Imprimir();
        }
        private void Imprimir()
        {

            Ticket ticket = new Ticket();
            //ticket.HeaderImage = picturebox1.Image;
            ticket.AddHeaderLine("GRUPO TRANSPESA");
            ticket.AddHeaderLine("CONTROL-NOCHE");
            ticket.AddSubHeaderLine2("COD.COND.: " + IDPersona + "                         ");
            ticket.AddSubHeaderLine("NOMBRE:" + NombrePersona);
            ticket.AddSubHeaderLine("FH.IMPR.: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket.AddSubHeaderLine("USUARIO: " + Utilitario.Instancia.SesionUsuario.usuario);
            ticket.AddSubHeaderLine("DEL " + dtpDesde.Text + " AL " + dtpHasta.Text);
            ticket.AddSubHeaderLine("NOCHE|FECHA|CANTIDAD|D.COMPENSA");

            int filas = dgvItemsView.RowCount;
            if (filas!= 0)
            {
                for (int i = 0; i < filas; i++)
                {
                    if (dgvItemsView.GetRowCellValue(i, "Compensado").ToString().Length == 0)
                    {
                        ticket.AddSubHeaderLine(dgvItemsView.GetRowCellValue(i, "Noche").ToString().Substring(0, 3) + " " + dgvItemsView.GetRowCellValue(i, "Fecha").ToString().Substring(0, 10) + " " + dgvItemsView.GetRowCellValue(i, "CantidadNoche").ToString() + " " + dgvItemsView.GetRowCellValue(i, "Compensado").ToString());
                    }
                    else
                    {
                        ticket.AddSubHeaderLine(dgvItemsView.GetRowCellValue(i, "Noche").ToString().Substring(0, 3) + " " + dgvItemsView.GetRowCellValue(i, "Fecha").ToString().Substring(0, 10) + " " + dgvItemsView.GetRowCellValue(i, "CantidadNoche").ToString() + " " + dgvItemsView.GetRowCellValue(i, "Compensado").ToString().Substring(0, 10));
                    }
                }

                // ticket.AddFooterLine(pistola + " : " + totalizador);
                ticket.AddFooterLine("");
                // ticket.AddFooterLine(Frase01);
                ticket.AddFooterLine("    ** VIAJA CON CUIDADO **");
                ticket.PrintTicket("POS-80-Series");

                /***ACTUALIZAR EL CAMPO IMPRESO EN LA TABLA PREVIAJES******/

                MessageBox.Show("Ticket Informe Impreso....!");

            }

        }

        private void turnoPagadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
         
            String Respuesta  = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Nro de Recibo de Pago", "Nro Recibo");
            if (Respuesta.Length > 50)
            {
                MessageBox.Show("Usted a ingresado demaciado caracteres,", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Respuesta.Length > 0)
            {

                DateTime fecha = Convert.ToDateTime(dgvItemsView.GetRowCellValue(dgvItemsView.FocusedRowHandle, "Fecha"));
                DataTable dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_EliminarAsistenciaNoche(IDPersona, fecha.ToShortDateString(), Utilitario.Instancia.SesionUsuario.usuario, Respuesta);
                string Respuesta2;

                Respuesta2 = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Refrescar = true;
                    cargaFechasDisponibles(IDPersona, dtpDesde.Text, dtpHasta.Text);
               
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
