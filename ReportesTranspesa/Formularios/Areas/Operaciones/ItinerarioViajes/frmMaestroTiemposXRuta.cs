using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
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
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ItinerarioViajes
{
    public partial class frmMaestroTiemposXRuta : Form
    {
        public string Trafico;
        public int idRuta;
        public int xClick = 0, yClick = 0;
        public DataTable dtListaParada, dtListaRuta;

        public frmMaestroTiemposXRuta()
        {
            InitializeComponent();
            cbxPuntoInicio.SelectedIndexChanged -= cbxPuntoInicio_SelectedIndexChanged;
            cbxPuntoParada.SelectedIndexChanged -= cbxPuntoParada_SelectedIndexChanged;
        }

        private void cbxPuntoInicio_SelectedIndexChanged(object sender, EventArgs e) { ListarParadas(); }

        private void cbxPuntoParada_SelectedIndexChanged(object sender, EventArgs e) { ListarParadas(); }

        private void frmMaestroTiemposXRuta_Load(object sender, EventArgs e)
        {
            ListarParadas();
            ListarParadas2();
            ListarRutas();
            ListarParada();
            rbIda.Checked = true;
            rbIda_Click(sender, e);
        }


        public void ListarParadas()
        {
            DataTable dtPuntosParadas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarPuntosParada(1);
            cbxPuntoInicio.DataSource = dtPuntosParadas;
            cbxPuntoInicio.DisplayMember = "Descripcion";
            cbxPuntoInicio.ValueMember = "idPuntoParada";
        }

        public void ListarParadas2()
        {
            DataTable dtPuntosParadas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarPuntosParada(1);
            cbxPuntoParada.DataSource = dtPuntosParadas;
            cbxPuntoParada.DisplayMember = "Descripcion";
            cbxPuntoParada.ValueMember = "idPuntoParada";
        }

        public void ListarParada()
        {
            dtListaParada = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarParadasRutas(1, txtBuscarP.Text);
            dtgListaParadas.DataSource = dtListaParada;
            if (dtListaParada.Rows.Count > 0)
            {
                dgvListaParadas.Columns["idParada"].Visible = false;
                dgvListaParadas.Columns["idRuta"].Visible = false;

                //dgvLimaNorteBG.Columns["PORCENTAJE"].Summary.Clear();
                //dgvLimaNorteBG.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "PORCENTAJE", "Total = {0:N2} %");

                dgvListaParadas.BestFitColumns();
            }
        }

        public void ListarRutas()
        {
            dtListaRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarParadasRutas(2, txtBuscarR.Text);
            dtgTiempoXRuta.DataSource = dtListaRuta;
            if (dtListaRuta.Rows.Count > 0)
            {
                dgvTiempoXRuta.Columns["idRuta"].Visible = false;
                
                //dgvLimaNorteBG.Columns["PORCENTAJE"].Summary.Clear();
                //dgvLimaNorteBG.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "PORCENTAJE", "Total = {0:N2} %");

                dgvTiempoXRuta.BestFitColumns();
            }
        }


        // pAgregarParada
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pAgregarParada.Visible = true;
            pAgregarParada.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pAgregarParada.Visible = false;
            pAgregarParada.SendToBack();
        }

        private void btnGuardarP_Click(object sender, EventArgs e)
        {
            if (txtPuntoParada.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un punto de parada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPuntoParada.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarPuntoParada(txtPuntoParada.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarParadas();
                    ListarParadas2();
                    txtPuntoParada.Clear();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void pAgregarParada_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pAgregarParada.Left = pAgregarParada.Left + (e.X - xClick);
                pAgregarParada.Top = pAgregarParada.Top + (e.Y - yClick);
            }
        }
        // pAgregarParada

        // RegistroParada
        private void rbIda_Click(object sender, EventArgs e)
        {
            Trafico = "IDA";
            rbIda.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbRetorno.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
        }

        private void rbRetorno_Click(object sender, EventArgs e)
        {
            Trafico = "RETORNO";
            rbIda.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbRetorno.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        }

        private void txtRutasActivas_Enter(object sender, EventArgs e) { txtRutasActivas.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtRutasActivas_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lvRuta, clsConsultaBL.Instancia.GetRutasActivas(txtRutasActivas.Text), true, false, false);
            lvRuta.Columns[0].Width = 0;
            lvRuta.Columns[1].Width = 350;
            lvRuta.BringToFront();
            lvRuta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                lvRuta.SendToBack();
                idRuta = -1;
            }
        }

        private void txtRutasActivas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lvRuta.Focus(); }
        }

        private void txtRutasActivas_Leave(object sender, EventArgs e) { txtRutasActivas.BackColor = Color.White; }

        private void lvRuta_Enter(object sender, EventArgs e)
        {
            if (!lvRuta.Items.Count.Equals(0)) { lvRuta.Items[0].Selected = true; }
        }

        private void lvRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lvRuta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvRuta.SelectedItems[0];
                idRuta = Int32.Parse(ItemActual.Text);
                txtRutasActivas.Text = ItemActual.SubItems[1].Text;

                lvRuta.Visible = false;
                lvRuta.SendToBack();
                cbxPuntoInicio.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                lvRuta.SendToBack();
                txtRutasActivas.Focus();
                idRuta = -1;
            }
        }

        private void lvRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lvRuta.SelectedItems[0];
            idRuta = Int32.Parse(ItemActual.Text);
            txtRutasActivas.Text = ItemActual.SubItems[1].Text;

            lvRuta.Visible = false;
            lvRuta.SendToBack();
            cbxPuntoInicio.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            idRuta = -1;
            rbIda.Checked = true;
            rbIda_Click(sender, e);
            txtRutasActivas.Clear();
            cbxPuntoInicio.Text = "Base Trujillo";
            cbxPuntoParada.Text = "Base Trujillo";
            dtpTiempoViaje.Text = "00:00:00";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRutasActivas.Text.Length == 0 || cbxPuntoInicio.Text.Length == 0 || cbxPuntoParada.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                if (txtRutasActivas.Text.Length == 0) { txtRutasActivas.Focus(); }
                else
                {
                    if (cbxPuntoInicio.Text.Length == 0) { cbxPuntoInicio.Focus(); }
                    else { cbxPuntoParada.Focus(); }
                }
                
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarEliminarParada(1, 0, Trafico, idRuta, cbxPuntoInicio.Text, cbxPuntoParada.Text, dtpTiempoViaje.Value, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancelar_Click(sender, e);
                    ListarRutas();
                    ListarParada();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgListaParadas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Parada = dgvListaParadas.GetRowCellValue(dgvListaParadas.FocusedRowHandle, "idParada").ToString();

                if (Parada != "") { tsEliminar.Enabled = true; }
                else { tsEliminar.Enabled = false; }
            }
            catch { tsEliminar.Enabled = false; }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idParada = Convert.ToInt32(dgvListaParadas.GetRowCellValue(dgvListaParadas.FocusedRowHandle, "idParada"));
                int idRuta = Convert.ToInt32(dgvListaParadas.GetRowCellValue(dgvListaParadas.FocusedRowHandle, "idRuta"));

                if (MessageBox.Show("¿Desea eliminar esta parada?", "ELIMINAR PARADA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarEliminarParada(2, idParada, "", idRuta, "", "", DateTime.Now, "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarRutas();
                        ListarParada();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar la parada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgTiempoXRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                idRuta = Convert.ToInt32(dgvTiempoXRuta.GetRowCellValue(dgvTiempoXRuta.FocusedRowHandle, "idRuta"));
                txtRutasActivas.Text = Convert.ToString(dgvTiempoXRuta.GetRowCellValue(dgvTiempoXRuta.FocusedRowHandle, "RUTA"));
            }
            catch { }
        }

        private void dtpTiempoViaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }
        // RegistroParada

        // BuscarListaParada
        private void txtBuscarR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtBuscarP.Text = txtBuscarR.Text;
                ListarRutas();
                ListarParada();
            }
        }

        private void txtBuscarP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtBuscarP.Text = txtBuscarR.Text;
                ListarRutas();
                ListarParada();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dtgListaParadas.ForceInitialize();
                dtgTiempoXRuta.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "Maestro de Tiempo X Ruta - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            try
            {
                dtgListaParadas.ForceInitialize();
                dtgTiempoXRuta.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "Maestro de Tiempo X Ruta - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        // BuscarListaParada
    }
}
