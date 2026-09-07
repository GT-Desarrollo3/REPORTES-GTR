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
using Entidades;
using Negocio;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmListarGuiaEvento : Form
    {
        public FrmListarGuiaEvento()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsOperacionesBL.Instancia.ReportesaApp_Operaciones_ListarGuiasEvento(txtSerie.Text,txtNumero.Text);
                if (dt.Rows.Count > 0) 
                {
                    dtgListaGuiasTransportista.DataSource = dt;
                    dgvListaGuiaTraspExpressVista.Columns["idGuiaEvento"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["idRemitenteTrans"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["idDestinatarioTrans"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["SecuenciaPartida"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["SecuenciaDestino"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["idRuta"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["idViaje"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["xml_Conductores"].Visible = false;
                }
                else
                {
                    dtgListaGuiasTransportista.DataSource = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmListarGuiaEvento_Load(object sender, EventArgs e)
        {

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (/*Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat")) == "APROBADO" && */Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RemitenteTrans")) == "LIMA GAS S A")
                {
                    FrmGuiaDeEvento open = new FrmGuiaDeEvento();
                    open.txtSerie.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuiaTrans"));
                    open.entGuiaTransportista.entGRT_Generales_Serie_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuiaTrans"));
                    open.txtNumero.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuiaTrans"));
                    open.entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuiaTrans"));
                    open.txtRemitente.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RemitenteTrans"));
                    open.entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRemitenteTrans"));
                    open.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RemitenteTrans"));
                    open.txtRemitente.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRemitenteTrans"));
                    open.entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRemitenteTrans"));

                    open.txtDestinatario.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DestinatarioTrans"));
                    open.tipoEvento = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoEvento"));
                    open.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DestinatarioTrans"));
                    open.entGuiaTransportista.idDestinatario = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idDestinatarioTrans"));
                    open.txtDestinatario.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idDestinatarioTrans"));
                    open.entGuiaTransportista.ruta = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                    open.entGuiaTransportista.idRuta = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRuta"));
                    open.entGuiaTransportista.xml_entGTR_Conductor_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Conductores"));
                    open.entGuiaTransportista.CodigoProgramacion = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion"));
                    open.entGuiaTransportista.idviaje = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idViaje"));
                    open.entGuiaTransportista.viaje = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje"));
                    open.entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "LineaOT"));
                    open.txtOTOriginal.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                    open.txtOT.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                    open.button1.Enabled = false;
                    open.btnAgregarConductor.Enabled = false;
                    open.txtRuta.Enabled = false;
                    open.btnGuardar.Text = "Actualizar";
                    open.txtRutaOrigen.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                    open.txtDireccionPartida.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPartida"));
                    open.entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPartida"));
                    open.txtDireccionPartida.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SecuenciaPartida"));
                    open.entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SecuenciaPartida"));
                    open.entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionDestino"));
                    open.txtDireccionDestino.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionDestino"));
                    open.txtDireccionDestino.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SecuenciaDestino"));
                    open.entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SecuenciaDestino"));

                    open.txtOT_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                    /*DataTable dt = clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica(Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DestinatarioTrans")));
                    if (dt.Rows.Count <= 0) { MessageBox.Show("No se obtuvo pk del destinatario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    open.txtDestinatario.Tag = dt.Rows[0]["Persona"].ToString();*/
                    open.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Para generar Guia de Evento, la Guia  de Transportista tiene que estar en estado Aprobado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
