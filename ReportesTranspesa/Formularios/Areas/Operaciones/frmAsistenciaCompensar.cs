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
    
    public partial class frmAsistenciaCompensar : Form
    {
        private frmAsistenciaRegularizaDiaCompensar frmAsistenciaRegularizaDiaCompensar;
        public frmAsistenciaCompensar()
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
        public string NombreImpresora;
        public Boolean Refrescar = false;
        private void frmAsistenciaCompensar_Load(object sender, EventArgs e)
        {

            DateTime date = DateTime.Now.AddMonths(-2);

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpDesde.Value = oPrimerDiaDelMes;


            lblnombre.Text = NombrePersona;
            txtFechaACompensar.Text = FechaCompensa;
            cargaFechasDisponibles(IDPersona,dtpDesde.Text,dtpHasta.Text);
        }
        private void cargaFechasDisponibles(int IDPersona,string FechaIni,string FechaFin)
        {
            DataTable dt;
            
            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasBuscarXCompensar("10000000", "CD", IDPersona, FechaIni, FechaFin);

            if (dt.Rows.Count > 0)
            {
                dgvTipoAsis.DataSource = dt;
                dgvTipoAsis.Columns["IDPersona"].Visible = false;
                this.dgvTipoAsis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            txtDiaQueCompesa.Text = dgvTipoAsis.CurrentRow.Cells["FechaDF"].Value.ToString().Substring(0,10);
            txtFechaLiberar.Text = dgvTipoAsis.CurrentRow.Cells["FechaDF"].Value.ToString().Substring(0, 10);
            if (dgvTipoAsis.CurrentRow.Cells["Compensado"].Value.ToString().Length!=0)
            {
                txtFechaCompensada.Text = dgvTipoAsis.CurrentRow.Cells["Compensado"].Value.ToString().Substring(0, 10);
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(txtDiaQueCompesa.Text.Length==0)
            {
                MessageBox.Show("Debe seleccionar una Fecha a Compensar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasCompensar("10000000", Periodo, Planilla, IDPersona, txtDiaQueCompesa.Text, 32, txtFechaACompensar.Text, Utilitario.Instancia.SesionUsuario.usuario);
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

        private void button2_Click(object sender, EventArgs e)
        {
            cargaFechasDisponibles(IDPersona, dtpDesde.Text, dtpHasta.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtFechaCompensada.Text.Length ==0)
            {
                MessageBox.Show("Debe seleccionar una Fecha Compensada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasLiberarCompensacion("10000000", Planilla, IDPersona, txtFechaLiberar.Text, txtFechaCompensada.Text, Utilitario.Instancia.SesionUsuario.usuario);
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

        private void button4_Click(object sender, EventArgs e)
        {
            frmAsistenciaRegularizaDiaCompensar = new frmAsistenciaRegularizaDiaCompensar();
            frmAsistenciaRegularizaDiaCompensar.IDPersona = IDPersona;
            frmAsistenciaRegularizaDiaCompensar.NombrePersona = NombrePersona;
            frmAsistenciaRegularizaDiaCompensar.Planilla = Planilla;
            frmAsistenciaRegularizaDiaCompensar.ShowDialog();
            if (frmAsistenciaRegularizaDiaCompensar.Refrescar == true)
            { cargaFechasDisponibles(IDPersona, dtpDesde.Text, dtpHasta.Text);}
        }

        private void button5_Click(object sender, EventArgs e)
        {          

            DataTable dtConsultarImpresora = new DataTable();
            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
            NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

            if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
            {
                MessageBox.Show("No tiene Impresora asignada");
                return;
            }

            Imprimir();

        }
        private void Imprimir()
        {

            Ticket ticket = new Ticket();
            //ticket.HeaderImage = picturebox1.Image;
            ticket.AddHeaderLine("GRUPO TRANSPESA");
            ticket.AddHeaderLine("COMPENSACIONES");
            ticket.AddSubHeaderLine2("COD.COND.: " + IDPersona + "                         ");
            ticket.AddSubHeaderLine("NOMBRE:" + NombrePersona);
            ticket.AddSubHeaderLine("FH.IMPR.: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket.AddSubHeaderLine("USUARIO: " + Utilitario.Instancia.SesionUsuario.usuario);
            ticket.AddSubHeaderLine("DEL "+dtpDesde.Text + " AL "+dtpHasta.Text);
            ticket.AddSubHeaderLine("DIA|FECHA DF|ESTADO|COMPENSADO");

            foreach (DataGridViewRow dgvRenglon in dgvTipoAsis.Rows)
            {
                if(dgvRenglon.Cells[4].Value.ToString().Length==0)
                {
                    ticket.AddSubHeaderLine(dgvRenglon.Cells[1].Value.ToString().Substring(0, 3) + " " + dgvRenglon.Cells[2].Value.ToString().Substring(0, 10) + " " + dgvRenglon.Cells[3].Value.ToString() + " " + dgvRenglon.Cells[4].Value.ToString().Substring(0, 0));
                }
                else
                {
                    ticket.AddSubHeaderLine(dgvRenglon.Cells[1].Value.ToString().Substring(0, 3) + " " + dgvRenglon.Cells[2].Value.ToString().Substring(0, 10) + " " + dgvRenglon.Cells[3].Value.ToString() + " " + dgvRenglon.Cells[4].Value.ToString().Substring(0, 10));
                }
                           
            }

            // ticket.AddFooterLine(pistola + " : " + totalizador);
            ticket.AddFooterLine("");
            // ticket.AddFooterLine(Frase01);
            ticket.AddFooterLine("    ** VIAJA CON CUIDADO **");
            ticket.PrintTicket(NombreImpresora);
            
          //  ticket.PrintTicket("POS-80-SeriesCompensa");

            /***ACTUALIZAR EL CAMPO IMPRESO EN LA TABLA PREVIAJES******/
          
                MessageBox.Show("Ticket Informe Impreso....!");
           
        }

    }
}
