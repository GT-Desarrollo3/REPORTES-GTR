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
    public partial class frmPlanVacaComp : Form
    {
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int saveRow = 0, saveCol = 0, OpcionVC = 0, RegistroVC = 0, Persona = -1;
        int IDPersona, e1 = 0;
        public int FechaRetorno;
        string xmlProgramacion;
        int xClick = 0, yClick = 0, xClick2 = 0, yClick2 = 0;
        public DataTable dtListaTicket = new DataTable();
        public DataTable dtListaAsistencias = new DataTable();
        public DataTable dtListaVacaciones = new DataTable();
        public DataTable dtListaCompensacion = new DataTable();

        public frmPlanVacaComp()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void frmPlanVacaComp_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmPlanVacaComp");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    tsProgramarA.Enabled = true;
                    tsProgramarV.Enabled = true;
                    tsProgramarC.Enabled = true;
                }
                else
                {
                    tsProgramarA.Enabled = false;
                    tsProgramarV.Enabled = false;
                    tsProgramarC.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarProg2.Enabled = true; }
                else { tsEliminarProg2.Enabled = false; }
            }

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Aprobar")
                    {
                        tsRegistrar.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { tsRegistrar.Enabled = false; }
                }
            }
            else { tsRegistrar.Enabled = false; }

            dgvProgVC.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvProgVC.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvProgVC.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvTotales.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvTotales.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvTotales.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            DateTime date = DateTime.Now;
            dtpPeriodo.Value = new DateTime(date.Year, date.Month, 1);

            CargarComboOperaciones();
            cbxOperaciones.SelectedValue = 5;

            CargarTabla();
            rbAsistencias.Checked = true;
            rbAsistencias_Click(sender, e);
        }


        private void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperaciones;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        public void CargarTabla()
        {
            DataTable dtListaCompensacion = new DataTable();
            dtListaCompensacion = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ContarCompensaciones(0);
            
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC(dtpPeriodo.Text, txtConductor.Text, Convert.ToString(cbxOperaciones.SelectedValue));

            dgvProgVC.DataSource = null;
            dgvProgVC.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvProgVC.DataSource = dt;
                dgvProgVC.AutoResizeColumns();

                dgvProgVC.Columns["IDPersona"].Visible = false;

                dgvProgVC.Columns["CONDUCTOR"].Frozen = true;
                dgvProgVC.Columns["CONDUCTOR"].ReadOnly = true;
                dgvProgVC.Columns["FECHA_INGRESO"].Frozen = true;
                dgvProgVC.Columns["FECHA_INGRESO"].ReadOnly = true;
                dgvProgVC.Columns["PROGRAMACION"].Frozen = true;
                dgvProgVC.Columns["PROGRAMACION"].ReadOnly = true;
                dgvProgVC.Columns["ORIGEN_CONDUCTOR"].Frozen = true;
                dgvProgVC.Columns["ORIGEN_CONDUCTOR"].ReadOnly = true;
                dgvProgVC.Columns["RUTAS"].Frozen = true;
                dgvProgVC.Columns["RUTAS"].ReadOnly = true;
                dgvProgVC.Columns["TRANSMISION"].Frozen = true;
                dgvProgVC.Columns["TRANSMISION"].ReadOnly = true;
                dgvProgVC.Columns["VAC_PROG"].Frozen = true;
                dgvProgVC.Columns["VAC_PROG"].ReadOnly = true;
                dgvProgVC.Columns["VAC_PEND"].Frozen = true;
                dgvProgVC.Columns["VAC_PEND"].ReadOnly = true;
                dgvProgVC.Columns["COMP_PROG"].Frozen = true;
                dgvProgVC.Columns["COMP_PROG"].ReadOnly = true;
                dgvProgVC.Columns["COMP_PEND"].Frozen = true;
                dgvProgVC.Columns["COMP_PEND"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvProgVC.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#9786F7");
                dgvProgVC.EnableHeadersVisualStyles = false;

                try
                {
                    if (saveRow != 0 && saveRow < dgvProgVC.Rows.Count)
                    {
                        dgvProgVC.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvProgVC.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }

                // CONTAR TOTAL DE CONDUCTORES
                dgvTotales.DataSource = null;
                dgvTotales.Columns.Clear();

                DataTable dtResumen = new DataTable();
                dtResumen.Columns.Add(" ", typeof(string));

                int colCompPend = dt.Columns.IndexOf("COMP_PEND");
                if (colCompPend == -1)
                {
                    MessageBox.Show("No se encontró la columna COMP_PEND");
                    return;
                }

                int firstDayColIndex = colCompPend + 1;
                int numDias = dt.Columns.Count - firstDayColIndex;

                for (int i = 0; i < numDias; i++)
                {
                    string nombreDia = dt.Columns[firstDayColIndex + i].ColumnName;
                    dtResumen.Columns.Add(nombreDia, typeof(int));
                }

                int[] totalConDato = new int[numDias];
                int[] disponibles = new int[numDias];
                int[] noDisponibles = new int[numDias];

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < numDias; i++)
                    {
                        int colIndexOriginal = firstDayColIndex + i;
                        object celda = row[colIndexOriginal];

                        string valor = celda == DBNull.Value ? "" : celda.ToString().Trim();

                        /*
                        if (string.IsNullOrEmpty(valor))
                            continue;
                        */

                        totalConDato[i]++;

                        if (valor == "A") { disponibles[i]++; }
                        else { noDisponibles[i]++; }
                    }
                }

                DataRow filaTotal = dtResumen.NewRow();
                filaTotal[" "] = "TOTAL_CONDUCTORES";

                DataRow filaDisp = dtResumen.NewRow();
                filaDisp[" "] = "DISPONIBLES";

                DataRow filaNoDisp = dtResumen.NewRow();
                filaNoDisp[" "] = "NO_DISPONIBLES";

                for (int i = 0; i < numDias; i++)
                {
                    string nombreDia = dt.Columns[firstDayColIndex + i].ColumnName;

                    filaTotal[nombreDia] = totalConDato[i];
                    filaDisp[nombreDia] = disponibles[i];
                    filaNoDisp[nombreDia] = noDisponibles[i];
                }

                dtResumen.Rows.Add(filaTotal);
                dtResumen.Rows.Add(filaDisp);
                dtResumen.Rows.Add(filaNoDisp);

                dgvTotales.DataSource = dtResumen;
                dgvTotales.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#86F3F7");
                dgvTotales.EnableHeadersVisualStyles = false;
                dgvTotales.AutoResizeColumns();

                gcExcelProg2.DataSource = null;
                gvExcelProg2.Columns.Clear();
                gcExcelProg2.DataSource = dtResumen;
            }
        }

        public static string CalcularDia(int col)
        {
            string respuesta;

            if (col < 11) { respuesta = "0" + col.ToString(); }
            else { respuesta = col.ToString(); }

            return respuesta;
        }

        public void ListarAsistencias(int Persona)
        {
            dtListaAsistencias = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor(3, Persona);
            dtgCompensarUsuario.DataSource = dtListaAsistencias;

            if (dtListaAsistencias.Rows.Count > 0)
            {
                dtgvCompensarUsuario.Columns["PERSONA"].Visible = false;

                dtgvCompensarUsuario.BestFitColumns();
            }
        }

        public void ListarVacaciones(int Persona)
        {
            dtListaVacaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor(1, Persona);
            dtgCompensarUsuario.DataSource = dtListaVacaciones;

            if (dtListaVacaciones.Rows.Count > 0)
            {
                dtgvCompensarUsuario.Columns["PERSONA"].Visible = false;

                dtgvCompensarUsuario.BestFitColumns();
            }
        }

        public void ListarCompensacion(int Persona)
        {
            dtListaCompensacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor(2, Persona);

            dtgCompensarUsuario.DataSource = null;
            dtgvCompensarUsuario.Columns.Clear();
            dtgCompensarUsuario.DataSource = dtListaCompensacion;

            if (dtListaCompensacion.Rows.Count > 0)
            {
                dtgvCompensarUsuario.Columns["PERSONA"].Visible = false;

                dtgvCompensarUsuario.BestFitColumns();
            }
        }

        public void ListarProgramaciones(int OpcionVC)
        {
            DataTable dtListaProgramaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionesVC(OpcionVC, dtpPeriodo.Text, txtConductor.Text, Convert.ToString(cbxOperaciones.SelectedValue));

            dtgProgramacionVC.DataSource = null;
            dgvProgramacionVC.Columns.Clear();
            dtgProgramacionVC.DataSource = dtListaProgramaciones;
            if (dtListaProgramaciones.Rows.Count > 0)
            {
                dgvProgramacionVC.Columns["idProgVC"].Visible = false;
                dgvProgramacionVC.Columns["CODIGO"].Visible = false;
                dgvProgramacionVC.Columns["RETORNO"].Visible = false;

                if (OpcionVC == 1)      // VACACIONES
                {
                    dgvProgramacionVC.Columns["FECHA_INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FECHA_FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_FIN"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FECHA_RETORNO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_RETORNO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FechaRegistra"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FechaRegistra"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvProgramacionVC.Columns["FechaAprueba"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FechaAprueba"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                }

                if (OpcionVC == 2)      // COMPENSACIONES
                {
                    dgvProgramacionVC.Columns["FECHA_PENDIENTE"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_PENDIENTE"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FECHA_COMPENSACION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_COMPENSACION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FECHA_RETORNO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_RETORNO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FechaRegistra"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FechaRegistra"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvProgramacionVC.Columns["FechaAprueba"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FechaAprueba"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                }

                if (OpcionVC == 3)      // ASISTENCIAS
                {
                    dgvProgramacionVC.Columns["FECHA_INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FECHA_FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FECHA_FIN"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvProgramacionVC.Columns["FechaRegistra"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvProgramacionVC.Columns["FechaRegistra"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                }

                dgvProgramacionVC.BestFitColumns();
            }
        }

        public void Imprimir(int OpcionVC, int idProgVC, int Retorno, int IDPersona, DateTime FechaRetorno)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
                string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionConductor(OpcionVC, idProgVC, IDPersona, FechaRetorno);

                if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                {
                    MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Ticket ticket = new Ticket();
                    string Programacion = Convert.ToString(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "PROGRAMACION"));

                    if (Programacion == "VACACIONES")
                    {
                        if (Retorno == 1)
                        {
                            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket.AddSubHeaderLine2("PROGRAMACIÓN DE");
                            ticket.AddSubHeaderLine2("VACACIONES");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Nombre: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                            ticket.AddSubHeaderLine("DNI: " + dtListaTicket.Rows[0]["DOCUMENTO"].ToString());
                            ticket.AddSubHeaderLine("Inicio: " + dtListaTicket.Rows[0]["FECHA_INICIO"].ToString());
                            ticket.AddSubHeaderLine("Fin: " + dtListaTicket.Rows[0]["FECHA_FIN"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine(dtListaTicket.Rows[0]["DÍAS"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Retorno: " + dtListaTicket.Rows[0]["FECHA_RETORNO"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("     FIRMA DEL TRABAJADOR     ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("   FIRMA DEL JEFE INMEDIATO   ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Registrado por: " + dtListaTicket.Rows[0]["USUARIO_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("F.Registro: " + dtListaTicket.Rows[0]["FECHA_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("Aprobado por: " + dtListaTicket.Rows[0]["USUARIO"].ToString());
                            ticket.AddSubHeaderLine("F.Aprobacion: " + dtListaTicket.Rows[0]["FECHA_APRUEBA"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.PrintTicket(NombreImpresora);
                        }
                        else
                        {
                            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket.AddSubHeaderLine2("PROGRAMACIÓN DE");
                            ticket.AddSubHeaderLine2("VACACIONES");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Nombre: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                            ticket.AddSubHeaderLine("DNI: " + dtListaTicket.Rows[0]["DOCUMENTO"].ToString());
                            ticket.AddSubHeaderLine("Inicio: " + dtListaTicket.Rows[0]["FECHA_INICIO"].ToString());
                            ticket.AddSubHeaderLine("Fin: " + dtListaTicket.Rows[0]["FECHA_FIN"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine(dtListaTicket.Rows[0]["DÍAS"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("     FIRMA DEL TRABAJADOR     ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("   FIRMA DEL JEFE INMEDIATO   ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Registrado por: " + dtListaTicket.Rows[0]["USUARIO_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("F.Registro: " + dtListaTicket.Rows[0]["FECHA_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("Aprobado por: " + dtListaTicket.Rows[0]["USUARIO"].ToString());
                            ticket.AddSubHeaderLine("F.Aprobacion: " + dtListaTicket.Rows[0]["FECHA_APRUEBA"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.PrintTicket(NombreImpresora);
                        }
                    }

                    if (Programacion == "COMPENSACIÓN")
                    {
                        if (Retorno == 1)
                        {
                            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket.AddSubHeaderLine2("PROGRAMACIÓN DE");
                            ticket.AddSubHeaderLine2("COMPENSACIÓN");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Nombre: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                            ticket.AddSubHeaderLine("DNI: " + dtListaTicket.Rows[0]["DOCUMENTO"].ToString());
                            ticket.AddSubHeaderLine("Fechas: " + dtListaTicket.Rows[0]["FECHAS"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine(dtListaTicket.Rows[0]["DÍAS"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Retorno: " + dtListaTicket.Rows[0]["FECHA_RETORNO"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("     FIRMA DEL TRABAJADOR     ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("   FIRMA DEL JEFE INMEDIATO   ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Registrado por: " + dtListaTicket.Rows[0]["USUARIO_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("F.Registro: " + dtListaTicket.Rows[0]["FECHA_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("Aprobado por: " + dtListaTicket.Rows[0]["USUARIO"].ToString());
                            ticket.AddSubHeaderLine("F.Aprobacion: " + dtListaTicket.Rows[0]["FECHA_APRUEBA"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.PrintTicket(NombreImpresora);
                        }
                        else
                        {
                            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket.AddSubHeaderLine2("PROGRAMACIÓN DE");
                            ticket.AddSubHeaderLine2("COMPENSACIÓN");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Nombre: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                            ticket.AddSubHeaderLine("DNI: " + dtListaTicket.Rows[0]["DOCUMENTO"].ToString());
                            ticket.AddSubHeaderLine("Fechas: " + dtListaTicket.Rows[0]["FECHAS"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine(dtListaTicket.Rows[0]["DÍAS"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("     FIRMA DEL TRABAJADOR     ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("______________________________");
                            ticket.AddSubHeaderLine("   FIRMA DEL JEFE INMEDIATO   ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("Registrado por: " + dtListaTicket.Rows[0]["USUARIO_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("F.Registro: " + dtListaTicket.Rows[0]["FECHA_REGISTRA"].ToString());
                            ticket.AddSubHeaderLine("Aprobado por: " + dtListaTicket.Rows[0]["USUARIO"].ToString());
                            ticket.AddSubHeaderLine("F.Aprobacion: " + dtListaTicket.Rows[0]["FECHA_APRUEBA"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.PrintTicket(NombreImpresora);
                        }
                    }
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTabla();
            ListarProgramaciones(OpcionVC);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC(dtpPeriodo.Text, txtConductor.Text, Convert.ToString(cbxOperaciones.SelectedValue));
            gcExcelProg.DataSource = null;
            gvExcelProg.Columns.Clear();
            gcExcelProg.DataSource = dt2;

            if (dt2.Rows.Count > 0) { gvExcelProg.Columns["IDPersona"].Visible = false; }

            if (dtgProgramacionVC.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                gcExcelProg.ForceInitialize();
                gcExcelProg2.ForceInitialize();
                dtgProgramacionVC.ForceInitialize();
                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "PLAN DE VACACIONES Y COMPENSACIONES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
        }

        private void dgvProgVC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvProgVC.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("VAC_PROG"))
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.LemonChiffon;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("VAC_PEND"))
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.LemonChiffon;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);

                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToInt32(e.Value) <= 30) { e.CellStyle.ForeColor = Color.LimeGreen; }
                            else { e.CellStyle.ForeColor = Color.Red; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("COMP_PROG"))
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.SeaShell;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("COMP_PEND"))
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.SeaShell;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);

                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToInt32(e.Value) <= 10) { e.CellStyle.ForeColor = Color.LimeGreen; }
                            else { e.CellStyle.ForeColor = Color.Red; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }

                if (this.dgvProgVC.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("CO ("))
                            {
                                e.CellStyle.BackColor = Color.LightPink;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("VA ("))
                            {
                                e.CellStyle.BackColor = Color.LemonChiffon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            if (Convert.ToString(e.Value).Contains("(AP)")) { e.CellStyle.ForeColor = Color.LimeGreen; }

                            if (Convert.ToString(e.Value).Equals("A "))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }

                            if (Convert.ToString(e.Value).Equals("DF")) { e.CellStyle.ForeColor = Color.Red; }
                            if (Convert.ToString(e.Value).Equals("A")) { e.CellStyle.ForeColor = Color.LimeGreen; }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvProgVC_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvProgVC.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 11)
                        {
                            dgvProgVC.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvProgVC.ContextMenuStrip = null;
                        }
                        else
                        {
                            //dgvProgVC.ContextMenuStrip = contextMenuStrip1;
                            
                            if (dgvProgVC.Rows.Count > 0 && dgvProgVC.FirstDisplayedCell != null)
                            {
                                saveRow = e.RowIndex;
                                saveCol = e.ColumnIndex;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvProgVC_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvProgVC.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex == 5)
                        {
                            DataTable dtListaRutas = new DataTable();
                            int PersonaC = Convert.ToInt32(dgvProgVC.CurrentRow.Cells["IDPersona"].Value.ToString());

                            dtListaRutas.Clear();
                            dtgListaRutas.DataSource = null;
                            dgvListaRutasView.Columns.Clear();
                            dtListaRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarRutasXConductor(PersonaC);

                             if (dtListaRutas.Rows.Count > 0)
                             {
                                 lblConductor.Text = Convert.ToString(dgvProgVC.CurrentRow.Cells["CONDUCTOR"].Value.ToString());;
                                 dtgListaRutas.DataSource = dtListaRutas;

                                 dgvListaRutasView.Columns["RUTA"].Summary.Clear();
                                 dgvListaRutasView.Columns["RUTA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "RUTA", "Total: {0}");
                                 dgvListaRutasView.BestFitColumns();

                                 pListarRutas.Location = new System.Drawing.Point(665, 236);
                                 pListarRutas.Visible = true;
                                 pListarRutas.BringToFront();
                             }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void pListarRutas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pListarRutas.Left = pListarRutas.Left + (e.X - xClick2);
                pListarRutas.Top = pListarRutas.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            pListarRutas.Visible = false;
            pListarRutas.SendToBack();
            lblConductor.Text = "";
        }

        private void dgvTotales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvTotales.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (Convert.ToString(dgvTotales.Rows[e.RowIndex].Cells[" "].Value) == "TOTAL_CONDUCTORES")
                {
                    e.CellStyle.BackColor = Color.Gold;
                    e.CellStyle.ForeColor = Color.DarkBlue;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if (Convert.ToString(dgvTotales.Rows[e.RowIndex].Cells[" "].Value) == "DISPONIBLES")
                {
                    e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }

                if (Convert.ToString(dgvTotales.Rows[e.RowIndex].Cells[" "].Value) == "NO_DISPONIBLES")
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 192);
                    e.CellStyle.ForeColor = Color.DarkRed;
                }
            }
            catch (Exception) { throw; }
        }

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CargarTabla();
                ListarProgramaciones(OpcionVC);
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CargarTabla();
                ListarProgramaciones(OpcionVC);

                /*
                if (dgvProgVC.DataSource != null)
                { ((DataTable)dgvProgVC.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "CONDUCTOR", txtConductor.Text); }
                */
            }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            CargarTabla();
            ListarProgramaciones(OpcionVC);

            /*
            if (cbxOperaciones.Text == "TODO")
            {
                if (dgvProgVC.DataSource != null)
                { ((DataTable)dgvProgVC.DataSource).DefaultView.RowFilter = null; }
            }
            else
            {
                if (dgvProgVC.DataSource != null)
                { ((DataTable)dgvProgVC.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PROGRAMACION", cbxOperaciones.Text); }
            }
            */ 
        }

        private void dtgProgramacionVC_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idProgramacionVC = Convert.ToString(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "idProgVC"));

                if (idProgramacionVC != "")
                {
                    string Registro = Convert.ToString(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "REGISTRADO"));
                    string Programacion = Convert.ToString(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "PROGRAMACION"));

                    if (Programacion != "ASISTENCIA")
                    {
                        if (Registro == "NO") 
                        {
                            tsReimprimir.Enabled = false;
                            if (e1 == 1) { tsRegistrar.Enabled = true; }
                        }
                        else
                        {
                            tsReimprimir.Enabled = true;
                            tsRegistrar.Enabled = false;
                            //tsEliminarProg2.Enabled = false;
                        }

                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarProg2.Enabled = true; }
                    }
                    else
                    {
                        tsReimprimir.Enabled = false;
                        tsRegistrar.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarProg2.Enabled = true; }
                    }
                }
                else
                {
                    tsReimprimir.Enabled = false;
                    tsRegistrar.Enabled = false;
                    tsEliminarProg2.Enabled = false;
                }
            }
            catch
            {
                tsReimprimir.Enabled = false;
                tsRegistrar.Enabled = false;
                tsEliminarProg2.Enabled = false;
            }
        }

        private void pProgramaciones_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pProgramaciones.Left = pProgramaciones.Left + (e.X - xClick);
                pProgramaciones.Top = pProgramaciones.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pProgramaciones.Visible = false;
            pProgramaciones.SendToBack();
            dtgCompensarUsuario.DataSource = null;
            dtgvCompensarUsuario.Columns.Clear();

            Persona = -1;
            txtDiasPendientes.Text = "0";
            txtCantidadDias.Text = "1";
            txtConductor2.Clear();
            txtDiasPendientes.Clear();
            lstPersonal.Visible = false;
            lstPersonal.SendToBack();
        }

        private void rbAsistencias_Click(object sender, EventArgs e)
        {
            OpcionVC = 3;
            rbAsistencias.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            rbCompensaciones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbVacaciones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            ListarProgramaciones(OpcionVC);
        }

        private void rbVacaciones_Click(object sender, EventArgs e)
        {
            OpcionVC = 1;
            rbAsistencias.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbVacaciones.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            rbCompensaciones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            ListarProgramaciones(OpcionVC);
        }

        private void rbCompensaciones_Click(object sender, EventArgs e)
        {
            OpcionVC = 2;
            rbAsistencias.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbVacaciones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbCompensaciones.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            ListarProgramaciones(OpcionVC);
        }

        private void tsProgramarA_Click(object sender, EventArgs e)
        {
            RegistroVC = 3;
            label15.Text = "PROGRAMAR ASISTENCIA";
            dtgCompensarUsuario.DataSource = null;
            dtgvCompensarUsuario.Columns.Clear();
            dtpCFechaIni.Enabled = true;
            txtDiasPendientes.Text = "0";
            txtCantidadDias.Text = "1";
            dtpCFechaIni.Value = DateTime.Now;
            dtpCFechaFin.Value = DateTime.Now.AddDays(Convert.ToInt32(txtCantidadDias.Text) - 1);

            cbFechaRetorno.Checked = false;
            cbFechaRetorno_CheckedChanged(sender, e);
            cbFechaRetorno.Enabled = false;

            pProgramaciones.Location = new System.Drawing.Point(723, 238);
            pProgramaciones.Visible = true;
            pProgramaciones.BringToFront();
        }

        private void tsProgramarV_Click(object sender, EventArgs e)
        {
            RegistroVC = 1;
            label15.Text = "PROGRAMAR VACACIONES";
            dtgCompensarUsuario.DataSource = null;
            dtgvCompensarUsuario.Columns.Clear();
            dtpCFechaIni.Enabled = true;
            txtDiasPendientes.Text = "0";
            txtCantidadDias.Text = "1";
            dtpCFechaIni.Value = DateTime.Now;
            dtpCFechaFin.Value = DateTime.Now.AddDays(Convert.ToInt32(txtCantidadDias.Text) - 1);

            cbFechaRetorno.Checked = false;
            cbFechaRetorno_CheckedChanged(sender, e);
            cbFechaRetorno.Enabled = true;

            pProgramaciones.Location = new System.Drawing.Point(723, 238);
            pProgramaciones.Visible = true;
            pProgramaciones.BringToFront();
        }

        private void tsProgramarC_Click(object sender, EventArgs e)
        {
            RegistroVC = 2;
            label15.Text = "PROGRAMAR COMPENSACIONES";
            dtgCompensarUsuario.DataSource = null;
            dtgvCompensarUsuario.Columns.Clear();
            dtpCFechaIni.Enabled = true;
            txtDiasPendientes.Text = "0";
            txtCantidadDias.Text = "1";
            dtpCFechaIni.Value = DateTime.Now;
            dtpCFechaFin.Value = DateTime.Now.AddDays(Convert.ToInt32(txtCantidadDias.Text) - 1);

            cbFechaRetorno.Checked = false;
            cbFechaRetorno_CheckedChanged(sender, e);
            cbFechaRetorno.Enabled = true;

            pProgramaciones.Location = new System.Drawing.Point(723, 238);
            pProgramaciones.Visible = true;
            pProgramaciones.BringToFront();
        }

        private void dgvProgramacionVC_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (OpcionVC != 3)
            {
                if (e.Column.FieldName == "REGISTRADO")
                {
                    if (e.CellValue.ToString() == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                    if (e.CellValue.ToString() == "NO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
                }
            }
        }

        private void txtConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (RegistroVC == 1)
            { clsVisuales.Instancia.LlenarLw(lstPersonal, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarConductores(1, txtConductor2.Text), true, false, false); }

            if (RegistroVC == 2)
            { clsVisuales.Instancia.LlenarLw(lstPersonal, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarConductores(2, txtConductor2.Text), true, false, false); }

            if (RegistroVC == 3)
            { clsVisuales.Instancia.LlenarLw(lstPersonal, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarConductores(3, txtConductor2.Text), true, false, false); }

            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 350;
            lstPersonal.Columns[2].Width = 0;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                txtDiasPendientes.Text = "0";
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                dtgCompensarUsuario.DataSource = null;
                dtgvCompensarUsuario.Columns.Clear();
            }
        }

        private void txtConductor2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void lstPersonal_Enter(object sender, EventArgs e)
        {
            if (!lstPersonal.Items.Count.Equals(0)) { lstPersonal.Items[0].Selected = true; }
        }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal.SelectedItems[0];

                Persona = Int32.Parse(ItemActual.Text);
                txtConductor2.Text = ItemActual.SubItems[1].Text;
                txtDiasPendientes.Text = ItemActual.SubItems[2].Text;

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();

                if (RegistroVC == 1)
                {
                    txtCantidadDias.Focus();
                    ListarVacaciones(Persona);
                }

                if (RegistroVC == 2)
                {
                    dtpCFechaFin.Focus();
                    ListarCompensacion(Persona);
                }

                if (RegistroVC == 3)
                {
                    txtCantidadDias.Focus();
                    ListarAsistencias(Persona);
                }
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                txtDiasPendientes.Text = "0";
                dtgCompensarUsuario.DataSource = null;
                dtgvCompensarUsuario.Columns.Clear();

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void lstPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal.SelectedItems[0];

            Persona = Int32.Parse(ItemActual.Text);
            txtConductor2.Text = ItemActual.SubItems[1].Text;
            txtDiasPendientes.Text = ItemActual.SubItems[2].Text;

            lstPersonal.Visible = false;
            lstPersonal.SendToBack();

            if (RegistroVC == 1)
            {
                txtCantidadDias.Focus();
                ListarVacaciones(Persona);
            }

            if (RegistroVC == 2)
            {
                dtpCFechaFin.Focus();
                ListarCompensacion(Persona);
            }

            if (RegistroVC == 3)
            {
                txtCantidadDias.Focus();
                ListarAsistencias(Persona);
            }
        }

        private void txtCantidadDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToInt32(txtCantidadDias.Text) <= Convert.ToInt32(txtDiasPendientes.Text))
                {
                    dtpCFechaIni.Focus();
                    dtpCFechaFin.Value = dtpCFechaIni.Value.AddDays(Convert.ToInt32(txtCantidadDias.Text) - 1);
                }
                else
                {
                    if (RegistroVC == 3)
                    {
                        dtpCFechaIni.Focus();
                        dtpCFechaFin.Value = dtpCFechaIni.Value.AddDays(Convert.ToInt32(txtCantidadDias.Text) - 1);
                    }
                    else
                    {
                        MessageBox.Show("No puede ingresar una cantidad mayor a la de los días pendientes.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        dtgCompensarUsuario.DataSource = null;
                        dtgvCompensarUsuario.Columns.Clear();
                        Persona = -1;
                        txtDiasPendientes.Text = "0";
                        txtCantidadDias.Text = "1";
                        txtConductor2.Clear();
                        lstPersonal.Visible = false;
                        lstPersonal.SendToBack();
                    }
                }
            }
        }

        private void dtpCFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToInt32(txtCantidadDias.Text) <= Convert.ToInt32(txtDiasPendientes.Text))
                {
                    dtpCFechaFin.Value = dtpCFechaIni.Value.AddDays(Convert.ToInt32(txtCantidadDias.Text) - 1);
                    btnAgregar.Focus();
                }
                else
                {
                    if (RegistroVC == 3)
                    {
                        dtpCFechaIni.Focus();
                        dtpCFechaFin.Value = dtpCFechaIni.Value.AddDays(Convert.ToInt32(txtCantidadDias.Text) - 1);
                    }
                    else
                    {
                        MessageBox.Show("No puede ingresar una cantidad mayor a la de los días pendientes.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        dtgCompensarUsuario.DataSource = null;
                        dtgvCompensarUsuario.Columns.Clear();
                        Persona = -1;
                        txtDiasPendientes.Text = "0";
                        txtCantidadDias.Text = "1";
                        txtConductor2.Clear();
                        txtDiasPendientes.Clear();
                        lstPersonal.Visible = false;
                        lstPersonal.SendToBack();
                    }
                }
            }
        }

        private void dtpCFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar.Focus(); }
        }

        private void dtgCompensarUsuario_Click(object sender, EventArgs e)
        {
            /*
            if (RegistroVC == 2)
            {
                try
                { dtpCFechaIni.Value = Convert.ToDateTime(dtgvCompensarUsuario.GetRowCellValue(dtgvCompensarUsuario.FocusedRowHandle, "DIA_PENDIENTE")); } 
                catch
                { dtpCFechaIni.Value = DateTime.Now; }
            }
            */ 
        }

        private void dtgvCompensarUsuario_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (RegistroVC == 2 || RegistroVC == 3)
            {
                GridView currentView = sender as GridView;
                DataRow dr = currentView.GetFocusedDataRow();

                if (e.Column.FieldName == "ESTADO")
                {
                    if (e.CellValue.ToString() == "APROBADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                    if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
                }
            }
        }

        private void cbFechaRetorno_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechaRetorno.Checked == true) { FechaRetorno = 1; }

            if (cbFechaRetorno.Checked == false) { FechaRetorno = 0; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (RegistroVC == 1)
            {
                if (MessageBox.Show("¿Desea programar las vacaciones de este conductor?", "PROGRAMAR VACACIONES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (txtConductor2.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese el nombre de un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConductor2.Focus();
                        return;
                    }
                    else
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarVacaciones(dtpPeriodo.Text, Persona, Convert.ToInt32(txtDiasPendientes.Text),
                                                                 dtpCFechaIni.Value, dtpCFechaFin.Value, "VA (PR)", FechaRetorno, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCerrar_Click(sender, e);
                            CargarTabla();
                            ListarProgramaciones(OpcionVC);
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }

            if (RegistroVC == 2)
            {
                if (MessageBox.Show("¿Desea programar las compensaciones de este conductor?", "PROGRAMAR COMPENSACIONES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (Persona == -1)
                    {
                        MessageBox.Show("Por favor, ingrese el nombre de un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConductor2.Focus();
                        return;
                    }
                    else
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarCompensaciones(dtpPeriodo.Text, Persona, Convert.ToInt32(txtDiasPendientes.Text),
                                                                 dtpCFechaIni.Value, dtpCFechaFin.Value, "CO (PR)", FechaRetorno, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCerrar_Click(sender, e);
                            ListarCompensacion(Persona);
                            CargarTabla();
                            ListarProgramaciones(OpcionVC);
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }

            if (RegistroVC == 3)
            {
                if (MessageBox.Show("¿Desea programar las asistencias de este conductor?", "PROGRAMAR ASISTENCIA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (txtConductor2.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese el nombre de un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConductor2.Focus();
                        return;
                    }
                    else
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarAsistencias(dtpPeriodo.Text, Persona, Convert.ToInt32(txtDiasPendientes.Text),
                                                                 dtpCFechaIni.Value, dtpCFechaFin.Value, "A ", Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCerrar_Click(sender, e);
                            CargarTabla();
                            ListarProgramaciones(OpcionVC);
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
        }

        private void tsReimprimir_Click(object sender, EventArgs e)
        {
            int idProgVC = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "idProgVC"));
            int Retorno = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "RETORNO"));
            int IDPersona = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "CODIGO"));
            DateTime FechaRetorno = Convert.ToDateTime(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "FECHA_RETORNO"));

            Imprimir(OpcionVC, idProgVC, Retorno, IDPersona, FechaRetorno);
        }

        private void tsRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProgVC = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "idProgVC"));
                string Estado = Convert.ToString(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "REGISTRADO"));
                int IDPersona = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "CODIGO"));
                int Retorno = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "RETORNO"));
                DateTime FechaRetorno;

                if (OpcionVC != 3)
                {
                    FechaRetorno = Convert.ToDateTime(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "FECHA_RETORNO"));

                    if (Estado == "NO")
                    {
                        if (MessageBox.Show("¿Desea registrar la programación de este conductor?", "REGISTRAR PROGRAMACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string Respuesta;
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            DataTable dtRespuesta2 = new DataTable();
                            dtRespuesta2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionesVC(OpcionVC, dtpPeriodo.Text, idProgVC, IDPersona, FechaRetorno, Usuario);

                            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionSPRING(OpcionVC, dtpPeriodo.Text, idProgVC, IDPersona, FechaRetorno, Usuario);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito2"]);
                            string NroRPTA = Respuesta.Substring(0, 1);

                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Imprimir(OpcionVC, idProgVC, Retorno, IDPersona, FechaRetorno);
                                ListarProgramaciones(OpcionVC);
                            }
                            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }
                }
            }
            catch { MessageBox.Show("No se pudo registrar la programación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminarProg2_Click(object sender, EventArgs e)
        {
            try
            {
                int idProgVC = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "idProgVC"));
                string Estado = Convert.ToString(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "REGISTRADO"));
                int IDPersona = Convert.ToInt32(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "CODIGO"));
                DateTime FechaRetorno;

                if (OpcionVC == 3) { FechaRetorno = DateTime.Now; }
                else { FechaRetorno = Convert.ToDateTime(dgvProgramacionVC.GetRowCellValue(dgvProgramacionVC.FocusedRowHandle, "FECHA_RETORNO")); }

                if (MessageBox.Show("¿Desea quitar la programación de este conductor?", "ELIMINAR PROGRAMACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionesVC(OpcionVC, dtpPeriodo.Text, idProgVC, IDPersona, FechaRetorno);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        CargarTabla();
                        ListarProgramaciones(OpcionVC);
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("No se pudo eliminar la programación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsVacaciones_Click(object sender, EventArgs e)
        {
            /*
            if (MessageBox.Show("¿Desea programar las vacaciones de este conductor?", "PROGRAMAR VACACIONES", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable workTable = new DataTable("Marcaciones");
                DataColumn column1 = new DataColumn("IDPersona");
                DataColumn column2 = new DataColumn("Fecha");

                workTable.Columns.Add(column1);
                workTable.Columns.Add(column2);
                DataRow row1 = workTable.NewRow();
                row1["IDPersona"] = "1";
                row1["Fecha"] = "2";
                workTable.Rows.Add(row1);

                Int32 selectedCellCount = dgvProgVC.GetCellCount(DataGridViewElementStates.Selected);

                if (selectedCellCount > 0)
                {
                    if (dgvProgVC.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<r>");

                        for (int i = 0; i < selectedCellCount; i++)
                        {
                            int col = Convert.ToInt32(dgvProgVC.SelectedCells[i].ColumnIndex.ToString()) - 7;
                            string dia = CalcularDia(col);
                            string PendV = Convert.ToString(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[5].Value);
                            string ProgV = Convert.ToString(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[4].Value);

                            if (selectedCellCount <= Convert.ToInt32(PendV) - Convert.ToInt32(ProgV))
                            {
                                dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                                sb.Append("<d ");
                                sb.Append("IDPersona=\"");
                                sb.Append(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                                sb.Append("\"");
                                sb.Append(" Fecha=\"");
                                sb.Append(dia);
                                sb.Append("\"");
                                sb.Append(" Pendiente=\"");
                                sb.Append(PendV);
                                sb.Append("\"");
                                sb.Append(" Programada=\"");
                                sb.Append(ProgV);
                                sb.Append("\"");
                                sb.Append(" />");
                            }
                            else
                            {
                                MessageBox.Show("No puede programar una mayor cantidad de días a los pendientes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        sb.Append("</r>");
                        xmlProgramacion = sb.ToString();
                    }

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionVC(dtpPeriodo.Text, xmlProgramacion, "VA (PR)", Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarTabla();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                { MessageBox.Show("No puede ingresar una cantidad en una casilla sin seleccionar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            */
        }

        private void tsCompensaciones_Click(object sender, EventArgs e)
        {
            /*
            if (MessageBox.Show("¿Desea programar las compensaciones de este conductor?", "PROGRAMAR COMPENSACIONES", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable workTable = new DataTable("Marcaciones");
                DataColumn column1 = new DataColumn("IDPersona");
                DataColumn column2 = new DataColumn("Fecha");

                workTable.Columns.Add(column1);
                workTable.Columns.Add(column2);
                DataRow row1 = workTable.NewRow();
                row1["IDPersona"] = "1";
                row1["Fecha"] = "2";
                workTable.Rows.Add(row1);

                Int32 selectedCellCount = dgvProgVC.GetCellCount(DataGridViewElementStates.Selected);

                if (selectedCellCount > 0)
                {
                    if (dgvProgVC.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<r>");

                        for (int i = 0; i < selectedCellCount; i++)
                        {
                            int col = Convert.ToInt32(dgvProgVC.SelectedCells[i].ColumnIndex.ToString()) - 7;
                            string dia = CalcularDia(col);
                            string PendC = Convert.ToString(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[7].Value);
                            string ProgC = Convert.ToString(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[6].Value);

                            if (selectedCellCount <= Convert.ToInt32(PendC) - Convert.ToInt32(ProgC))
                            {
                                dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                                sb.Append("<d ");
                                sb.Append("IDPersona=\"");
                                sb.Append(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                                sb.Append("\"");
                                sb.Append(" Fecha=\"");
                                sb.Append(dia);
                                sb.Append("\"");
                                sb.Append(" Pendiente=\"");
                                sb.Append(PendC);
                                sb.Append("\"");
                                sb.Append(" Programada=\"");
                                sb.Append(ProgC);
                                sb.Append("\"");
                                sb.Append(" />");
                            }
                            else
                            {
                                MessageBox.Show("No puede programar una mayor cantidad de días a los pendientes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        sb.Append("</r>");
                        xmlProgramacion = sb.ToString();
                    }

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionVC(dtpPeriodo.Text, xmlProgramacion, "CO (PR)", Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarTabla();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                { MessageBox.Show("No puede ingresar una cantidad en una casilla sin seleccionar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            */ 
        }

        private void tsEliminarProg_Click(object sender, EventArgs e)
        {
            /*
            DataTable workTable = new DataTable("Marcaciones");
            DataColumn column1 = new DataColumn("IDPersona");
            DataColumn column2 = new DataColumn("Fecha");

            workTable.Columns.Add(column1);
            workTable.Columns.Add(column2);
            DataRow row1 = workTable.NewRow();
            row1["IDPersona"] = "1";
            row1["Fecha"] = "2";
            workTable.Rows.Add(row1);

            Int32 selectedCellCount = dgvProgVC.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvProgVC.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvProgVC.SelectedCells[i].ColumnIndex.ToString()) - 7;
                        string dia = CalcularDia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        sb.Append("<d ");
                        sb.Append("IDPersona=\"");
                        sb.Append(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");
                        sb.Append(" />");
                    }

                    sb.Append("</r>");
                    xmlProgramacion = sb.ToString();

                    if (MessageBox.Show("¿Desea quitar la programación de este conductor?", "ELIMINAR PROGRAMACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionVC(dtpPeriodo.Text, xmlProgramacion, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarTabla();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            else
            { MessageBox.Show("No puede eliminar una casilla sin programar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */ 
        }

        private void tsAprobarProg_Click(object sender, EventArgs e)
        {
            /*
            DataTable workTable = new DataTable("Marcaciones");
            DataColumn column1 = new DataColumn("IDPersona");
            DataColumn column2 = new DataColumn("Fecha");

            workTable.Columns.Add(column1);
            workTable.Columns.Add(column2);
            DataRow row1 = workTable.NewRow();
            row1["IDPersona"] = "1";
            row1["Fecha"] = "2";
            workTable.Rows.Add(row1);

            Int32 selectedCellCount = dgvProgVC.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvProgVC.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvProgVC.SelectedCells[i].ColumnIndex.ToString()) - 7;
                        string dia = CalcularDia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        sb.Append("<d ");
                        sb.Append("IDPersona=\"");
                        sb.Append(dgvProgVC.Rows[dgvProgVC.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");
                        sb.Append(" />");
                    }

                    sb.Append("</r>");
                    xmlProgramacion = sb.ToString();

                    if (MessageBox.Show("¿Desea aprobar la programación de este conductor?", "APROBAR PROGRAMACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionVC(dtpPeriodo.Text, xmlProgramacion, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            dtListaTicket = new DataTable();
                            //dtListaTicket = clsLogisticaBL.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionesVC(xmlProgramacion);



                            CargarTabla();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            else
            { MessageBox.Show("No puede dar visto bueno a una casilla sin programar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */ 
        }
    }
}
