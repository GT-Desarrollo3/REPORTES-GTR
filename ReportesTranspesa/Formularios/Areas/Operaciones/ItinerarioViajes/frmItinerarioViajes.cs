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
    public partial class frmItinerarioViajes : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaConsolidado;
        public DataTable dtPermisos = new DataTable();

        public frmItinerarioViajes()
        {
            InitializeComponent();
        }

        private void frmItinerarioViajes_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmItinerarioViajes");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevoTiempo.Enabled = true; }
                    else { btnNuevoTiempo.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminar.Enabled = true; }
                    else { tsEliminar.Enabled = false; }
                }
            }

            ListarConsolidado();
        }


        public void ListarConsolidado()
        {
            dtListaConsolidado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidado(dtpFechaIni.Text, dtpFechaFin.Text, txtVehiculo.Text, txtConductor.Text, txtRuta.Text);
            dtgListaItinerario.DataSource = dtListaConsolidado;
            if (dtListaConsolidado.Rows.Count > 0)
            {
                dgvListaItinerario.Columns["idConsolidado"].Visible = false;
                dgvListaItinerario.Columns["idRuta"].Visible = false;

                dgvListaItinerario.Columns["FECHA_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FECHA_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaItinerario.Columns["FECHA_ESTIMADA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FECHA_ESTIMADA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaItinerario.Columns["FECHA_TERMINO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FECHA_TERMINO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaItinerario.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dgvListaItinerario.Columns["CONDUCTOR"].Summary.Clear();
                dgvListaItinerario.Columns["CONDUCTOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                dgvListaItinerario.BestFitColumns();
            }
        }


        private void btnNuevoTiempo_Click(object sender, EventArgs e)
        {
            frmMaestroTiemposXRuta frmMaestroTiemposXRuta = new frmMaestroTiemposXRuta();
            frmMaestroTiemposXRuta.ShowDialog();
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConsolidado(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConsolidado(); }
        }

        private void txtVehiculo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConsolidado(); }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConsolidado(); }
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConsolidado(); }
        }

        private void dtgListaItinerario_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvListaItinerario.GetRowCellValue(dgvListaItinerario.FocusedRowHandle, "CÓDIGO").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminar.Enabled = true; }
                }
                else { tsEliminar.Enabled = false; }
            }
            catch { tsEliminar.Enabled = false; }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idConsolidado = Convert.ToInt32(dgvListaItinerario.GetRowCellValue(dgvListaItinerario.FocusedRowHandle, "idConsolidado"));

                if (MessageBox.Show("¿Desea eliminar este consolidado?", "ELIMINAR CONSOLIDADO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidado(2, idConsolidado, 0, DateTime.Now, 0, 0, 0, 0, DateTime.Now, "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarConsolidado(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el consolidado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarConsolidado(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaItinerario.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "CONSOLIDADO DE VIAJES - " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgListaItinerario.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvListaItinerario_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == " ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "  ")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dtgListaItinerario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    frmRegistrarTiempoParada frmRegistrarTiempoParada = new frmRegistrarTiempoParada();
                    frmRegistrarTiempoParada.idConsolidado = Convert.ToInt32(dgvListaItinerario.GetRowCellValue(dgvListaItinerario.FocusedRowHandle, "idConsolidado"));
                    frmRegistrarTiempoParada.formulario = this;

                    frmRegistrarTiempoParada.txtConductor.Text = dgvListaItinerario.GetRowCellValue(dgvListaItinerario.FocusedRowHandle, "CONDUCTOR").ToString();
                    frmRegistrarTiempoParada.txtRuta.Text = dgvListaItinerario.GetRowCellValue(dgvListaItinerario.FocusedRowHandle, "RUTA").ToString();
                    frmRegistrarTiempoParada.txtFechaInicio.Text = dgvListaItinerario.GetRowCellValue(dgvListaItinerario.FocusedRowHandle, "FECHA_VIAJE").ToString();
                    frmRegistrarTiempoParada.txtFechaFin.Text = dgvListaItinerario.GetRowCellValue(dgvListaItinerario.FocusedRowHandle, "FECHA_TERMINO").ToString();
                    frmRegistrarTiempoParada.ShowDialog();
                }
            }
            catch { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
