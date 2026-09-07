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

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmListarTardanzas : MetroFramework.Forms.MetroForm
    {
        string Turno;
        int idPersona;
        DateTime FechaIngreso;
        int xClick = 0, yClick = 0;
        int xClick2 = 0, yClick2 = 0;

        public frmListarTardanzas()
        {
            InitializeComponent();
        }

        private void frmListarTardanzas_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpHoraInicio.Value = new DateTime(dtpHoraInicio.Value.Year, dtpHoraInicio.Value.Month, 1, 8, 0, 59);
            dtpHoraFin.Value = new DateTime(dtpHoraInicio.Value.Year, dtpHoraInicio.Value.Month, 1, 10, 0, 0);
            rbManiana.Checked = true;
            rbManiana_CheckedChanged(sender, e);
        }


        public void ListarTardanzas()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                /*
                DataTable dtListaTardanzas = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarTardanzas(1,dtpFechaInicio.Text, dtpFechaFin.Text,
                                             dtpHoraInicio.Text, dtpHoraFin.Text, txtEmpleado.Text, Turno);
                dtgTardanzas.DataSource = dtListaTardanzas;
                if (dtListaTardanzas.Rows.Count > 0)
                {
                    dtgvTardanzasView.Columns["DNI"].Visible = false;

                    dtgvTardanzasView.BestFitColumns();
                }
                */ 
            }
        }

        public void ListarTardanzasUsuario()
        {
            /*
            dtgTardanzaUsuario.DataSource = null;
            DataTable dtListaTardanzasEmpleado = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarTardanzas(2, dtpFechaInicio.Text, dtpFechaFin.Text,
                                                 dtpHoraInicio.Text, dtpHoraFin.Text, lblEmpleado.Text, Turno);
            dtgTardanzaUsuario.DataSource = dtListaTardanzasEmpleado;
            if (dtListaTardanzasEmpleado.Rows.Count > 0)
            {
                dgvTardanzaUsuarioVista.Columns["DNI"].Visible = false;

                dgvTardanzaUsuarioVista.BestFitColumns();
            }
            */
        }


        private void rbManiana_CheckedChanged(object sender, EventArgs e)
        {
            if (rbManiana.Checked == true)
            {
                Turno = "M";
                rbManiana.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                rbTarde.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbNoche.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                ListarTardanzas();
            }
        }

        private void rbTarde_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTarde.Checked == true)
            {
                Turno = "T";
                rbManiana.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbTarde.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                rbNoche.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                ListarTardanzas();
            }
        }

        private void rbNoche_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoche.Checked == true)
            {
                Turno = "N";
                rbManiana.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbTarde.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbNoche.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                ListarTardanzas();
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTardanzas(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTardanzas(); }
        }

        private void dtpHoraInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTardanzas(); }
        }

        private void dtpHoraFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTardanzas(); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTardanzas(); }
        }

        private void dtgTardanzas_DoubleClick(object sender, EventArgs e)
        {
            lblEmpleado.Text = dtgvTardanzasView.GetRowCellValue(dtgvTardanzasView.FocusedRowHandle, "EMPLEADO").ToString();
            ListarTardanzasUsuario();
            pTardanzasUsuario.Visible = true;
            pTardanzasUsuario.BringToFront();
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            lblEmpleado.Text = "";
            dtgTardanzaUsuario.DataSource = null;
            pTardanzasUsuario.Visible = false;
            pTardanzasUsuario.SendToBack();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarTardanzas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgTardanzas.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                DataTable dtListaTardanzasE = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarTardanzasExcel(dtpFechaInicio.Text, dtpFechaFin.Text,
                                              dtpHoraInicio.Text, dtpHoraFin.Text, txtEmpleado.Text);
                dtgTardanzasExc.DataSource = null;
                dtgvTardanzasExcView.Columns.Clear();
                dtgTardanzasExc.DataSource = dtListaTardanzasE;

                if (dtListaTardanzasE.Rows.Count > 0)
                {
                    dtgvTardanzasExcView.BestFitColumns();

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Registro de Tardanzas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgTardanzasExc.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }

        private void pTardanzasUsuario_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pTardanzasUsuario.Left = pTardanzasUsuario.Left + (e.X - xClick);
                pTardanzasUsuario.Top = pTardanzasUsuario.Top + (e.Y - yClick);
            }
        }

        private void pDescontar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pDescontar.Left = pDescontar.Left + (e.X - xClick2);
                pDescontar.Top = pDescontar.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            txtMotivo.Clear();
            pDescontar.Visible = false;
            pDescontar.SendToBack();
        }

        private void tsJustificacion_Click(object sender, EventArgs e)
        {
            idPersona = Convert.ToInt32(dgvTardanzaUsuarioVista.GetRowCellValue(dgvTardanzaUsuarioVista.FocusedRowHandle, "PERSONA"));
            FechaIngreso = Convert.ToDateTime(dgvTardanzaUsuarioVista.GetRowCellValue(dgvTardanzaUsuarioVista.FocusedRowHandle, "HORA_LLEGADA"));
            pDescontar.Visible = true;
            pDescontar.BringToFront();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingrese el motivo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMotivo.Focus();
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(1, 0, idPersona, FechaIngreso, txtMotivo.Text, Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarTardanzas();
                    ListarTardanzasUsuario();
                    btnCerrar2_Click(sender, e);
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarMotivo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el motivo de esta tardanza?", "MOTIVO DE TARDANZA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                idPersona = Convert.ToInt32(dgvTardanzaUsuarioVista.GetRowCellValue(dgvTardanzaUsuarioVista.FocusedRowHandle, "PERSONA"));
                FechaIngreso = Convert.ToDateTime(dgvTardanzaUsuarioVista.GetRowCellValue(dgvTardanzaUsuarioVista.FocusedRowHandle, "HORA_LLEGADA"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(2, 0, idPersona, FechaIngreso, "", Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    ListarTardanzas();
                    ListarTardanzasUsuario();
                    btnCerrar2_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
