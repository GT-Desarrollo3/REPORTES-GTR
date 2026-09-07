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
using System.Xml;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas.ControlCotizacion
{
    public partial class frmControlCotizacion : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaCotizaciones = new DataTable();
        DataTable dtPermisos = new DataTable();

        public frmControlCotizacion()
        {
            InitializeComponent();
        }

        private void frmControlCotizacion_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmControlCotizacion");
            
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevaCoti.Enabled = true; }
                else { btnNuevaCoti.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true)
                {
                    tsCotizar.Enabled = true;
                    tsHistorialCrediticio.Enabled = true;
                    tsModificarSolicitud.Enabled = true;
                }
                else
                {
                    tsCotizar.Enabled = false;
                    tsHistorialCrediticio.Enabled = false;
                    tsModificarSolicitud.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsCompletarCoti.Enabled = true; }
                else { tsCompletarCoti.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarCoti.Enabled = true; }
                else { tsEliminarCoti.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            cbxFlota.Text = "TODOS";
            cbxEstado.Text = "TODOS";
            ListarCotizaciones();
        }


        public void ListarCotizaciones()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaCotizaciones = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_ListarRegistroCotizaciones(dtpFechaIni.Text, dtpFechaFin.Text, 
                                                              txtCliente.Text, cbxFlota.Text, cbxEstado.Text);
                dtgCotizacion.DataSource = dtListaCotizaciones;
                if (dtListaCotizaciones.Rows.Count > 0)
                {
                    dgvCotizacionVista.Columns["1"].Visible = false;
                    dgvCotizacionVista.Columns["2"].Visible = false;
                    dgvCotizacionVista.Columns["3"].Visible = false;
                    dgvCotizacionVista.Columns["4"].Visible = false;
                    dgvCotizacionVista.Columns["5"].Visible = false;
                    dgvCotizacionVista.Columns["idRuta"].Visible = false;
                    dgvCotizacionVista.Columns["KM"].Visible = false;
                    dgvCotizacionVista.Columns["HORAS"].Visible = false;
                    dgvCotizacionVista.Columns["ContratoInicio"].Visible = false;
                    dgvCotizacionVista.Columns["ContratoInicio"].Visible = false;
                    dgvCotizacionVista.Columns["ContratoFin"].Visible = false;
                    dgvCotizacionVista.Columns["HorarioIni"].Visible = false;
                    dgvCotizacionVista.Columns["HorarioFin"].Visible = false;

                    dgvCotizacionVista.Columns["ContratoInicio"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvCotizacionVista.Columns["ContratoInicio"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvCotizacionVista.Columns["ContratoFin"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvCotizacionVista.Columns["ContratoFin"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvCotizacionVista.Columns["FECHA_INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvCotizacionVista.Columns["FECHA_INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvCotizacionVista.Columns["FECHA_SOLICITUD"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvCotizacionVista.Columns["FECHA_SOLICITUD"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvCotizacionVista.Columns["FECHA_REGISTRO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvCotizacionVista.Columns["FECHA_REGISTRO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvCotizacionVista.Columns["FECHA_COTIZACION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvCotizacionVista.Columns["FECHA_COTIZACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvCotizacionVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvCotizacionVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvCotizacionVista.BestFitColumns();
                }
            }
        }

        private void btnNuevaCoti_Click(object sender, EventArgs e)
        {
            frmNuevaCotizacion frmNuevaCotizacion = new frmNuevaCotizacion();
            frmNuevaCotizacion.dtpHorasDuracion.Text = "00:00:00";
            frmNuevaCotizacion.dtpContratoIni.Value = new DateTime(frmNuevaCotizacion.dtpContratoIni.Value.Year, frmNuevaCotizacion.dtpContratoIni.Value.Month, 1);
            frmNuevaCotizacion.dtpContratoFin.Value = DateTime.Now.AddYears(1);
            frmNuevaCotizacion.dtpHorarioIni.Text = "08:00:00";
            frmNuevaCotizacion.dtpHorarioFin.Text = "23:00:00";
            frmNuevaCotizacion.dtpFechaInicio.Value = DateTime.Now;
            frmNuevaCotizacion.txtFrecuencia.Text = "0";
            frmNuevaCotizacion.txtValor.Text = "0";
            frmNuevaCotizacion.txtTonelaje.Text = "0";
            frmNuevaCotizacion.txtMermas.Text = "0";
            frmNuevaCotizacion.txtNumeroCond.Text = "0";

            frmNuevaCotizacion.cbxTipoViaje.Text = "COMPLETO";
            frmNuevaCotizacion.cbxResponsable.Text = "TRANSPESA";
            frmNuevaCotizacion.cbxFlota.Text = "SÍ";
            frmNuevaCotizacion.cbxStandby.Text = "SÍ";
            frmNuevaCotizacion.cbxPoliticas.Text = "SÍ";

            frmNuevaCotizacion.cbCortinera.Checked = false;
            frmNuevaCotizacion.cbCortinera_CheckedChanged(sender, e);
            frmNuevaCotizacion.cbPlataforma.Checked = false;
            frmNuevaCotizacion.cbPlataforma_CheckedChanged(sender, e);
            frmNuevaCotizacion.cbTolva.Checked = false;
            frmNuevaCotizacion.cbTolva_CheckedChanged(sender, e);
            frmNuevaCotizacion.cbCisterna.Checked = false;
            frmNuevaCotizacion.cbCisterna_CheckedChanged(sender, e);
            frmNuevaCotizacion.cbOtros.Checked = false;
            frmNuevaCotizacion.cbOtros_CheckedChanged(sender, e);
            frmNuevaCotizacion.rbUnidad.Checked = true;
            frmNuevaCotizacion.rbUnidad_Click(sender, e);
            frmNuevaCotizacion.idCotizacionC = 0;
            frmNuevaCotizacion.ListarImplementos(frmNuevaCotizacion.idCotizacionC);

            frmNuevaCotizacion.groupBox2.Enabled = false;
            frmNuevaCotizacion.groupBox3.Enabled = false;
            frmNuevaCotizacion.groupBox4.Enabled = false;

            frmNuevaCotizacion.Opcion = 0;
            frmNuevaCotizacion.formulario = this;
            frmNuevaCotizacion.ShowDialog();
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCotizaciones(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCotizaciones(); }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCotizaciones(); }
        }

        private void cbxFlota_DropDownClosed(object sender, EventArgs e) { ListarCotizaciones(); }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarCotizaciones(); }

        private void dgvCotizacionVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "CO") { if (e.CellValue.ToString() == "X") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); } }
            if (e.Column.FieldName == "P") { if (e.CellValue.ToString() == "X") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); } }
            if (e.Column.FieldName == "T") { if (e.CellValue.ToString() == "X") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); } }
            if (e.Column.FieldName == "CI") { if (e.CellValue.ToString() == "X") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); } }
            if (e.Column.FieldName == "O") { if (e.CellValue.ToString() == "X") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); } }

            if (e.Column.FieldName == "HISTORIAL_CREDITICIO")
            {
                if (e.CellValue.ToString() == "OBSERVADO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
                if (e.CellValue.ToString() == "CONFORME") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "SOLICITADO") { e.Appearance.BackColor = Color.Aqua; }
                if (e.CellValue.ToString() == "REGISTRADO") { e.Appearance.BackColor = Color.Yellow; }
                if (e.CellValue.ToString() == "COTIZADO") { e.Appearance.BackColor = Color.Lime; }
            }
        }

        private void dtgCotizacion_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idCotizacion = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));
                string Estado = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ESTADO"));

                if (idCotizacion != "")
                {
                    if (Estado == "SOLICITADO")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                        {
                            tsModificarSolicitud.Enabled = true;
                            tsCompletarCoti.Enabled = true;
                        }

                        tsCotizar.Enabled = false;

                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true) { tsHistorialCrediticio.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarCoti.Enabled = true; }
                    }

                    if (Estado == "REGISTRADO")
                    {
                        tsModificarSolicitud.Enabled = false;

                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsCompletarCoti.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true)
                        {
                            tsHistorialCrediticio.Enabled = true;
                            tsCotizar.Enabled = true;
                        }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarCoti.Enabled = true; }
                    }

                    if (Estado == "COTIZADO")
                    {
                        tsModificarSolicitud.Enabled = false;
                        tsCompletarCoti.Enabled = false;

                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true)
                        {
                            tsHistorialCrediticio.Enabled = true;
                            tsCotizar.Enabled = true;
                        }

                        tsEliminarCoti.Enabled = false;
                    }
                }
                else
                {
                    tsHistorialCrediticio.Enabled = false;
                    tsCotizar.Enabled = false;
                    tsModificarSolicitud.Enabled = false;
                    tsCompletarCoti.Enabled = false;
                    tsEliminarCoti.Enabled = false;
                }
            }
            catch
            {
                tsHistorialCrediticio.Enabled = false;
                tsCotizar.Enabled = false;
                tsModificarSolicitud.Enabled = false;
                tsCompletarCoti.Enabled = false;
                tsEliminarCoti.Enabled = false;
            }
        }

        private void tsModificarSolicitud_Click(object sender, EventArgs e)
        {
            int idCotizacion = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));
           
            frmNuevaCotizacion frmNuevaCotizacion = new frmNuevaCotizacion();

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CO")) == "X")
            { frmNuevaCotizacion.cbCortinera.Checked = true; }
            else { frmNuevaCotizacion.cbCortinera.Checked = false; }
            frmNuevaCotizacion.cbCortinera_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "P")) == "X")
            { frmNuevaCotizacion.cbPlataforma.Checked = true; }
            else { frmNuevaCotizacion.cbPlataforma.Checked = false; }
            frmNuevaCotizacion.cbPlataforma_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "T")) == "X")
            { frmNuevaCotizacion.cbTolva.Checked = true; }
            else { frmNuevaCotizacion.cbTolva.Checked = false; }
            frmNuevaCotizacion.cbTolva_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CI")) == "X")
            { frmNuevaCotizacion.cbCisterna.Checked = true; }
            else { frmNuevaCotizacion.cbCisterna.Checked = false; }
            frmNuevaCotizacion.cbCisterna_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "O")) == "X")
            { frmNuevaCotizacion.cbOtros.Checked = true; }
            else { frmNuevaCotizacion.cbOtros.Checked = false; }
            frmNuevaCotizacion.cbOtros_CheckedChanged(sender, e);

            frmNuevaCotizacion.idRuta = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "idRuta"));
            frmNuevaCotizacion.txtRuta.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RUTA"));
            frmNuevaCotizacion.txtKilometraje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "KM"));
            frmNuevaCotizacion.txtPuntoInicio.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PUNTO_INICIO"));
            frmNuevaCotizacion.txtPuntoFin.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PUNTO_FIN"));
            frmNuevaCotizacion.txtFrecuencia.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "FRECUENCIA"));
            frmNuevaCotizacion.txtRUC.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RUC"));
            frmNuevaCotizacion.txtCliente.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CLIENTE"));
            frmNuevaCotizacion.txtTelefono.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TELEFONO"));
            frmNuevaCotizacion.txtContacto.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CONTACTO"));
            frmNuevaCotizacion.txtValor.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "VALOR_PRODUCTO"));
            frmNuevaCotizacion.txtEmbalaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "EMBALAJE"));
            frmNuevaCotizacion.txtProducto.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PRODUCTO"));
            frmNuevaCotizacion.cbxTipoViaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TIPO_VIAJE"));

            frmNuevaCotizacion.dtpHorasDuracion.Text = "00:00:00";
            frmNuevaCotizacion.dtpContratoIni.Value = new DateTime(frmNuevaCotizacion.dtpContratoIni.Value.Year, frmNuevaCotizacion.dtpContratoIni.Value.Month, 1);
            frmNuevaCotizacion.dtpContratoFin.Value = DateTime.Now.AddYears(1);
            frmNuevaCotizacion.dtpHorarioIni.Text = "08:00:00";
            frmNuevaCotizacion.dtpHorarioFin.Text = "23:00:00";
            frmNuevaCotizacion.dtpFechaInicio.Value = DateTime.Now;
            frmNuevaCotizacion.txtTonelaje.Text = "0";
            frmNuevaCotizacion.txtMermas.Text = "0";
            frmNuevaCotizacion.txtNumeroCond.Text = "0";

            frmNuevaCotizacion.cbxResponsable.Text = "TRANSPESA";
            frmNuevaCotizacion.cbxFlota.Text = "SÍ";
            frmNuevaCotizacion.cbxStandby.Text = "SÍ";
            frmNuevaCotizacion.cbxPoliticas.Text = "SÍ";

            frmNuevaCotizacion.Opcion = 1;
            frmNuevaCotizacion.idCotizacionC = idCotizacion;
            frmNuevaCotizacion.ListarImplementos(frmNuevaCotizacion.idCotizacionC);

            frmNuevaCotizacion.groupBox2.Enabled = false;
            frmNuevaCotizacion.groupBox3.Enabled = false;
            frmNuevaCotizacion.groupBox4.Enabled = false;
            frmNuevaCotizacion.btnCancelar.Enabled = false;

            frmNuevaCotizacion.formulario = this;
            frmNuevaCotizacion.ShowDialog();
        }

        private void tsEditarCoti_Click(object sender, EventArgs e)
        {
            int idCotizacion = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));

            frmNuevaCotizacion frmNuevaCotizacion = new frmNuevaCotizacion();
            
            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CO")) == "X")
            { frmNuevaCotizacion.cbCortinera.Checked = true; } else { frmNuevaCotizacion.cbCortinera.Checked = false; }
            frmNuevaCotizacion.cbCortinera_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "P")) == "X")
            { frmNuevaCotizacion.cbPlataforma.Checked = true; } else { frmNuevaCotizacion.cbPlataforma.Checked = false; }
            frmNuevaCotizacion.cbPlataforma_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "T")) == "X")
            { frmNuevaCotizacion.cbTolva.Checked = true; } else { frmNuevaCotizacion.cbTolva.Checked = false; }
            frmNuevaCotizacion.cbTolva_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CI")) == "X")
            { frmNuevaCotizacion.cbCisterna.Checked = true; } else { frmNuevaCotizacion.cbCisterna.Checked = false; }
            frmNuevaCotizacion.cbCisterna_CheckedChanged(sender, e);

            if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "O")) == "X")
            { frmNuevaCotizacion.cbOtros.Checked = true; } else { frmNuevaCotizacion.cbOtros.Checked = false; }
            frmNuevaCotizacion.cbOtros_CheckedChanged(sender, e);

            frmNuevaCotizacion.idRuta = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "idRuta"));
            frmNuevaCotizacion.txtRuta.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RUTA"));
            frmNuevaCotizacion.txtKilometraje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "KM"));
            frmNuevaCotizacion.txtPuntoInicio.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PUNTO_INICIO"));
            frmNuevaCotizacion.txtPuntoFin.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PUNTO_FIN"));
            frmNuevaCotizacion.txtFrecuencia.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "FRECUENCIA"));
            frmNuevaCotizacion.txtRUC.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RUC"));
            frmNuevaCotizacion.txtCliente.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CLIENTE"));
            frmNuevaCotizacion.txtTelefono.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TELEFONO"));
            frmNuevaCotizacion.txtContacto.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CONTACTO"));
            frmNuevaCotizacion.txtValor.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "VALOR_PRODUCTO"));
            frmNuevaCotizacion.txtEmbalaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "EMBALAJE"));
            frmNuevaCotizacion.txtProducto.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PRODUCTO"));
            frmNuevaCotizacion.cbxTipoViaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TIPO_VIAJE"));

            frmNuevaCotizacion.cbxResponsable.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RESPONSABLE"));
            frmNuevaCotizacion.dtpHorasDuracion.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "HORAS_DURACION"));
            frmNuevaCotizacion.dtpContratoIni.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ContratoInicio"));
            frmNuevaCotizacion.dtpContratoFin.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ContratoFin"));
            frmNuevaCotizacion.txtPermisos.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PERMISOS"));
            frmNuevaCotizacion.txtTonelaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TONELAJE"));
            frmNuevaCotizacion.txtGastosA.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "GASTOS_ADICIONALES"));
            frmNuevaCotizacion.dtpHorarioIni.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "HorarioIni"));
            frmNuevaCotizacion.dtpHorarioFin.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "HorarioFin"));

            frmNuevaCotizacion.cbxFlota.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "FLOTA"));
            frmNuevaCotizacion.txtMermas.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "MERMAS"));
            frmNuevaCotizacion.cbxStandby.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "STANDBY"));
            frmNuevaCotizacion.cbxPoliticas.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "POLÍTICAS"));
            frmNuevaCotizacion.dtpFechaInicio.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "FECHA_INICIO"));
            frmNuevaCotizacion.txtNumeroCond.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO_CONDUCTOR"));
            frmNuevaCotizacion.txtOrigen.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ORIGEN"));

            frmNuevaCotizacion.idCotizacionC = idCotizacion;
            frmNuevaCotizacion.Opcion = 2;
            frmNuevaCotizacion.ListarImplementos(frmNuevaCotizacion.idCotizacionC);
            frmNuevaCotizacion.rbUnidad.Checked = true;
            frmNuevaCotizacion.rbUnidad_Click(sender, e);

            frmNuevaCotizacion.btnCancelar.Enabled = false;
            frmNuevaCotizacion.formulario = this;
            frmNuevaCotizacion.ShowDialog();
        }

        private void tsCONFORME_Click(object sender, EventArgs e)
        {
            int idCotizacionC = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones(2, idCotizacionC, "CONFORME", Usuario);
            string respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = respta.Substring(0, 1);
            if (NroRPTA == "0") { ListarCotizaciones(); }
            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsOBSERVADO_Click(object sender, EventArgs e)
        {
            int idCotizacionC = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones(2, idCotizacionC, "OBSERVADO", Usuario);
            string respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = respta.Substring(0, 1);
            if (NroRPTA == "0") { ListarCotizaciones(); }
            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminarCoti_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar esta cotización?", "ELIMINAR COTIZACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idCotizacionC = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones(1, idCotizacionC, "", Usuario);
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarCotizaciones(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("La cotización seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarCotizaciones(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgCotizacion.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE CONTROL DE COTIZACIONES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgCotizacion.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgCotizacion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Coti = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));

                if (Coti != "")
                {
                    int idCotizacion = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));

                    frmNuevaCotizacion frmNuevaCotizacion = new frmNuevaCotizacion();

                    if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CO")) == "X")
                    { frmNuevaCotizacion.cbCortinera.Checked = true; }
                    else { frmNuevaCotizacion.cbCortinera.Checked = false; }
                    frmNuevaCotizacion.cbCortinera_CheckedChanged(sender, e);

                    if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "P")) == "X")
                    { frmNuevaCotizacion.cbPlataforma.Checked = true; }
                    else { frmNuevaCotizacion.cbPlataforma.Checked = false; }
                    frmNuevaCotizacion.cbPlataforma_CheckedChanged(sender, e);

                    if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "T")) == "X")
                    { frmNuevaCotizacion.cbTolva.Checked = true; }
                    else { frmNuevaCotizacion.cbTolva.Checked = false; }
                    frmNuevaCotizacion.cbTolva_CheckedChanged(sender, e);

                    if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CI")) == "X")
                    { frmNuevaCotizacion.cbCisterna.Checked = true; }
                    else { frmNuevaCotizacion.cbCisterna.Checked = false; }
                    frmNuevaCotizacion.cbCisterna_CheckedChanged(sender, e);

                    if (Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "O")) == "X")
                    { frmNuevaCotizacion.cbOtros.Checked = true; }
                    else { frmNuevaCotizacion.cbOtros.Checked = false; }
                    frmNuevaCotizacion.cbOtros_CheckedChanged(sender, e);

                    frmNuevaCotizacion.txtRuta.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RUTA"));
                    frmNuevaCotizacion.txtKilometraje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "KM"));
                    frmNuevaCotizacion.txtPuntoInicio.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PUNTO_INICIO"));
                    frmNuevaCotizacion.txtPuntoFin.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PUNTO_FIN"));
                    frmNuevaCotizacion.txtFrecuencia.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "FRECUENCIA"));
                    frmNuevaCotizacion.txtRUC.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RUC"));
                    frmNuevaCotizacion.txtCliente.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CLIENTE"));
                    frmNuevaCotizacion.txtTelefono.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TELEFONO"));
                    frmNuevaCotizacion.txtContacto.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "CONTACTO"));
                    frmNuevaCotizacion.txtValor.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "VALOR_PRODUCTO"));
                    frmNuevaCotizacion.txtEmbalaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "EMBALAJE"));
                    frmNuevaCotizacion.txtProducto.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PRODUCTO"));
                    frmNuevaCotizacion.cbxTipoViaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TIPO_VIAJE"));

                    frmNuevaCotizacion.cbxResponsable.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RESPONSABLE"));
                    frmNuevaCotizacion.dtpHorasDuracion.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "HORAS_DURACION"));
                    frmNuevaCotizacion.dtpContratoIni.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ContratoInicio"));
                    frmNuevaCotizacion.dtpContratoFin.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ContratoFin"));
                    frmNuevaCotizacion.txtPermisos.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "PERMISOS"));
                    frmNuevaCotizacion.txtTonelaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TONELAJE"));
                    frmNuevaCotizacion.txtGastosA.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "GASTOS_ADICIONALES"));
                    frmNuevaCotizacion.dtpHorarioIni.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "HorarioIni"));
                    frmNuevaCotizacion.dtpHorarioFin.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "HorarioFin"));

                    frmNuevaCotizacion.cbxFlota.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "FLOTA"));
                    frmNuevaCotizacion.txtMermas.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "MERMAS"));
                    frmNuevaCotizacion.cbxStandby.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "STANDBY"));
                    frmNuevaCotizacion.cbxPoliticas.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "POLÍTICAS"));
                    frmNuevaCotizacion.dtpFechaInicio.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "FECHA_INICIO"));
                    frmNuevaCotizacion.txtNumeroCond.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO_CONDUCTOR"));
                    frmNuevaCotizacion.txtOrigen.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ORIGEN"));

                    frmNuevaCotizacion.idCotizacionC = idCotizacion;
                    frmNuevaCotizacion.ListarImplementos(frmNuevaCotizacion.idCotizacionC);
                    frmNuevaCotizacion.rbUnidad.Checked = true;
                    frmNuevaCotizacion.rbUnidad_Click(sender, e);

                    frmNuevaCotizacion.groupBox1.Enabled = false;
                    frmNuevaCotizacion.groupBox2.Enabled = false;
                    frmNuevaCotizacion.groupBox3.Enabled = false;
                    frmNuevaCotizacion.btnGuardar.Enabled = false;
                    frmNuevaCotizacion.btnCancelar.Enabled = false;
                    frmNuevaCotizacion.groupBox4.Enabled = false;
                    frmNuevaCotizacion.dtgImplementos.ContextMenuStrip = null;
                    frmNuevaCotizacion.formulario = this;
                    frmNuevaCotizacion.ShowDialog();
                }
            }
            catch { }
        }

        private void tsCotizar_Click(object sender, EventArgs e)
        {
            string Estado = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "ESTADO"));

            if (Estado != "COTIZADO")
            {
                frmCuadroCostos frmCuadroCostos = new frmCuadroCostos();

                frmCuadroCostos.txtRuta.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "RUTA"));
                frmCuadroCostos.txtKilometraje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "KM"));
                frmCuadroCostos.txtHorasViaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "HORAS"));
                frmCuadroCostos.txtDiasViaticos.Text = Math.Round((Convert.ToDecimal(frmCuadroCostos.txtHorasViaje.Text) / 24),2).ToString();
                frmCuadroCostos.txtTonelaje.Text = Convert.ToString(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "TONELAJE"));
            
                frmCuadroCostos.idCotizacionC = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "NRO"));
                frmCuadroCostos.idRuta = Convert.ToInt32(dgvCotizacionVista.GetRowCellValue(dgvCotizacionVista.FocusedRowHandle, "idRuta"));

                DataTable dtRendimientos = new DataTable();
                dtRendimientos = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_ListarDatos(1, frmCuadroCostos.txtRuta.Text);
                if (dtRendimientos.Rows.Count > 0)
                {
                    frmCuadroCostos.txtMin.Text = dtRendimientos.Rows[0]["MINIMO"].ToString();
                    frmCuadroCostos.txtMax.Text = dtRendimientos.Rows[0]["MAXIMO"].ToString();
                    frmCuadroCostos.txtProm.Text = dtRendimientos.Rows[0]["PROMEDIO"].ToString();
                }

                DataTable dtCosto = new DataTable();
                dtCosto = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_ListarDatos(2, " ");
                if (dtCosto.Rows.Count > 0) { frmCuadroCostos.txtPrecioOriginal.Text = dtCosto.Rows[0]["PrecioUnitario"].ToString(); }

                frmCuadroCostos.txtRendCargado.Text = "0.00";
                frmCuadroCostos.txtReefer.Text = "0.00";
                frmCuadroCostos.txtRendimiento.Text = "0.00";
                frmCuadroCostos.txtGalones.Text = "0.00";
                frmCuadroCostos.txtPorc.Text = "0.00";
                frmCuadroCostos.txtCosto.Text = Math.Round(Convert.ToDecimal(frmCuadroCostos.txtPrecioOriginal.Text), 2).ToString();
                frmCuadroCostos.txtTotalCombustible.Text = Convert.ToString(Convert.ToDecimal(frmCuadroCostos.txtRendCargado.Text) + Convert.ToDecimal(frmCuadroCostos.txtReefer.Text) +
                                                           Convert.ToDecimal(frmCuadroCostos.txtRendimiento.Text) + Convert.ToDecimal(frmCuadroCostos.txtGalones.Text) +
                                                           Convert.ToDecimal(frmCuadroCostos.txtCosto.Text));

                frmCuadroCostos.txtMtto.Text = "0.00";
                frmCuadroCostos.txtNeumaticos.Text = "0.00";
                frmCuadroCostos.txtAros.Text = "0.00";
                frmCuadroCostos.txtDepreciacion.Text = "0.00";
                frmCuadroCostos.txtM1M0.Text = "0.00";
                frmCuadroCostos.txtTotalMtto.Text = "0.00";

                frmCuadroCostos.txtPeajes.Text = "0.00";

                frmCuadroCostos.txtDesencarpada.Text = "0.00";
                frmCuadroCostos.txtHospedaje.Text = "0.00";
                frmCuadroCostos.txtAlimentacion.Text = "0.00";
                frmCuadroCostos.txtTotalViaticos.Text = "0.00";

                frmCuadroCostos.txtTotalCV.Text = "0.00";

                frmCuadroCostos.txtImpGas.Text = "0.00";
                frmCuadroCostos.txtPersonal.Text = "0.00";
                frmCuadroCostos.txtDepreciacion2.Text = "0.00";
                frmCuadroCostos.txtGPS.Text = "0.00";
                frmCuadroCostos.txtPoliza.Text = "0.00";
                frmCuadroCostos.txtInspTecnica.Text = "0.00";
                frmCuadroCostos.txtSOAT.Text = "0.00";
                frmCuadroCostos.txtCostosIndirectos.Text = "0.00";
                frmCuadroCostos.txtCostosFijos.Text = "0.00";

                frmCuadroCostos.txtTotalCOP.Text = "0.00";

                frmCuadroCostos.txtGastosA.Text = "0.00";
                frmCuadroCostos.txtGastosV.Text = "0.00";
                frmCuadroCostos.txtGastosF.Text = "0.00";
                frmCuadroCostos.txtTotalCAF.Text = "0.00";

                frmCuadroCostos.txtCostroTransporte.Text = "0.00";
                frmCuadroCostos.txtTarifa2.Text = "0.00";
                frmCuadroCostos.txtTarifa.Text = "0.00";
                frmCuadroCostos.txtUtilidad.Text = "0.00";
                frmCuadroCostos.txtPorcentaje.Text = "0.00";

                frmCuadroCostos.ShowDialog();
            }
        }
    }
}
