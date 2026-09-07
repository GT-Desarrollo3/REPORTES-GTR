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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    public partial class frmMecanicosAsignados : Form
    {
        int Persona = -1, PersonaMotivo = -1;
        string Codigo = "";
        public DataTable dtListaAsignaciones = new DataTable();
        public DataTable dtPermisos = new DataTable();
        public int xClick = 0, yClick = 0;

        public frmMecanicosAsignados()
        {
            InitializeComponent();
        }

        private void frmMecanicosAsignados_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroOT");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { quitarMecanicoToolStripMenuItem.Enabled = true; }
                    else { quitarMecanicoToolStripMenuItem.Enabled = false; }
                }
            }

            dtpHora.Value = DateTime.Now;
        }


        public void BuscarMecanico()
        {
            DataTable dtMecanico = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(2, txtCodigo.Text);
            if (dtMecanico.Rows.Count > 0)
            {
                Persona = Convert.ToInt32(dtMecanico.Rows[0]["Persona"]);
                Codigo = dtMecanico.Rows[0]["CODIGO"].ToString();
                txtNombre.Text = dtMecanico.Rows[0]["NOMBRE"].ToString();
                txtTurno.Text = dtMecanico.Rows[0]["TURNO"].ToString();
            }
        }

        public void ListarAsignaciones(string OT)
        {
            dtListaAsignaciones = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(3, OT);
            dtgListaAsignaciones.DataSource = dtListaAsignaciones;
            if (dtListaAsignaciones.Rows.Count > 0)
            {
                dgvListaAsignacionesVista.Columns["Persona"].Visible = false;
                dgvListaAsignacionesVista.Columns["Estado"].Visible = false;

                dgvListaAsignacionesVista.Columns["INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaAsignacionesVista.Columns["INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaAsignacionesVista.Columns["FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaAsignacionesVista.Columns["FIN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvListaAsignacionesVista.BestFitColumns();
            }
        }


        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { BuscarMecanico(); }

            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                Persona = -1;
                Codigo = "";
                txtNombre.Clear();
                txtTurno.Clear();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese a un mecánico.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Focus();
                return;
            }
            else
            {
                DataTable dtAsignacion = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAsignacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico(1, Persona, lblCodigoOT.Text);
                respta = Convert.ToString(dtAsignacion.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    //MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Persona = -1;
                    Codigo = "";
                    txtNombre.Clear();
                    txtCodigo.Clear();
                    txtTurno.Clear();
                    ListarAsignaciones(lblCodigoOT.Text);
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnHorasExtra_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese a un mecánico.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigo.Focus();
                return;
            }
            else
            {
                DataTable dtTiempoExtra = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtTiempoExtra = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarTiempoExtra(Persona, lblCodigoOT.Text, dtpHora.Value);
                respta = Convert.ToString(dtTiempoExtra.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Persona = -1;
                    Codigo = "";
                    txtNombre.Clear();
                    txtCodigo.Clear();
                    txtTurno.Clear();
                    ListarAsignaciones(lblCodigoOT.Text);
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        }

        private void terminarOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea dar por finalizado el trabajo del mecánico?", "TERMINAR TRABAJO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Persona = Convert.ToInt32(dgvListaAsignacionesVista.GetRowCellValue(dgvListaAsignacionesVista.FocusedRowHandle, "Persona"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico(2, Persona, lblCodigoOT.Text);
                    ListarAsignaciones(lblCodigoOT.Text);
                }
            }
            catch { MessageBox.Show("El mecánico seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void quitarMecanicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea quitar a este mecánico de la OT?", "QUITAR MECÁNICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Persona = Convert.ToInt32(dgvListaAsignacionesVista.GetRowCellValue(dgvListaAsignacionesVista.FocusedRowHandle, "Persona"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico(3, Persona, lblCodigoOT.Text);
                    ListarAsignaciones(lblCodigoOT.Text);
                }
            }
            catch { MessageBox.Show("El mecánico seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pausarTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonaMotivo = Convert.ToInt32(dgvListaAsignacionesVista.GetRowCellValue(dgvListaAsignacionesVista.FocusedRowHandle, "Persona"));
            txtNombrePausa.Text = Convert.ToString(dgvListaAsignacionesVista.GetRowCellValue(dgvListaAsignacionesVista.FocusedRowHandle, "NOMBRE"));

            pMotivoPausa.Visible = true;
            pMotivoPausa.BringToFront();
        }

        private void btnAgregarMotivo_Click(object sender, EventArgs e)
        {
            DataTable dtRegistroOT = new DataTable();
            string respta;

            dtRegistroOT = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_PausarOTMecanico(PersonaMotivo, lblCodigoOT.Text, txtMotivo.Text);
            respta = Convert.ToString(dtRegistroOT.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);

            if (NroRspta == "0")
            {
                MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarAsignaciones(lblCodigoOT.Text);
                pictureBox1_Click(sender, e);
            }
            else
            { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaAsignaciones_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idPersona = Convert.ToString(dgvListaAsignacionesVista.GetRowCellValue(dgvListaAsignacionesVista.FocusedRowHandle, "Persona"));
                string Estado = Convert.ToString(dgvListaAsignacionesVista.GetRowCellValue(dgvListaAsignacionesVista.FocusedRowHandle, "Estado"));

                if (idPersona != "")
                {
                    if (Estado == "0")
                    {
                        terminarOTToolStripMenuItem.Enabled = true;
                        pausarTrabajoToolStripMenuItem.Enabled = true;
                        quitarMecanicoToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        terminarOTToolStripMenuItem.Enabled = false;
                        pausarTrabajoToolStripMenuItem.Enabled = false;
                        quitarMecanicoToolStripMenuItem.Enabled = false;
                    }
                    
                }
                else
                {
                    terminarOTToolStripMenuItem.Enabled = false;
                    pausarTrabajoToolStripMenuItem.Enabled = false;
                    quitarMecanicoToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                terminarOTToolStripMenuItem.Enabled = false;
                pausarTrabajoToolStripMenuItem.Enabled = false;
                quitarMecanicoToolStripMenuItem.Enabled = false;
            }
        }

        private void pMotivoPausa_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pMotivoPausa.Left = pMotivoPausa.Left + (e.X - xClick);
                pMotivoPausa.Top = pMotivoPausa.Top + (e.Y - yClick);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pMotivoPausa.Visible = false;
            pMotivoPausa.SendToBack();
            txtNombrePausa.Clear();
            txtMotivo.Clear();
            PersonaMotivo = -1;
        }
    }
}
