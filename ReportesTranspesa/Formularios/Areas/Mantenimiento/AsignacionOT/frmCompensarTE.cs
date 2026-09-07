using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    public partial class frmCompensarTE : Form
    {
        public int xClick = 0, yClick = 0;
        public int Persona;
        public string TotalHE;

        public frmCompensarTE()
        {
            InitializeComponent();
        }

        private void frmCompensarTE_Load(object sender, EventArgs e)
        {
            ListarHorasCompensadas();
        }


        public void ListarHorasCompensadas()
        {
            DataTable dtListaCompensaciones = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarHEMecanico(txtNombre.Text);
            dtgCompensacion.DataSource = dtListaCompensaciones;
            if (dtListaCompensaciones.Rows.Count > 0)
            {
                dtgvCompensacionView.Columns["PERSONA"].Visible = false;
                
                dtgvCompensacionView.BestFitColumns();
            }
        }

        public void ListarCompensaciones()
        {
            DataTable dtListaUsuariosComp = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarCompensaciones(Persona);
            dtgCompensarUsuario.DataSource = dtListaUsuariosComp;
            if (dtListaUsuariosComp.Rows.Count > 0)
            {
                dtgvCompensarUsuarioView.Columns["TOTAL_COMP"].Visible = false;

                dtgvCompensarUsuarioView.Columns["HORAS_COMP"].Summary.Clear();
                dtgvCompensarUsuarioView.Columns["HORAS_COMP"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL_COMP", "Total Comp.: " + Convert.ToString(dtgvCompensarUsuarioView.GetRowCellValue(0, "TOTAL_COMP")));

                dtgvCompensarUsuarioView.BestFitColumns();
            }
        }

        public void Imprimir(string NroTicket)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                {
                    MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Ticket ticket = new Ticket();

                    ticket.AddSubHeaderLine2("COMPENSACIÓN DE");
                    ticket.AddSubHeaderLine2("HORAS EXTRAS");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine2("CÓDIGO: " + NroTicket + "                         ");
                    ticket.AddSubHeaderLine("Mecánico: " + txtMecanico.Text);
                    ticket.AddSubHeaderLine("Fecha Inicio: " + Convert.ToString(dtgvCompensarUsuarioView.GetRowCellValue(dtgvCompensarUsuarioView.FocusedRowHandle, "FECHA_INICIO")) + "                         ");
                    ticket.AddSubHeaderLine("Fecha Fin: " + Convert.ToString(dtgvCompensarUsuarioView.GetRowCellValue(dtgvCompensarUsuarioView.FocusedRowHandle, "FECHA_FIN")));
                    ticket.AddSubHeaderLine("Horas Comp.: " + Convert.ToString(dtgvCompensarUsuarioView.GetRowCellValue(dtgvCompensarUsuarioView.FocusedRowHandle, "HORAS_COMP")));
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.PrintTicket(NombreImpresora);
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHorasCompensadas(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarHorasCompensadas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgCompensacion.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Lista de Horas Extra de Mecánicos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgCompensacion.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgCompensacion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                pCompensacion.Visible = true;
                pCompensacion.BringToFront();

                Persona = Convert.ToInt32(dtgvCompensacionView.GetRowCellValue(dtgvCompensacionView.FocusedRowHandle, "PERSONA"));
                txtMecanico.Text = Convert.ToString(dtgvCompensacionView.GetRowCellValue(dtgvCompensacionView.FocusedRowHandle, "NOMBRE"));
                TotalHE = Convert.ToString(dtgvCompensacionView.GetRowCellValue(dtgvCompensacionView.FocusedRowHandle, "HORAS_EXTRA"));
                dtpCFechaIni.Value = DateTime.Now;
                dtpCHoraIni.Value = DateTime.Now;
                dtpCFechaFin.Value = DateTime.Now;
                dtpCHoraFin.Value = DateTime.Now;

                if (TotalHE.Substring(0, 5) == "00:00") { btnAgregar.Enabled = false; }
                else { btnAgregar.Enabled = true; } 

                ListarCompensaciones();
                dtpCFechaIni.Focus();
            }
            catch {}
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pCompensacion.Visible = false;
            pCompensacion.SendToBack();
            dtgCompensarUsuario.DataSource = null;
            dtgvCompensarUsuarioView.Columns["HORAS_COMP"].Summary.Clear();
            TotalHE = "";
        }

        private void pCompensacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pCompensacion.Left = pCompensacion.Left + (e.X - xClick);
                pCompensacion.Top = pCompensacion.Top + (e.Y - yClick);
            }
        }

        private void dtpCFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpCHoraIni.Focus(); }
        }

        private void dtpCHoraIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpCFechaFin.Focus(); }
        }

        private void dtpCFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpCHoraFin.Focus(); }
        }

        private void dtpCHoraFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar_Click(sender, e); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMecanico.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMecanico.Focus();
            }
            else
            {
                if (MessageBox.Show("¿Desea compensar las horas extras del mecánico?", "COMPENSAR HORAS EXTRAS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtCompensarC = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtCompensarC = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp(1, "", Persona, dtpCFechaIni.Text, dtpCHoraIni.Text,
                                                                dtpCFechaFin.Text, dtpCHoraFin.Text, TotalHE, Usuario);
                    respta = Convert.ToString(dtCompensarC.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarCompensaciones();
                        ListarHorasCompensadas();
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                dtpCFechaIni.Focus();
            }
        }

        private void imprimirTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Codigo = Convert.ToString(dtgvCompensarUsuarioView.GetRowCellValue(dtgvCompensarUsuarioView.FocusedRowHandle, "CÓDIGO"));

                if (MessageBox.Show("¿Desea imprimir el ticket?", "IMPRIMIR TICKET", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { Imprimir(Codigo); }
            }
            catch { MessageBox.Show("Se produjo un error al imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar esta compensación?", "ELIMINAR COMPENSACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string CodigoComp = Convert.ToString(dtgvCompensarUsuarioView.GetRowCellValue(dtgvCompensarUsuarioView.FocusedRowHandle, "CÓDIGO"));
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp(2, CodigoComp, Persona, "", "", "", "", "", Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarCompensaciones();
                        ListarHorasCompensadas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("La compensación seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgCompensarUsuario_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dtgvCompensarUsuarioView.GetRowCellValue(dtgvCompensarUsuarioView.FocusedRowHandle, "CÓDIGO").ToString();

                if (Codigo != "") { tsEliminar.Enabled = true; }
                else { tsEliminar.Enabled = false; }
            }
            catch { tsEliminar.Enabled = false; }
        }
    }
}
