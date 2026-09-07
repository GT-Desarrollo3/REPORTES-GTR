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
    public partial class frmAsistenciasCompensarAdelantado : Form
    {
        public int IDPersona;
        public string NombrePersona;
        public string Planilla;
        public int Dia;
        public string Periodo;
        public string FechaCompensa;
        public string XmlAsistencias;
        public string NombreImpresora;
        public Boolean Refrescar = false;

        public frmAsistenciasCompensarAdelantado()
        {
            InitializeComponent();
        }

        private void frmAsistenciasCompensarAdelantado_Load(object sender, EventArgs e)
        {
            lblnombre.Text = NombrePersona;
            txtFechaACompensar.Text = FechaCompensa;
            comboBox1.SelectedIndex = 0;
            dtpFechaAsiste.Value = Convert.ToDateTime(FechaCompensa).AddDays(1);

            DateTime date = DateTime.Now.AddMonths(-2);
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpFechaIni.Value = oPrimerDiaDelMes;

            cargaFechasAdelantadas(IDPersona, dtpFechaIni.Text, dtpFechaFin.Text);
        }

        private void cargaFechasAdelantadas(int IDPersona, string FechaIni, string FechaFin)
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                DataTable dt;
                dt = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_BuscarCompensacionAdelantada("10000000", "CD", IDPersona, FechaIni, FechaFin);

                if (dt.Rows.Count > 0)
                {
                    dgvTipoAsis.DataSource = dt;
                    dgvTipoAsis.Columns["IDPersona"].Visible = false;
                    this.dgvTipoAsis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                else { MessageBox.Show("No hay Datos", "Aviso"); }
            }
        }

        private void Imprimir()
        {
            Ticket ticket = new Ticket();
            //ticket.HeaderImage = picturebox1.Image;
            //ticket.AddHeaderLine("GRUPO TRANSPESA");
            ticket.AddHeaderLine("COMPENSACIÓN ADELANTADA");
            ticket.AddSubHeaderLine2("COD.COND.: " + IDPersona + "                         ");
            ticket.AddSubHeaderLine("NOMBRE:" + NombrePersona);
            ticket.AddSubHeaderLine("FH.IMPR.: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket.AddSubHeaderLine("USUARIO: " + Utilitario.Instancia.SesionUsuario.usuario);
            ticket.AddSubHeaderLine("COMPENSACIÓN ADELANTADA DEL " + txtFComp.Text + " POR EL " + txtFAsis.Text);
            ticket.AddFooterLine("");
            ticket.AddFooterLine("    ** VIAJA CON CUIDADO **");
            ticket.PrintTicket(NombreImpresora);

            /***ACTUALIZAR EL CAMPO IMPRESO EN LA TABLA PREVIAJES******/

            MessageBox.Show("Ticket Informe Impreso....!");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtFechaACompensar.Text) >= dtpFechaAsiste.Value)
            {
                MessageBox.Show("La Fecha a compensar no debe ser mayor a la fecha de asistencia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaAsiste.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistencias_CompensacionAdelantada_Registrar("10000000", Periodo, Planilla, IDPersona, FechaCompensa, dtpFechaAsiste.Text, comboBox1.Text, Utilitario.Instancia.SesionUsuario.usuario);
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistencias_CompensacionAdelantada_Liberar("10000000", Periodo, Planilla, IDPersona, txtFComp.Text, txtFAsis.Text, Utilitario.Instancia.SesionUsuario.usuario);
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

        private void btnBuscar_Click(object sender, EventArgs e) { cargaFechasAdelantadas(IDPersona, dtpFechaIni.Text, dtpFechaFin.Text); }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cargaFechasAdelantadas(IDPersona, dtpFechaIni.Text, dtpFechaFin.Text); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cargaFechasAdelantadas(IDPersona, dtpFechaIni.Text, dtpFechaFin.Text); }
        }

        private void dgvTipoAsis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFComp.Text = dgvTipoAsis.CurrentRow.Cells["FECHA_COMP"].Value.ToString();
            txtFAsis.Text = dgvTipoAsis.CurrentRow.Cells["FECHA_ASISTE"].Value.ToString();
            string Estado = dgvTipoAsis.CurrentRow.Cells["ESTADO"].Value.ToString();

            button3.Enabled = true;

            btnImprimir.Enabled = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
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

        private void dgvTipoAsis_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvTipoAsis.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvTipoAsis.Columns[e.ColumnIndex].Name.Contains("ESTADO"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("COMPENSADO")) { e.CellStyle.BackColor = Color.PaleGreen; }
                            else { e.CellStyle.BackColor = Color.DeepSkyBlue; }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }
    }
}
