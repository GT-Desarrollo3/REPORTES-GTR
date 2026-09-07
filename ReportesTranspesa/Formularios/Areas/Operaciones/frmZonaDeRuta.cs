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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmZonaDeRuta : Form
    {
        public string esVALE;
        public int idRuta, idOperacion, Opcion = 0;
        public int xClick = 0, yClick = 0;

        public frmZonaDeRuta()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxOperacion2.SelectedIndexChanged -= cbxOperacion2_SelectedIndexChanged;
            cbxZona.SelectedIndexChanged -= cbxZona_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboOperacion(); }
        
        private void cbxOperacion2_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboOperacion2(); }

        private void cbxZona_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboZona(); }

        private void frmZonaDeRuta_Load(object sender, EventArgs e)
        {
            CargarComboOperacion();
            CargarComboOperacion2();
            CargarComboZona();
            cbxOperaciones.SelectedValue = 5;
            ListarZonas();
        }

        
        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacion2()
        {
            DataTable dtOperacion2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperacion2.DataSource = dtOperacion2;
            cbxOperacion2.DisplayMember = "Descripcion";
            cbxOperacion2.ValueMember = "IdOperacion";
        }

        public void CargarComboZona()
        {
            DataTable dtZonas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_RutaZona_ListarZonas();
            cbxZona.DataSource = dtZonas;
            cbxZona.DisplayMember = "Descripcion";
            cbxZona.ValueMember = "idZona";
        }

        public void ListarZonas()
        {
            dtgZonaRuta.DataSource = null;
            dgvZonaRutaVista.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_RutaZona_ListarRegistroZonas(Convert.ToInt32(cbxOperaciones.SelectedValue), txtRuta.Text);
            if (dt.Rows.Count > 0)
            {
                dtgZonaRuta.DataSource = dt;
                dgvZonaRutaVista.Columns["IdRuta"].Visible = false;
                dgvZonaRutaVista.Columns["IdOperacion"].Visible = false;
                dgvZonaRutaVista.Columns["idZona"].Visible = false;
                dgvZonaRutaVista.BestFitColumns();
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pNuevaZona.Visible = false;
            pNuevaZona.SendToBack();
            cbxOperacion2.SelectedValue = 1;
            txtNuevaRuta.Clear();
            idRuta = 0;
            cbxZona.SelectedValue = 1;
        }

        private void pNuevaZona_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevaZona.Left = pNuevaZona.Left + (e.X - xClick);
                pNuevaZona.Top = pNuevaZona.Top + (e.Y - yClick);
            }
        }

        private void btnNuevaZona_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            cbxOperacion2.SelectedValue = 2;
            pNuevaZona.Visible = true;
            pNuevaZona.BringToFront();
        }

        private void txtNuevaRuta_Enter(object sender, EventArgs e)
        { txtNuevaRuta.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtNuevaRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtNuevaRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas))
                {
                    if (esVALE == "SI") { cbxZona.Focus(); }
                    else
                    {
                        ListViewItem RutaActual;
                        RutaActual = lvRuta.SelectedItems[0];
                        idRuta = Convert.ToInt32(RutaActual.SubItems[0].Text);
                        txtNuevaRuta.Text = RutaActual.SubItems[1].Text;
                        lvRuta.Visible = false;
                        cbxZona.Focus();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtNuevaRuta.Focus(); }

            if (e.KeyChar == (char)Keys.Back) { idRuta = 0; }
        }

        private void txtNuevaRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtNuevaRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (e.KeyCode == Keys.Down) { lvRuta.Focus(); }
        }

        private void txtNuevaRuta_Leave(object sender, EventArgs e)
        { txtNuevaRuta.BackColor = Color.White; }

        private void lvRuta_Enter(object sender, EventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtNuevaRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lvRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtNuevaRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas))
                {
                    if (esVALE == "SI") { cbxZona.Focus(); }
                    else { txtNuevaRuta.Select(); }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if ((e.KeyChar == (char)Keys.Enter) && !lvRuta.Items.Count.Equals(0))
            {
                ListViewItem RutaActual;
                RutaActual = lvRuta.SelectedItems[0];
                idRuta = Convert.ToInt32(RutaActual.SubItems[0].Text);
                txtNuevaRuta.Text = RutaActual.SubItems[1].Text;
                lvRuta.Visible = false;
                cbxZona.Focus();
            }
        }

        private void lvRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtNuevaRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutasActivas); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lvRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem RutaActual;
            RutaActual = lvRuta.SelectedItems[0];
            idRuta = Convert.ToInt32(RutaActual.SubItems[0].Text);
            txtNuevaRuta.Text = RutaActual.SubItems[1].Text;
            lvRuta.Visible = false;
            cbxZona.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cbxOperacion2.SelectedValue = 1;
            txtNuevaRuta.Clear();
            idRuta = 0;
            cbxZona.SelectedValue = 1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNuevaRuta.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese una ruta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevaRuta.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_RutaZona_AsignarEditarZonas(Opcion,idRuta,Convert.ToInt32(cbxOperacion2.SelectedValue),
                                                         Convert.ToInt32(cbxZona.SelectedValue), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarZonas();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgZonaRuta_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Opcion = 2;
                cbxOperacion2.Text = dgvZonaRutaVista.GetRowCellValue(dgvZonaRutaVista.FocusedRowHandle, "OPERACION").ToString();
                idRuta = Convert.ToInt32(dgvZonaRutaVista.GetRowCellValue(dgvZonaRutaVista.FocusedRowHandle, "IdRuta"));
                txtNuevaRuta.Text = dgvZonaRutaVista.GetRowCellValue(dgvZonaRutaVista.FocusedRowHandle, "RUTA").ToString();
                cbxZona.Text = dgvZonaRutaVista.GetRowCellValue(dgvZonaRutaVista.FocusedRowHandle, "ZONA").ToString();
                pNuevaZona.Visible = true;
                pNuevaZona.BringToFront();
            }
            catch { MessageBox.Show("La ruta seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarZonas(); }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { ListarZonas(); }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarZonas(); } }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgZonaRuta.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE ZONAS POR RUTA - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgZonaRuta.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
