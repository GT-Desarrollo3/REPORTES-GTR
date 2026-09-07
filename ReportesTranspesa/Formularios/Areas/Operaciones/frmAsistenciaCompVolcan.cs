using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using System.Diagnostics;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmAsistenciaCompVolcan : Form
    {
        public int idPersona, Opcion;
        public DateTime FechaSeleccionada;
        public Boolean Refrescar = false;
        DataTable dtCompVolcan;
        
        public frmAsistenciaCompVolcan()
        {
            InitializeComponent();
        }

        private void frmAsistenciaCompVolcan_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.AddMonths(-2);
            dtpDesde.Value = new DateTime(date.Year, date.Month, 1);
            dtpCompAdelantada.Value = FechaSeleccionada.AddDays(2);

            ContarAsistComp();

            if (Opcion == 1) { ListarCompensacionVolcan(); }
            else { ListarCompAdelantadaVolcan(); }
        }


        public void ContarAsistComp()
        {
            DataTable dtListaCompensacion = new DataTable();
            dtListaCompensacion = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ContarCompensaciones(idPersona);
            
            if (dtListaCompensacion.Rows.Count > 0) { txtNroComp.Text = dtListaCompensacion.Rows[0]["COMP_PENDIENTES"].ToString(); }
        }

        public void ListarCompensacionVolcan()
        {
            dtCompVolcan = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarCompensacion_Volcan(1, idPersona, dtpDesde.Value, dtpHasta.Value);
            dtgListaComp.DataSource = dtCompVolcan;
            if (dtCompVolcan.Rows.Count > 0)
            {
                dgvListaCompView.Columns["IDPersona"].Visible = false;
                dgvListaCompView.Columns["IDTipoAsist"].Visible = false;

                dgvListaCompView.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaCompView.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                dgvListaCompView.BestFitColumns();
            }
        }

        public void ListarCompAdelantadaVolcan()
        {
            dtCompVolcan = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarCompensacion_Volcan(2, idPersona, dtpDesde.Value, dtpHasta.Value);
            dtgListaComp.DataSource = dtCompVolcan;
            if (dtCompVolcan.Rows.Count > 0)
            {
                dgvListaCompView.Columns["IDPersona"].Visible = false;
                dgvListaCompView.Columns["IDTipoAsist"].Visible = false;

                dgvListaCompView.Columns["FECHA_COMP"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaCompView.Columns["FECHA_COMP"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvListaCompView.Columns["FECHA_ASISTE"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaCompView.Columns["FECHA_ASISTE"].DisplayFormat.FormatString = "dd/MM/yyyy";

                dgvListaCompView.BestFitColumns();
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                if (Convert.ToInt32(txtNroComp.Text) <= 0)
                {
                    MessageBox.Show("No puede generar porque no hay compensaciones pendientes.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_Compensar_Volcan(idPersona, FechaSeleccionada, Utilitario.Instancia.SesionUsuario.usuario);
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
            else
            {
                if (dtpCompAdelantada.Value <= FechaSeleccionada)
                {
                    MessageBox.Show("No puede compensar en una fecha menor a la seleccionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                DataTable dtRespuesta2 = new DataTable();
                string Respuesta2;

                dtRespuesta2 = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CompensarAdelantado_Volcan(idPersona, FechaSeleccionada, dtpCompAdelantada.Value, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                string NroRPTA2 = Respuesta2.Substring(0, 1);

                if (NroRPTA2 == "0")
                {
                    MessageBox.Show(Respuesta2, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Refrescar = true;
                    Close();
                }
                else { MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea liberar estas compensaciones?", "LIBERAR COMPENSACIONES", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int IDPersona, IDTipoAsist;
                DateTime Fecha, FechaComp, FechaAsist;
                int[] filas = dgvListaCompView.GetSelectedRows();

                if (filas.Length != 0)
                {
                    int Correcto = 0;
                    string Respuesta = "0 = Compensaciones Liberadas correctamente.";

                    for (int i = 0; i < filas.Length; i++)
                    {
                        if (Opcion == 1)
                        {
                            IDPersona = Convert.ToInt32(dgvListaCompView.GetRowCellValue(filas[i], "IDPersona"));
                            IDTipoAsist = Convert.ToInt32(dgvListaCompView.GetRowCellValue(filas[i], "IDTipoAsist"));
                            Fecha = Convert.ToDateTime(dgvListaCompView.GetRowCellValue(filas[i], "FECHA"));

                            DataTable dtRespuesta = new DataTable();
                            dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_EliminarCompensacion_Volcan(IDPersona, IDTipoAsist, Fecha, Utilitario.Instancia.SesionUsuario.usuario);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRPTA = Respuesta.Substring(0, 1);
                            if (NroRPTA == "0") { Correcto = Correcto + 1; }
                        }
                        else
                        {
                            IDPersona = Convert.ToInt32(dgvListaCompView.GetRowCellValue(filas[i], "IDPersona"));
                            IDTipoAsist = Convert.ToInt32(dgvListaCompView.GetRowCellValue(filas[i], "IDTipoAsist"));
                            FechaComp = Convert.ToDateTime(dgvListaCompView.GetRowCellValue(filas[i], "FECHA_COMP"));
                            FechaAsist = Convert.ToDateTime(dgvListaCompView.GetRowCellValue(filas[i], "FECHA_ASISTE"));

                            DataTable dtRespuesta = new DataTable();
                            dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_EliminarCompAdelantado_Volcan(IDPersona, IDTipoAsist, FechaComp, FechaAsist, Utilitario.Instancia.SesionUsuario.usuario);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRPTA = Respuesta.Substring(0, 1);
                            if (NroRPTA == "0") { Correcto = Correcto + 1; }
                        }
                    }

                    if (Correcto == filas.Length)
                    {
                        Refrescar = true;
                        Close();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No ha seleccionado ninguna compensación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtpDesde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                if (Opcion == 1) { ListarCompensacionVolcan(); }
                else { ListarCompAdelantadaVolcan(); }
            }
        }

        private void dtpHasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                if (Opcion == 1) { ListarCompensacionVolcan(); }
                else { ListarCompAdelantadaVolcan(); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarCompensacionVolcan(); }
            else { ListarCompAdelantadaVolcan(); }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DataTable dtConsultarImpresora = new DataTable();
            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
            string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

            if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
            {
                MessageBox.Show("No tiene una impresora asignada");
                return;
            }
            else
            {
                Ticket ticket = new Ticket();
                ticket.AddHeaderLine("GRUPO TRANSPESA");

                if (Opcion == 1)
                {
                    ticket.AddSubHeaderLine2("COMPENSACIONES");
                    ticket.AddSubHeaderLine2("CÓDIGO: " + idPersona + "                         ");
                    ticket.AddSubHeaderLine("NOMBRE:" + lblNombre.Text);
                    ticket.AddSubHeaderLine("F. IMPRESIÓN: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.AddSubHeaderLine("USUARIO: " + Utilitario.Instancia.SesionUsuario.usuario);
                    ticket.AddSubHeaderLine("DEL " + dtpDesde.Text + " AL " + dtpHasta.Text);
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("NRO | DÍA | FECHA");

                    DataTable dt = (DataTable)dtgListaComp.DataSource;
                    foreach (DataRow row in dt.Rows)
                    { ticket.AddSubHeaderLine(row["NRO"].ToString() + "     " + row["DIA"].ToString() + "     " + row["FECHA"].ToString()); }
                }
                else
                {
                    ticket.AddSubHeaderLine2("COMP. ADELANTADA");
                    ticket.AddSubHeaderLine2("CÓDIGO: " + idPersona + "                         ");
                    ticket.AddSubHeaderLine("NOMBRE:" + lblNombre.Text);
                    ticket.AddSubHeaderLine("F. IMPRESIÓN: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.AddSubHeaderLine("USUARIO: " + Utilitario.Instancia.SesionUsuario.usuario);
                    ticket.AddSubHeaderLine("DEL " + dtpDesde.Text + " AL " + dtpHasta.Text);
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("DÍA | FECHA COMP | FECHA ASIST");

                    DataTable dt = (DataTable)dtgListaComp.DataSource;
                    foreach (DataRow row in dt.Rows)
                    { ticket.AddSubHeaderLine(row["DIA"].ToString() + "     " + row["FECHA_COMP"].ToString() + "     " + row["FECHA_ASISTE"].ToString()); }
                }

                ticket.AddFooterLine("");
                ticket.AddFooterLine("    ** VIAJA CON CUIDADO **");
                ticket.PrintTicket(NombreImpresora);

                MessageBox.Show("Ticket Impreso.");
            }
        }

        private void dgvListaCompView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "COMPENSADO") { e.Appearance.BackColor = Color.PaleGreen; }

                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.DeepSkyBlue; }
            }
        }
    }
}
