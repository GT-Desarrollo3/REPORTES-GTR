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
    public partial class frmRegistrarTiempoParada : Form
    {
        public int idConsolidado, idParada;
        public frmItinerarioViajes formulario;
        public DataTable dtListaConsolidado;

        public frmRegistrarTiempoParada()
        {
            InitializeComponent();
        }

        private void frmRegistrarTiempoParada_Load(object sender, EventArgs e)
        {
            dtpTiempoViaje.Text = "00:00:00";
            ListarConsolidado();
        }


        public void ListarConsolidado()
        {
            dtListaConsolidado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidadoDetalle(idConsolidado);
            dtgTiempoParada.DataSource = dtListaConsolidado;
            if (dtListaConsolidado.Rows.Count > 0)
            {
                dgvTiempoParada.Columns["idConsolidado"].Visible = false;
                dgvTiempoParada.Columns["idParada"].Visible = false;

                dgvTiempoParada.BestFitColumns();
            }
        }


        private void dtgTiempoParada_Click(object sender, EventArgs e)
        {
            try
            {
                idConsolidado = Convert.ToInt32(dgvTiempoParada.GetRowCellValue(dgvTiempoParada.FocusedRowHandle, "idConsolidado"));
                idParada = Convert.ToInt32(dgvTiempoParada.GetRowCellValue(dgvTiempoParada.FocusedRowHandle, "idParada"));

                txtPuntoInicio.Text = Convert.ToString(dgvTiempoParada.GetRowCellValue(dgvTiempoParada.FocusedRowHandle, "PUNTO_INICIO"));
                txtPuntoParada.Text = Convert.ToString(dgvTiempoParada.GetRowCellValue(dgvTiempoParada.FocusedRowHandle, "PUNTO_PARADA"));
            }
            catch { MessageBox.Show("La parada seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtPuntoInicio.Text.Length == 0 || txtPuntoParada.Text.Length == 0)
            {
                MessageBox.Show("Por favor, seleccione una parada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidadoDetalle(idConsolidado, idParada, dtpTiempoViaje.Value);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    formulario.ListarConsolidado();
                    DataTable dtFechaTermino = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarFechaTermino(idConsolidado);
                    txtFechaFin.Text = Convert.ToString(dtFechaTermino.Rows[0]["FechaTermino"]);
                    ListarConsolidado();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtpTiempoViaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void dtgTiempoParada_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string HoraDuracion = dgvTiempoParada.GetRowCellValue(dgvTiempoParada.FocusedRowHandle, "HORA_DURACION").ToString();

                if (HoraDuracion != "") { tsEliminar.Enabled = true; }
                else { tsEliminar.Enabled = false; }
            }
            catch { tsEliminar.Enabled = false; }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idConsolidado = Convert.ToInt32(dgvTiempoParada.GetRowCellValue(dgvTiempoParada.FocusedRowHandle, "idConsolidado"));
                int idParada = Convert.ToInt32(dgvTiempoParada.GetRowCellValue(dgvTiempoParada.FocusedRowHandle, "idParada"));

                DataTable dtRespuesta = new DataTable();
                string respta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_EliminarConsolidadoDetalle(idConsolidado, idParada);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    formulario.ListarConsolidado();
                    DataTable dtFechaTermino = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarFechaTermino(idConsolidado);
                    txtFechaFin.Text = Convert.ToString(dtFechaTermino.Rows[0]["FechaTermino"]);
                    ListarConsolidado();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el tiempo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
