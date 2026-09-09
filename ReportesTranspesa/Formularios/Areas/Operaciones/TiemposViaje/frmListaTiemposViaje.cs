using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Properties;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TiemposViaje
{
    public partial class frmListaTiemposViaje : Form
    {
        DataTable dtListaTiempos = new DataTable();
        DataTable dtListaPernoctes = new DataTable();
        DataTable dtPermisos = new DataTable();
        public int Filtro, Lindley = 0;
        DataTable dtListaTV;
        int xClick = 0, yClick = 0;
        String CarpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        String xmlTiempos;

        public frmListaTiemposViaje()
        {
            InitializeComponent();
        }

        private void frmListaTiemposViaje_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaTiemposViaje");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                    {
                        tsEliminarTiempo.Enabled = true;
                        tsQuitarPernocte.Enabled = true;
                    }
                    else
                    {
                        tsEliminarTiempo.Enabled = false;
                        tsQuitarPernocte.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true || Convert.ToBoolean(dtPermisos.Rows[0]["Modificar"]) == true)
                    {
                        tsCopiarTiempo.Enabled = true;
                    }
                    else
                    {
                        tsCopiarTiempo.Enabled = false;
                    }
                }
            }

            cbxProgramacion.Text = "LINDLEY";
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            rbPernoctes.Checked = true;
            rbPernoctes_Click(sender, e);
            rbPernoctes.Checked = false;
            rbUbicacion.Checked = true;
            rbUbicacion_Click(sender, e);
        }


        public void ListarTiemposViaje()
        {
            // Guardar fila enfocada antes de recargar
            int focusedRowHandle = dgvTiempoViajesVista.FocusedRowHandle;

            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                if (Filtro == 1)
                {
                    dtListaTiempos = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarTiemposViajes(txtPreviaje.Text, dtpFechaInicio.Text, dtpFechaFin.Text, txtRuta.Text,
                                                                                                                  cbxProgramacion.Text, txtPlaca.Text, txtConductor.Text);
                    dtgTiempoViajes.DataSource = null;
                    dgvTiempoViajesVista.Columns.Clear();
                    dtgTiempoViajes.DataSource = dtListaTiempos;
                    if (dtListaTiempos.Rows.Count > 0)
                    {
                        dgvTiempoViajesVista.Columns["idRuta"].Visible = false;

                        if (cbxProgramacion.Text == "LINDLEY")
                        {
                            dgvTiempoViajesVista.Columns["LLEGADA_PLANTA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["LLEGADA_PLANTA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["INGRESO_PLANTA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["INGRESO_PLANTA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["INICIO_ATENCION"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["INICIO_ATENCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["FIN_ATENCION"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["FIN_ATENCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["ENTREGA_GUIA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["ENTREGA_GUIA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["SALIDA_PLANTA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["SALIDA_PLANTA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["SALIDA_RUTA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["SALIDA_RUTA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["LLEGADA_CDA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["LLEGADA_CDA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["INICIO_DESCARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["INICIO_DESCARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["FIN_DESCARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["FIN_DESCARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            if (dgvTiempoViajesVista.Columns["INGRESO_PLANTA_2"] != null)
                            {
                                dgvTiempoViajesVista.Columns["INGRESO_PLANTA_2"].DisplayFormat.FormatType = FormatType.DateTime;
                                dgvTiempoViajesVista.Columns["INGRESO_PLANTA_2"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            }
                            if (dgvTiempoViajesVista.Columns["SALIDA_PLANTA_2"] != null)
                            {
                                dgvTiempoViajesVista.Columns["SALIDA_PLANTA_2"].DisplayFormat.FormatType = FormatType.DateTime;
                                dgvTiempoViajesVista.Columns["SALIDA_PLANTA_2"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            }
                            dgvTiempoViajesVista.Columns["LLEGADA_CDA_2"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["LLEGADA_CDA_2"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["INICIO_DESCARGA_2"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["INICIO_DESCARGA_2"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["FIN_DESCARGA_2"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["FIN_DESCARGA_2"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["LLEGADA_BASE"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["LLEGADA_BASE"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                        }
                        else
                        {
                            dgvTiempoViajesVista.Columns["SALIDA_BASE"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["SALIDA_BASE"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["LLEGADA_CARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["LLEGADA_CARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["INICIO_CARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["INICIO_CARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["SALIDA_PLANTA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["SALIDA_PLANTA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["LLEGADA_DESCARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["LLEGADA_DESCARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["INICIO_DESCARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["INICIO_DESCARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["SALIDA_DESCARGA"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["SALIDA_DESCARGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                            dgvTiempoViajesVista.Columns["LLEGADA_BASE"].DisplayFormat.FormatType = FormatType.DateTime;
                            dgvTiempoViajesVista.Columns["LLEGADA_BASE"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                        }

                        dgvTiempoViajesVista.Columns["ESTADO"].Summary.Clear();
                        dgvTiempoViajesVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total = {0}");
                        dgvTiempoViajesVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvTiempoViajesVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                        ConfigurarColumnasFechas();
                        dgvTiempoViajesVista.BestFitColumns();
                    }
                }
                
                if (Filtro == 2)
                {
                    dtListaPernoctes = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarTiemposPernocte(txtPreviaje.Text, dtpFechaInicio.Text, dtpFechaFin.Text, txtRuta.Text,
                                                                                                                  cbxProgramacion.Text, txtPlaca.Text, txtConductor.Text);

                    dtgListaPernoctes.DataSource = dtListaPernoctes;
                    if (dtListaPernoctes.Rows.Count > 0)
                    {
                        dgvListaPernoctesView.Columns["idRuta"].Visible = false;
                        dgvListaPernoctesView.Columns["idPernocte"].Visible = false;

                        dgvListaPernoctesView.Columns["ESTADO"].Summary.Clear();
                        dgvListaPernoctesView.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total = {0}");

                        dgvListaPernoctesView.BestFitColumns();
                    }
                }
            }

            // Restaurar fila enfocada
            if (focusedRowHandle >= 0 && focusedRowHandle < dgvTiempoViajesVista.RowCount) { dgvTiempoViajesVista.FocusedRowHandle = focusedRowHandle; }
        }

        public void ConfigurarColumnasFechas()
        {
            var repoDateTime = new RepositoryItemDateEdit();

            repoDateTime.CalendarView = CalendarView.Vista;
            repoDateTime.VistaDisplayMode = DefaultBoolean.True;
            repoDateTime.VistaEditTime = DefaultBoolean.True;

            // Mostrar selector de hora
            repoDateTime.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            repoDateTime.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            repoDateTime.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;

            // Máscara fecha + hora
            repoDateTime.Mask.UseMaskAsDisplayFormat = true;
            repoDateTime.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";

            // Formatos
            repoDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repoDateTime.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            repoDateTime.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repoDateTime.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

            dtgTiempoViajes.RepositoryItems.Add(repoDateTime);

            dgvTiempoViajesVista.Columns["PREVIAJE"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["CODIGO_VIAJE"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["FECHA_PROGRAMACION"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["PROGRAMACION"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["RUTA"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["CONDUCTOR"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["TRACTO"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["SEMIRREMOLQUE"].OptionsColumn.AllowEdit = false;
            dgvTiempoViajesVista.Columns["SUCURSAL"].OptionsColumn.AllowEdit = false;

            // Asignar el editor a las columnas de fecha
            if (cbxProgramacion.Text == "LINDLEY")
            {
                dgvTiempoViajesVista.Columns["LLEGADA_PLANTA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["INGRESO_PLANTA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["INICIO_ATENCION"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["FIN_ATENCION"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["ENTREGA_GUIA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["SALIDA_PLANTA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["SALIDA_RUTA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["LLEGADA_CDA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["INICIO_DESCARGA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["FIN_DESCARGA"].ColumnEdit = repoDateTime;
                if (dgvTiempoViajesVista.Columns["INGRESO_PLANTA_2"] != null) dgvTiempoViajesVista.Columns["INGRESO_PLANTA_2"].ColumnEdit = repoDateTime;
                if (dgvTiempoViajesVista.Columns["SALIDA_PLANTA_2"] != null) dgvTiempoViajesVista.Columns["SALIDA_PLANTA_2"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["LLEGADA_CDA_2"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["INICIO_DESCARGA_2"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["FIN_DESCARGA_2"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["LLEGADA_BASE"].ColumnEdit = repoDateTime;
            }
            else
            {
                dgvTiempoViajesVista.Columns["SALIDA_BASE"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["LLEGADA_CARGA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["INICIO_CARGA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["SALIDA_PLANTA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["LLEGADA_DESCARGA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["INICIO_DESCARGA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["SALIDA_DESCARGA"].ColumnEdit = repoDateTime;
                dgvTiempoViajesVista.Columns["LLEGADA_BASE"].ColumnEdit = repoDateTime;
            }

            var repoComboBox = new RepositoryItemComboBox();
            repoComboBox.Items.Add("IDA");
            repoComboBox.Items.Add("RETORNO");
            dgvTiempoViajesVista.Columns["ESTADO_VIAJE"].ColumnEdit = repoComboBox;

            DataTable dtListaRutas = new DataTable();
            dtListaRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(1, "", "", "");

            if (dtListaRutas.Rows.Count > 0)
            {
                var repoComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                repoComboBox3.Items.AddRange(dtListaRutas.AsEnumerable().Select(r => r.Field<string>("Ruta")).ToArray());

                dtgTiempoViajes.RepositoryItems.Add(repoComboBox3);
                dgvTiempoViajesVista.Columns["RUTA_VIAJE"].ColumnEdit = repoComboBox3;
            }

            RepositoryItemComboBox repoUbic = new RepositoryItemComboBox();
            dtgTiempoViajes.RepositoryItems.Add(repoUbic);
            dgvTiempoViajesVista.Columns["UBICACION"].ColumnEdit = repoUbic;

            var cbEstado = new RepositoryItemComboBox();
            cbEstado.Items.Add("PENDIENTE");
            cbEstado.Items.Add("SALIDA");
            cbEstado.Items.Add("CARGA EN BASE");
            cbEstado.Items.Add("CARGANDO");
            cbEstado.Items.Add("CARGADO");
            cbEstado.Items.Add("EN TRÁNSITO");
            cbEstado.Items.Add("ESPERA DESCARGA");
            cbEstado.Items.Add("DESCARGANDO");
            cbEstado.Items.Add("RETORNO");
            cbEstado.Items.Add("DETENIDO");
            cbEstado.Items.Add("FINALIZADO");
            dgvTiempoViajesVista.Columns["ESTADO"].ColumnEdit = cbEstado;

            dgvTiempoViajesVista.BestFitColumns();
        }

        public void CargarArchivo()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = CarpetaDestino;
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsb|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtTiempoViaje.Text = op.FileName;
                        btnGenerar.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTiemposViaje(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTiemposViaje(); }
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTiemposViaje(); }
        }

        private void cbxProgramacion_DropDownClosed(object sender, EventArgs e) { ListarTiemposViaje(); }

        private void txtPreviaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTiemposViaje(); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTiemposViaje(); }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTiemposViaje(); }
        }

        private void rbUbicacion_Click(object sender, EventArgs e)
        {
            if (rbUbicacion.Checked == true)
            {
                tabTiemposViaje.SelectedTab = tabTiempoUbicacion;
                Filtro = 1;
                ListarTiemposViaje();
            }
        }

        private void rbPernoctes_Click(object sender, EventArgs e)
        {
            if (rbPernoctes.Checked == true)
            {
                tabTiemposViaje.SelectedTab = tabPernoctes;
                Filtro = 2;
                ListarTiemposViaje();
            }
        }

        private void dtgTiempoViajes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    frmRegistrarTiemposViaje frmRegistrarTiemposViaje = new frmRegistrarTiemposViaje();
                    frmRegistrarTiemposViaje.EstadoV = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO").ToString();
                    frmRegistrarTiemposViaje.txtPreviaje.Text = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PREVIAJE").ToString();
                    frmRegistrarTiemposViaje.dtpFechaProg.Text = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FECHA_PROGRAMACION").ToString();
                    frmRegistrarTiemposViaje.txtOperacion.Text = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PROGRAMACION").ToString();
                    frmRegistrarTiemposViaje.txtConductor.Text = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "CONDUCTOR").ToString();
                    frmRegistrarTiemposViaje.txtRuta.Text = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "RUTA").ToString();
                    frmRegistrarTiemposViaje.txtTracto.Text = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "TRACTO").ToString();
                    frmRegistrarTiemposViaje.txtSR.Text = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SEMIRREMOLQUE").ToString();

                    frmRegistrarTiemposViaje.Opcion = 1;
                    frmRegistrarTiemposViaje.formulario = this;
                    frmRegistrarTiemposViaje.idRuta = Convert.ToInt32(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "idRuta"));
                    frmRegistrarTiemposViaje.CargarComboUbicacion();
                    frmRegistrarTiemposViaje.ShowDialog(this);
                }
            }
            catch { }
        }

        private void dtgTiempoViajes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Previaje = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PREVIAJE").ToString();

                if (Previaje != "")
                {
                    if (dtPermisos != null && dtPermisos.Rows.Count > 0)
                    {
                        tsEliminarTiempo.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]);
                        tsCopiarTiempo.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) || Convert.ToBoolean(dtPermisos.Rows[0]["Modificar"]);
                    }
                    else
                    {
                        tsEliminarTiempo.Enabled = true;
                        tsCopiarTiempo.Enabled = true;
                    }
                }
                else
                {
                    tsEliminarTiempo.Enabled = false;
                    tsCopiarTiempo.Enabled = false;
                }
            }
            catch
            {
                tsEliminarTiempo.Enabled = false;
                tsCopiarTiempo.Enabled = false;
            }
        }

        private void tsEliminarTiempo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar los tiempos para este viaje?", "ELIMINAR TIEMPOS DE VIAJE", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int NroTicket = Convert.ToInt32(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PREVIAJE"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(2, NroTicket, "", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,
                                                         DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,
                                                         DateTime.Now, DateTime.Now, "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    Filtro = 1;
                    ListarTiemposViaje();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsCopiarTiempo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTiempoViajesVista.FocusedRowHandle < 0) return;

                string previaje = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PREVIAJE"));
                if (string.IsNullOrEmpty(previaje)) return;

                string conductor = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "CONDUCTOR"));
                string tracto = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "TRACTO"));
                string ruta = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "RUTA"));
                string prog = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PROGRAMACION"));

                txtPreviajeOrigen.Text = previaje;
                lblInfoOrigen.Text = string.Format("Prog: {0} | Ruta: {1}\nTracto: {2} | Conductor: {3}", prog, ruta, tracto, conductor);
                txtPreviajeDestino.Clear();

                pCopiarTiempos.Left = Math.Max(10, (this.ClientSize.Width - pCopiarTiempos.Width) / 2);
                pCopiarTiempos.Top = Math.Max(10, (this.ClientSize.Height - pCopiarTiempos.Height) / 2);
                pCopiarTiempos.Location = new System.Drawing.Point(430, 240);
                pCopiarTiempos.Visible = true;
                pCopiarTiempos.BringToFront();
                txtPreviajeDestino.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void pCopiarTiempos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pCopiarTiempos.Left = pCopiarTiempos.Left + (e.X - xClick);
                pCopiarTiempos.Top = pCopiarTiempos.Top + (e.Y - yClick);
            }
        }

        private void btnCerrarCopiar_Click(object sender, EventArgs e)
        {
            pCopiarTiempos.Visible = false;
            pCopiarTiempos.SendToBack();
            txtPreviajeDestino.Clear();
        }

        private void txtPreviajeDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) { e.Handled = true; }
            
            if (e.KeyChar == (char)Keys.Enter) { btnCopiarTiempos_Click(sender, e); }
        }

        private void btnCopiarTiempos_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPreviajeDestino.Text))
                {
                    MessageBox.Show("Por favor ingrese el código de previaje destino.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPreviajeDestino.Focus();
                    return;
                }

                int previajeOrigen = Convert.ToInt32(txtPreviajeOrigen.Text.Trim());
                int previajeDestino = Convert.ToInt32(txtPreviajeDestino.Text.Trim());

                if (previajeOrigen == previajeDestino)
                {
                    MessageBox.Show("El previaje destino debe ser diferente al previaje origen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPreviajeDestino.Focus();
                    return;
                }

                if (MessageBox.Show(string.Format("¿Está seguro de copiar los tiempos del previaje {0} al previaje {1}?", previajeOrigen, previajeDestino),
                                    "COPIAR TIEMPOS DE VIAJE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                { return; }

                string usuario = Utilitario.Instancia.SesionUsuario.usuario;
                string programacion = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PROGRAMACION"));
                if (string.IsNullOrEmpty(programacion)) { programacion = cbxProgramacion.Text; }

                string estadoV = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO"));
                string rutaViaje = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "RUTA_VIAJE"));
                string estadoViaje = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO_VIAJE"));
                string ubicacion = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "UBICACION"));
                decimal porcTransito = Convert.ToDecimal(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PORC_TRANSITO") == DBNull.Value ? 0 : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PORC_TRANSITO"));

                DataTable dtRespuesta = new DataTable();
                string respuesta = "";

                if (programacion == "LINDLEY" || cbxProgramacion.Text == "LINDLEY")
                {
                    DateTime llegadaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_PLANTA"));
                    DateTime ingresoPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA"));
                    DateTime inicioAtencion = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_ATENCION") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_ATENCION"));
                    DateTime finAtencion = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_ATENCION") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_ATENCION"));
                    DateTime entregaGuia = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ENTREGA_GUIA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ENTREGA_GUIA"));
                    DateTime salidaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA"));
                    DateTime salidaRuta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_RUTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_RUTA"));
                    DateTime llegadaCDA = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA"));
                    DateTime inicioDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA"));
                    DateTime finDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA"));
                    DateTime ingresoPlanta2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA_2"));
                    DateTime salidaPlanta2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA_2"));
                    DateTime llegadaCDA2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA_2"));
                    DateTime inicioDescarga2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA_2"));
                    DateTime finDescarga2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA_2"));
                    DateTime llegadaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE"));

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(1, previajeDestino, estadoV, llegadaPlanta, ingresoPlanta, inicioAtencion, finAtencion, entregaGuia,
                                                             salidaPlanta, salidaRuta, llegadaCDA, inicioDescarga, finDescarga, ingresoPlanta2, salidaPlanta2, llegadaCDA2, inicioDescarga2, finDescarga2, llegadaBase, usuario);
                }
                else
                {
                    DateTime salidaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_BASE"));
                    DateTime llegadaCarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CARGA"));
                    DateTime inicioCarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_CARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_CARGA"));
                    DateTime salidaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA"));
                    DateTime llegadaDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_DESCARGA"));
                    DateTime inicioDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA"));
                    DateTime salidaDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_DESCARGA"));
                    DateTime llegadaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE"));

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas(1, previajeDestino, estadoV, salidaBase, llegadaCarga, inicioCarga, salidaPlanta, llegadaDescarga, inicioDescarga, salidaDescarga, llegadaBase, usuario);
                }

                if (dtRespuesta != null && dtRespuesta.Rows.Count > 0)
                {
                    respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string nroRpta = respuesta.Substring(0, 1);
                    if (nroRpta == "0")
                    {
                        clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ModificarTiempoViajes(previajeDestino, estadoV, rutaViaje, estadoViaje, ubicacion, porcTransito, usuario);

                        MessageBox.Show("Tiempos copiados correctamente al previaje " + previajeDestino + ".", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCerrarCopiar_Click(sender, e);
                        ListarTiemposViaje();
                    }
                    else { MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No se pudo obtener respuesta al registrar los tiempos en el previaje destino.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show("Error al copiar tiempos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvTiempoViajesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "SALIDA") { e.Appearance.BackColor = Color.Aqua; }

                if (Convert.ToString(e.CellValue) == "CARGA EN BASE") { e.Appearance.BackColor = Color.Aqua; }

                if (Convert.ToString(e.CellValue) == "CARGANDO") { e.Appearance.BackColor = Color.Aqua; }

                if (Convert.ToString(e.CellValue) == "CARGADO") { e.Appearance.BackColor = Color.Aqua; }

                if (Convert.ToString(e.CellValue) == "EN TRÁNSITO") { e.Appearance.BackColor = Color.Yellow; }

                if (Convert.ToString(e.CellValue) == "ESPERA DESCARGA") { e.Appearance.BackColor = Color.Orange; }

                if (Convert.ToString(e.CellValue) == "DESCARGANDO") { e.Appearance.BackColor = Color.Orange; }
                
                if (Convert.ToString(e.CellValue) == "RETORNO") { e.Appearance.BackColor = Color.PaleGreen; }

                if (Convert.ToString(e.CellValue) == "PENDIENTE") { e.Appearance.BackColor = Color.PaleTurquoise; }

                if (Convert.ToString(e.CellValue) == "FINALIZADO") { e.Appearance.BackColor = Color.PaleGreen; }

                if (Convert.ToString(e.CellValue) == "DETENIDO")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dgvTiempoViajesVista_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                int Previaje;
                DateTime SalidaBase, LlegadaPlanta, IngresoPlanta, InicioAtencion, FinAtencion, EntregaGuia, SalidaPlanta, SalidaRuta, LlegadaCDA, InicioDescarga,
                FinDescarga, IngresoPlanta2, SalidaPlanta2, LlegadaCDA2, InicioDescarga2, FinDescarga2, LlegadaBase, LlegadaCarga, InicioCarga, LlegadaDescarga, SalidaDescarga;
                string EstadoV;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (e.KeyCode == Keys.Tab)
                {
                    view.CloseEditor();
                    Previaje = Convert.ToInt32(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PREVIAJE"));
                    
                    if (cbxProgramacion.Text == "LINDLEY")
                    {
                        EstadoV = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO").ToString();
                        LlegadaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_PLANTA"));
                        IngresoPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA"));
                        InicioAtencion = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_ATENCION") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_ATENCION"));
                        FinAtencion = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_ATENCION") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_ATENCION"));
                        EntregaGuia = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ENTREGA_GUIA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ENTREGA_GUIA"));
                        SalidaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA"));
                        SalidaRuta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_RUTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_RUTA"));
                        LlegadaCDA = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA"));
                        InicioDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA"));
                        FinDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA"));
                        IngresoPlanta2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA_2"));
                        SalidaPlanta2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA_2"));
                        LlegadaCDA2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA_2"));
                        InicioDescarga2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA_2"));
                        FinDescarga2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA_2"));
                        LlegadaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE"));
                    
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(1, Previaje, EstadoV, LlegadaPlanta, IngresoPlanta, InicioAtencion, FinAtencion, EntregaGuia,
                                                                 SalidaPlanta, SalidaRuta, LlegadaCDA, InicioDescarga, FinDescarga, IngresoPlanta2, SalidaPlanta2, LlegadaCDA2, InicioDescarga2, FinDescarga2, LlegadaBase, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA != "0")
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            view.PostEditor();
                            view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                            view.UpdateCurrentRow();
                        }
                    }
                    else
                    {
                        EstadoV = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO").ToString();
                        SalidaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_BASE"));
                        LlegadaCarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CARGA"));
                        InicioCarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_CARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_CARGA"));
                        SalidaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA"));
                        LlegadaDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_DESCARGA"));
                        InicioDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA"));
                        SalidaDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_DESCARGA"));
                        LlegadaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE"));

                        DataTable dtRespuesta2 = new DataTable();
                        string Respuesta2;

                        dtRespuesta2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas(1, Previaje, EstadoV, SalidaBase, LlegadaCarga, InicioCarga,
                                                                  SalidaPlanta, LlegadaDescarga, InicioDescarga, SalidaDescarga, LlegadaBase, Usuario);
                        Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                        string NroRPTA2 = Respuesta2.Substring(0, 1);

                        if (NroRPTA2 != "0")
                        {
                            MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            view.PostEditor();
                            view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                            view.UpdateCurrentRow();
                        }
                    }

                    DataTable dtRespuesta3 = new DataTable();
                    string Respuesta3;
                    string EstadoV2 = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO").ToString();
                    string RutaViaje = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "RUTA_VIAJE"));
                    string EstadoViaje = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO_VIAJE"));
                    string Ubicacion = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "UBICACION"));
                    decimal PorcTransito = Convert.ToDecimal(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PORC_TRANSITO"));

                    dtRespuesta3 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ModificarTiempoViajes(Previaje, EstadoV2, RutaViaje, EstadoViaje, Ubicacion, PorcTransito, Usuario);
                    Respuesta3 = Convert.ToString(dtRespuesta3.Rows[0]["exito"]);
                    string NroRPTA3 = Respuesta3.Substring(0, 1);

                    if (NroRPTA3 != "0")
                    {
                        MessageBox.Show(Respuesta3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        view.PostEditor();
                        view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                        view.UpdateCurrentRow();
                    }
                    else
                    {
                        dtListaTiempos.AcceptChanges();
                        dgvTiempoViajesVista.UpdateCurrentRow();
                    }
                }

                if (e.KeyCode == Keys.Enter)
                {
                    view.CloseEditor();
                    Previaje = Convert.ToInt32(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PREVIAJE"));

                    if (cbxProgramacion.Text == "LINDLEY")
                    {
                        EstadoV = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO").ToString();
                        LlegadaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_PLANTA"));
                        IngresoPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA"));
                        InicioAtencion = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_ATENCION") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_ATENCION"));
                        FinAtencion = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_ATENCION") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_ATENCION"));
                        EntregaGuia = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ENTREGA_GUIA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ENTREGA_GUIA"));
                        SalidaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA"));
                        SalidaRuta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_RUTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_RUTA"));
                        LlegadaCDA = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA"));
                        InicioDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA"));
                        FinDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA"));
                        IngresoPlanta2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INGRESO_PLANTA_2"));
                        SalidaPlanta2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA_2"));
                        LlegadaCDA2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CDA_2"));
                        InicioDescarga2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA_2"));
                        FinDescarga2 = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA_2") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "FIN_DESCARGA_2"));
                        LlegadaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE"));

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(1, Previaje, EstadoV, LlegadaPlanta, IngresoPlanta, InicioAtencion, FinAtencion, EntregaGuia,
                                                                 SalidaPlanta, SalidaRuta, LlegadaCDA, InicioDescarga, FinDescarga, IngresoPlanta2, SalidaPlanta2, LlegadaCDA2, InicioDescarga2, FinDescarga2, LlegadaBase, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA != "0")
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            view.PostEditor();
                            view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                            view.UpdateCurrentRow();
                        }
                    }
                    else
                    {
                        EstadoV = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO").ToString();
                        SalidaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_BASE"));
                        LlegadaCarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_CARGA"));
                        InicioCarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_CARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_CARGA"));
                        SalidaPlanta = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_PLANTA"));
                        LlegadaDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_DESCARGA"));
                        InicioDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "INICIO_DESCARGA"));
                        SalidaDescarga = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_DESCARGA") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "SALIDA_DESCARGA"));
                        LlegadaBase = Convert.ToDateTime(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE") == DBNull.Value ? DateTime.Today : dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "LLEGADA_BASE"));

                        DataTable dtRespuesta2 = new DataTable();
                        string Respuesta2;

                        dtRespuesta2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas(1, Previaje, EstadoV, SalidaBase, LlegadaCarga, InicioCarga,
                                                                  SalidaPlanta, LlegadaDescarga, InicioDescarga, SalidaDescarga, LlegadaBase, Usuario);
                        Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                        string NroRPTA2 = Respuesta2.Substring(0, 1);

                        if (NroRPTA2 != "0")
                        {
                            MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            view.PostEditor();
                            view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                            view.UpdateCurrentRow();
                        }
                    }

                    DataTable dtRespuesta3 = new DataTable();
                    string Respuesta3;
                    string EstadoV2 = dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO").ToString();
                    string RutaViaje = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "RUTA_VIAJE"));
                    string EstadoViaje = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "ESTADO_VIAJE"));
                    string Ubicacion = Convert.ToString(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "UBICACION"));
                    decimal PorcTransito = Convert.ToDecimal(dgvTiempoViajesVista.GetRowCellValue(dgvTiempoViajesVista.FocusedRowHandle, "PORC_TRANSITO"));

                    dtRespuesta3 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ModificarTiempoViajes(Previaje, EstadoV2, RutaViaje, EstadoViaje, Ubicacion, PorcTransito, Usuario);
                    Respuesta3 = Convert.ToString(dtRespuesta3.Rows[0]["exito"]);
                    string NroRPTA3 = Respuesta3.Substring(0, 1);

                    if (NroRPTA3 != "0")
                    {
                        MessageBox.Show(Respuesta3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        view.PostEditor();
                        view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                        view.UpdateCurrentRow();
                    }
                    else
                    {
                        view.CloseEditor();
                        view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                        view.UpdateCurrentRow();
                        ListarTiemposViaje();
                    }
                }

                if (e.KeyCode == Keys.Escape)
                {
                    view.CloseEditor();
                    view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                    ListarTiemposViaje();
                }
            }
            catch { }
        }

        private void dtgListaPernoctes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Previaje = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "PREVIAJE").ToString();

                if (Previaje != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsQuitarPernocte.Enabled = true; }
                }
                else { tsQuitarPernocte.Enabled = false; }
            }
            catch { tsQuitarPernocte.Enabled = false; }
        }

        private void dtgListaPernoctes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    frmRegistrarTiempoPernocte frmRegistrarTiempoPernocte = new frmRegistrarTiempoPernocte();
                    frmRegistrarTiempoPernocte.txtPreviaje.Text = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "PREVIAJE").ToString();
                    frmRegistrarTiempoPernocte.dtpFechaProg.Text = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "FECHA_PROGRAMACION").ToString();
                    frmRegistrarTiempoPernocte.txtOperacion.Text = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "PROGRAMACION").ToString();
                    frmRegistrarTiempoPernocte.txtConductor.Text = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "CONDUCTOR").ToString();
                    frmRegistrarTiempoPernocte.txtRuta.Text = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "RUTA").ToString();
                    frmRegistrarTiempoPernocte.txtTracto.Text = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "TRACTO").ToString();
                    frmRegistrarTiempoPernocte.txtSR.Text = dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "SEMIRREMOLQUE").ToString();

                    frmRegistrarTiempoPernocte.formulario = this;
                    frmRegistrarTiempoPernocte.ShowDialog(this);
                }
            }
            catch { }
        }

        private void dgvListaPernoctesView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "ATENDIDO") { e.Appearance.BackColor = Color.PaleGreen; }

                if (Convert.ToString(e.CellValue) == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    e.Appearance.ForeColor = Color.White;
                }

                if (Convert.ToString(e.CellValue) == "PROGRAMADO") { e.Appearance.BackColor = Color.Yellow; }

                if (Convert.ToString(e.CellValue) == "COLA IN") { e.Appearance.BackColor = Color.Orange; }

                if (Convert.ToString(e.CellValue) == "COLA OUT") { e.Appearance.BackColor = Color.Red; }

                if (Convert.ToString(e.CellValue) == "CARGANDO") { e.Appearance.BackColor = Color.MediumPurple; }
            }
        }

        private void tsQuitarPernocte_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este tiempo de pernocte?", "ELIMINAR PERNOCTE", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idPernocte = Convert.ToInt32(dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "idPernocte"));
                int NroTicket = Convert.ToInt32(dgvListaPernoctesView.GetRowCellValue(dgvListaPernoctesView.FocusedRowHandle, "PREVIAJE"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte(2, NroTicket, idPernocte, DateTime.Now, DateTime.Now, "", "", "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    Filtro = 2;
                    ListarTiemposViaje();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnTiemposAtención_Click(object sender, EventArgs e)
        {
            frmTiemposAtencion frmTiemposAtencion = new frmTiemposAtencion();
            frmTiemposAtencion.ShowDialog(this);
        }

        private void pImportarTiempos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pImportarTiempos.Left = pImportarTiempos.Left + (e.X - xClick);
                pImportarTiempos.Top = pImportarTiempos.Top + (e.Y - yClick);
            }
        }

        private void btnImportarTiempos_Click(object sender, EventArgs e)
        {
            cbLindley.Checked = false;
            cbLindley_CheckedChanged(sender, e);
            
            pImportarTiempos.Visible = true;
            pImportarTiempos.BringToFront();
        }

        private void cbLindley_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLindley.Checked == true) { Lindley = 1; }

            if (cbLindley.Checked == false) { Lindley = 0; }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pImportarTiempos.Visible = false;
            pImportarTiempos.SendToBack();
            dgvTiempos.DataSource = null;
            xmlTiempos = "";
            btnGenerar.Enabled = false;
            txtTiempoViaje.Clear();
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                dgvTiempos.DataSource = null;
                dgvTiempos.Columns.Clear();
                CargarArchivo();

                if (System.IO.File.Exists(txtTiempoViaje.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtTiempoViaje.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "TABLA");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaTV = dataSetDetalle.Tables[0];

                    if (txtTiempoViaje.Text.Contains("LINDLEY") || txtTiempoViaje.Text.Contains("ACL")) { cbLindley.Checked = true; }
                    else { cbLindley.Checked = false; }
                    cbLindley_CheckedChanged(sender, e);
                }

                if (dtListaTV.Rows.Count > 0)
                {
                    xmlTiempos = "";
                    dgvTiempos.DataSource = dtListaTV;

                    if (txtTiempoViaje.Text.Contains("LINDLEY") || txtTiempoViaje.Text.Contains("ACL"))
                    {
                        dgvTiempos.Columns["SALIDA_BASE"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["LLEGADA_PLANTA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["INGRESO_PLANTA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["INICIO_ATENCION"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["FIN_ATENCION"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["ENTREGA_GUIA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["SALIDA_PLANTA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["SALIDA_RUTA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["LLEGADA_CDA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["INICIO_DESCARGA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["FIN_DESCARGA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["LLEGADA_CDA_2"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["INICIO_DESCARGA_2"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["FIN_DESCARGA_2"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["LLEGADA_BASE"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    }
                    else
                    {
                        dgvTiempos.Columns["SALIDA_BASE"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["LLEGADA_CARGA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["INICIO_CARGA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["SALIDA_PLANTA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["LLEGADA_DESCARGA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["INICIO_DESCARGA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["SALIDA_DESCARGA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvTiempos.Columns["LLEGADA_BASE"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    }

                    xmlTiempos = Comun.Utilitario.Instancia.DatatableToXml(dtListaTV);
                    btnGenerar.Enabled = true;
                }
                else
                {
                    xmlTiempos = "";
                    btnGenerar.Enabled = false;
                    txtTiempoViaje.Clear();
                }
            }
            catch (Exception ex)
            {
                xmlTiempos = "";
                btnGenerar.Enabled = false;
                dgvTiempos.DataSource = null;
                txtTiempoViaje.Clear();
                MessageBox.Show("El archivo seleccionado no es el correcto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta = "";

            try
            {
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (Lindley == 1) { dtRespuesta =  clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ImportarTiempoViajes(1, xmlTiempos, Usuario); }
                else { dtRespuesta =  clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ImportarTiempoViajes(2, xmlTiempos, Usuario); }

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarTiemposViaje();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarTiemposViaje(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtListaTiempos == null || dtListaTiempos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos de tiempos de viaje para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Tiempos de Viaje y Pernocte - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");

                ExportarExcelTiemposViaje(nombre);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ExportarExcelTiemposViaje(string rutaArchivo)
        {
            IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);

            using (FileStream stream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write))
            {
                using (IXlDocument document = exporter.CreateDocument(stream))
                {
                    // --- HOJA 1: TIEMPOS DE VIAJE ---
                    using (IXlSheet sheet = document.CreateSheet())
                    {
                        sheet.Name = "Sheet1";

                        // Ancho de columnas (19 columnas)
                        int[] columnWidths = new int[]
                        {
                            85,  // 0. Fecha
                            110, // 1. Ruta
                            100, // 2. Tracto
                            200, // 3. Conductor
                            100, // 4. Carreta
                            145, // 5. Salida de Planta
                            145, // 6. Salida a Ruta
                            155, // 7. Llegada a Zona de Espera
                            145, // 8. Inicio Descarga
                            145, // 9. Fin Descarga
                            145, // 10. Ingreso Planta 2
                            145, // 11. Salida Planta 2
                            155, // 12. Llegada a Zona de Espera 2
                            145, // 13. Inicio Descarga 2
                            145, // 14. Fin Descarga 2
                            145, // 15. Llegada a Base
                            150, // 16. Ubicación
                            95,  // 17. %Tránsito
                            160  // 18. Status
                        };
                        for (int c = 0; c < columnWidths.Length; c++)
                        {
                            using (IXlColumn col = sheet.CreateColumn()) { col.WidthInPixels = columnWidths[c]; }
                        }

                        // Colores
                        Color colorBorde = Color.FromArgb(217, 217, 217);
                        Color colorNaranjaTexto = Color.FromArgb(237, 125, 49);
                        Color colorAzulHeader = Color.FromArgb(46, 117, 182);
                        Color colorVerdeHeader = Color.FromArgb(112, 173, 71);
                        Color colorRojoHeader = Color.FromArgb(255, 0, 0);
                        Color colorRosaPendiente = Color.FromArgb(254, 204, 208);
                        Color colorCelesteTransito = Color.FromArgb(189, 215, 238);
                        Color colorTextoTransito = Color.FromArgb(31, 78, 121);
                        Color colorTextoUbicacion = Color.FromArgb(0, 32, 96);

                        XlBorder bordeThin = XlBorder.OutlineBorders(colorBorde, XlBorderLineStyle.Thin);

                        // Estilos de encabezado
                        string[] headerTexts = new string[]
                        {
                            "Fecha", "Ruta", "Tracto", "Conductor", "Carreta",
                            "Salida de Planta", "Salida a Ruta", "Llegada a Zona\nde Espera",
                            "Inicio Descarga", "Fin Descarga",
                            "Ingreso Planta 2", "Salida Planta 2", "Llegada a Zona\nde Espera 2",
                            "Inicio Descarga 2", "Fin Descarga 2",
                            "Llegada a Base", "Ubicación", "%Tránsito", "Status"
                        };

                        using (IXlRow headerRow = sheet.CreateRow())
                        {
                            headerRow.HeightInPixels = 38;

                            for (int i = 0; i < headerTexts.Length; i++)
                            {
                                using (IXlCell cell = headerRow.CreateCell())
                                {
                                    cell.Value = headerTexts[i];

                                    XlCellFormatting hFormat = new XlCellFormatting();
                                    hFormat.Font = new XlFont();
                                    hFormat.Font.Name = "Calibri";
                                    hFormat.Font.Size = 10;
                                    hFormat.Font.Bold = true;
                                    hFormat.Alignment = new XlCellAlignment();
                                    hFormat.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                                    hFormat.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                                    hFormat.Alignment.WrapText = true;
                                    hFormat.Border = bordeThin;

                                    if (i == 5 || i == 10 || i == 11) // Salida de Planta, Ingreso Planta 2, Salida Planta 2 (Azul)
                                    {
                                        hFormat.Fill = XlFill.SolidFill(colorAzulHeader);
                                        hFormat.Font.Color = Color.White;
                                    }
                                    else if ((i >= 7 && i <= 9) || (i >= 12 && i <= 14)) // Llegada a Zona de Espera, Inicio Descarga, Fin Descarga y Segunda Descarga (Verde)
                                    {
                                        hFormat.Fill = XlFill.SolidFill(colorVerdeHeader);
                                        hFormat.Font.Color = Color.White;
                                    }
                                    else if (i >= 16 && i <= 18) // Ubicación, %Tránsito, Status (Rojo)
                                    {
                                        hFormat.Fill = XlFill.SolidFill(colorRojoHeader);
                                        hFormat.Font.Color = Color.White;
                                    }
                                    else // Fecha, Ruta, Tracto, Conductor, Carreta, Salida a Ruta, Llegada a Base (Naranja)
                                    {
                                        hFormat.Fill = XlFill.SolidFill(Color.White);
                                        hFormat.Font.Color = colorNaranjaTexto;
                                    }

                                    cell.Formatting = hFormat;
                                }
                            }
                        }

                        // Filas de datos
                        int rowCount = dtListaTiempos.Rows.Count;
                        for (int r = 0; r < rowCount; r++)
                        {
                            DataRow dr = dtListaTiempos.Rows[r];

                            // Obtener valores de la fila
                            string fechaProg = FormatearFechaCorta(dr["FECHA_PROGRAMACION"]);
                            string ruta = dr["RUTA"] != DBNull.Value ? dr["RUTA"].ToString().Trim() : "";
                            string tracto = dr["TRACTO"] != DBNull.Value ? dr["TRACTO"].ToString().Trim() : "";
                            string conductor = dr["CONDUCTOR"] != DBNull.Value ? dr["CONDUCTOR"].ToString().Trim() : "";
                            string carreta = dr.Table.Columns.Contains("SEMIRREMOLQUE") && dr["SEMIRREMOLQUE"] != DBNull.Value ? dr["SEMIRREMOLQUE"].ToString().Trim() : "";

                            string salidaPlanta = FormatearFechaHora(dr.Table.Columns.Contains("SALIDA_PLANTA") ? dr["SALIDA_PLANTA"] : null);
                            string salidaRuta = FormatearFechaHora(dr.Table.Columns.Contains("SALIDA_RUTA") ? dr["SALIDA_RUTA"] : null);
                            if (string.IsNullOrEmpty(salidaRuta) && dr.Table.Columns.Contains("SALIDA_DESCARGA"))
                            { salidaRuta = FormatearFechaHora(dr["SALIDA_DESCARGA"]); }

                            string llegadaZonaEspera = FormatearFechaHora(dr.Table.Columns.Contains("LLEGADA_CDA") ? dr["LLEGADA_CDA"] : null);
                            if (string.IsNullOrEmpty(llegadaZonaEspera) && dr.Table.Columns.Contains("LLEGADA_DESCARGA"))
                            { llegadaZonaEspera = FormatearFechaHora(dr["LLEGADA_DESCARGA"]); }

                            string inicioDescarga = FormatearFechaHora(dr.Table.Columns.Contains("INICIO_DESCARGA") ? dr["INICIO_DESCARGA"] : null);
                            string finDescarga = FormatearFechaHora(dr.Table.Columns.Contains("FIN_DESCARGA") ? dr["FIN_DESCARGA"] : null);

                            // Columnas de Planta 2 y Descarga 2
                            string ingresoPlanta2 = FormatearFechaHora(dr.Table.Columns.Contains("INGRESO_PLANTA_2") ? dr["INGRESO_PLANTA_2"] : (dr.Table.Columns.Contains("INGRESO_PLANTA2") ? dr["INGRESO_PLANTA2"] : null));
                            string salidaPlanta2 = FormatearFechaHora(dr.Table.Columns.Contains("SALIDA_PLANTA_2") ? dr["SALIDA_PLANTA_2"] : (dr.Table.Columns.Contains("SALIDA_PLANTA2") ? dr["SALIDA_PLANTA2"] : null));
                            string llegadaZonaEspera2 = FormatearFechaHora(dr.Table.Columns.Contains("LLEGADA_CDA_2") ? dr["LLEGADA_CDA_2"] : (dr.Table.Columns.Contains("LLEGADA_CDA2") ? dr["LLEGADA_CDA2"] : null));
                            string inicioDescarga2 = FormatearFechaHora(dr.Table.Columns.Contains("INICIO_DESCARGA_2") ? dr["INICIO_DESCARGA_2"] : (dr.Table.Columns.Contains("INICIO_DESCARGA2") ? dr["INICIO_DESCARGA2"] : null));
                            string finDescarga2 = FormatearFechaHora(dr.Table.Columns.Contains("FIN_DESCARGA_2") ? dr["FIN_DESCARGA_2"] : (dr.Table.Columns.Contains("FIN_DESCARGA2") ? dr["FIN_DESCARGA2"] : null));

                            string llegadaBase = FormatearFechaHora(dr.Table.Columns.Contains("LLEGADA_BASE") ? dr["LLEGADA_BASE"] : null);

                            string ubicacion = dr.Table.Columns.Contains("UBICACION") && dr["UBICACION"] != DBNull.Value ? dr["UBICACION"].ToString().Trim() : "";
                            string porcTransito = FormatearPorcentaje(dr.Table.Columns.Contains("PORC_TRANSITO") ? dr["PORC_TRANSITO"] : null);
                            string status = dr.Table.Columns.Contains("ESTADO") && dr["ESTADO"] != DBNull.Value ? dr["ESTADO"].ToString().Trim() : "";

                            using (IXlRow dataRow = sheet.CreateRow())
                            {
                                dataRow.HeightInPixels = 24;

                                // 0. Fecha
                                CrearCeldaDatos(dataRow, fechaProg, XlHorizontalAlignment.Center, bordeThin);

                                // 1. Ruta
                                CrearCeldaDatos(dataRow, ruta, XlHorizontalAlignment.Center, bordeThin);

                                // 2. Tracto
                                CrearCeldaDatos(dataRow, tracto, XlHorizontalAlignment.Center, bordeThin);

                                // 3. Conductor
                                CrearCeldaDatos(dataRow, conductor, XlHorizontalAlignment.Left, bordeThin);

                                // 4. Carreta
                                CrearCeldaDatos(dataRow, carreta, XlHorizontalAlignment.Center, bordeThin);

                                // 5. Salida de Planta
                                CrearCeldaFecha(dataRow, salidaPlanta, false, bordeThin, colorRosaPendiente);

                                // 6. Salida a Ruta
                                CrearCeldaFecha(dataRow, salidaRuta, false, bordeThin, colorRosaPendiente);

                                // 7. Llegada a Zona de Espera (rosa si está pendiente/vacío)
                                CrearCeldaFecha(dataRow, llegadaZonaEspera, string.IsNullOrEmpty(llegadaZonaEspera), bordeThin, colorRosaPendiente);

                                // 8. Inicio Descarga (rosa si está pendiente y ya llegó a zona de espera)
                                bool inicioDescargaPendiente = string.IsNullOrEmpty(inicioDescarga) && !string.IsNullOrEmpty(llegadaZonaEspera);
                                CrearCeldaFecha(dataRow, inicioDescarga, inicioDescargaPendiente, bordeThin, colorRosaPendiente);

                                // 9. Fin Descarga
                                CrearCeldaFecha(dataRow, finDescarga, false, bordeThin, colorRosaPendiente);

                                // 10. Ingreso Planta 2
                                CrearCeldaFecha(dataRow, ingresoPlanta2, false, bordeThin, colorRosaPendiente);

                                // 11. Salida Planta 2
                                CrearCeldaFecha(dataRow, salidaPlanta2, false, bordeThin, colorRosaPendiente);

                                // 12. Llegada a Zona de Espera 2
                                CrearCeldaFecha(dataRow, llegadaZonaEspera2, false, bordeThin, colorRosaPendiente);

                                // 13. Inicio Descarga 2
                                CrearCeldaFecha(dataRow, inicioDescarga2, false, bordeThin, colorRosaPendiente);

                                // 14. Fin Descarga 2
                                CrearCeldaFecha(dataRow, finDescarga2, false, bordeThin, colorRosaPendiente);

                                // 15. Llegada a Base (rosa si está pendiente/vacío)
                                CrearCeldaFecha(dataRow, llegadaBase, string.IsNullOrEmpty(llegadaBase), bordeThin, colorRosaPendiente);

                                // 16. Ubicación (Azul negrita centrado)
                                CrearCeldaUbicacion(dataRow, ubicacion, bordeThin, colorTextoUbicacion);

                                // 17. %Tránsito (Fondo celeste, texto azul, centrado)
                                CrearCeldaTransito(dataRow, porcTransito, bordeThin, colorCelesteTransito, colorTextoTransito);

                                // 18. Status (Color condicional según estado)
                                CrearCeldaStatus(dataRow, status, bordeThin);
                            }
                        }

                        // Habilitar autofiltro en el encabezado
                        sheet.AutoFilterRange = new XlCellRange(new XlCellPosition(0, 0), new XlCellPosition(18, rowCount));
                    }

                    // --- HOJA 2: PERNOCTES (si existen datos) ---
                    if (dtListaPernoctes != null && dtListaPernoctes.Rows.Count > 0)
                    {
                        using (IXlSheet sheet2 = document.CreateSheet())
                        {
                            sheet2.Name = "Sheet2";

                            int visibleColCount = 0;
                            List<string> pernocteCols = new List<string>();
                            for (int c = 0; c < dtListaPernoctes.Columns.Count; c++)
                            {
                                string colName = dtListaPernoctes.Columns[c].ColumnName;
                                if (colName != "idRuta" && colName != "idPernocte")
                                {
                                    pernocteCols.Add(colName);
                                    using (IXlColumn col = sheet2.CreateColumn()) { col.WidthInPixels = 120; }
                                    visibleColCount++;
                                }
                            }

                            Color colorBorde = Color.FromArgb(217, 217, 217);
                            Color colorHeader = Color.FromArgb(237, 125, 49);
                            XlBorder bordeThin = XlBorder.OutlineBorders(colorBorde, XlBorderLineStyle.Thin);

                            using (IXlRow headerRow = sheet2.CreateRow())
                            {
                                headerRow.HeightInPixels = 28;
                                for (int i = 0; i < pernocteCols.Count; i++)
                                {
                                    using (IXlCell cell = headerRow.CreateCell())
                                    {
                                        cell.Value = pernocteCols[i];
                                        XlCellFormatting hFormat = new XlCellFormatting();
                                        hFormat.Font = new XlFont();
                                        hFormat.Font.Name = "Calibri";
                                        hFormat.Font.Size = 10;
                                        hFormat.Font.Bold = true;
                                        hFormat.Font.Color = colorHeader;
                                        hFormat.Alignment = new XlCellAlignment();
                                        hFormat.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                                        hFormat.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                                        hFormat.Border = bordeThin;
                                        cell.Formatting = hFormat;
                                    }
                                }
                            }

                            for (int r = 0; r < dtListaPernoctes.Rows.Count; r++)
                            {
                                DataRow dr = dtListaPernoctes.Rows[r];
                                using (IXlRow dataRow = sheet2.CreateRow())
                                {
                                    dataRow.HeightInPixels = 22;
                                    for (int i = 0; i < pernocteCols.Count; i++)
                                    {
                                        string colName = pernocteCols[i];
                                        object val = dr[colName];
                                        string text = val != null && val != DBNull.Value ? val.ToString() : "";
                                        DateTime dt;
                                        if (DateTime.TryParse(text, out dt) && dt.Year > 2000 && text.Length > 10) { text = dt.ToString("dd/MM/yyyy HH:mm:ss"); }

                                        CrearCeldaDatos(dataRow, text, XlHorizontalAlignment.Center, bordeThin);
                                    }
                                }
                            }

                            if (visibleColCount > 0)
                            { sheet2.AutoFilterRange = new XlCellRange(new XlCellPosition(0, 0), new XlCellPosition(visibleColCount - 1, dtListaPernoctes.Rows.Count)); }
                        }
                    }
                }
            }
        }

        private void CrearCeldaDatos(IXlRow row, string valor, XlHorizontalAlignment alineacion, XlBorder borde)
        {
            using (IXlCell cell = row.CreateCell())
            {
                cell.Value = valor;
                XlCellFormatting fmt = new XlCellFormatting();
                fmt.Font = new XlFont();
                fmt.Font.Name = "Calibri";
                fmt.Font.Size = 9.5;
                fmt.Alignment = new XlCellAlignment();
                fmt.Alignment.HorizontalAlignment = alineacion;
                fmt.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                fmt.Border = borde;
                cell.Formatting = fmt;
            }
        }

        private void CrearCeldaFecha(IXlRow row, string valor, bool esPendienteRosa, XlBorder borde, Color colorRosa)
        {
            using (IXlCell cell = row.CreateCell())
            {
                cell.Value = valor;
                XlCellFormatting fmt = new XlCellFormatting();
                fmt.Font = new XlFont();
                fmt.Font.Name = "Calibri";
                fmt.Font.Size = 9.5;
                fmt.Alignment = new XlCellAlignment();
                fmt.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                fmt.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                fmt.Border = borde;

                if (esPendienteRosa)
                { fmt.Fill = XlFill.SolidFill(colorRosa); }

                cell.Formatting = fmt;
            }
        }

        private void CrearCeldaUbicacion(IXlRow row, string valor, XlBorder borde, Color colorTextoAzul)
        {
            using (IXlCell cell = row.CreateCell())
            {
                cell.Value = valor;
                XlCellFormatting fmt = new XlCellFormatting();
                fmt.Font = new XlFont();
                fmt.Font.Name = "Calibri";
                fmt.Font.Size = 9.5;
                fmt.Font.Bold = true;
                fmt.Font.Color = colorTextoAzul;
                fmt.Alignment = new XlCellAlignment();
                fmt.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                fmt.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                fmt.Border = borde;
                cell.Formatting = fmt;
            }
        }

        private void CrearCeldaTransito(IXlRow row, string valor, XlBorder borde, Color colorFondoCeleste, Color colorTextoAzul)
        {
            using (IXlCell cell = row.CreateCell())
            {
                cell.Value = valor;
                XlCellFormatting fmt = new XlCellFormatting();
                fmt.Font = new XlFont();
                fmt.Font.Name = "Calibri";
                fmt.Font.Size = 9.5;
                fmt.Font.Bold = true;
                fmt.Font.Color = colorTextoAzul;
                fmt.Fill = XlFill.SolidFill(colorFondoCeleste);
                fmt.Alignment = new XlCellAlignment();
                fmt.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                fmt.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                fmt.Border = borde;
                cell.Formatting = fmt;
            }
        }

        private void CrearCeldaStatus(IXlRow row, string valor, XlBorder borde)
        {
            using (IXlCell cell = row.CreateCell())
            {
                cell.Value = valor;
                XlCellFormatting fmt = new XlCellFormatting();
                fmt.Font = new XlFont();
                fmt.Font.Name = "Calibri";
                fmt.Font.Size = 9.5;
                fmt.Font.Bold = true;
                fmt.Alignment = new XlCellAlignment();
                fmt.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;
                fmt.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                fmt.Border = borde;

                string valUpper = valor.ToUpper();
                if (valUpper.Contains("RETORNO") || valUpper.Contains("TRÁNSITO") || valUpper.Contains("TRANSITO"))
                { fmt.Font.Color = Color.FromArgb(0, 32, 96); } // Azul
                else if (valUpper.Contains("ESPERA") || valUpper.Contains("DESCARGA") || valUpper.Contains("FINALIZADO"))
                { fmt.Font.Color = Color.FromArgb(56, 87, 35); } // Verde
                else if (valUpper.Contains("DETENIDO") || valUpper.Contains("ANULADO"))
                { fmt.Font.Color = Color.FromArgb(192, 0, 0); } // Rojo
                else { fmt.Font.Color = Color.FromArgb(0, 32, 96); }
            }
        }

        private string FormatearFechaCorta(object valor)
        {
            if (valor == null || valor == DBNull.Value) return string.Empty;
            DateTime dt;
            if (DateTime.TryParse(valor.ToString(), out dt))
            {
                string[] meses = new string[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Dic" };
                int mesIdx = dt.Month - 1;
                string mes = (mesIdx >= 0 && mesIdx < 12) ? meses[mesIdx] : dt.Month.ToString();
                return string.Format("{0}-{1}", dt.Day, mes);
            }
            return valor.ToString();
        }

        private string FormatearFechaHora(object valor)
        {
            if (valor == null || valor == DBNull.Value) return string.Empty;
            string strVal = valor.ToString().Trim();
            if (string.IsNullOrEmpty(strVal)) return string.Empty;

            DateTime dt;
            if (DateTime.TryParse(strVal, out dt))
            {
                if (dt == DateTime.MinValue || dt.Year < 2000) return string.Empty;
                return dt.ToString("d/MM/yyyy HH:mm");
            }
            return strVal;
        }

        private string FormatearPorcentaje(object valor)
        {
            if (valor == null || valor == DBNull.Value || string.IsNullOrWhiteSpace(valor.ToString())) return "0%";
            string s = valor.ToString().Replace("%", "").Trim();
            decimal d;
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out d) ||
                decimal.TryParse(s, NumberStyles.Any, new CultureInfo("es-PE"), out d))
            {
                if (d > 0 && d <= 1) d = d * 100;
                return string.Format("{0:0}%", d);
            }
            return valor.ToString();
        }

        // Evita que se edite UBICACION si faltan RUTA_VIAJE o ESTADO_VIAJE
        private void dgvTiempoViajesVista_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = (GridView)sender;
            if (view.FocusedColumn.FieldName == "UBICACION")
            {
                string ruta = Convert.ToString(view.GetFocusedRowCellValue("RUTA_VIAJE"));
                string estado = Convert.ToString(view.GetFocusedRowCellValue("ESTADO_VIAJE"));

                if (string.IsNullOrEmpty(ruta) || string.IsNullOrEmpty(estado)) { e.Cancel = true; } // no abre el combo
            }
        }

        // Cuando sí puede editarse UBICACION, llena el combo según RUTA_VIAJE + ESTADO_VIAJE
        private void dgvTiempoViajesVista_ShownEditor(object sender, EventArgs e)
        {
            var view = (GridView)sender;
            if (view.FocusedColumn.FieldName != "UBICACION") return;

            DataTable dtListaUbicaciones = new DataTable();
            string RutaViaje2 = Convert.ToString(view.GetFocusedRowCellValue("RUTA_VIAJE"));
            string Operacion = Convert.ToString(view.GetFocusedRowCellValue("PROGRAMACION"));
            string Estado = Convert.ToString(view.GetFocusedRowCellValue("ESTADO_VIAJE"));
            
            var combo = view.ActiveEditor as ComboBoxEdit;
            if (combo != null)
            {
                combo.Properties.Items.Clear();
                dtListaUbicaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(2, Operacion, Estado, RutaViaje2);
                combo.Properties.Items.AddRange(dtListaUbicaciones.AsEnumerable().Select(r => r.Field<string>("Ubicacion")).ToArray());
            }
        }

        // Si cambian RUTA_VIAJE o ESTADO_VIAJE, refresca el editor de UBICACION
        private void dgvTiempoViajesVista_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "RUTA_VIAJE" || e.Column.FieldName == "ESTADO_VIAJE")
            {
                var view = (GridView)sender;
                
                view.PostEditor();
                view.UpdateCurrentRow();
                view.SetRowCellValue(e.RowHandle, "UBICACION", "");

                if (view.FocusedRowHandle == e.RowHandle && view.FocusedColumn != null && view.FocusedColumn.FieldName == "UBICACION" && view.ActiveEditor != null)
                {
                    var editor = view.ActiveEditor as BaseEdit;
                    if (editor != null) editor.EditValue = null; 
                    view.HideEditor();  
                    view.ShowEditor(); 
                }

                view.RefreshRowCell(e.RowHandle, view.Columns["UBICACION"]);
            }

            if (e.Column.FieldName == "UBICACION")
            {
                var view = (GridView)sender;

                DataTable dtListaUbicaciones = new DataTable();
                string RutaViaje2 = Convert.ToString(view.GetFocusedRowCellValue("RUTA_VIAJE"));
                string Operacion = Convert.ToString(view.GetFocusedRowCellValue("PROGRAMACION"));
                string Estado = Convert.ToString(view.GetFocusedRowCellValue("ESTADO_VIAJE"));
                string ubicacion = Convert.ToString(e.Value);

                dtListaUbicaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(2, Operacion, Estado, RutaViaje2);

                string avance = "0.00";

                if (dtListaUbicaciones != null && dtListaUbicaciones.Rows.Count > 0 && dtListaUbicaciones.Columns.Contains("Ubicacion") && dtListaUbicaciones.Columns.Contains("AVANCE"))
                {
                    DataRow fila = null;

                    foreach (DataRow r in dtListaUbicaciones.Rows)
                    {
                        string u = (r["Ubicacion"] == DBNull.Value || r["Ubicacion"] == null)? string.Empty: r["Ubicacion"].ToString().Trim();

                        if (string.Equals(u, ubicacion, StringComparison.OrdinalIgnoreCase))
                        {
                            fila = r;
                            break;
                        }
                    }

                    if (fila != null) { avance = (fila["AVANCE"] == DBNull.Value || fila["AVANCE"] == null)? "0": fila["AVANCE"].ToString().Trim(); }
                }

                view.PostEditor();
                view.UpdateCurrentRow();
                view.SetRowCellValue(e.RowHandle, "PORC_TRANSITO", avance);
            }
        }
    }
}
