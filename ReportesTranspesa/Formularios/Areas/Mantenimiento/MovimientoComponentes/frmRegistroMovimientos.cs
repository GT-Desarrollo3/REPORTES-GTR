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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.MovimientoComponentes
{
    public partial class frmRegistroMovimientos : MetroFramework.Forms.MetroForm
    {
        public int idSistema, FiltroS;
        public DataTable dtListaMovimientos;
        public DataTable dtPermisos = new DataTable();

        public frmRegistroMovimientos()
        {
            InitializeComponent();
            cbxSistema.SelectedIndexChanged -= cbxSistema_SelectedIndexChanged;
            cbxSubSistema.SelectedIndexChanged -= cbxSubSistema_SelectedIndexChanged;
        }

        private void cbxSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSistema(); }

        private void cbxSubSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSubSistema(); }

        private void frmRegistroMovimientos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroMovimientos");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevoMovimiento.Enabled = true; }
                    else { btnNuevoMovimiento.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsEditarMovimiento.Enabled = true; }
                    else { tsEditarMovimiento.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarMovimiento.Enabled = true; }
                    else { tsEliminarMovimiento.Enabled = false; }
                }
            }

            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            cbFiltroComponentes.Checked = false;
            cbFiltroComponentes_CheckedChanged(sender, e);

            idSistema = 3;
            CargarComboSistema();
            CargarComboSubSistema();

            ListarMovimientos();
        }


        public void CargarComboSistema()
        {
            DataTable dtSistema = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos();
            cbxSistema.DataSource = dtSistema;
            cbxSistema.DisplayMember = "Descripcion";
            cbxSistema.ValueMember = "idSistemaVehiculo";
        }

        public void CargarComboSubSistema()
        {
            DataTable dtSubSistema = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos(idSistema);
            cbxSubSistema.DataSource = dtSubSistema;
            cbxSubSistema.DisplayMember = "Descripcion";
            cbxSubSistema.ValueMember = "idSubSistema";
        }

        public void ListarMovimientos()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaMovimientos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MovimientoC_ListarComponentes(FiltroS, txtPlaca.Text, dtpFechaInicio.Text, dtpFechaFin.Text,
                                                                  Convert.ToInt32(cbxSistema.SelectedValue), Convert.ToInt32(cbxSubSistema.SelectedValue));
                dtgMovimientos.DataSource = dtListaMovimientos;
                if (dtListaMovimientos.Rows.Count > 0)
                {
                    dgvMovimientosView.Columns["idUnidadProcedencia"].Visible = false;
                    dgvMovimientosView.Columns["idUnidadDestino"].Visible = false;
                    dgvMovimientosView.Columns["idSistemaVehiculo"].Visible = false;
                    dgvMovimientosView.Columns["idSubSistema"].Visible = false;
                    dgvMovimientosView.Columns["PersonaAutorizada"].Visible = false;

                    dgvMovimientosView.Columns["FECHA_EJECUCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMovimientosView.Columns["FECHA_EJECUCION"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvMovimientosView.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMovimientosView.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvMovimientosView.Columns["FechaModifica"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMovimientosView.Columns["FechaModifica"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                    dgvMovimientosView.Columns["COMPONENTE"].Summary.Clear();
                    dgvMovimientosView.Columns["COMPONENTE"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvMovimientosView.BestFitColumns();
                }
            }
        }


        private void btnNuevoMovimiento_Click(object sender, EventArgs e)
        {
            frmNuevoMovimiento frmNuevoMovimiento = new frmNuevoMovimiento();
            frmNuevoMovimiento.formulario = this;
            frmNuevoMovimiento.Opcion = 1;
            frmNuevoMovimiento.idMovimientoC = 0;
            frmNuevoMovimiento.label1.Text = "REGISTRAR COMPONENTE";
            frmNuevoMovimiento.dtpFechaEjecucion.Value = DateTime.Now;
            frmNuevoMovimiento.idSistema = 3;
            frmNuevoMovimiento.CargarComboSistema();
            frmNuevoMovimiento.CargarComboSubSistema();
            frmNuevoMovimiento.ShowDialog();
        }

        private void cbFiltroComponentes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFiltroComponentes.Checked == true)
            {
                FiltroS = 1;
                groupBox2.Enabled = true;
            }

            if (cbFiltroComponentes.Checked == false)
            {
                FiltroS = 0;
                groupBox2.Enabled = false;
            }

            ListarMovimientos();
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMovimientos(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMovimientos(); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMovimientos(); }
        }

        private void cbxSistema_DropDownClosed(object sender, EventArgs e)
        {
            idSistema = Convert.ToInt32(cbxSistema.SelectedValue);
            CargarComboSubSistema();
        }

        private void cbxSubSistema_DropDownClosed(object sender, EventArgs e) { ListarMovimientos(); }

        private void dtgMovimientos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idMovimientoC = Convert.ToInt32(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "NRO"));

                if (idMovimientoC != 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsEditarMovimiento.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarMovimiento.Enabled = true; }
                }
                else
                {
                    tsEditarMovimiento.Enabled = false;
                    tsEliminarMovimiento.Enabled = false;
                }
            }
            catch
            {
                tsEditarMovimiento.Enabled = false;
                tsEliminarMovimiento.Enabled = false;
            }
        }

        private void tsEditarMovimiento_Click(object sender, EventArgs e)
        {
            frmNuevoMovimiento frmNuevoMovimiento = new frmNuevoMovimiento();
            frmNuevoMovimiento.formulario = this;
            frmNuevoMovimiento.Opcion = 2;
            frmNuevoMovimiento.label1.Text = "EDITAR COMPONENTE";

            frmNuevoMovimiento.idMovimientoC = Convert.ToInt32(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "NRO"));
            frmNuevoMovimiento.CargarComboSistema();
            frmNuevoMovimiento.idSistema = Convert.ToInt32(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "idSistemaVehiculo"));
            frmNuevoMovimiento.cbxSistema.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "SISTEMA"));
            frmNuevoMovimiento.CargarComboSubSistema();
            frmNuevoMovimiento.cbxSubSistema.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "SUBSISTEMA"));
            frmNuevoMovimiento.txtComponente.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "COMPONENTE"));
            frmNuevoMovimiento.idVehiculoO = Convert.ToInt32(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "idUnidadProcedencia"));
            frmNuevoMovimiento.txtPlacaProd.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "UNIDAD_PROCEDENCIA"));
            frmNuevoMovimiento.idVehiculoD = Convert.ToInt32(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "idUnidadDestino"));
            frmNuevoMovimiento.txtPlacaDest.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "UNIDAD_DESTINO"));
            frmNuevoMovimiento.dtpFechaEjecucion.Value = Convert.ToDateTime(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "FECHA_EJECUCION"));
            frmNuevoMovimiento.txtNroReq.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "NRO_REQUERIMIENTO"));
            frmNuevoMovimiento.txtDescipcionR.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "REQUERIMIENTO"));
            frmNuevoMovimiento.Persona = Convert.ToInt32(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "PersonaAutorizada"));
            frmNuevoMovimiento.txtNombre.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "AUTORIZADO_POR"));
            frmNuevoMovimiento.txtMotivo.Text = Convert.ToString(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "MOTIVO"));
            frmNuevoMovimiento.btnCancelar.Enabled = false;
            frmNuevoMovimiento.ShowDialog();
        }

        private void tsEliminarMovimiento_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este movimiento?", "ELIMINAR MOVIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idMovimientoC = Convert.ToInt32(dgvMovimientosView.GetRowCellValue(dgvMovimientosView.FocusedRowHandle, "NRO"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MovimientoC_RegistrarEditarComponentes(3, idMovimientoC, 0, 0, "", 0, 0, DateTime.Now, "", 0, "", "");
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarMovimientos(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El movimiento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarMovimientos(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgMovimientos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE MOVIMIENTOS DE COMPONENTES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgMovimientos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
