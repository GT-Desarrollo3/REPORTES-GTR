using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmAgendaContactos : Form
    {
        int xClick = 0, yClick = 0;
        public int Opcion = 1;
        public DataTable ContactosLista = new DataTable();
        public DataTable dtContacto = new DataTable();
        public int idContacto = 0;

        public frmAgendaContactos()
        {
            InitializeComponent();
            cbxRubro.SelectedIndexChanged -= cbxRubro_SelectedIndexChanged;
            cbxRubro2.SelectedIndexChanged -= cbxRubro2_SelectedIndexChanged;
        }

        private void cbxRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboRubro1();
        }

        private void cbxRubro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboRubro2();
        }

        private void frmAgendaContactos_Load(object sender, EventArgs e)
        {
            CargarComboRubro1();
            CargarComboRubro2();
        }


        private void CargarComboRubro1()
        {
            DataTable dtRubro1 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarComboRubros(1);
            cbxRubro.DataSource = dtRubro1;
            cbxRubro.DisplayMember = "Descripcion";
            cbxRubro.ValueMember = "idRubro";
        }

        private void CargarComboRubro2()
        {
            DataTable dtRubro2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarComboRubros(2);
            cbxRubro2.DataSource = dtRubro2;
            cbxRubro2.DisplayMember = "Descripcion";
            cbxRubro2.ValueMember = "idRubro";
        }

        public void ListarContactos()
        {
            ContactosLista = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarContactos(Convert.ToInt32(cbxRubro.SelectedValue), txtRuta.Text);
            dtgContactos.DataSource = ContactosLista;
            if (ContactosLista.Rows.Count > 0)
            {
                dgvContactosVista.Columns["idContacto"].Visible = false;

                dgvContactosVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvContactosVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvContactosVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvContactosVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvContactosVista.BestFitColumns();
                dgvContactosVista.ExpandAllGroups();
            }
        }


        private void pDatosContacto_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pDatosContacto.Left = pDatosContacto.Left + (e.X - xClick);
                pDatosContacto.Top = pDatosContacto.Top + (e.Y - yClick);
            }
        }

        private void btnAgenda_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
            pDatosContacto.Visible = true;
            txtNombre.Focus();
            Opcion = 1;
            idContacto = 0;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pDatosContacto.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtNumero.Clear();
            txtUbicacion.Clear();
            cbxRubro2.SelectedValue = 1;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtNumero.Focus();
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtUbicacion.Focus();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length == 0 || txtNumero.Text.Length == 0 || txtUbicacion.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtNombre.Text.Length == 0) { txtNombre.Focus(); }
                else
                {
                    if (txtNumero.Text.Length == 0) { txtNumero.Focus(); }
                    else { txtUbicacion.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarContacto(Opcion, idContacto, txtNombre.Text, txtNumero.Text, txtUbicacion.Text, Convert.ToInt32(cbxRubro2.SelectedValue), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pDatosContacto.Visible = false;
                    ListarContactos();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                }
            }
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarContactos();
            }
        }

        private void cbxRubro_DropDownClosed(object sender, EventArgs e)
        {
            ListarContactos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarContactos();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgContactos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Contactos de Auxilios Mecánicos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgContactos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgContactos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idContacto = Convert.ToString(dgvContactosVista.GetRowCellValue(dgvContactosVista.FocusedRowHandle, "idContacto"));

                if (idContacto == "") { eliminarToolStripMenuItem.Enabled = false; }
                else { eliminarToolStripMenuItem.Enabled = true; }
            }
            catch
            {
                MessageBox.Show("El contacto seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgContactos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                btnCancelar_Click(sender, e);
                pDatosContacto.Visible = true;
                Opcion = 2;
                idContacto = Convert.ToInt32(dgvContactosVista.GetRowCellValue(dgvContactosVista.FocusedRowHandle, "idContacto"));

                dtContacto = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_BuscarContactos(idContacto);
                if (dtContacto.Rows.Count > 0)
                {
                    txtNombre.Text = dtContacto.Rows[0]["Nombre"].ToString();
                    txtNumero.Text = dtContacto.Rows[0]["Contacto"].ToString();
                    txtUbicacion.Text = dtContacto.Rows[0]["Ubicacion"].ToString();
                    cbxRubro2.Text = dtContacto.Rows[0]["Descripcion"].ToString();
                }
            }
            catch
            {
                MessageBox.Show("El contacto seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este contacto de forma permanente?", "ELIMINAR CONTACTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtRespuesta = new DataTable();
                idContacto = Convert.ToInt32(dgvContactosVista.GetRowCellValue(dgvContactosVista.FocusedRowHandle, "idContacto"));
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_EliminarContacto(idContacto);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarContactos();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
